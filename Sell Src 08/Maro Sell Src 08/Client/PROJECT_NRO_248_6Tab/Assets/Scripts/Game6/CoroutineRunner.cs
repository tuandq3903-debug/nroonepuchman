using System;
using System.Collections;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000024 RID: 36
	public class CoroutineRunner : MonoBehaviour
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00027416 File Offset: 0x00025616
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

		// Token: 0x0600019C RID: 412 RVA: 0x00027444 File Offset: 0x00025644
		public void RunCoroutine(IEnumerator coroutine)
		{
			base.StartCoroutine(coroutine);
		}

		// Token: 0x04000344 RID: 836
		private static CoroutineRunner _instance;
	}
}
