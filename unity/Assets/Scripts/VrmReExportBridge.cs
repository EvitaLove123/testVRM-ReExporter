using UnityEngine;

/// <summary>Browser-to-Unity WebGL bridge entry point; importer/exporter TODO.</summary>
public class VrmReExportBridge : MonoBehaviour
{
    public void ImportAndReExport(byte[] inputVrm)
    {
        if (inputVrm == null || inputVrm.Length == 0)
        {
            Debug.LogError("No VRM bytes were provided.");
            return;
        }
        // Placeholder only: UniVRM 0.x runtime import/export is not wired yet.
        Debug.Log("Received VRM bytes: " + inputVrm.Length);
    }
}
