# PROJECT_NRO_248_6Tab — Claude Guide

Unity 2022.3.62f3 client cho **NRO 6-Tab** (Ngọc Rồng Online — bản mod 6 tài khoản đồng thời). Repo này chứa toàn bộ client game; server tương ứng nằm ở thư mục `htdocs/` ngang hàng với `Client/PROJECT_NRO_248_6Tab`.

Mọi thay đổi kỹ thuật nên bắt đầu bằng việc đọc file này. Khi sửa code, giữ nguyên phong cách đã có (xem "Quy ước code" bên dưới).

---

## 1. Tech stack

| Mục | Giá trị |
|---|---|
| Engine | Unity **2022.3.62f3** (`ProjectSettings/ProjectVersion.txt`) |
| Ngôn ngữ | C# (đơn assembly `Assembly-CSharp`, **không có `.asmdef`**) |
| .NET | Mono / IL2CPP tuỳ Player Settings |
| Graphics API | OpenGL ES / Metal / DirectX (mặc định Unity 2022 LTS) |
| Modules bật | UI, Tilemap, Physics2D, Video, Animation, Particles, Cloth, Terrain, AI Navigation, AR/VR, AssetBundle, Visual Scripting, JSON Serialization |

Xem `Packages/manifest.json` để biết danh sách package đầy đủ.

---

## 2. Cấu trúc thư mục

```
PROJECT_NRO_248_6Tab/
├── Assets/
│   ├── Resources/            ← Logo, font, asset runtime-load được
│   │   ├── fontsys/          ← Font bitmap
│   │   ├── res/              ← Asset chung
│   │   └── logoapp.png
│   ├── Scenes/
│   │   ├── NRO2..NRO6.unity  ← 5 scene gameplay (1 scene / tab đang chạy)
│   │   ├── NROL.unity        ← Scene loading
│   │   └── SampleScene.unity
│   ├── Scripts/
│   │   ├── TabManagement.cs  ← Static class quản lý 6 tab
│   │   ├── TabType.cs        ← enum Tab1..Tab6
│   │   ├── VideoScript.cs    ← Singleton phát video (intro/login)
│   │   ├── Properties/       ← AssemblyInfo.cs
│   │   ├── Game1/            ← Mã nguồn game world #1 (Char, Controller, Service…)
│   │   ├── Game2/            ← Bản sao #2 (chạy song song)
│   │   ├── Game3..Game6/     ← Bản sao #3..#6
│   │   └── Game1/Mod/        ← Mod hook (XMAP, ShowBoss…)
│   └── NewSurfaceShader.shader
├── Packages/manifest.json
├── ProjectSettings/          ← Gitignored ở root, nhưng local project giữ
├── .vscode/                  ← Editor config (settings, launch, extensions)
├── .vsconfig                 ← VS workload: ManagedGame
├── .gitignore                ← Local ignore (Temp/, Library/, .vscode/, *.sln…)
└── UpgradeLog.htm            ← Log migrate project lên Unity 2022
```

> **Quan trọng:** Mỗi `Game1..Game6` gần như là bản sao y hệt nhau của nhau (cùng file, cùng class name, chỉ khác namespace `Game1`…`Game6`). Đây là pattern "chạy 6 game song song trong 1 process" — mỗi tab là 1 instance runtime với state riêng.

---

## 3. Entry points & file quan trọng

### Khởi động runtime (theo scene)
1. **`Assets/Scenes/NROL.unity`** — scene loading đầu tiên (`NROLSettings.lighting` đi kèm). Thường dùng phát video intro.
2. **`Assets/Scripts/VideoScript.cs`** — `MonoBehaviour` singleton gắn lên Main Camera; phát `VideoPlayer` (UnityEngine.Video) full màn hình, tự hủy khi video kết thúc.
3. **`Assets/Scripts/TabManagement.cs`** — chứa state `tab`, `tabs[]`, `tabIndex`, `SyncTab`, `Hien_Menu_Dong_Bo` quyết định tab nào đang active, có đồng bộ server hay không.

### Vòng đời game chính
- **`Assets/Scripts/Game1/SplashScr.cs`** → **`LoginScr.cs`** → **`ServerListScreen.cs`** → **`GameScr.cs`** (gameplay loop)
- **`Controller.cs`** (namespace `Game1`) — singleton `Controller.gI()` xử lý `IMessageHandler.onMessage(Message)`. Đây là **switchcase khổng lồ** theo `msg.command` (cmd từ -112 đến 100+). Mọi packet từ server đều đi qua đây.
- **`Service.cs`** (namespace `Game1`) — outbound: gửi message lên server.

