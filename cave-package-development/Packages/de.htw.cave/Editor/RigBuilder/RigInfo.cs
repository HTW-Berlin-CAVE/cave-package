using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Htw.Cave.Projector;
using Htw.Cave.Util;

namespace Htw.Cave.RigBuilder
{
	[System.Serializable]
	public enum RigTemplate
	{
		Basic,
		Cube,
		Custom
	}

	[Flags]
	[System.Serializable]
	public enum RigSelection
	{
		Front = 1 << 0,
		Back = 1 << 1,
		Top = 1 << 2,
		Bottom = 1 << 3,
		Left = 1 << 4,
		Right = 1 << 5
	}

    public class RigInfo
    {
		public RigTemplate template;
		public RigSelection selection;
		public string name;
		public float width;
		public float height;
		public float length;
		public Vector3 position;
		public bool buildReady;
		public string assetsFolder;
		public bool sceneTools;
		public bool menu;
		public bool kinect;
		public bool joycon;
    }
}
