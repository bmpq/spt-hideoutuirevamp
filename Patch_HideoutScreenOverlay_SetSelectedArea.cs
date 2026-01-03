using EFT;
using EFT.Hideout;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;

namespace tarkin.huir
{
    internal class Patch_HideoutScreenOverlay_SetSelectedArea : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_HideoutScreenOverlay_SetSelectedArea));

        public static event Action OnPostfix;

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(HideoutScreenOverlay), nameof(HideoutScreenOverlay.method_10));
        }

        [PatchPostfix]
        private static void PatchPostfix(HideoutScreenOverlay __instance, AreaData area, AreasPanel ____areasPanel)
        {
            try
            {
                bool requiredTabState = ModState.IsOperatable(area.Template);
                if (ModState.CurrentShowOperatable != requiredTabState)
                {
                    ModState.SetActiveTab(requiredTabState);
                    ModState.RefreshVisibility(____areasPanel);
                }
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}