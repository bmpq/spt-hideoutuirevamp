using EFT.Hideout;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace tarkin.huir
{
    internal class Patch_AreasPanel_Show : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(AreasPanel), nameof(AreasPanel.Show));
        }

        [PatchPrefix]
        private static void PatchPrefix(AreasPanel __instance)
        {
            if (__instance.transform.Find("ModTabContainer") == null)
            {
                InitializeTabs(__instance);
            }
        }

        [PatchPostfix]
        private static void PatchPostfix(AreasPanel __instance)
        {
            ModState.RefreshVisibility(__instance);
        }

        private static void InitializeTabs(AreasPanel panel)
        {
            if (ModState.TabPrefab == null) return;
            ModState.CustomTabs.Clear();

            GameObject container = new GameObject("ModTabContainer", typeof(RectTransform));
            container.transform.SetParent(panel.transform, false);

            container.AddComponent<LayoutElement>().ignoreLayout = true;

            RectTransform rt = container.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(0.5f, 0);
            rt.anchoredPosition = new Vector2(0, 0);
            rt.offsetMin = new Vector2(0, 0);
            rt.offsetMax = new Vector2(0, 32);

            HorizontalLayoutGroup layout = container.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = -25f;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            CreateTab(container, panel, true);
            CreateTab(container, panel, false);

            container.SetActive(true);
        }

        private static void CreateTab(GameObject container, AreasPanel panel, bool isOperatableTab)
        {
            GameObject tabObj = UnityEngine.Object.Instantiate(ModState.TabPrefab, container.transform);
            tabObj.name = isOperatableTab ? "Tab_Operatable" : "Tab_Passive";

            tabObj.GetComponent<LocalizedText>().LocalizationKey = string.Empty;
            tabObj.GetComponent<LocalizedText>().method_2(isOperatableTab ? "OPERATIONS" : "PASSIVE"); // override all labels with SetLabelText()

            Tab tab = tabObj.GetComponent<Tab>();
            ModState.CustomTabs.Add(tab);

            tab.OnSelectionChanged += (t, selected) =>
            {
                if (selected)
                {
                    ModState.CurrentShowOperatable = isOperatableTab;
                    UpdateTabVisuals(tab);

                    ModState.RefreshVisibility(panel);
                }
            };

            if (ModState.CurrentShowOperatable == isOperatableTab)
                tab.UpdateVisual(true);
            else
                tab.UpdateVisual(false);

            tabObj.SetActive(true);
        }

        private static void UpdateTabVisuals(Tab selectedTab)
        {
            foreach (var tab in ModState.CustomTabs)
            {
                tab.UpdateVisual(tab == selectedTab);
                if (tab == selectedTab)
                    tab.transform.SetAsLastSibling();
            }
        }
    }
}