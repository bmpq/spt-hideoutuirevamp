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
        internal static ManualLogSource Log = new ManualLogSource(MyPluginInfo.PLUGIN_NAME);

        private void Start()
        {
            new Patch_AreasPanel_Awake().Enable();
        }
    }
}
