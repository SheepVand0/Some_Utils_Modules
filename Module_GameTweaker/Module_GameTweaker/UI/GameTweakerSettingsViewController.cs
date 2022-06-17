using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using Module_GameTweaker.Configuration;


namespace Module_GameTweaker.UI
{
    [HotReload(RelativePathToLayout = @"GameTweakerSettingsViewController.bsml")]
    [ViewDefinition("Module_GameTweaker.UI.GameTweakerSettingsViewController.bsml")]
    internal class GameTweakerSettingsViewController : BSMLAutomaticViewController
    {

        [UIValue("disableBeatLines")]
        protected bool disableBeatLines
        {
            get => PluginConfig.Instance.m_disableBeatLines;
            set => PluginConfig.Instance.m_disableBeatLines = value;
        }

        [UIValue("disableSpectrograms")]
        protected bool disableSpectrograms
        {
            get => PluginConfig.Instance.m_disableSpectrograms;
            set => PluginConfig.Instance.m_disableSpectrograms = value;
        }

        [UIValue("ringScale")]
        protected float ringScale
        {
            get => PluginConfig.Instance.m_ringsScale;
            set => PluginConfig.Instance.m_ringsScale = value;
        }

        [UIValue("ringCount")]
        protected int ringCount
        {
            get => PluginConfig.Instance.m_ringsCount;
            set => PluginConfig.Instance.m_ringsCount = value;
        }

        [UIValue("ringRotSpeed")]
        protected float ringRotSpeed
        {
            get => PluginConfig.Instance.m_ringsTurnSpeed;
            set => PluginConfig.Instance.m_ringsTurnSpeed = value;
        }

        [UIValue("ringMovSpeed")]
        protected float ringMovSpeed
        {
            get => PluginConfig.Instance.m_ringsMoveSpeed;
            set => PluginConfig.Instance.m_ringsMoveSpeed = value;
        }


        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }
    }
}
