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
using IPALogger = IPA.Logging.Logger;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Module_GameTweaker
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin : Module
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

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
            Init("Module GameTweaker", "Allows you to custom game", m_settingsController);
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
            new GameObject("Module_GameTweakerController").AddComponent<Module_GameTweakerController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }

        public override void StartModule()
        {
            base.StartModule();
        }

        public override void StopModule()
        {
            base.StopModule();
        }

        public override BSMLViewController AskForModuleSettingsViewController()
        {
            return base.AskForModuleSettingsViewController();
        }
    }
}
