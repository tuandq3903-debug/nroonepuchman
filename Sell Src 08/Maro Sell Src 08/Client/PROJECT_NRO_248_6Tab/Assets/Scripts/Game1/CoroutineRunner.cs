using System;
using System.Collections;
using UnityEngine;

namespace Game1
{
	// Token: 0x0200045C RID: 1116
	public class CoroutineRunner : MonoBehaviour
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x00310872 File Offset: 0x0030EA72
		public static CoroutineRunner Instance
		{
			get
			{
				if (CoroutineRunner._instance == null)
				{
					GameObject gameObject = new GameObject("CoroutineRunner");
					CoroutineRunner._instance = gameObject.AddComponent<CoroutineRunner>();
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
				}
				return CoroutineRunner._instance;
			}
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x00027444 File Offset: 0x00025644
		public void RunCoroutine(IEnumerator coroutine)
		{
			base.StartCoroutine(coroutine);
		}

		// Token: 0x04005FBE RID: 24510
		private static CoroutineRunner _instance;
	}
}
