using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Htw.Cave.Projector;

namespace Htw.Cave.Util
{
	[AddComponentMenu("Htw.Cave/Misc/Old Format Converter")]
	[RequireComponent(typeof(ProjectorEmitter))]
	[ExecuteInEditMode]
    public sealed class OldFormatConverter : MonoBehaviour
    {
		[SerializeField]
		private TextAsset txtConfiguration;
		public TextAsset TxtConfiguration
		{
			get { return this.txtConfiguration; }
			set { this.txtConfiguration = value; }
		}

		public ProjectorConfiguration Configuration { get; private set; }

		public void Awake()
		{
			if(Application.isPlaying)
				base.enabled = false;
		}

		public void Convert()
		{
			this.Configuration = base.GetComponent<ProjectorEmitter>().Configuration;

			Vector2[] anchors = new Vector2[4];

            MemoryStream stream = new MemoryStream(this.txtConfiguration.bytes);

			using(StreamReader reader = new StreamReader(stream))
			{
				for(int i = 0; i < 16; ++i)
					reader.ReadLine();

				for(int i = 0; i < 4; ++i)
				{
					anchors[i].x = float.Parse(reader.ReadLine());
					anchors[i].y = float.Parse(reader.ReadLine());

					anchors[i] = (anchors[i] + Vector2.one) * 0.5f;
				}
			}

			this.Configuration.EqualizationAnchors = anchors;
			this.Configuration.ComputeEqualization();
		}
    }
}
