using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;


namespace Some_Utils.UI
{
    [HotReload(RelativePathToLayout = @"BaseModuleSettingsViewController.bsml")]
    [ViewDefinition("Some_Utils.UI.BaseModuleSettingsViewController.bsml")]
    internal class BaseModuleSettingsViewController : BSMLAutomaticViewController
    {
        private string m_baseText = "Hello World";
        public string baseText
        {
            get { return m_baseText; }
            set
            {
                if (m_baseText == value) return;
                m_baseText = value;
                NotifyPropertyChanged();
            }
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }
    }
}
