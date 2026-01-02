using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;

namespace tarkin.huir
{
    internal class Patch_HideoutScreenOverlay_Awake : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_HideoutScreenOverlay_Awake));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(HideoutScreenOverlay), nameof(HideoutScreenOverlay.Awake));
        }

        [PatchPostfix]
        private static void PatchPostfix(
            ComplementaryButton ____nightVisionButton,
            ComplementaryButton ____changeLightButton,
            ComplementaryButton ____customizationButton,
            ComplementaryButton ____generatorButton)
        {
            try
            {
                const float startX = 380f;
                const float startY = 40f;
                const float spacing = 75f;

                var buttons = new[]
                {
                    ____nightVisionButton,
                    ____changeLightButton,
                    ____customizationButton,
                    ____generatorButton
                };

                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].RectTransform.anchorMin = Vector2.zero;
                    buttons[i].RectTransform.anchorMax = Vector2.zero;
                    buttons[i].RectTransform.pivot = Vector2.zero;

                    buttons[i].RectTransform.anchoredPosition = new Vector2(startX + (spacing * i), startY);
                }
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}
