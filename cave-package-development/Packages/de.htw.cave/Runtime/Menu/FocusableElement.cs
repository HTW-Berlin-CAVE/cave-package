using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Htw.Cave.Joycon;

namespace Htw.Cave.Menu
{
	[RequireComponent(typeof(Button))]
    public sealed class FocusableElement : MonoBehaviour
    {
		[SerializeField]
		private int order;
		public int Order
		{
			get { return this.order; }
			set { this.order = value; }
		}
    }
}
