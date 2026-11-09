using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UniGLTF;
using VRM;
using VRMShaders;

/// <summary>
/// Runtime VRM 0.x import/re-export bridge for the browser UI.
/// The browser calls ImportAndReExportBase64 through Unity SendMessage.
/// </summary>
public class VrmReExportBridge : MonoBehaviour
{
    private RuntimeGltfInstance _loadedInstance;
    private bool _isProcessing;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void DownloadVrm(string base64, string fileName);
#endif

    // Called by the WebGL page using:
    // unityInstance.SendMessage("VrmReExportBridge", "ImportAndReExportBase64", base64);
    public async void ImportAndReExportBase64(string inputBase64)
    {
        if (_isProcessing)
        {
            Debug.LogWarning("A VRM conversion is already running.");
            return;
        }

        if (string.IsNullOrEmpty(inputBase64))
        {
            Debug.LogError("No VRM data was provided.");
            return;
        }

        _isProcessing = true;
        RuntimeGltfInstance newlyLoaded = null;

        try
        {
            var inputBytes = Convert.FromBase64String(inputBase64);
            if (inputBytes.Length == 0)
            {
                throw new InvalidDataException("The selected VRM file is empty.");
            }

            Debug.Log("Importing VRM 0.x (" + inputBytes.Length + " bytes) with UniVRM 0.106.0...");

            // This is the byte-array runtime import API from UniVRM 0.106.0.
            newlyLoaded = await VrmUtility.LoadBytesAsync("input.vrm", inputBytes);
            newlyLoaded.ShowMeshes();
            newlyLoaded.EnableUpdateWhenOffscreen();

            // Keep the previous avatar until the new input has imported successfully.
            if (_loadedInstance != null)
            {
                Destroy(_loadedInstance.gameObject);
            }
            _loadedInstance = newlyLoaded;
            newlyLoaded = null;

            Debug.Log("VRM imported. Exporting as VRM 0.x...");

            // VRMExporter is the VRM 0.x exporter in UniVRM 0.106.0.
            var exportingData = VRMExporter.Export(
                new GltfExportSettings(),
                _loadedInstance.gameObject,
                new RuntimeTextureSerializer());

            var outputBytes = exportingData.ToGlbBytes();
            if (outputBytes == null || outputBytes.Length == 0)
            {
                throw new InvalidDataException("UniVRM returned an empty export.");
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            DownloadVrm(Convert.ToBase64String(outputBytes), "re-exported.vrm");
#elif UNITY_EDITOR
            var outputPath = Path.Combine(Application.persistentDataPath, "re-exported.vrm");
            File.WriteAllBytes(outputPath, outputBytes);
            Debug.Log("VRM exported to: " + outputPath);
#else
            var outputPath = Path.Combine(Application.persistentDataPath, "re-exported.vrm");
            File.WriteAllBytes(outputPath, outputBytes);
            Debug.Log("VRM exported to: " + outputPath);
#endif
            Debug.Log("VRM 0.x re-export completed (" + outputBytes.Length + " bytes).");
        }
        catch (Exception exception)
        {
            if (newlyLoaded != null)
            {
                Destroy(newlyLoaded.gameObject);
            }
            Debug.LogError("VRM re-export failed: " + exception);
        }
        finally
        {
            _isProcessing = false;
        }
    }
}
