using System;
using UnityEngine.Networking;

namespace Game3
{
	// Token: 0x020002A1 RID: 673
	public class BypassCertificateHandler : CertificateHandler
	{
		// Token: 0x06001D88 RID: 7560 RVA: 0x00006D67 File Offset: 0x00004F67
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
