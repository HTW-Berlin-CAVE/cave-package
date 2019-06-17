using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Htw.Cave.Hub
{
    public static class HubBuilder
    {
		[MenuItem("GameObject/Htw.Cave/Hub", false, 31)]
		public static void CreateHub()
		{
			GameObject hub = new GameObject("Hub");

			GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
			floor.name = "Hub Floor";
			floor.transform.parent = hub.transform;
			floor.transform.localPosition = Vector3.zero;
			floor.transform.localScale = new Vector3(2f, 1f, 2f);
			floor.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles20x20();

			GameObject roof = GameObject.CreatePrimitive(PrimitiveType.Plane);
			roof.name = "Hub Roof";
			roof.transform.parent = hub.transform;
			roof.transform.localPosition = new Vector3(0f, 20f, 0f);
			roof.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			roof.transform.localScale = new Vector3(2f, 1f, 2f);
			roof.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles20x20();

			GameObject wallFront = GameObject.CreatePrimitive(PrimitiveType.Plane);
			wallFront.name = "Hub Wall Front";
			wallFront.transform.parent = hub.transform;
			wallFront.transform.localPosition = new Vector3(0f, 10f, 10f);
			wallFront.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
			wallFront.transform.localScale = new Vector3(2f, 1f, 2f);
			wallFront.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles20x20();

			GameObject wallBack = GameObject.CreatePrimitive(PrimitiveType.Plane);
			wallBack.name = "Hub Wall Back";
			wallBack.transform.parent = hub.transform;
			wallBack.transform.localPosition = new Vector3(0f, 10f, -10f);
			wallBack.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			wallBack.transform.localScale = new Vector3(2f, 1f, 2f);
			wallBack.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles20x20();

			GameObject wallLeft = GameObject.CreatePrimitive(PrimitiveType.Plane);
			wallLeft.name = "Hub Wall Left";
			wallLeft.transform.parent = hub.transform;
			wallLeft.transform.localPosition = new Vector3(-10f, 10f, 0f);
			wallLeft.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			wallLeft.transform.localScale = new Vector3(2f, 1f, 2f);
			wallLeft.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles20x20();

			GameObject wallRight = GameObject.CreatePrimitive(PrimitiveType.Plane);
			wallRight.name = "Hub Wall Right";
			wallRight.transform.parent = hub.transform;
			wallRight.transform.localPosition = new Vector3(10f, 10f, 0f);
			wallRight.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			wallRight.transform.localScale = new Vector3(2f, 1f, 2f);
			wallRight.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles20x20();

			GameObject cubeFlying = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubeFlying.name = "Hub Cube Flying";
			cubeFlying.transform.parent = hub.transform;
			cubeFlying.transform.localPosition = new Vector3(2f, 2f, 2f);
			cubeFlying.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			cubeFlying.transform.localScale = Vector3.one;
			cubeFlying.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles2x2();

			GameObject cubeLying  = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubeLying.name = "Hub Cube Lying";
			cubeLying.transform.parent = hub.transform;
			cubeLying.transform.localPosition = new Vector3(-3f, 0.5f, -1f);
			cubeLying.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			cubeLying.transform.localScale = Vector3.one;
			cubeLying.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles2x2();

			GameObject cubeTiny = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubeTiny.name = "Hub Cube Large";
			cubeTiny.transform.parent = hub.transform;
			cubeTiny.transform.localPosition = new Vector3(4f, 0.25f, -2f);
			cubeTiny.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			cubeTiny.transform.localScale = Vector3.one * 0.5f;
			cubeTiny.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles3x3();

			GameObject cubeLarge = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cubeLarge.name = "Hub Cube Tiny";
			cubeLarge.transform.parent = hub.transform;
			cubeLarge.transform.localPosition = new Vector3(-4f, 2.5f, 4f);
			cubeLarge.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			cubeLarge.transform.localScale = Vector3.one * 1.5f;
			cubeLarge.GetComponent<MeshRenderer>().material = MaterialUtility.Tiles3x3();
		}
    }
}
