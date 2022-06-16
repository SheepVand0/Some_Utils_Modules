using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Components;
using HMUI;

namespace Some_Utils.UI
{
    //Flow coordinator to show the module list to their settings
    public class UtilsSettingsFlowCoordinator : FlowCoordinator
    {
        SettingsController settingsController;

        public void Awake()
        {
            if (settingsController == null)
                settingsController = BeatSaberUI.CreateViewController<SettingsController>();
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;

            SetTitle("Some Utils Modules");
            showBackButton = true;
            ProvideInitialViewControllers(settingsController);
        }

        public void ShowFlow()
        {
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal, false, false);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal, false);
        }
    }
}
