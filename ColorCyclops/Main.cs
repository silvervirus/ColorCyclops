using BepInEx;
using BepInEx.Configuration;
using ColorCyclops.options;
using HarmonyLib;
using Nautilus.Handlers;
using System;
using System.Reflection;
using UnityEngine;

namespace ColorCyclops.Main
{
    [BepInPlugin(Main.PLUGIN_GUID, Main.PLUGIN_NAME, Main.PLUGIN_VERSION)]
    public class Main : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "ColorCyclop";
        public const string PLUGIN_NAME = "ColorCyclop.SN";
        public const string PLUGIN_VERSION = "1.0.0";

        public static Main Instance { get; private set; }

        public ConfigEntry<float> CycloprValue { get; private set; }
        public ConfigEntry<float> CyclopgValue { get; private set; }
        public ConfigEntry<float> CyclopbValue { get; private set; }
        public ConfigEntry<float> CyclopaValue { get; private set; }
        public ConfigEntry<float> updateInterval;
        public ConfigEntry<float> Timer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);

            // Call Start method of MaterialNameExporter to export material names
            MaterialNameExporter exporter = FindObjectOfType<MaterialNameExporter>();
            if (exporter != null)
            {
                exporter.Start();
            }
            else
            {
                Debug.LogError("MaterialNameExporter not found in the scene.");
            }

            // Setup plugin configuration
            SetupConfig();

            // Patch Harmony methods
            new Harmony("ColorCyclops.mod").PatchAll(Assembly.GetExecutingAssembly());
            OptionsPanelHandler.RegisterModOptions(new ColorCyclopConfig.ColorCyclopOptions());
            Debug.Log("[ColorCyclops] Successfully patched.");
        }

        private void SetupConfig()
        {
            CycloprValue = Config.Bind("General", "Redcolor", 255f, "Change this to change the amount of Red used.");
            CyclopgValue = Config.Bind("General", "Greencolor", 255f, "Change this to change the amount of Green used.");
            CyclopbValue = Config.Bind("General", "Bluecolor", 255f, "Change this to change the amount of Blue used.");
            updateInterval = Config.Bind("General", "UpdateInterval", 30f, "Change the update interval for the color updater.");
            Timer = Config.Bind("General", "Timer", 0f, "Change the update timer value.");
        }
    }
}
