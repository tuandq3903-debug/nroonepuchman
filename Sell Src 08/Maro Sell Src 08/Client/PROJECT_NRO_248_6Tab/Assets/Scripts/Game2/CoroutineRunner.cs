using System;
using System.Collections;
using UnityEngine;

namespace Game2
{
	// Token: 0x02000384 RID: 900
	public class CoroutineRunner : MonoBehaviour
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x0027B7CE File Offset: 0x002799CE
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

		// Token: 0x0600282C RID: 10284 RVA: 0x00027444 File Offset: 0x00025644
		public void RunCoroutine(IEnumerator coroutine)
		{
			base.StartCoroutine(coroutine);
		}

		// Token: 0x04004D3F RID: 19775
		private static CoroutineRunner _instance;
	}
}
