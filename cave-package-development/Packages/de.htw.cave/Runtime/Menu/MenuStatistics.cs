using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Htw.Cave.Joycon;

namespace Htw.Cave.Menu
{
    public sealed class MenuStatistics : MonoBehaviour
    {
		private const float fpsMeasurePeriod = 0.5f;

		[SerializeField]
		private Text framesPerSecondText;
		public Text FramesPerSecondText
		{
			get { return this.framesPerSecondText; }
			set { this.framesPerSecondText = value; }
		}

		[SerializeField]
		private Text timesSinceStartupText;
		public Text TimesSinceStartupText
		{
			get { return this.timesSinceStartupText; }
			set { this.timesSinceStartupText = value; }
		}

		[SerializeField]
		private Text connectedControllerText;
		public Text ConnectedControllerText
		{
			get { return this.connectedControllerText; }
			set { this.connectedControllerText = value; }
		}

		[SerializeField]
		private Text controllerLeftRightText;
		public Text ControllerLeftRightText
		{
			get { return this.controllerLeftRightText; }
			set { this.controllerLeftRightText = value; }
		}

		private MenuManager manager;
		private int fpsAccumulator = 0;
		private float fpsNextPeriod = 0;

		public void Awake()
		{
			this.manager = base.GetComponentInParent<MenuManager>();
		}

		public void OnEnable()
		{
			this.fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

			if(JoyconHelper.Ready())
			{
				this.connectedControllerText.text = string.Format("{0}", JoyconHelper.GetJoycons().Count);
				this.controllerLeftRightText.text = string.Format("{0} / {1}", JoyconHelper.GetLeftJoycons().Count, JoyconHelper.GetRightJoycons().Count);
			}
		}

		public void Update()
		{
			++fpsAccumulator;

			if (Time.realtimeSinceStartup > fpsNextPeriod)
			{
				this.framesPerSecondText.text = string.Format("{0:###, ###}", this.fpsAccumulator / fpsMeasurePeriod);
				this.fpsAccumulator = 0;
				this.fpsNextPeriod += fpsMeasurePeriod;
			}

			this.timesSinceStartupText.text = string.Format("{0:###, ###.0}s", Time.realtimeSinceStartup);
		}
    }
}
