using System;
using System.Collections;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002AC RID: 684
	public class CoroutineRunner : MonoBehaviour
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x001E672A File Offset: 0x001E492A
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

		// Token: 0x06001E88 RID: 7816 RVA: 0x00027444 File Offset: 0x00025644
		public void RunCoroutine(IEnumerator coroutine)
		{
			base.StartCoroutine(coroutine);
		}

		// Token: 0x04003AC0 RID: 15040
		private static CoroutineRunner _instance;
	}
}
