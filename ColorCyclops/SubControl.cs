using System;
using ColorCyclops;
using HarmonyLib;
using UnityEngine;
using ColorCyclops.Main;
namespace Colorlife
{
    [HarmonyPatch(typeof(SubControl))]
    [HarmonyPatch("Update")]
    internal class cyclopstest_Color_Patch
    {
        public static bool Prefix(SubControl __instance)
        {
            MeshRenderer[] allComponentsInChildren = __instance.GetAllComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in allComponentsInChildren)
            {
                if (meshRenderer.name.Contains("submarine_hologram_projector"))
                {
                    meshRenderer.material.color = new Color32(
                        Convert.ToByte(Main.Instance.CycloprValue.Value),
                        Convert.ToByte(Main.Instance.CyclopgValue.Value),
                        Convert.ToByte(Main.Instance.CyclopbValue.Value),
                        1);
                }
                else if (meshRenderer.name.Contains("Submarine_Steering_Console"))
                {
                    meshRenderer.material.color = new Color32(
                        Convert.ToByte(Main.Instance.CycloprValue.Value),
                        Convert.ToByte(Main.Instance.CyclopgValue.Value),
                        Convert.ToByte(Main.Instance.CyclopbValue.Value),
                        1);
                }
                else if (meshRenderer.name.Contains("Cyclops_Bridge_control_room"))
                {
                    meshRenderer.sharedMaterial.color = new Color32(
                        Convert.ToByte(Main.Instance.CycloprValue.Value),
                        Convert.ToByte(Main.Instance.CyclopgValue.Value),
                        Convert.ToByte(Main.Instance.CyclopbValue.Value),
                        1);
                }
                else if (meshRenderer.name.Contains("launch_bay_arms_left") ||
                         meshRenderer.name.Contains("_walls") ||
                         meshRenderer.name.Contains("_floors") ||
                         meshRenderer.name.Contains("launch_bay_"))
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
