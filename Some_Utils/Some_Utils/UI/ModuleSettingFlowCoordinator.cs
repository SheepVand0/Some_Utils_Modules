using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMUI;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.MenuButtons;
using Some_Utils.ModuleSystem;

namespace Some_Utils.UI
{
    internal class ModuleSettingFlowCoordinator : FlowCoordinator
    {
        //Flow coordinator for a module settings

        FlowCoordinator m_lastFlow;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;

            SetTitle("Settings");
            showBackButton = true;
            
        }

        public void Awake()
        {

        }

        public void ShowFlow(int p_moduleIndex)
        {
            SetTitle(Plugin.m_modules[p_moduleIndex].m_name + " settings");

            ProvideInitialViewControllers(Plugin.m_modules[p_moduleIndex].AskForModuleSettingsViewController());
            
            m_lastFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            m_lastFlow.PresentFlowCoordinator(this, null, ViewController.AnimationDirection.Vertical, false, false);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            m_lastFlow.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal, false);
        }
    }
}
