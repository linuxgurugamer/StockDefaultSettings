using System.IO;
using KSP_Log;
using Steamworks;

namespace Stock_Default_Settings
{
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, new GameScenes[] { GameScenes.SPACECENTER })]
    class ScenarioData : ScenarioModule
    {
        static internal ScenarioData Instance;

        const string NODENAME = "StockDefaultSettings";
        const string DEFAULTS = "defaults.cfg";
        const string GLOBAL = "global.cfg";

        Log Log = new Log("StockDefaultSettings.ScenarioData", Log.LEVEL.INFO);

        void Start()
        {
            Instance = this;
        }
        void LoadDataFromNode(ConfigNode node)
        {
            node.TryGetValue("HIGHLIGHT_FX", ref GameSettings.HIGHLIGHT_FX);
            node.TryGetValue("INFLIGHT_HIGHLIGHT", ref GameSettings.INFLIGHT_HIGHLIGHT);
            node.TryGetValue("TEMPERATURE_GAUGES_MODE", ref GameSettings.TEMPERATURE_GAUGES_MODE);
            node.TryGetValue("CAMERA_DOUBLECLICK_MOUSELOOK", ref GameSettings.CAMERA_DOUBLECLICK_MOUSELOOK);
            node.TryGetValue("ADVANCED_TWEAKABLES", ref GameSettings.ADVANCED_TWEAKABLES);
            node.TryGetValue("CONFIRM_MESSAGE_DELETION", ref GameSettings.CONFIRM_MESSAGE_DELETION);
            node.TryGetValue("ADVANCED_MESSAGESAPP", ref GameSettings.ADVANCED_MESSAGESAPP);
            node.TryGetValue("EXTENDED_BURNTIME", ref GameSettings.EXTENDED_BURNTIME);

            node.TryGetValue("ANTI_ALIASING", ref GameSettings.ANTI_ALIASING);
            node.TryGetValue("TEXTURE_QUALITY", ref GameSettings.TEXTURE_QUALITY);

        }
        // ScenarioModule methods
        public override void OnLoad(ConfigNode rootNode)
        {
            string fname = KSPUtil.ApplicationRootPath + "GameData/StockDefaultSettings/PluginData/" + DEFAULTS;
            if (System.IO.File.Exists(fname))
            {
                Log.Info("defaults.cfg found");
                ConfigNode node = ConfigNode.Load(fname);
                LoadDataFromNode(node);
            }
            if (rootNode != null)
            {
                Settings.LocalSettings = rootNode.HasNode(NODENAME);
            }
            if (Settings.LocalSettings)
            {
                if (rootNode != null)
                {
                    var node = rootNode.GetNode(NODENAME);
                    LoadDataFromNode(node);
                }
            }
            else
            {
                fname = KSPUtil.ApplicationRootPath + "GameData/StockDefaultSettings/PluginData/" + GLOBAL;
                if (System.IO.File.Exists(fname))
                {
                    Log.Info("defaults.cfg found");
                    ConfigNode node = ConfigNode.Load(fname);
                    LoadDataFromNode(node);
                }
            }
        }

        public override void OnSave(ConfigNode rootNode)
        {
            ConfigNode node = new ConfigNode(NODENAME);
            Settings settings = new Settings();

            node.AddValue("HIGHLIGHT_FX", settings.HIGHLIGHT_FX);
            node.AddValue("INFLIGHT_HIGHLIGHT", settings.INFLIGHT_HIGHLIGHT);
            node.AddValue("TEMPERATURE_GAUGES_MODE", settings.TEMPERATURE_GAUGES_MODE);
            node.AddValue("CAMERA_DOUBLECLICK_MOUSELOOK", settings.CAMERA_DOUBLECLICK_MOUSELOOK);
            node.AddValue("ADVANCED_TWEAKABLES", settings.ADVANCED_TWEAKABLES);
            node.AddValue("CONFIRM_MESSAGE_DELETION", settings.CONFIRM_MESSAGE_DELETION);
            node.AddValue("ADVANCED_MESSAGESAPP", settings.ADVANCED_MESSAGESAPP);
            node.AddValue("EXTENDED_BURNTIME", settings.EXTENDED_BURNTIME);

            node.AddValue("ANTI_ALIASING", (int)settings.ANTI_ALIASING);
            node.AddValue("TEXTURE_QUALITY", (int)settings.TEXTURE_QUALITY);
            if (Settings.LocalSettings)
            {

                rootNode.RemoveNode(NODENAME);

                rootNode.AddNode(node);

            }
            else
            {
                var fname = KSPUtil.ApplicationRootPath + "GameData/StockDefaultSettings/PluginData/" + GLOBAL;

                node.Save(fname);
                rootNode.RemoveNode(NODENAME);
            }
        }

    }
}
