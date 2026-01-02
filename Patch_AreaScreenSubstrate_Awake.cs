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
        private static void PatchPostfix(AreaScreenSubstrate __instance, 
            ref float ____maxHeight,
            AreaPanel ____areaPanel)
        {
            try
            {
                __instance.RectTransform.anchoredPosition = new Vector2(__instance.RectTransform.anchoredPosition.x, 60f);
                ____maxHeight = 850f;

                ____areaPanel.RectTransform.anchorMin = new Vector2(0, 0.5f);
                ____areaPanel.RectTransform.anchorMax = new Vector2(0, 0.5f);
                ____areaPanel.RectTransform.anchoredPosition = new Vector2(15f, 0);
                ____areaPanel.transform.parent.GetComponent<LayoutElement>().preferredHeight = 37f;

                ____areaPanel.AreaName.fontSize = 18f;
                ____areaPanel.AreaName.fontStyle = TMPro.FontStyles.Bold;

                __instance.RectTransform.Find("Border").SetSiblingIndex(1);
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}