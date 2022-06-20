using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using Some_Utils.ModuleSystem;
using TMPro;
using Some_Utils.UI.ModuleDownloadSystem;
using HMUI;

namespace Some_Utils.UI
{
    [HotReload(RelativePathToLayout = @"SettingsController.bsml")]
    [ViewDefinition("Some_Utils.UI.SettingsController.bsml")]
    class SettingsController : BSMLAutomaticViewController
    {

        ModuleDownloadFlowCoordinator _moduleDownloadFlowCoordinator;

        [UIValue("modulesSettings")]
        List<ModuleSettingUI> modulesSettings
        { get {
                List<ModuleSettingUI> l_currentModules = new List<ModuleSettingUI>();
                Plugin.Log.Info("Getting modules for add settings");
                for (int l_i = 0; l_i < Plugin.m_modules.Count;l_i++)
                {
                    ModuleSettingUI l_moduleSettingUI = new ModuleSettingUI(Plugin.m_modules[l_i], l_i);
                    Plugin.Log.Info("Adding module settings : " + Plugin.m_modules[l_i].m_name);
                    l_currentModules.Add(l_moduleSettingUI);
                }
                return l_currentModules;
                } 
        }

        [UIAction("openDownloadModulePage")]
        protected void openDownloadModulePage()
        {
            if (_moduleDownloadFlowCoordinator == null)
                _moduleDownloadFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<ModuleDownloadFlowCoordinator>();

            _moduleDownloadFlowCoordinator.ShowFlow();
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }
    }
}
