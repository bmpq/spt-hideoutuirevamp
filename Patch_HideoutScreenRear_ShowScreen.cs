using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;

namespace tarkin.huir
{
    internal class Patch_HideoutScreenRear_ShowAsync : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_HideoutScreenRear_ShowAsync));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(HideoutScreenRear), nameof(HideoutScreenRear.ShowAsync));
        }

        [PatchPostfix]
        private static void PatchPostfix(HideoutScreenRear __instance, Transform ____areaIconsContainer)
        {
            try
            {
                (____areaIconsContainer as RectTransform).offsetMin = new Vector2(360f, 0);
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}