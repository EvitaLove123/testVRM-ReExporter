# Implementation notes

## Goal
Import VRM 0.x, re-export VRM 0.x with **UniVRM 0.106.0**, then download the output in a browser.

## Dependencies
VRM 0.x uses UniVRM packages com.vrmc.gltf and com.vrmc.univrm. Pin the 0.106.0 release/tag and verify Unity compatibility.

## Remaining work
1. Create/open a Unity project and install UniVRM 0.106.0.
2. Implement runtime VRM 0.x import (not VRM 1.0 migration).
3. Export the loaded avatar as VRM 0.x.
4. Implement WebGL JS ↔ Unity byte transfer and browser Blob download.
5. Build WebGL and deploy its output to GitHub Pages.
6. Test with the sample model and VRM Posing Mobile.

A static HTML page cannot run Unity C# runtime. GitHub Pages can host compiled WebGL, but cannot compile Unity itself. Use exact APIs from UniVRM 0.106.0 source/samples; don't guess signatures.
