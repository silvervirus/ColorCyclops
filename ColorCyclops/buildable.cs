using System;
using ColorCyclops;
using HarmonyLib;
using UnityEngine;
using ColorCyclops.Main;
namespace Colorlife
{
    // Harmony patch for BulkheadDoor's Awake method
    [HarmonyPatch(typeof(BulkheadDoor))]
    [HarmonyPatch("Awake")]
    internal class constructable_Color_Patch
    {
        public static bool Prefix(BulkheadDoor __instance)
        {
            // Get all MeshRenderers in the children of the BulkheadDoor
            MeshRenderer[] allComponentsInChildren = __instance.GetAllComponentsInChildren<MeshRenderer>();

            // Loop through each MeshRenderer
            foreach (MeshRenderer meshRenderer in allComponentsInChildren)
            {
                // Apply color changes based on specific conditions
                if (meshRenderer.name.Contains("Submarine_Steering_Console") ||
                    meshRenderer.name.Contains("cyclops_engine_room") ||
                    meshRenderer.name.Contains("submarine_hatch_01") ||
                    meshRenderer.name.Contains("Submarine_steering_console_base_02") ||
                    meshRenderer.name.Contains("Inner_hatch") ||
                    meshRenderer.name.Contains("Inner_hatch_base") ||
                    meshRenderer.name.Contains("Outer_hatch_base") ||
                    meshRenderer.name.Contains("Outer_hatch"))
                {
                    meshRenderer.sharedMaterial.color = new Color32(
                        Convert.ToByte(Main.Instance.CycloprValue.Value),
                        Convert.ToByte(Main.Instance.CyclopgValue.Value),
                        Convert.ToByte(Main.Instance.CyclopbValue.Value),
                        1);
                }
            }

            return true;
        }
    }
}
