using System;
using System.Collections;
using UnityEngine;

namespace Game5
{
	// Token: 0x020000FC RID: 252
	public class CoroutineRunner : MonoBehaviour
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000BC5E2 File Offset: 0x000BA7E2
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

		// Token: 0x06000B40 RID: 2880 RVA: 0x00027444 File Offset: 0x00025644
		public void RunCoroutine(IEnumerator coroutine)
		{
			base.StartCoroutine(coroutine);
		}

		// Token: 0x040015C2 RID: 5570
		private static CoroutineRunner _instance;
	}
}
