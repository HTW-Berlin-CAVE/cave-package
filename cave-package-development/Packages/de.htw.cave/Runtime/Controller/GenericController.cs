using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Controller
{
	[AddComponentMenu("Htw.Cave/Controller/Generic Controller")]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]
    public sealed class GenericController : MonoBehaviour
    {
		[SerializeField]
		private Transform head;
		public Transform Head
		{
			get { return head; }
			set { this.head = value; }
		}

		[SerializeField]
		private float speed;
		public float Speed
		{
			get { return speed; }
			set { this.speed = value; }
		}

		[SerializeField]
		private float snappyness;
		public float Snappyness
		{
			get { return snappyness; }
			set { this.snappyness = value; }
		}

		[SerializeField]
		private float sensitivity;
		public float Sensitivity
		{
			get { return sensitivity; }
			set { this.sensitivity = value; }
		}

		[SerializeField]
		private bool freezeControls;
		public bool FreezeControls
		{
			get { return freezeControls; }
			set { this.freezeControls = value; }
		}

		private CapsuleCollider capsule;
		private Rigidbody rigid;
		private Vector3 direction;

		public void Awake()
		{
			this.capsule = base.GetComponent<CapsuleCollider>();
			this.rigid = base.GetComponent<Rigidbody>();
		}

		public void Start()
		{
			this.rigid.freezeRotation = true;
			CalculateCollider();
		}

		public void FixedUpdate()
		{
			Vector3 targetVelocity = this.direction * this.speed;
			Vector3 deltaVelocity = targetVelocity - this.rigid.velocity;

			if(this.rigid.useGravity)
				deltaVelocity.y = 0;

			this.rigid.AddForce(deltaVelocity * this.snappyness, ForceMode.Acceleration);
		}

		public void Update()
		{
			CalculateCollider();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.capsule = base.GetComponent<CapsuleCollider>();
			this.capsule.center = new Vector3(0f, 1f, 0f);
			this.capsule.height = 2f;
			this.capsule.radius = 0.3f;
		}
#endif

		public void Move(float h, float v)
		{
			if(this.freezeControls)
				return;

			this.direction = (transform.forward * v + transform.right * h).normalized;
		}

		public void Rotate(float y)
		{
			if(this.freezeControls)
				return;

			Vector3 rotation = new Vector3(0f, y, 0f);
			rotation = rotation * sensitivity;

			transform.Rotate(rotation);
		}

		private void CalculateCollider()
		{
			Vector3 position = this.head.localPosition;

			this.capsule.center = new Vector3(position.x, position.y * 0.5f, position.z);
			this.capsule.height = position.y;
		}
    }
}
