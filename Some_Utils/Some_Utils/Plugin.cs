using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Some_Utils.ModuleSystem;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage;
using Some_Utils.UI;
using BS_Utils.Utilities;
using IPALogger = IPA.Logging.Logger;

namespace Some_Utils
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        public static List<Module> m_modules = new List<Module>();

        UtilsSettingsFlowCoordinator _settingsFlowCoordinator;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("Some_Utils initialized.");

            MenuButtons.instance.RegisterButton(new MenuButton("Some Utils", "Open settings of Some Utils Modules", OnMenuButtonClick, true));
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");

            BSEvents.lateMenuSceneLoadedFresh += BSEvents_lateMenuSceneLoadedFresh;
        }

        private void BSEvents_lateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            Plugin.Log.Info("Startings modules");
            foreach (Module l_module in m_modules)
            {
                l_module.StartModule();
            }
        }

        void OnMenuButtonClick()
        {
            if (_settingsFlowCoordinator == null)
                _settingsFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<UtilsSettingsFlowCoordinator>();

            _settingsFlowCoordinator.ShowFlow();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

            foreach (Module l_module in m_modules)
            {
                    l_module.StopModule();
            }
        }

        public static void AddModule(Module l_module)
        {

            if (l_module.m_name == null)
            {
                Log.Error("A module doesn't have a valid name, ingnoring");
                return;
            }
            Plugin.Log.Info("Adding module : " + l_module.m_name);
            m_modules.Add(l_module);
        }
    }
}
