using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using Module_GameTweaker.Configuration;
using UnityEngine;

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

        [UIValue("multiLights")]
        protected Color multiLights
        {
            get => PluginConfig.Instance.m_multiLightsColor;
            set => PluginConfig.Instance.m_multiLightsColor = value;
        }

        [UIValue("preview1Disable")]
        protected bool preview1Disable
        {
            get => PluginConfig.Instance.m_disablePreviewOfTheBestInMulti;
            set => PluginConfig.Instance.m_disablePreviewOfTheBestInMulti = value;
        }

        [UIValue("useGradientCombo")]
        protected bool useGradientCombo
        {
            get => PluginConfig.Instance.m_useComboGradient;
            set => PluginConfig.Instance.m_useComboGradient = value;
        }

        [UIValue("comboGradient1")]
        protected Color comboGradient1
        {
            get => PluginConfig.Instance.m_comboGradient1;
            set => PluginConfig.Instance.m_comboGradient1 = value;
        }

        [UIValue("comboGradient2")]
        protected Color comboGradient2
        {
            get => PluginConfig.Instance.m_comboGradient2;
            set => PluginConfig.Instance.m_comboGradient2 = value;
        }

        [UIValue("EditPlrHeight")]
        protected bool EditPlrheight
        {
            get => PluginConfig.Instance.EditPlayerHeight;
            set => PluginConfig.Instance.EditPlayerHeight = value;
        }

        [UIValue("PlrHeightAdd")]
        protected float PlrHeightAdd
        {
            get => PluginConfig.Instance.PlayerHeightAdd;
            set => PluginConfig.Instance.PlayerHeightAdd = value;
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }
    }
}
