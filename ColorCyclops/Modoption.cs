using Nautilus.Options;
using BepInEx.Configuration;
using Nautilus.Handlers;
using System.Collections.Generic;
using Nautilus.Utility;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using ConfigFile = BepInEx.Configuration.ConfigFile;
using System.Xml;
using ColorCyclops.Main;

namespace ColorCyclops.options
{

    public class ColorCyclopConfig
    {
        

       

       

       
        public class ColorCyclopOptions : ModOptions
        {
            public ColorCyclopOptions() : base("ColorCyclops")
            {

                AddItem(Main.Main.Instance.CycloprValue.ToModSliderOption(0f, 255f));
                AddItem(Main.Main.Instance.CyclopgValue.ToModSliderOption(0f, 255f));
                AddItem(Main.Main.Instance.CyclopbValue.ToModSliderOption(0f, 255f));
                AddItem(Main.Main.Instance.updateInterval.ToModSliderOption(10f, 50f));
                AddItem(Main.Main.Instance.Timer.ToModSliderOption(0f, 1f));
               
            }
        }
    }
}