using EFT.Hideout;
using EFT;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine.UI;

namespace tarkin.huir
{
    public static class ModState
    {
        public static GameObject TabPrefab { get; set; }
        public static List<Tab> CustomTabs = new List<Tab>();

        public static bool CurrentShowOperatable = true;

        public static bool ShouldShow(AreaData data)
        {
            if (data == null) 
                return false;

            return IsOperatable(data.Template) == CurrentShowOperatable;
        }

        private static bool IsOperatable(AreaTemplate template)
        {
            switch (template.Type)
            {
                // -- PASSIVE
                // You only look at these to upgrade them
                case EAreaType.Vents:
                case EAreaType.Security:
                case EAreaType.Stash:
                case EAreaType.Heating:
                case EAreaType.RestSpace:
                case EAreaType.Illumination:
                case EAreaType.Library:
                case EAreaType.SolarPower:

                // Physical interactions only (first person view required)
                case EAreaType.Gym:
                case EAreaType.ShootingRange:

                // Containers/Display
                case EAreaType.WeaponStand:
                case EAreaType.WeaponStandSecondary:
                case EAreaType.EquipmentPresetsStand:
                case EAreaType.PlaceOfFame:
                    return false;

                // -- ACTIVE MANAGEMENT
                // Crafting, Fueling, Gambling
                case EAreaType.Generator:
                case EAreaType.WaterCollector:
                case EAreaType.ScavCase:
                case EAreaType.AirFilteringUnit:
                case EAreaType.BoozeGenerator:
                case EAreaType.BitcoinFarm:
                case EAreaType.EmergencyWall:
                case EAreaType.CircleOfCultists:

                // The Big Crafting Stations
                case EAreaType.WaterCloset:
                case EAreaType.MedStation:
                case EAreaType.Kitchen:
                case EAreaType.Workbench:
                case EAreaType.IntelligenceCenter:
                    return true;

                default:
                    return true;
            }
        }

        public static void RefreshVisibility(AreasPanel panel)
        {
            var container = (RectTransform)AccessTools.Field(typeof(AreasPanel), "_areaPanelsContainer").GetValue(panel);
            if (container == null) return;

            foreach (Transform child in container)
            {
                var view = child.GetComponent<AreaPanel>();
                if (view != null && view.Data != null)
                {
                    bool shouldBeVisible = ShouldShow(view.Data);

                    if (view.gameObject.activeSelf != shouldBeVisible)
                    {
                        view.gameObject.SetActive(shouldBeVisible);
                    }
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(container);
        }
    }
}