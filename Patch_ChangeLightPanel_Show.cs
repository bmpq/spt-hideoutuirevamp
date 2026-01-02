using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace tarkin.huir
{
    internal class Patch_ChangeLightPanel_Show : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_ChangeLightPanel_Show));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(ChangeLightPanel), nameof(ChangeLightPanel.Show));
        }

        [PatchPostfix]
        private static void PatchPostfix(ChangeLightPanel __instance)
        {
            try
            {
                if (__instance.TryGetComponent<HorizontalLayoutGroup>(out var layout))
                {
                    Component.DestroyImmediate(layout);

                    __instance.RectTransform.pivot = Vector2.zero;
                    __instance.RectTransform.anchorMin = Vector2.zero;
                    __instance.RectTransform.anchorMax = Vector2.zero;
                    __instance.RectTransform.anchoredPosition = new Vector2(35f, 60f);

                    var staircase = __instance.gameObject.AddComponent<StaircaseVerticalLayoutGroup>();
                    staircase.spacing = -45f;
                    staircase.staircaseOffset = 30;
                    staircase.childControlWidth = true;
                    staircase.childControlHeight = true;
                    staircase.childForceExpandWidth = false;
                    staircase.childForceExpandHeight = false;
                    staircase.reverseArrangement = true;
                }
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}