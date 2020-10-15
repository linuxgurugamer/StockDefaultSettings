

namespace Stock_Default_Settings
{
    internal class Settings
    {
        internal static bool LocalSettings = false;
        internal enum AntiAlias { _off = 0, _2x = 2, _4x = 4, _8x = 8 };
        internal enum TextureQuality { FullRes = 0, HalfRes = 1, QuarterRes = 2, EighthRes = 3 };

        internal bool HIGHLIGHT_FX;
        internal bool INFLIGHT_HIGHLIGHT;
        internal int TEMPERATURE_GAUGES_MODE;
        internal bool CAMERA_DOUBLECLICK_MOUSELOOK;
        internal bool ADVANCED_TWEAKABLES;
        internal bool CONFIRM_MESSAGE_DELETION;
        internal bool ADVANCED_MESSAGESAPP;
        internal bool EXTENDED_BURNTIME;

        internal AntiAlias ANTI_ALIASING;
        internal TextureQuality TEXTURE_QUALITY;

        internal Settings()
        {
            HIGHLIGHT_FX = GameSettings.HIGHLIGHT_FX;
            INFLIGHT_HIGHLIGHT = GameSettings.INFLIGHT_HIGHLIGHT;
            TEMPERATURE_GAUGES_MODE = GameSettings.TEMPERATURE_GAUGES_MODE;
            CAMERA_DOUBLECLICK_MOUSELOOK = GameSettings.CAMERA_DOUBLECLICK_MOUSELOOK;
            ADVANCED_TWEAKABLES = GameSettings.ADVANCED_TWEAKABLES;
            CONFIRM_MESSAGE_DELETION = GameSettings.CONFIRM_MESSAGE_DELETION;
            ADVANCED_MESSAGESAPP = GameSettings.ADVANCED_MESSAGESAPP;
            EXTENDED_BURNTIME = GameSettings.EXTENDED_BURNTIME;

            ANTI_ALIASING = (AntiAlias)GameSettings.ANTI_ALIASING;
            TEXTURE_QUALITY = (TextureQuality)GameSettings.TEXTURE_QUALITY;
        }
        internal void Save(bool local = false)
        {
            GameSettings.HIGHLIGHT_FX = HIGHLIGHT_FX;
            GameSettings.INFLIGHT_HIGHLIGHT = INFLIGHT_HIGHLIGHT;
            GameSettings.TEMPERATURE_GAUGES_MODE = TEMPERATURE_GAUGES_MODE;
            GameSettings.CAMERA_DOUBLECLICK_MOUSELOOK = CAMERA_DOUBLECLICK_MOUSELOOK;
            GameSettings.ADVANCED_TWEAKABLES = ADVANCED_TWEAKABLES;
            GameSettings.CONFIRM_MESSAGE_DELETION = CONFIRM_MESSAGE_DELETION;
            GameSettings.ADVANCED_MESSAGESAPP = ADVANCED_MESSAGESAPP;
            GameSettings.EXTENDED_BURNTIME = EXTENDED_BURNTIME;

            GameSettings.ANTI_ALIASING = (int)ANTI_ALIASING;
            GameSettings.TEXTURE_QUALITY = (int)TEXTURE_QUALITY;

            if (local)
            {
                LocalSettings = true;
            }
        }
    }
}

