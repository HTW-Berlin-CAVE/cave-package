using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JoyconLib;

namespace Htw.Cave.Joycon
{
	public static class JoyconHelper
    {
		public static bool Ready()
		{
			return JoyconManager.Instance != null;
		}

		public static List<JoyconLib.Joycon> GetJoycons()
		{
			return JoyconManager.Instance.joycons;
		}

		public static JoyconLib.Joycon GetLeftJoycon()
		{
			foreach(JoyconLib.Joycon joycon in GetJoycons())
			{
				if(joycon.isLeft)
					return joycon;
			}

			return null;
		}

		public static JoyconLib.Joycon GetRightJoycon()
		{
			foreach(JoyconLib.Joycon joycon in GetJoycons())
			{
				if(!joycon.isLeft)
					return joycon;
			}

			return null;
		}

		public static List<JoyconLib.Joycon> GetLeftJoycons()
		{
			List<JoyconLib.Joycon> list = new List<JoyconLib.Joycon>();

			foreach(JoyconLib.Joycon joycon in GetJoycons())
			{
				if(joycon.isLeft)
					list.Add(joycon);
			}

			return list;
		}

		public static List<JoyconLib.Joycon> GetRightJoycons()
		{
			List<JoyconLib.Joycon> list = new List<JoyconLib.Joycon>();

			foreach(JoyconLib.Joycon joycon in GetJoycons())
			{
				if(!joycon.isLeft)
					list.Add(joycon);
			}

			return list;
		}

		public static int GetJoyconsCount()
		{
			return GetJoycons().Count;
		}
	}
}
