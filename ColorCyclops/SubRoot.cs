using System;
using HarmonyLib;
using UnityEngine;
using ColorCyclops.Main;
namespace Colorlife
{
    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch("Update")]
    internal class Cyclops_Color_Patch
    {
        public static bool Prefix(SubRoot __instance)
        {
            SkinnedMeshRenderer[] skinnedMeshRenderers = __instance.GetAllComponentsInChildren<SkinnedMeshRenderer>();
            MeshRenderer[] meshRenderers = __instance.GetAllComponentsInChildren<MeshRenderer>();

            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                if (skinnedMeshRenderer.name.Contains("Submarine") ||
                    skinnedMeshRenderer.name.Contains("Steering_Console_Geo"))
                {
                    skinnedMeshRenderer.material.color = new Color32(
                        Convert.ToByte(Main.Instance.CycloprValue.Value),
                        Convert.ToByte(Main.Instance.CyclopgValue.Value),
                        Convert.ToByte(Main.Instance.CyclopbValue.Value),
                        1);
                }
            }

            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (meshRenderer.name.Contains("ladder_hall_walls") ||
                    meshRenderer.name.Contains("main_room_walls") ||
                  
                    meshRenderer.name.Contains("compartment_cyclops_diving_chamber_01") ||
                    meshRenderer.name.Contains("launch_bay_01_01") ||
                    meshRenderer.name.Contains("launch_bay_01_02") ||
                    meshRenderer.name.Contains("Submarine_tech_box") ||
                    meshRenderer.name.Contains("Object024") ||
                    meshRenderer.name.Contains("submarine_engine_01_base") ||
                    meshRenderer.name.Contains("submarine_locker_01_door") ||
                    meshRenderer.name.Contains("submarine_engine_power_cells_01") ||
                    meshRenderer.name.Contains("Submarine_console_02") ||
                    meshRenderer.name.Contains("Submarine_console_03") ||
                    meshRenderer.name.Contains("submarine_hatch_01_base") ||
                    meshRenderer.name.Contains("submarine_hatch_01") ||
                    meshRenderer.name.Contains("Outer_hatch_base") ||
                    meshRenderer.name.Contains("Outer_hatch") ||
                    meshRenderer.name.Contains("submarine_ladder_hallway_hatch_02") ||
                    meshRenderer.name.Contains("submarine_hatch_03"))
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
