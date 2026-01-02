using EFT.Hideout;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace tarkin.huir
{
    internal class Patch_CommonUI_Awake : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_CommonUI_Awake));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(CommonUI), nameof(CommonUI.Awake));
        }

        [PatchPostfix]
        private static void PatchPostfix(CommonUI __instance)
        {
            try
            {
                IReadOnlyDictionary<EInventoryTab, Tab> _tabDictionary = (IReadOnlyDictionary<EInventoryTab, Tab>)AccessTools.Field(typeof(InventoryScreen), "_tabDictionary").GetValue(__instance.InventoryScreen);
                
                ModState.TabPrefab = _tabDictionary[EInventoryTab.Overall].gameObject;
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}