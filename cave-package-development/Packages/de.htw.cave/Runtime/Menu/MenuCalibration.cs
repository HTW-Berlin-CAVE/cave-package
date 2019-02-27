using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using JoyconLib;
using Htw.Cave.Projector;
using Htw.Cave.Joycon;
using Htw.Cave.ImportExport;

namespace Htw.Cave.Menu
{
	public class CalibrationBackup
	{
		public Vector2[] equalizationAnchors;

		public CalibrationBackup(Vector2[] equalizationAnchors)
		{
			this.equalizationAnchors = new Vector2[equalizationAnchors.Length];

			for(int i = equalizationAnchors.Length - 1; i >= 0; --i)
				this.equalizationAnchors[i] = equalizationAnchors[i];
		}
	}

    public sealed class MenuCalibration : MonoBehaviour
    {
		private const float minSensitivity = 0.001f;
		private const float maxSensitivity = 0.1f;

		[SerializeField]
		private Text displayLabelText;
		public Text DisplayLabelText
		{
			get { return this.displayLabelText; }
			set { this.displayLabelText = value; }
		}

		[SerializeField]
		private Text sensitivityLabelText;
		public Text SensitivityLabelText
		{
			get { return this.sensitivityLabelText; }
			set { this.sensitivityLabelText = value; }
		}

		[SerializeField]
		private Image calibrationTopLeftImage;
		public Image CalibrationTopLeftImage
		{
			get { return this.calibrationTopLeftImage; }
			set { this.calibrationTopLeftImage = value; }
		}

		[SerializeField]
		private Image calibrationTopRightImage;
		public Image CalibrationTopRightImage
		{
			get { return this.calibrationTopRightImage; }
			set { this.calibrationTopRightImage = value; }
		}

		[SerializeField]
		private Image calibrationBottomLeftImage;
		public Image CalibrationBottomLeftImage
		{
			get { return this.calibrationBottomLeftImage; }
			set { this.calibrationBottomLeftImage = value; }
		}

		[SerializeField]
		private Image calibrationBottomRightImage;
		public Image CalibrationBottomRightImage
		{
			get { return this.calibrationBottomRightImage; }
			set { this.calibrationBottomRightImage = value; }
		}

		[SerializeField]
		private Text enableSpheresText;
		public Text EnableSpheresText
		{
			get { return this.enableSpheresText; }
			set { this.enableSpheresText = value; }
		}

		[SerializeField]
		private Shader sphereShader;
		public Shader SphereShader
		{
			get { return this.sphereShader; }
			set { this.sphereShader = value; }
		}

		private MenuManager manager;
		private ProjectorEmitter[] emitters;
		private CalibrationBackup[] backups;
		private int display;
		private int sensitivity;
		private int anchor;
		private float joyconDelay;
		private JoyconLib.Joycon rightJoycon;
		private List<GameObject> spheres;

		public void Awake()
		{
			this.manager = base.GetComponentInParent<MenuManager>();
			this.emitters = this.manager.ProjectorBrain.GetComponentInChildren<ProjectorMount>().Get();
			this.backups = new CalibrationBackup[this.emitters.Length];
			this.joyconDelay = 0f;
			this.spheres = new List<GameObject>();
		}

		public void OnEnable()
		{
			this.display = 0;
			this.sensitivity = 1;
			this.anchor = 0;

			Backup();
			ShowDisplay();
			ShowSensitivity();
			ShowAnchorImage();
		}

		public void Start()
		{
			this.rightJoycon = JoyconHelper.GetRightJoycon();
		}

		public void Update()
		{
			if(Input.GetKeyUp(KeyCode.Keypad8))
				Calibrate(new Vector2(0f, 1f));

			if(Input.GetKeyUp(KeyCode.Keypad2))
				Calibrate(new Vector2(0f, -1f));

			if(Input.GetKeyUp(KeyCode.Keypad4))
				Calibrate(new Vector2(-1f, 0f));

			if(Input.GetKeyUp(KeyCode.Keypad6))
				Calibrate(new Vector2(1f, 0f));

			if(this.rightJoycon != null && Time.time > this.joyconDelay)
			{
				float[] stickRight = this.rightJoycon.GetStick();

				if(stickRight[0] < 0f)
					Calibrate(new Vector2(-1f, 0f));

				if(stickRight[0] > 0f)
					Calibrate(new Vector2(1f, 0f));

				if(stickRight[1] < 0f)
					Calibrate(new Vector2(0f, -1f));

				if(stickRight[1] > 0f)
					Calibrate(new Vector2(0f, 1f));

				this.joyconDelay = Time.time + 0.5f;
			}
		}

