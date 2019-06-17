using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Joycon;

namespace Htw.Cave.Controller
{
	[AddComponentMenu("Htw.Cave/Controller/Joycon User Control")]
	[RequireComponent(typeof(GenericController))]
	public class JoyconUserControl : MonoBehaviour
    {
		private GenericController controller;

		public void Awake()
		{
			this.controller = base.GetComponent<GenericController>();
		}

		public void Update()
		{
			if(this.controller.FreezeControls)
				return;

			int joycons = JoyconHelper.GetJoyconsCount();
			if(joycons > 0)
				if(joycons > 1)
					JoyconPair();
				else
					JoyconSingle();
		}

		private void JoyconSingle()
		{
			float[] stick = JoyconHelper.GetLeftJoycon().GetStick();

			this.controller.Move(0f, stick[1]);
			this.controller.Rotate(stick[0]);
		}

		private void JoyconPair()
		{
			float[] stickLeft = JoyconHelper.GetLeftJoycon().GetStick();
			float[] stickRight = JoyconHelper.GetRightJoycon().GetStick();

			this.controller.Move(stickLeft[0], stickLeft[1]);
			this.controller.Rotate(stickRight[0]);
		}
	}
}
