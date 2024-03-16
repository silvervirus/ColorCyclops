
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ColorCyclops.Main;
using HarmonyLib;
using Newtonsoft.Json;
using UnityEngine;

[HarmonyPatch(typeof(CyclopsDestructionEvent))]
[HarmonyPatch("Update")]
internal class cyclopsengine_Color_Patch
{
    private static Material excludedMaterial;
    private static bool materialsCached;
    private static float updateInterval;
    private static float timer;
    private static bool isFirstUpdate;
    private static HashSet<string> includedFullNames;
    private static HashSet<string> excludedFullNames;

    static cyclopsengine_Color_Patch()
    {
        materialsCached = false;
        updateInterval = 30f;
        timer = 0f;
        isFirstUpdate = true;
        LoadFullNames();
    }

    private static void LoadFullNames()
    {
        try
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string includePath = Path.Combine(directoryName, "IncludeObjectList.json");
            string excludePath = Path.Combine(directoryName, "ExcludeObjectList.json");

            List<string> includeCollection = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(includePath));
            includedFullNames = new HashSet<string>(includeCollection);

            List<string> excludeCollection = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(excludePath));
            excludedFullNames = new HashSet<string>(excludeCollection);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load object lists: " + ex.Message);
            includedFullNames = new HashSet<string>();
            excludedFullNames = new HashSet<string>();
        }
    }

    public static bool Prefix(CyclopsDestructionEvent __instance)
    {
        if (!materialsCached)
        {
            try
            {
                excludedMaterial = Resources.Load<Material>("Materials/ExcludedMaterial");
                materialsCached = true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load excluded material: " + ex.Message);
            }
        }
        if (isFirstUpdate)
        {
            UpdateMeshRenderers(TransformExtensions.GetAllComponentsInChildren<MeshRenderer>((Component)__instance));
            isFirstUpdate = false;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                UpdateMeshRenderers(TransformExtensions.GetAllComponentsInChildren<MeshRenderer>((Component)__instance));
                timer = 0f;
            }
        }
        return true;
    }

    private static void UpdateMeshRenderers(MeshRenderer[] meshRenderers)
    {
        float r = Main.Instance.CycloprValue.Value / 255f;
        float g = Main.Instance.CyclopgValue.Value / 255f;
        float b = Main.Instance.CyclopbValue.Value / 255f;

        foreach (MeshRenderer val in meshRenderers)
        {
            string name = ((UnityEngine.Object)val).name;
            // Check if the object's name is the Cyclops main prefab clone and does not contain excluded items before coloring
            if ((name.Equals("Cyclops-MainPrefab(Clone)") || includedFullNames.Contains(name)) && !ContainsExcludedItems(name))
            {
                Material[] sharedMaterials = ((Renderer)val).sharedMaterials;
                for (int j = 0; j < sharedMaterials.Length; j++)
                {
                    sharedMaterials[j].color = new Color(r, g, b, ((Renderer)val).material.color.a);
                }
                ((Renderer)val).sharedMaterials = sharedMaterials;
            }
        }
    }

    private static bool ContainsExcludedItems(string name)
    {
        foreach (string excludedName in excludedFullNames)
        {
            // Check for an exact match of the excluded name
            if (name.Equals(excludedName))
            {
                return true;
            }
        }
        return false;
    }
}