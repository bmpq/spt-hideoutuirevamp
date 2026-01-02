using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System.IO;
using UnityEngine;

namespace tarkin.huir
{
    [BepInPlugin("com.tarkin.huir", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        public new ManualLogSource Logger => base.Logger;

        private void Start()
        {
            new Patch_AreasPanel_Awake().Enable();
            new Patch_AreaPanel_Init().Enable();
            new Patch_HideoutScreenRear_ShowAsync().Enable();
            new Patch_AreasPanel_DoScroll().Enable();
            new Patch_AreaWorldPanel_Awake().Enable();
            new Patch_AreaScreenSubstrate_Awake().Enable();
        }
    }
}
