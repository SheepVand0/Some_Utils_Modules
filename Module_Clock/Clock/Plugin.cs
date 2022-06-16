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
using BS_Utils.Utilities;
using Clock.UI;
using BeatSaberMarkupLanguage;
using Clock.Settings;
using IPALogger = IPA.Logging.Logger;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Clock
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin : Module
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        public static bool m_hasLoadedClock = false;   

        public static List<string> clocks_formats = new List<string>();

        public static string m_currentSceneName;

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

            clocks_formats.Add("hh:mn:ss dd");
            clocks_formats.Add("hh:mn:ss");
            clocks_formats.Add("dd hh:mn:ss");
            clocks_formats.Add("hh:mn");
            clocks_formats.Add("dd hh:mn");

            Init("Clock", "A simple and optimized clock", m_settingsController);
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        
        [Init]
        public void InitWithConfig(IPA.Config.Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            m_currentSceneName = arg0.name;
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");   
        }

        public override void StartModule()
        {
            base.StartModule();
            if (m_hasLoadedClock == true)
                return;

            ClockUI.Create();
            m_hasLoadedClock=true;
            m_settingsController = BeatSaberUI.CreateViewController<ClockSettingsController>();
            Plugin.Log.Info("Clock Loaded");
        }

        public override void StopModule()
        {
            base.StopModule();
        }

        public override BSMLViewController AskForModuleSettingsViewController()
        {
            if (m_settingsController == null)
                m_settingsController = BeatSaberUI.CreateViewController<ClockSettingsController>();

            return m_settingsController;
        }
    }
}
