using UnityEngine;
using ToolbarControl_NS;

namespace Stock_Default_Settings
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class ToolbarRegistration : MonoBehaviour
    {
        void Start()
        {
            ToolbarControl.RegisterMod(StockDefaultSettings.MODID, StockDefaultSettings.MODNAME);
        }
    }
}