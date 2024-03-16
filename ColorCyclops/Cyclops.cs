using System;
using ColorCyclops;
using HarmonyLib;
using UnityEngine;
using ColorCyclops.Main;
namespace Colorlife
{
    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch("Update")]
    internal class test_Color_Patch
    {
        public static bool Prefix(SubRoot __instance)
        {
            // Get all SkinnedMeshRenderer components in the submarine's children
            SkinnedMeshRenderer[] allComponentsInChildren = __instance.GetAllComponentsInChildren<SkinnedMeshRenderer>();

            // Iterate through each SkinnedMeshRenderer
            foreach (SkinnedMeshRenderer meshRenderer in allComponentsInChildren)
            {
                // Update color based on specific conditions
                if (meshRenderer.name.Contains("Inner_hatch") ||
                    meshRenderer.name.Contains("Inner_hatch_base"))
                {
                    meshRenderer.sharedMaterial.color = new Color32(
                        Convert.ToByte(Main.Instance.CycloprValue.Value),
                        Convert.ToByte(Main.Instance.CyclopgValue.Value),
                        Convert.ToByte(Main.Instance.CyclopbValue.Value),
                        1);
                }
                else if (meshRenderer.name.Contains("submarine_ladder_hallway_hatch_02") ||
                         meshRenderer.name.Contains("submarine_hatch_03") ||
                         meshRenderer.name.Contains("submarine_hatch_02 3") ||
                         meshRenderer.name.Contains("Submarine_Steering_Console"))
                {
                    meshRenderer.material.color = new Color32(
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
