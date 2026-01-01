using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine.UI;

namespace tarkin.huir
{
    internal class Patch_AreaPanel_Init : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_AreaPanel_Init));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreaPanel), nameof(AreaPanel.Init));
        }

        [PatchPostfix]
        private static void PatchPostfix(AreaPanel __instance)
        {
            try
            {
                if (__instance.gameObject.TryGetComponent<HorizontalLayoutGroup>(out var group))
                    group.childForceExpandWidth = false;
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}