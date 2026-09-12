using System;
using UnityEngine.Networking;

namespace Game1
{
	// Token: 0x02000451 RID: 1105
	public class BypassCertificateHandler : CertificateHandler
	{
		// Token: 0x060030D0 RID: 12496 RVA: 0x00006D67 File Offset: 0x00004F67
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}
}
