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
		private const int minRange = 1;
		private const int maxRange = 10;

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
		private Text enableGuidesText;
		public Text EnableGuidesText
		{
			get { return this.enableGuidesText; }
			set { this.enableGuidesText = value; }
		}

		[SerializeField]
		private GameObject visualGuidePrefab;
		public GameObject VisualGuidePrefab
		{
			get { return this.visualGuidePrefab; }
			set { this.visualGuidePrefab = value; }
		}

		private MenuManager manager;
		private ProjectorEmitter[] emitters;
		private CalibrationBackup[] backups;
		private int display;
		private int sensitivity;
		private int anchor;
		private float joyconDelay;
		private JoyconLib.Joycon rightJoycon;
		private List<GameObject> guides;

		public void Awake()
		{
			this.manager = base.GetComponentInParent<MenuManager>();
			this.emitters = this.manager.ProjectorBrain.GetComponentInChildren<ProjectorMount>().Get();
			this.backups = new CalibrationBackup[this.emitters.Length];
			this.joyconDelay = 0f;
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
			DisableCalibrationGuides();
		}

#if UNITY_EDITOR
		public void OnApplicationQuit()
		{
			ResetCalibration();
		}
#endif

		public void Calibrate(Vector2 direction)
		{
			float smoothSensitivity = Mathf.Lerp(minSensitivity, maxSensitivity, (float)this.sensitivity / 10f);
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

		public void InvertDisplay()
		{
			if(this.emitters[this.display].Configuration.InvertStereo)
				this.emitters[this.display].Configuration.InvertStereo = false;
			else
				this.emitters[this.display].Configuration.InvertStereo = true;

			this.emitters[this.display].SetStereoEffect(this.manager.ProjectorBrain.Settings.StereoConvergence, this.manager.ProjectorBrain.Settings.StereoSeparation);
		}

		public void LessSensitivity()
		{
			--this.sensitivity;

			if(this.sensitivity < minRange)
				this.sensitivity = minRange;

			ShowSensitivity();
		}

		public void MoreSensitivity()
		{
			++this.sensitivity;

			if(this.sensitivity > maxRange)
				this.sensitivity = maxRange;

			ShowSensitivity();
		}

		public void ToggleCalibrationGuide()
		{
			if(this.guides == null)
				EnableCalibrationGuides();
			else
				DisableCalibrationGuides();
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

		private void EnableCalibrationGuides()
		{
			this.enableGuidesText.text = "Disable Visual Guide";
			this.guides = MenuCalibrationHelper.CreateVisualGuide(this.emitters, this.visualGuidePrefab);
		}

		private void DisableCalibrationGuides()
		{
			this.enableGuidesText.text = "Enable Visual Guide";

			if(this.guides == null)
				return;

			foreach(GameObject guide in this.guides)
				Destroy(guide);

			this.guides = null;
		}

		private void ShowDisplay()
		{
			this.displayLabelText.text = this.emitters[this.display].Configuration.DisplayName;
		}

		private void ShowSensitivity()
		{
			this.sensitivityLabelText.text = string.Format("{0}", this.sensitivity);
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
