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
using Htw.Cave.Controller;

namespace Htw.Cave.Menu
{
	[RequireComponent(typeof(ProjectorBrain))]
    public sealed class MenuController : MonoBehaviour
    {
		public MenuManager Manager { get; set; }

		private GenericController controller;
		private JoyconLib.Joycon leftJoycon;

		public void Awake()
		{
			this.controller = base.GetComponent<GenericController>();
		}

		public void Start()
		{
			this.leftJoycon = JoyconHelper.GetLeftJoycon();
			CloseMenu();
		}

		public void Update()
		{
			if(Input.GetKeyUp(KeyCode.Escape))
				ToggleMenu();

			if(this.leftJoycon != null)
			{
				if(this.leftJoycon.GetButtonUp(JoyconLib.Joycon.Button.MINUS))
					ToggleMenu();
			}
		}

		public void ToggleMenu()
		{
			if(this.Manager.gameObject.activeSelf)
				CloseMenu();
			else
				OpenMenu();
		}

		public void OpenMenu()
		{
			if(this.controller != null)
				this.controller.FreezeControls = true;

			this.Manager.gameObject.SetActive(true);
		}

		public void CloseMenu()
		{
			if(this.controller != null)
				this.controller.FreezeControls = false;

			this.Manager.gameObject.SetActive(false);
		}
    }
}
