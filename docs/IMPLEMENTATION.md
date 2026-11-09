# UniVRM 0.106.0 implementation plan

This repository targets **VRM 0.x in → VRM 0.x out**, using UniVRM 0.106.0.

## Current state
- Android-friendly file picker UI: `web/index.html`
- Unity bridge scaffold: `unity/Assets/Scripts/VrmReExportBridge.cs`
- Actual import/export and WebGL byte transfer: **not implemented yet**

## Required implementation
1. Add a Unity project and install UniVRM 0.106.0.
2. Implement VRM 0.x import with the exact API in the installed package.
3. Export the imported instance as VRM 0.x (do not convert to VRM 1.0).
4. Add a WebGL .jslib bridge: browser File bytes → Unity; exported bytes → browser Blob download.
5. Build WebGL and publish its build output under `web/`.
6. Test with the supplied sample and VRM Posing Mobile.

Do not treat changing the metadata exporter string alone as a re-export: the model must pass through the actual UniVRM importer/exporter.
