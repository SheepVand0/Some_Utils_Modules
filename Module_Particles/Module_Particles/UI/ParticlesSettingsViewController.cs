using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using Module_Particles.Configuration;
using UnityEngine;

namespace Module_Particles.UI
{
    [HotReload(RelativePathToLayout = @"ParticlesSettingsViewController.bsml")]
    [ViewDefinition("Module_Particles.UI.ParticlesSettingsViewController.bsml")]
    class ParticlesSettingsViewController : BSMLAutomaticViewController
    {

        [UIValue("sparklesCount")]
        protected int sparklesCount
        {
            get => PluginConfig.Instance.m_sparklesCount;
            set => PluginConfig.Instance.m_sparklesCount = value;
        }

        [UIValue("explosionParticlesCount")]
        protected int explosionParticlesCount
        {
            get => PluginConfig.Instance.m_explosionParticlesCount;
            set => PluginConfig.Instance.m_explosionParticlesCount = value;
        }

        [UIValue("saberSpeedMultiplier")]
        protected float saberSpeedMultiplier
        {
            get => PluginConfig.Instance.m_saberSpeedMultiplier;
            set => PluginConfig.Instance.m_saberSpeedMultiplier = value;
        }

        [UIValue("lifetimeMultiplier")]
        protected float lifetimeMultiplier
        {
            get => PluginConfig.Instance.m_lifeTimeMultiplier;
            set => PluginConfig.Instance.m_lifeTimeMultiplier = value;
        }

        [UIValue("overrideDefaultParticlesColor")]
        protected bool overrideDefaultParticlesColor
        {
            get => PluginConfig.Instance.m_overrideDefaultParticlesColor;
            set => PluginConfig.Instance.m_overrideDefaultParticlesColor = value;
        }

        [UIValue("particleOverrideColor")]
        protected Color particleOverrideColor
        {
            get => PluginConfig.Instance.m_overrideParticleColor;
            set => PluginConfig.Instance.m_overrideParticleColor = value;
        }

        [UIValue("useRandomParticleColor")]
        protected bool useRandomParticleColor
        {
            get => PluginConfig.Instance.m_useRandomParticleColor;
            set => PluginConfig.Instance.m_useRandomParticleColor = value;
        }

        [UIValue("multPartDirX")]
        protected bool multPartDirX {
            get => PluginConfig.Instance.m_multiplyParticleDirectionX;
            set => PluginConfig.Instance.m_multiplyParticleDirectionX = value;
        }

        [UIValue("multPartDirY")]
        protected bool multPartDirY
        {
            get => PluginConfig.Instance.m_multiplyParticleDirectionY;
            set => PluginConfig.Instance.m_multiplyParticleDirectionY = value;
        }

        [UIValue("multCoefX")]
        protected float multiCoefX
        {
            get => PluginConfig.Instance.m_particleDirectionMultX;
            set => PluginConfig.Instance.m_particleDirectionMultX = value;
        }

        [UIValue("multCoefY")]
        protected float multCoefY
        {
            get => PluginConfig.Instance.m_particleDirectionMultY;
            set => PluginConfig.Instance.m_particleDirectionMultY = value;
        }


        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }
    }
}
