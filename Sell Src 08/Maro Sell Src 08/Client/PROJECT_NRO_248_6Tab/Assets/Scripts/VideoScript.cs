using System;
using UnityEngine;
using UnityEngine.Video;

// Token: 0x02000004 RID: 4
public class VideoScript : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x000020C0 File Offset: 0x000002C0
	// (set) Token: 0x06000003 RID: 3 RVA: 0x000020C7 File Offset: 0x000002C7
	public static VideoScript instance { get; private set; }

	// Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
	private void Awake()
	{
		if (VideoScript.instance == null)
		{
			VideoScript.instance = this;
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
	private void Start()
	{
		if (this.video == null)
		{
			Camera main = Camera.main;
			this.video = ((main != null) ? main.GetComponent<VideoPlayer>() : null);
		}
		if (this.video != null)
		{
			this.video.Prepare();
			this.video.loopPointReached += this.OnVideoEnd;
			this.video.Play();
			this.videoRenderer = base.GetComponent<Renderer>();
			return;
		}
		Debug.LogError("Không tìm thấy VideoPlayer trên Main Camera!");
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002174 File Offset: 0x00000374
	public void paint()
	{
		if (this.video != null && this.video.texture != null)
		{
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.video.texture);
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000021CD File Offset: 0x000003CD
	private void OnVideoEnd(VideoPlayer vp)
	{
		Debug.Log("Video đã kết thúc!");
		vp.Stop();
		vp.targetTexture = null;
		if (this.videoRenderer != null)
		{
			this.videoRenderer.enabled = false;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000220C File Offset: 0x0000040C
	private void OnGUI()
	{
		if (this.video != null)
		{
			this.paint();
		}
	}

	// Token: 0x0400000F RID: 15
	private Renderer videoRenderer;

	// Token: 0x04000010 RID: 16
	public VideoPlayer video;

	// Token: 0x04000011 RID: 17
	public Texture2D videoFrames;
}
