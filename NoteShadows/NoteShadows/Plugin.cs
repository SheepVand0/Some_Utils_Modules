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
using HarmonyLib;
using System.Reflection;
using BeatSaberMarkupLanguage.ViewControllers;
using NoteShadows.UI;
using BeatSaberMarkupLanguage;

namespace NoteShadows
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin : Some_Utils.ModuleSystem.Module
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        Harmony m_harmony = new Harmony("BeatSaber.Some_Utils.Modules.NoteShadows");

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
            Log.Info("NoteShadows initialized.");
            Init("Note Shadows","Allows you to add note shadows",m_settingsController);
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
            new GameObject("NoteShadowsController").AddComponent<NoteShadowsController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }

        public override void StartModule()
        {
            base.StartModule();

            m_harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void StopModule()
        {
            base.StopModule();

            m_harmony.UnpatchSelf();
        }

        public override BSMLViewController AskForModuleSettingsViewController()
        {
            if (m_settingsController == null)
                m_settingsController = BeatSaberUI.CreateViewController<NoteShadowsSettingsViewController>();

            return m_settingsController;
        }
    }
}
