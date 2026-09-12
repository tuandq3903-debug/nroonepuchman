using System;
using UnityEngine.Networking;

namespace Game6
{
	// Token: 0x02000019 RID: 25
	public class BypassCertificateHandler : CertificateHandler
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00006D67 File Offset: 0x00004F67
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
