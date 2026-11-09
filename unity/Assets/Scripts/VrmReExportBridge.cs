// Integration scaffold — wire this to UniVRM 0.106.0 APIs in the Unity project.
// This file is intentionally not presented as a working exporter yet.
using UnityEngine;

public class VrmReExportBridge : MonoBehaviour
{
    // WebGL JS should pass the selected VRM bytes into Unity, then receive exported bytes.
    // TODO: Implement VRM 0.x import and export using the installed UniVRM 0.106.0 package.
    public void ImportAndReExport(byte[] inputVrm)
    {
        if (inputVrm == null || inputVrm.Length == 0)
        {
            Debug.LogError("No VRM data received.");
            return;
        }

        Debug.Log("VRM data received (" + inputVrm.Length + " bytes). UniVRM import/export is not wired yet.");
    }
}
