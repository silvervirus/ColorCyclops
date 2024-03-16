using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ObjectListsLoader : MonoBehaviour
{
    private const string includeJsonFileName = "IncludeObjectList.json";
    private const string excludeJsonFileName = "ExcludeObjectList.json";

    public HashSet<string> IncludedFullNames { get; private set; }
    public HashSet<string> ExcludedFullNames { get; private set; }

    private void Awake()
    {
        IncludedFullNames = LoadOrCreateJsonFile(includeJsonFileName, GetDefaultIncludedFullNames());
        ExcludedFullNames = LoadOrCreateJsonFile(excludeJsonFileName, new HashSet<string>());
    }

    private HashSet<string> LoadOrCreateJsonFile(string jsonFileName, HashSet<string> defaultData)
    {
        HashSet<string> result = new HashSet<string>();
        string jsonPath = Path.Combine(Application.persistentDataPath, jsonFileName);

        try
        {
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                if (!string.IsNullOrEmpty(json))
                {
                    List<string> items = JsonConvert.DeserializeObject<List<string>>(json);
                    result = new HashSet<string>(items);
                }
            }
            else
            {
                WriteJsonFile(jsonPath, defaultData);
                result = defaultData;
                Debug.Log($"JSON file '{jsonFileName}' not found. Creating with default data.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading JSON file '{jsonFileName}': {e.Message}");
            result = defaultData;
        }

        return result;
    }

    private void WriteJsonFile(string jsonPath, IEnumerable<string> data)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(jsonPath, jsonData);
            Debug.Log($"JSON file '{jsonPath}' created successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error writing JSON file '{jsonPath}': {e.Message}");
        }
    }

    private HashSet<string> GetDefaultIncludedFullNames()
    {
        return new HashSet<string>
        {
            "Submarine_tech_box_LOD1",
            "submarine_hologram_projector",
            "Inner_hatch",
            "Inner_hatch_base",
            "submarine_locker_01_01/hinge 2/submarine_locker_01_door",
            "Submarine_Steering_Console"
            // Add more default included full names as needed
        };
    }
}
