using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace tarkin.huir
{
    internal class Patch_AreasPanel_Awake : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_AreasPanel_Awake));

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreasPanel), nameof(AreasPanel.Awake));
        }

        [PatchPostfix]
        private static void PatchPostfix(AreasPanel __instance,
            Button ____leftButton, 
            Button ____rightButton,
            AreasScrollRect ____scrollView)
        {
            try
            {
                ____leftButton.gameObject.SetActive(false);
                ____rightButton.gameObject.SetActive(false);

                RectTransform rect = __instance.transform as RectTransform;
                rect.pivot = new Vector2(0, 0);
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(0, 1);
                rect.anchoredPosition = Vector2.zero;
                rect.offsetMin = new Vector2(0, 30f);
                rect.offsetMax = new Vector2(360f, 0);

                ____scrollView.vertical = true;
                ____scrollView.horizontal = false;
                ____scrollView.movementType = ScrollRect.MovementType.Clamped;
                ____scrollView.scrollSensitivity = 50;

                ____scrollView.viewport.anchorMin = new Vector2(0, 0);
                ____scrollView.viewport.anchorMax = new Vector2(1, 1);
                ____scrollView.viewport.offsetMin = new Vector2(0, 0);
                ____scrollView.viewport.offsetMax = new Vector2(0, 0);

                ____scrollView.content.pivot = new Vector2(0, 1);
                Component.DestroyImmediate(____scrollView.content.GetComponent<HorizontalLayoutGroup>());
                var vertical = ____scrollView.content.gameObject.AddComponent<VerticalLayoutGroup>();
                vertical.spacing = 10;
                vertical.padding = new RectOffset(10, 10, 10, 20);
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}