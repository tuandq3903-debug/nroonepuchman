using System;
using UnityEngine.Networking;

namespace Game2
{
	// Token: 0x02000379 RID: 889
	public class BypassCertificateHandler : CertificateHandler
	{
		// Token: 0x0600272C RID: 10028 RVA: 0x00006D67 File Offset: 0x00004F67
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
