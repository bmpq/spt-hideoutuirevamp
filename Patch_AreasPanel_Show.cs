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
            rt.offsetMin = new Vector2(0, 9);
            rt.offsetMax = new Vector2(0, 41);

            HorizontalLayoutGroup layout = container.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = -25f;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var initActivetab = CreateTab(container, panel, true);
            CreateTab(container, panel, false);

            LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
            Component.DestroyImmediate(layout);

            container.SetActive(true);

            ModState.SetActiveTab(true);
        }

        private static Tab CreateTab(GameObject container, AreasPanel panel, bool isOperatableTab)
        {
            GameObject tabObj = UnityEngine.Object.Instantiate(ModState.TabPrefab, container.transform);
            tabObj.name = isOperatableTab ? "Tab_Operatable" : "Tab_Passive";

            var localizedText = tabObj.GetComponent<LocalizedText>();
            localizedText.LocalizationKey = string.Empty;
            localizedText.method_2(isOperatableTab ? "OPERATIONS" : "PASSIVE"); // override all labels with SetLabelText()

            Sprite tabIcon = CacheResourcesPopAbstractClass.Pop<Sprite>(isOperatableTab ? "characteristics/icons/Repair" : "characteristics/icons/ricochet");

            tabObj.transform.Find("Normal/Icon").GetComponent<Image>().sprite = tabIcon;
            Image iconImageSelected = tabObj.transform.Find("Selected/Icon").GetComponent<Image>();
            iconImageSelected.sprite = tabIcon;
            iconImageSelected.color = Color.black;

            Tab tab = tabObj.GetComponent<Tab>();
            ModState.CustomTabs.Add(tab);

            tab.OnSelectionChanged += (t, selected) =>
            {
                if (selected)
                {
                    ModState.SetActiveTab(isOperatableTab);
                    ModState.RefreshVisibility(panel);
                }
            };

            if (ModState.CurrentShowOperatable == isOperatableTab)
                tab.UpdateVisual(true);
            else
                tab.UpdateVisual(false);

            tabObj.SetActive(true);

            return tab;
        }
    }
}