### Tab layer (game 6-tài khoản)
- `TabManagement.tab` (enum `TabType.Tab1..Tab6`)
- `TabManagement.tabIndex` (0..5)
- `TabManagement.tabs[]` = `{Tab1, Tab2, Tab3, Tab4, Tab5, Tab6}`
- `TabManagement.SyncTab` — bật/tắt đồng bộ qua lại giữa các tab
- `TabManagement.tabNames[]` = `{"Tab 1"…"Tab 6"}`

Mỗi tab có UI riêng (`Assets/Scripts/Game1/TabController.cs`, `TabCommand.cs`, `TabClanIcon.cs`…).

### Networking
- `Game1/BypassCertificateHandler.cs` — `UnityEngine.Networking.CertificateHandler` để bypass TLS verify (chứng tỏ client gọi HTTPS server test/dev).
- `Game1/Session_ME.cs`, `Session_ME2.cs` — session/connection manager.
- `Service.gI()` gửi message; `Controller.gI().onMessage(...)` nhận message.

---

## 4. Build & Scenes

`ProjectSettings/EditorBuildSettings.asset` liệt kê các scene build (theo thứ tự):

| # | Path | Lưu ý |
|---|---|---|
| 0 | `Assets/Scenes/NRO1.unity` | ⚠️ **scene này không tồn tại trong repo** — Unity sẽ tự tạo placeholder khi build hoặc remove khỏi build list |
| 1 | `Assets/Scenes/NRO2.unity` | ✅ |
| 2 | `Assets/Scenes/NRO3.unity` | ✅ |
| 3 | `Assets/Scenes/NRO4.unity` | ✅ |
| 4 | `Assets/Scenes/NRO5.unity` | ✅ |

Scenes có trên disk nhưng **không có trong build list**: `NRO6.unity`, `NROL.unity`, `SampleScene.unity`.

> 🔧 Nếu cần fix: mở Unity → `File ▸ Build Settings…` rồi add/remove scene cho khớp.

---

## 5. Quy ước code

- **Cảnh báo: code decompile.** Hầu hết `.cs` được tạo bởi tool decompiler nên có header token kiểu:
  ```csharp
  // Token: 0x020004ED RID: 1261
  // Token: 0x0600389C RID: 14492 RVA: 0x00371710 File Offset: 0x0036F910
  ```
  Khi sửa **giữ nguyên** các token comment này nếu có thể — chúng là dấu vết của ID cho tool như dnSpy/ILSpy; xoá đi thì lần sau re-decompile sẽ khó đối chiếu.
- **Ngôn ngữ chuỗi:** Tiếng Việt trong UI text (`mResources.*`, `T1.cs`). Ví dụ:
  ```csharp
  mResources.confirmChangeServer = "Bạn có muốn đổi máy chủ khác không?";
  ```
  Từng `GameN/T1.cs` định nghĩa lại resource → thay đổi UI phải sync cả 6 file.
- **Namespace:** Mỗi game copy dùng đúng namespace `GameN` tương ứng (1, 2, 3, 4, 5, 6). Refactor đổi namespace dễ vỡ linkage giữa 6 bản.
- **Singleton pattern phổ biến:**
  ```csharp
  public static T instance { get; private set; }
  private void Awake() {
      if (instance == null) { instance = this; return; }
      Destroy(gameObject);
  }
  ```
- **Mọi script gameplay thuộc `Assembly-CSharp.csproj`** (một file `.csproj` lớn sinh tự động trong `Assets/Scripts/`). Visual Studio / Rider mở file `.csproj` này là đủ.
- **Anti-pattern không sửa trừ khi bắt buộc:** `try { … } catch (Exception) { }` rỗng rất phổ biến; một số chỗ dùng reflection / `unsafe` / raw `byte[]` → giữ nguyên trừ khi có ticket riêng.
- **Mod folder** (`Game1/Mod/`): dùng cho hook mod (XMAP, ShowBoss). Mỗi Game có thể có mod riêng; check từng tab khi sửa.

---

## 6. Tác vụ thường gặp

