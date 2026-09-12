using System;
using System.Collections;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001D4 RID: 468
	public class CoroutineRunner : MonoBehaviour
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00151686 File Offset: 0x0014F886
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

		// Token: 0x060014E4 RID: 5348 RVA: 0x00027444 File Offset: 0x00025644
		public void RunCoroutine(IEnumerator coroutine)
		{
			base.StartCoroutine(coroutine);
		}

		// Token: 0x04002841 RID: 10305
		private static CoroutineRunner _instance;
	}
}
