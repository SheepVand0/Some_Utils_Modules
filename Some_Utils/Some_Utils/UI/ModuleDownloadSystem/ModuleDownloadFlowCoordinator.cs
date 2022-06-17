using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using HMUI;

namespace Some_Utils.UI.ModuleDownloadSystem
{
    class ModuleDownloadFlowCoordinator : FlowCoordinator
    {

        ModuleDownloadViewController m_downloadViewController;

        FlowCoordinator m_lastFlow;

        public void Awake()
        {
            if (m_downloadViewController == null)
                m_downloadViewController = BeatSaberUI.CreateViewController<ModuleDownloadViewController>();
        }
             
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;

            SetTitle("Download modules");
            showBackButton = true;
            ProvideInitialViewControllers(m_downloadViewController);
        }

        public void ShowFlow()
        {
            m_lastFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            m_lastFlow.PresentFlowCoordinator(this, null, ViewController.AnimationDirection.Vertical, false, false);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            m_lastFlow.DismissFlowCoordinator(this);
        }

    }
}