		public void OnDisable()
		{
			DisableCalibrationSpheres();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.sphereShader = Shader.Find("Standard");
		}

		public void OnApplicationQuit()
		{
			ResetCalibration();
		}
#endif

		public void Calibrate(Vector2 direction)
		{
			float absoluteSensitivity = 1f / ((float)this.sensitivity + 0.000001f);
			float smoothSensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, absoluteSensitivity / 10f);
			direction = direction * smoothSensitivity;

			this.emitters[this.display].Configuration.EqualizationAnchors[this.anchor] += direction;
			this.emitters[this.display].Equalize(true);
		}

		public void ResetCalibration()
		{
			for(int i = this.backups.Length - 1; i >= 0; --i)
			{
				this.emitters[i].Configuration.EqualizationAnchors = this.backups[i].equalizationAnchors;
				this.emitters[i].Equalize(true);
			}
		}

		public void ExportCalibration()
		{
			ImportExportSystem.Instance.Export(this.emitters[this.display].Configuration);
		}

		public void PreviousDisplay()
		{
			--this.display;

			if(this.display < 0)
				this.display = this.emitters.Length - 1;

			ShowDisplay();
		}

		public void NextDisplay()
		{
			++this.display;

			if(this.display > this.emitters.Length - 1)
				this.display = 0;

			ShowDisplay();
		}

		private void ShowDisplay()
		{
			this.displayLabelText.text = this.emitters[this.display].Configuration.DisplayName;
		}

		public void LessSensitivity()
		{
			--this.sensitivity;

			if(this.sensitivity < 1)
				this.sensitivity = 1;

			ShowSensitivity();
		}

		public void MoreSensitivity()
		{
			++this.sensitivity;

			if(this.sensitivity > 10)
				this.sensitivity = 10;

			ShowSensitivity();
		}

		public void ToggleCalibrationSpheres()
		{
			if(this.spheres.Count > 0)
				DisableCalibrationSpheres();
			else
				EnableCalibrationSpheres();
		}

		private void EnableCalibrationSpheres()
		{
			this.enableSpheresText.text = "Disable Spheres";

			List<Vector3> list = new List<Vector3>();

			for(int i = this.emitters.Length - 1; i >= 0; --i)
			{
				Vector3[] plane = this.emitters[i].TransformPlane();

				for(int k = plane.Length - 1; k >= 0; --k)
					list.Add(plane[k]);
			}

			foreach(Vector3 vector in list)
			{
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.position = vector;
				sphere.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
				Destroy(sphere.GetComponent<SphereCollider>());

				Renderer rend = sphere.GetComponent<Renderer>();
				rend.material.shader = this.sphereShader;
				rend.material.SetColor("_Color", new Color(0.973f, 0.475f, 0f));
				rend.material.SetFloat("_Metallic", 0f);
				rend.material.SetFloat("_Glossiness", 0f);

				this.spheres.Add(sphere);
			}
		}

		private void DisableCalibrationSpheres()
		{
			this.enableSpheresText.text = "Enable Spheres";

			foreach(GameObject sphere in this.spheres)
				Destroy(sphere);

			this.spheres.Clear();
		}

		private void ShowSensitivity()
		{
			this.sensitivityLabelText.text = string.Format("{0}", this.sensitivity);
		}

		public void PreviousAnchor()
		{
			--this.anchor;

			if(this.anchor < 0)
				this.anchor = 3;

			ShowAnchorImage();
		}

		public void NextAnchor()
		{
			++this.anchor;

			if(this.anchor > 3)
				this.anchor = 0;

			ShowAnchorImage();
		}

		private void ShowAnchorImage()
		{
			this.calibrationTopLeftImage.enabled = false;
			this.calibrationTopRightImage.enabled = false;
			this.calibrationBottomLeftImage.enabled = false;
			this.calibrationBottomRightImage.enabled = false;

			switch(this.anchor)
			{
				case 0:
					this.calibrationTopLeftImage.enabled = true;
					break;
				case 1:
					this.calibrationTopRightImage.enabled = true;
					break;
				case 2:
					this.calibrationBottomRightImage.enabled = true;
					break;
				case 3:
					this.calibrationBottomLeftImage.enabled = true;
					break;
			}
		}

		private void Backup()
		{
			for(int i = this.backups.Length - 1; i >= 0; --i)
				this.backups[i] = new CalibrationBackup(this.emitters[i].Configuration.EqualizationAnchors);
		}
    }
}