### Mở project lần đầu
1. Cài Unity **2022.3.62f3** (Unity Hub → install archive build).
2. Mở Unity Hub → `Add` → trỏ tới `PROJECT_NRO_248_6Tab/` (folder chứa `Assets/`, `Packages/`, `ProjectSettings/`).
3. Editor tự re-import; `Library/` được tạo lại. Lần đầu mất 5–15 phút.

### Build Android APK / iOS IPA
- Unity Hub → `Switch Platform` sang Android hoặc iOS → `File ▸ Build Settings…` → `Build`.
- ⚠️ Một số cmd id trong `Controller.onMessage` chỉ có trên server Việt Nam — test phải nối server dev ở `htdocs/`.

### Đổi chuỗi UI / resource
- Mở `Assets/Scripts/GameN/T1.cs` (N = 1..6).
- Sửa giá trị `mResources.xxx = "..."`.
- Build lại.

### Thêm tab thứ 7 (khuyến nghị KHÔNG làm)
- Trước tiên phải thay đổi cả server (cmd protocol, slot account).
- Nếu bắt buộc: thêm `Tab7` vào `TabType.cs`, mở rộng `TabManagement.tabs[]` / `tabNames[]`, tạo folder `Game7/`, copy toàn bộ `.cs` từ `Game1/` đổi namespace.

### Patch protocol server
- Đa số logic nằm trong `GameN/Controller.cs::onMessage` switch-case theo `msg.command`.
- Trước khi đụng: dùng GitHub đối chiếu với bản gốc (NRO Java) để biết command đó map sang gì.

---

## 7. Các file KHÔNG đụng vào

- `ProjectSettings/ProjectVersion.txt`, `ProjectSettings/EditorBuildSettings.asset` — chỉ sửa qua Editor UI, không sửa tay.
- `Packages/packages-lock.json` — Unity tự lock khi resolve; không tự ý thêm package.
- `Assets/Scripts/Assembly-CSharp.csproj` — file Unity generate, commit làm cache để IDE mở nhanh nhưng sẽ tự regen mỗi lần build.

---

## 8. Known issues / quirks

1. **NRO1.unity reference trong build settings nhưng file không tồn tại** — sửa trong Unity Editor.
2. **Trùng lặp code 6 bản** (Game1..Game6) → tăng gấp 6 lần effort khi patch logic. Chưa có script tự đồng bộ; phải sửa tay cả 6.
3. **Một số file `.meta` lệch GUID do copy/paste folder** (`*.meta` cùng GUID giữa các scene) → có thể gây import conflict. Khi thấy cảnh báo "Duplicate GUID" trong Unity Console, regenerate bằng `Assets ▸ Reimport`.
4. **Code decompile có TODO token** rất phổ biến — KHÔNG xoá, dùng làm anchor khi re-decompile đối chiếu.
5. **Tiếng Việt trong code** — đảm bảo editor lưu file dưới UTF-8 (có BOM hay không đều OK, Unity đọc được cả hai).
6. **Library/, Temp/, Logs/, UserSettings/, .vscode/, *.csproj, *.sln, *.htm** đã được ignore ở cả `.gitignore` repo root lẫn local project — KHÔNG commit các folder này.
7. Cảnh báo không hợp lệ về `[Serializable]` cho `enum TabType` nếu serialize ra JSON.

---

## 9. Tài liệu liên quan

- `Assets/Scripts/TabManagement.cs` — state của 6 tab.
- `Assets/Scripts/Game1/Controller.cs` — message handler chính (>200KB, ~1000 case).
- `Assets/Scripts/Game1/Service.cs` — outbound message API.
- `Assets/Scripts/Game1/Char.cs` — model nhân vật (>200KB).
- `Assets/Scripts/Game1/Mod/XMAP/` — auto map mod (XMAP).
- `htdocs/` (sibling folder) — PHP server backend tương ứng.

---

## 10. Tóm tắt nhanh cho agent

- **Mục tiêu:** Unity 2022.3.62f3, ngôn ngữ chính C# + Tiếng Việt trong string.
- **Pattern:** 6 bản sao `GameN/` chạy song song, quản lý bằng `TabManagement`.
- **Đường vào:** NROL.unity → NRO2..NRO6.unity → `TabManagement` switch.
- **Sửa protocol:** `Controller.cs` switch-case theo `msg.command`.
- **Không tự ý:** upgrade Unity, đổi namespace, xoá TODO token, thêm tab mới.
