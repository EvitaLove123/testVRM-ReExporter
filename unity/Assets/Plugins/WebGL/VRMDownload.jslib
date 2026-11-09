mergeInto(LibraryManager.library, {
  DownloadVrm: function (base64Ptr, fileNamePtr) {
    var base64 = UTF8ToString(base64Ptr);
    var fileName = UTF8ToString(fileNamePtr) || "re-exported.vrm";
    var binary = atob(base64);
    var bytes = new Uint8Array(binary.length);

    for (var i = 0; i < binary.length; i++) {
      bytes[i] = binary.charCodeAt(i);
    }

    var blob = new Blob([bytes], { type: "model/gltf-binary" });
    var objectUrl = URL.createObjectURL(blob);
    var anchor = document.createElement("a");
    anchor.href = objectUrl;
    anchor.download = fileName;
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
    URL.revokeObjectURL(objectUrl);

    var status = document.getElementById("info");
    if (status) status.textContent = "Đã xuất file " + fileName;
    var button = document.getElementById("export");
    if (button) button.disabled = false;
  }
});
