using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Some_Utils.ModuleSystem;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage;
using Module_Particles.UI;
using BS_Utils.Utilities;

namespace Module_Particles
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin : Some_Utils.ModuleSystem.Module
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        public static string m_harmonyId = "SheepVand.Some_Utils.Module.Particles";

        Harmony m_harmony = new Harmony(m_harmonyId);

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

            Init("Particles Tweaker","Allow you to custom the game particles", m_settingsController);

            m_harmony.PatchAll(Assembly.GetExecutingAssembly());
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
            //Log.Debug("OnApplicationStart");
            //new GameObject("Module_ParticlesController").AddComponent<Module_ParticlesController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            //Log.Debug("OnApplicationQuit");
            m_harmony.UnpatchSelf();
        }

        public override void StartModule()
        {
            base.StartModule();

            m_settingsController = BeatSaberUI.CreateViewController<ParticlesSettingsViewController>();

            BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
        }

        private void BSEvents_gameSceneLoaded()
        {
            GameObject.Find("Spectrograms").SetActive(false);
        }

        public override void StopModule()
        {
            base.StopModule();

            
        }

        public override BSMLViewController AskForModuleSettingsViewController()
        {
            if (m_settingsController == null)
                m_settingsController = BeatSaberUI.CreateViewController<ParticlesSettingsViewController>();

            return m_settingsController;
        }
    }
}
