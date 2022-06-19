using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using UnityEngine;
using NoteShadows.Configuration;

namespace NoteShadows.UI
{
    [HotReload(RelativePathToLayout = @"NoteShadowsSettingsViewController.bsml")]
    [ViewDefinition("NoteShadows.UI.NoteShadowsSettingsViewController.bsml")]
    internal class NoteShadowsSettingsViewController : BSMLAutomaticViewController
    {
        [UIValue("showNShad")]
        protected bool showNShad
        {
            get => PluginConfig.Instance.show_note_shadow;
            set => PluginConfig.Instance.show_note_shadow = value;
        }

        [UIValue("NShadScale")]
        protected float NShadScale
        {
            get => PluginConfig.Instance.note_shadow_scale;
            set => PluginConfig.Instance.note_shadow_scale = value;
        }

        [UIValue("NShadColor")]
        protected Color NShadColor
        {
            get => PluginConfig.Instance.note_shadow_color;
            set => PluginConfig.Instance.note_shadow_color = value;
        }

        [UIValue("NShadOpac")]
        protected float NshadOpac {
            get => PluginConfig.Instance.note_shadow_opacity;
            set => PluginConfig.Instance.note_shadow_opacity = value;
        }

        [UIValue("NShadOffset")]
        protected float NShadOffset
        {
            get => PluginConfig.Instance.m_noteShadowOffset;
            set => PluginConfig.Instance.m_noteShadowOffset = value;
        }

        [UIValue("NShadHeight")]
        protected float NShadHeight
        {
            get => PluginConfig.Instance.m_noteShadowHeight;
            set => PluginConfig.Instance.m_noteShadowHeight = value;
        }


        [UIAction("#post-parse")]
        internal void PostParse()
        {
            // Code to run after BSML finishes
        }
    }
}
