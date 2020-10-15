using System.Collections.Generic;
using UnityEngine;
using KSP.Localization;
using KSP.UI.Screens;
using ToolbarControl_NS;
using ClickThroughFix;

namespace Stock_Default_Settings
{

    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public partial class StockDefaultSettings : MonoBehaviour
    {
        const string ALT = "Use Alt.Skin";
        const float SDSWIDTH = 450;
        const float SDSHEIGHT = 100;
        Rect sdsRect = new Rect((Screen.width - SDSWIDTH) /2, Screen.height - (Screen.height - SDSHEIGHT) / 2, SDSWIDTH, SDSHEIGHT);

        bool useAltSkin = true;

        internal const string MODID = "StockDefaultSettings_NS";
        internal const string MODNAME = "StockDefaultSettings";        bool visible = false;
        Settings settings;
        ToolbarControl toolbarControl;


        string[] Aliases = new string []{ "off","", "2x", "", "4x","","","", "8x" };
        string[] TexQualities = new string[] {"Full Res", "Half Res","Quarter Res","Eighth Res" };

        void Start()
        {
            initToolbarButtons();
        }

        void initToolbarButtons()
        {
            toolbarControl = gameObject.AddComponent<ToolbarControl>();            toolbarControl.AddToAllToolbars(ToggleWindow, ToggleWindow,                ApplicationLauncher.AppScenes.SPACECENTER | ApplicationLauncher.AppScenes.FLIGHT |                ApplicationLauncher.AppScenes.MAPVIEW | ApplicationLauncher.AppScenes.VAB |                ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.TRACKSTATION,                MODID,                "StockSettingsButton",                "StockDefaultSettings/PluginData/icon-38",                "StockDefaultSettings/PluginData/icon-24",                "StockDefaultSettings"            );
        }
        void ToggleWindow()
        {
            visible = !visible;
            if (visible)
            {
                settings = new Settings();
            }

        }
        void removeLauncherButtons()
        {            toolbarControl.OnDestroy();
            Destroy(toolbarControl);
            toolbarControl = null;
        }
        void OnGUI()
        {
            if (visible)
            {
                if (!useAltSkin)
                    GUI.skin = HighLogic.Skin;
                sdsRect = GUILayout.Window(this.GetInstanceID() + 1, sdsRect, new GUI.WindowFunction(Window), "Stock Default Settings", new GUILayoutOption[0]);
            }
        }

        void Window(int id)
        {
            GUILayout.BeginVertical();

            settings.HIGHLIGHT_FX = GUILayout.Toggle(settings.HIGHLIGHT_FX, "Highlight FX");
            settings.INFLIGHT_HIGHLIGHT = GUILayout.Toggle(settings.INFLIGHT_HIGHLIGHT, "Part Highlighter Enabled in Flight");

            bool enableTemperatureGauges = ((settings.TEMPERATURE_GAUGES_MODE & 1) > 0);
            bool enableThermalHighlights = ((settings.TEMPERATURE_GAUGES_MODE & 2) > 0);

            enableTemperatureGauges = GUILayout.Toggle(enableTemperatureGauges, "Temperature Gauges");
            enableThermalHighlights = GUILayout.Toggle(enableThermalHighlights, "Thermal Highlights");

            settings.TEMPERATURE_GAUGES_MODE = (enableTemperatureGauges ? 1 : 0) + (enableThermalHighlights ? 2 : 0);

            settings.CAMERA_DOUBLECLICK_MOUSELOOK = GUILayout.Toggle(settings.CAMERA_DOUBLECLICK_MOUSELOOK, "Double-Click Mouse Look");
            settings.ADVANCED_TWEAKABLES = GUILayout.Toggle(settings.ADVANCED_TWEAKABLES, "Advanced Tweakables");
            settings.CONFIRM_MESSAGE_DELETION = GUILayout.Toggle(settings.CONFIRM_MESSAGE_DELETION, "Confirm Message Deletion");
            settings.ADVANCED_MESSAGESAPP = GUILayout.Toggle(settings.ADVANCED_MESSAGESAPP, "Advanced Message App");
            settings.EXTENDED_BURNTIME = GUILayout.Toggle(settings.EXTENDED_BURNTIME, "Extended Burn Indicator");

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Anti-Aliasing", GUILayout.Width(90));
            GUI.enabled = settings.ANTI_ALIASING > Settings.AntiAlias._off;
            if (GUILayout.Button("<", GUILayout.Width(30)))
            {
                settings.ANTI_ALIASING--;
                while (Aliases[(int)settings.ANTI_ALIASING] == "")
                    settings.ANTI_ALIASING--;
            }
            GUI.enabled = true;
            GUILayout.Space(45); 
            GUILayout.Label(Aliases[(int)settings.ANTI_ALIASING], GUILayout.Width(60));
            GUI.enabled = settings.ANTI_ALIASING < Settings.AntiAlias._8x;
            if (GUILayout.Button(">", GUILayout.Width(30)))
            {
                settings.ANTI_ALIASING++;
                while (Aliases[(int)settings.ANTI_ALIASING] == "")
                    settings.ANTI_ALIASING++;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Texture Quality", GUILayout.Width(90));
            GUI.enabled = settings.TEXTURE_QUALITY < Settings.TextureQuality.EighthRes;
            if (GUILayout.Button("<", GUILayout.Width(30)))
                settings.TEXTURE_QUALITY++;
            GUI.enabled = true;
            GUILayout.Space(25);
            GUILayout.Label(TexQualities[(int)settings.TEXTURE_QUALITY], GUILayout.Width(80));
            GUI.enabled = settings.TEXTURE_QUALITY > Settings.TextureQuality.FullRes;
            if (GUILayout.Button(">", GUILayout.Width(30)))
                settings.TEXTURE_QUALITY--;
            GUILayout.EndHorizontal();
            GUI.enabled = true;



            GUILayout.Space(10);
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Apply", GUILayout.Width(60)))
                settings.Save();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Accept", GUILayout.Width(60)))
            {
                settings.Save();
                visible = false;
            }
            if (GUILayout.Button("Reset to default", GUILayout.MinWidth(60)))
            {
                Settings.LocalSettings = false;
                ScenarioData.Instance.OnLoad(null);
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Cancel", GUILayout.Width(60)))
            {
                visible = false;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            GUI.enabled = !Settings.LocalSettings;
            if (GUILayout.Button("Save for current game"))
                settings.Save(true);
            GUILayout.FlexibleSpace();
            GUI.enabled = Settings.LocalSettings;
            if (GUILayout.Button("Clear local settings & load global settings"))
            {
                Settings.LocalSettings = false;
                ScenarioData.Instance.OnLoad(null);
            }
            GUI.enabled = true;
            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();


            GUILayout.EndVertical();
            GUI.DragWindow();
        }


    }

}
