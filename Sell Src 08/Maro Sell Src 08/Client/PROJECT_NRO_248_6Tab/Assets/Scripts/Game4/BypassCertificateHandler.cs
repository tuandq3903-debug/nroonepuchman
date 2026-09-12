using System;
using UnityEngine.Networking;

namespace Game4
{
	// Token: 0x020001C9 RID: 457
	public class BypassCertificateHandler : CertificateHandler
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x00006D67 File Offset: 0x00004F67
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
