# Implementation notes

## Goal
Import a VRM 0.x file, load it through **UniVRM 0.106.0**, export it again as VRM 0.x, and download the result in the browser.

## Source/version
- The supplied `UniVRM-0.106.0.zip` is the source archive for UniVRM 0.106.0.
- Its `ProjectSettings/ProjectVersion.txt` pins Unity **2020.3.34f1**.
- The runtime importer is `VRM.VrmUtility.LoadBytesAsync(string path, byte[] bytes, ...)`.
- The VRM 0.x exporter is `VRM.VRMExporter.Export(new UniGLTF.GltfExportSettings(), gameObject, new VRMShaders.RuntimeTextureSerializer())`, followed by `ToGlbBytes()`.
- The sample implementation is in `Assets/VRM/Samples~/RuntimeExporterSample/VRMRuntimeExporter.cs`.

## Unity setup
1. Open/create the Unity project with Unity **2020.3.34f1**.
2. Import the UniVRM 0.106.0 source from the supplied archive. For the full source archive, copy its required `Assets` content into the Unity project's `Assets` folder (including `UniGLTF`, `VRM`, `VRMShaders`, and their dependencies). Do not mix files from a newer UniVRM release.
3. Put `VrmReExportBridge.cs` on a scene GameObject named exactly `VrmReExportBridge`.
4. Keep `VRMDownload.jslib` under `Assets/Plugins/WebGL/`.
5. Build the project for WebGL. The exported build must be copied into the Pages-published web build directory.

## Runtime bridge added
- The browser passes the selected file to Unity as a Base64 string through `SendMessage`.
- `VrmReExportBridge.ImportAndReExportBase64` calls UniVRM 0.106.0's byte-array importer, then the VRM 0.x exporter.
- `VRMDownload.jslib` turns the exported bytes into a browser download.
- In the Unity Editor/non-WebGL player, the output is written to `Application.persistentDataPath/re-exported.vrm`.

## Remaining work / verification
1. Wire the WebGL build loader into `web/index.html` and make the UI call the bridge once the build is present.
2. Build with Unity 2020.3.34f1 and resolve any project-specific import/compiler issues.
3. Deploy the actual WebGL build to GitHub Pages.
4. Test with one VRM 0.x sample and verify the output opens in an independent VRM viewer and VRM Posing Mobile.

A static HTML page cannot execute Unity C# by itself. GitHub Pages can host a compiled WebGL build, but cannot compile Unity. **This source integration has not yet been compiled or runtime-tested**, so it must not be described as a working deployed exporter until those tests pass.

## Known implementation trade-off
The first browser bridge uses Base64 strings for the browser-to-Unity and Unity-to-browser transfers. This is simple to wire up but temporarily increases memory use for large VRM files. A pointer/byte-buffer bridge can replace it later if mobile memory testing shows this is too costly.
