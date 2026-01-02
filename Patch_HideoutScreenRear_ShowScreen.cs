using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private static void PatchPostfix(HideoutScreenRear __instance, 
            Transform ____areaIconsContainer,
            TMP_Text ____hideoutText)
        {
            try
            {
                (____areaIconsContainer as RectTransform).offsetMin = new Vector2(360f, 0);

                var logo = ____hideoutText.transform.parent?.Find("Image")?.GetComponent<LayoutElement>();
                if (logo != null)
                {
                    logo.preferredWidth = 43.15f;
                    logo.preferredHeight = 50f;
                }

            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}