using EFT;
using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;

namespace tarkin.huir
{
    internal class Patch_AreasPanel_DoScroll : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreasPanel), nameof(AreasPanel.method_4));
        }

        [PatchPrefix]
        private static bool PatchPrefix(AreasPanel __instance)
        {
            return false;
        }
    }
}