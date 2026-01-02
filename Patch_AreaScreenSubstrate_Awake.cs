using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace tarkin.huir
{
    internal class Patch_AreaScreenSubstrate_Awake : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_AreaScreenSubstrate_Awake));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreaScreenSubstrate), nameof(AreaScreenSubstrate.Awake));
        }

        [PatchPostfix]
        private static void PatchPostfix(AreaScreenSubstrate __instance, LayoutElement ____contentLayout, ref float ____maxHeight)
        {
            try
            {
                __instance.RectTransform.anchoredPosition = new Vector2(__instance.RectTransform.anchoredPosition.x, 60f);
                ____maxHeight = 850f;
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}