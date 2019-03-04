# Nintendo Joy-Con Manual

![Joy-Cons](Resources/Nintendo-JoyCon.png)

## Details
The Joy-Con implementation is based on the *JoyconLib* which uses the *HIDAPI* plugin.
A *Joycon Manager* is required to find or access the connected Joy-Cons.
There are two different types of Joy-Cons with different button layouts.
![Joy-Con Layout](Resources/Nintendo-JoyCon-Buttons.png)
Just connect the Joy-Cons with your PC via Bluetooth and they should be found by the
*Joycon Manager* at runtime.

## Example Input Implementation
To make things easier a *JoyconHelper* class will provide you the most common functionalities.
Note that the helper requires a existing *Joycon Manager* and will not be available at *Awake*.

```c#
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using JoyconLib;
	using Htw.Cave.Joycon;

	public class ExampleJoyconInput : MonoBehaviour
	{
		private JoyconLib.Joycon leftJoycon;

		public void Start()
		{
			this.leftJoycon = JoyconHelper.GetLeftJoycon();
		}

		public void Update()
		{
			if(this.leftJoycon != null)
			{
				if(this.leftJoycon.GetButtonUp(JoyconLib.Joycon.Button.SHOULDER_1))
					Debug.Log("The Shoulder 1 Button was pressed!");
			}
		}
	}

```
