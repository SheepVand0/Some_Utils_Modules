using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;

namespace Some_Utils.ModuleSystem
{
    public class Module
    {
        public Module()
        {

        }

        public string m_name { get; set; }
        public string m_description { get; set; }
        public BSMLViewController m_settingsController { get; set; }

        public void Init(string p_name, string p_description, BSMLViewController p_settingsControllers)
        {
            m_name = p_name;
            m_description = p_description;
            m_settingsController = p_settingsControllers;
            Plugin.AddModule(this);
        }

        public virtual void StartModule()
        {
            Plugin.Log.Info("Loading : " + m_name);
        }

        public virtual void StopModule()
        {

        }

        public virtual BSMLViewController AskForModuleSettingsViewController()
        {
            return m_settingsController;
        }
    }
}
