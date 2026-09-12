using System;
using UnityEngine.Networking;

namespace Game5
{
	// Token: 0x020000F1 RID: 241
	public class BypassCertificateHandler : CertificateHandler
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x00006D67 File Offset: 0x00004F67
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
