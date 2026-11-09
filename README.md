# testVRM-ReExporter

Unity WebGL project concept to import VRM 0.x and re-export as VRM 0.x using UniVRM 0.106.0, inspired by Napharmonia's workflow.

## Current status
- Mobile-friendly browser UI in web/index.html
- Unity bridge placeholder in unity/Assets/Scripts/VrmReExportBridge.cs
- Integration notes in docs/IMPLEMENTATION.md
- **Not a working exporter yet:** Unity import/export calls, WebGL byte transfer, and a compiled WebGL build are still missing.

## Target
VRM 0.x input -> UniVRM 0.106.0 import/export -> VRM 0.x output.

Changing only the exporter metadata string is not a real re-export; the avatar must pass through actual importer and exporter.

[Repository](https://github.com/EvitaLove123/testVRM-ReExporter)
