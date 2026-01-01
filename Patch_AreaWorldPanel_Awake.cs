using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;

namespace tarkin.huir
{
    internal class Patch_AreaWorldPanel_Awake : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreaWorldPanel), nameof(AreaWorldPanel.Awake));
        }

        [PatchPostfix]
        private static void PatchPostfix(AreaWorldPanel __instance, ref Vector2 ____restrictedBorder)
        {
            float targetX = 360f;

            // BSG's reference resolution
            const float refWidth = 1920f;
            const float refHeight = 1080f;

            // BSG's scalar logic (which is just ScreenMatchMode.Expand)
            float scaleX = (float)Screen.width / refWidth;
            float scaleY = (float)Screen.height / refHeight;
            float bsScale = Mathf.Min(scaleX, scaleY);

            float refCenter = refWidth / 2f;
            float offsetFromCenter = targetX - refCenter;

            float pixelOffset = offsetFromCenter * bsScale;
            float borderPixelEdge = ((float)Screen.width / 2f) + pixelOffset;

            float normalizedX = borderPixelEdge / (float)Screen.width;
            ____restrictedBorder = new Vector2(normalizedX, 1f);
        }
    }
}