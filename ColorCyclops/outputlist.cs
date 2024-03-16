using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MaterialNameExporter : MonoBehaviour
{
    public string outputFileName = "MaterialNames.json";

    public void Start()
    {
        // Find all materials in the project
        Material[] allMaterials = Resources.FindObjectsOfTypeAll<Material>();

        // Extract names of all materials
        HashSet<string> materialNames = new HashSet<string>();
        foreach (Material mat in allMaterials)
        {
            if (mat != null && !string.IsNullOrEmpty(mat.name))
            {
                materialNames.Add(mat.name);
            }
        }

        // Convert to list for JSON serialization
        List<string> materialNamesList = new List<string>(materialNames);

        // Convert to JSON
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(materialNamesList, Newtonsoft.Json.Formatting.Indented);

        // Write JSON to file
        string outputPath = Path.Combine(Path.Combine(UnityEngine.Application.dataPath, "BepInEx/plugins/ColorCyclops"), outputFileName);

        // Debug: Log JSON content
        Debug.Log("JSON content:\n" + json);

        // Debug: Log output path
        Debug.Log("Output path: " + outputPath);

        try
        {
            File.WriteAllText(outputPath, json);
            Debug.Log("Material names exported to: " + outputPath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to export material names: " + ex.Message);
        }
    }
}
