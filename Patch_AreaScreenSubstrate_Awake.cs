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
            AreaPanel ____areaPanel,
            RectTransform ____currentStageContainer,
            RectTransform ____nextStageContainer)
        {
            try
            {
                __instance.RectTransform.anchoredPosition = new Vector2(__instance.RectTransform.anchoredPosition.x, 60f);
                ____maxHeight = 890f;

                ____areaPanel.RectTransform.anchorMin = new Vector2(0, 0.5f);
                ____areaPanel.RectTransform.anchorMax = new Vector2(0, 0.5f);
                ____areaPanel.RectTransform.anchoredPosition = new Vector2(15f, 0);
                ____areaPanel.transform.parent.GetComponent<LayoutElement>().preferredHeight = 37f;

                ____areaPanel.AreaName.fontSize = 18f;
                ____areaPanel.AreaName.fontStyle = TMPro.FontStyles.Bold;

                __instance.RectTransform.Find("Border").SetSiblingIndex(1);

                CreateSimpleFilledHexagon(____areaPanel.Container, 37f, Color.black);

                ____currentStageContainer.GetComponent<VerticalLayoutGroup>().padding.top = 25;
                ____nextStageContainer.GetComponent<VerticalLayoutGroup>().padding.top = 25;
            }
            catch (Exception e) { Logger.LogError(e); }
        }

        private static void CreateSimpleFilledHexagon(RectTransform parent, float sideSize, Color color)
        {
            void CreateRect(Vector2 size, float rotationZ)
            {
                GameObject go = new GameObject("hexagon part");
                go.transform.SetParent(parent, false);

                Image img = go.AddComponent<Image>();
                img.color = color;

                RectTransform rt = go.GetComponent<RectTransform>();
                rt.sizeDelta = size;
                rt.localEulerAngles = new Vector3(0, 0, rotationZ);

                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.anchoredPosition = Vector2.zero;
            }

            float shortDim = sideSize;
            float longDim = sideSize * Mathf.Sqrt(3f);

            CreateRect(new Vector2(shortDim, longDim), -30f);
            CreateRect(new Vector2(shortDim, longDim), 30f);
            CreateRect(new Vector2(longDim, shortDim), 0f);
        }
    }
}