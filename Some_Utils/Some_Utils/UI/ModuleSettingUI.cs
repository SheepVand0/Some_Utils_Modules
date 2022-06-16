using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using Some_Utils.ModuleSystem;


namespace Some_Utils.UI
{
    class ModuleSettingUI
    {
        //Controller of the button for open the module settings

        public Module m_moduleRef = new Module();

        public int m_moduleRefIndex;

        public ModuleSettingUI(Module p_moduleRef, int p_moduleRefIndex)
        {
            m_moduleRef = p_moduleRef;
            m_moduleRefIndex = p_moduleRefIndex;
        }

        public ModuleSettingFlowCoordinator _settingsFlowCoordinator;

        public string moduleName
        {
            get { return (m_moduleRef.m_name != "") ? m_moduleRef.m_name : "Module Name Not Valid"; }
            set { }
        }

        

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }

        [UIAction("openModuleSettings")]
        protected void openModuleSettings()
        {
            if (_settingsFlowCoordinator == null)
                _settingsFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<ModuleSettingFlowCoordinator>();

            if (m_moduleRef != null)
                _settingsFlowCoordinator.ShowFlow(m_moduleRefIndex);
        }
    }
}
