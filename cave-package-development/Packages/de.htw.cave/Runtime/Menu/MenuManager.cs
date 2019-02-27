using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JoyconLib;
using Htw.Cave.Joycon;
using Htw.Cave.Projector;
using Htw.Cave.Kinect;

namespace Htw.Cave.Menu
{
	public enum MenuType
	{
		Calibration,
		Statistics,
		Kinect
	}

	[RequireComponent(typeof(Canvas))]
    public sealed class MenuManager : MonoBehaviour
    {
		[SerializeField]
		private Camera menuCamera;
		public Camera MenuCamera
		{
			get { return this.menuCamera; }
			set { this.menuCamera = value; }
		}

		[SerializeField]
		private ProjectorBrain projectorBrain;
		public ProjectorBrain ProjectorBrain
		{
			get { return this.projectorBrain; }
			set { this.projectorBrain = value; }
		}

		[SerializeField]
		private KinectBrain kinectBrain;
		public KinectBrain KinectBrain
		{
			get { return this.kinectBrain; }
			set { this.kinectBrain = value; }
		}

		[SerializeField]
		private float distance;
		public float Distance
		{
			get { return this.distance; }
			set { this.distance = value; }
		}

		private EventSystem eventSystem;
		private MenuType menu;
		private MenuCalibration calibration;
		private MenuStatistics statistics;
		private MenuKinect kinect;
		private FocusableElement[] focusables;
		private int focus;
		private float joyconDelay;
		private JoyconLib.Joycon leftJoycon;

		public void Awake()
		{
			this.eventSystem = EventSystem.current;
			this.menu = MenuType.Calibration;
			this.focus = 0;
			this.joyconDelay = 0f;
			this.calibration = base.GetComponentInChildren<MenuCalibration>();
			this.statistics = base.GetComponentInChildren<MenuStatistics>();
			this.kinect = base.GetComponentInChildren<MenuKinect>();
			SetMenuCamera();
			InitializeController();
		}

		public void OnEnable()
		{
			transform.rotation = this.menuCamera.transform.rotation;
			transform.position = this.projectorBrain.transform.position + this.menuCamera.transform.forward * this.distance;
			transform.position = new Vector3(transform.position.x, this.menuCamera.transform.position.y, transform.position.z);

			SwitchMenu(this.menu);
		}

		public void Start()
		{
			this.leftJoycon = JoyconHelper.GetLeftJoycon();
		}

#if UNITY_EDITOR
		public void OnValidate()
		{
			SetMenuCamera();
		}
#endif

		public void Update()
		{
			if(this.leftJoycon != null)
			{
				if(Time.time > this.joyconDelay)
				{
					float[] stickLeft = this.leftJoycon.GetStick();

					if(stickLeft[0] < 0f)
						FocusPrevious();

					if(stickLeft[0] > 0f)
						FocusNext();

					this.joyconDelay = Time.time + 0.5f;
				}

				if(this.leftJoycon.GetButtonUp(JoyconLib.Joycon.Button.SHOULDER_1))
					ExecuteFocus();
			}
		}

		public void SetMenuCamera()
		{
			base.GetComponent<Canvas>().worldCamera = this.menuCamera;
		}

		public void SwitchMenu(int type)
		{
			SwitchMenu((MenuType)type);
		}

		public void SwitchMenu(MenuType type)
		{
			DisableAllMenus();

			switch(type)
			{
				case MenuType.Calibration:
					this.calibration.gameObject.SetActive(true);
					break;
				case MenuType.Statistics:
					this.statistics.gameObject.SetActive(true);
					break;
				case MenuType.Kinect:
					this.kinect.gameObject.SetActive(true);
					break;
			}

			FindFocusable();
		}

		private void DisableAllMenus()
		{
			this.calibration.gameObject.SetActive(false);
			this.statistics.gameObject.SetActive(false);
			this.kinect.gameObject.SetActive(false);
		}

		private void FindFocusable()
		{
			this.focusables = base.GetComponentsInChildren<FocusableElement>().OrderBy(focusable => focusable.Order).ToArray();
			this.focus = 0;
			SetFocus();
		}

		private void SetFocus()
		{
			this.eventSystem.SetSelectedGameObject(this.focusables[focus].gameObject);
		}

		private void ExecuteFocus()
		{
			ExecuteEvents.Execute(this.focusables[focus].gameObject, new BaseEventData(this.eventSystem), ExecuteEvents.submitHandler);
		}

		private void FocusPrevious()
		{
			this.eventSystem.sendNavigationEvents = false;
			--this.focus;

			if(this.focus < 0)
				this.focus = this.focusables.Length - 1;

			SetFocus();
			this.eventSystem.sendNavigationEvents = true;
		}

		private void FocusNext()
		{
			this.eventSystem.sendNavigationEvents = false;
			++this.focus;

			if(this.focus > this.focusables.Length - 1)
				this.focus = 0;

			SetFocus();
		}

		private void InitializeController()
		{
			MenuController controller = this.projectorBrain.GetComponent<MenuController>();

			if(controller == null)
				controller = this.projectorBrain.gameObject.AddComponent<MenuController>();

			controller.Manager = this;
		}
    }
}
