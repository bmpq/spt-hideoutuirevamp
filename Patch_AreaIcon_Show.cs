using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;

namespace tarkin.huir
{
    internal class Patch_AreaIcon_Show : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_AreaIcon_Show));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreaIcon), nameof(AreaIcon.Show));
        }

        [PatchPostfix]
        private static void PatchPostfix(AreaIcon __instance)
        {
            try
            {
                __instance.BackgroundImage.raycastPadding = new Vector4(0, 20, 0, 20);
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}