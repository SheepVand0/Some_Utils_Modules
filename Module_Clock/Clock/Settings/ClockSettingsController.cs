using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Clock.Configuration;

namespace Clock.Settings
{
    internal class ClockSettingsController : BSMLResourceViewController
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public override string ResourceName => string.Join(".","Clock","Settings","ClockSettingsController","bsml");

        public Vector3 l_currentClockPos;

        public Vector3 l_currentClockRot;

        public Vector2 l_sizeDelta;

        public void Awake()
        {
            l_currentClockPos = PluginConfig.Instance.clock_pos;
            l_currentClockRot = PluginConfig.Instance.clock_rot;
        }
        public void ChangeClockRot()
        {
            PluginConfig.Instance.clock_rot = l_currentClockRot;
        }
        public void ChangeClockPos()
        {
            PluginConfig.Instance.clock_pos = l_currentClockPos;
        }

        public void ChangeSizeDelta()
        {
            PluginConfig.Instance.clock_sizeDelta = l_sizeDelta;
        }

        [UIValue("clock_enabled")]
        protected bool SettingClockEnabled
        {

            get => PluginConfig.Instance.clock_enabled;
            set { PluginConfig.Instance.clock_enabled = value; }
        }

        [UIValue("clock_formats")]
        public List<object> clock_formats = new List<object>(Clock.Plugin.clocks_formats);

        [UIValue("clock_format")]
        protected string ClockFormat
        {
            get => PluginConfig.Instance.clock_format;
            set { PluginConfig.Instance.clock_format = value; }
        }

        [UIValue("clock_color")]
        protected Color SettingClockColor
        {
            get => PluginConfig.Instance.clock_color;
            set { PluginConfig.Instance.clock_color = value; }
        }

        [UIValue("custom_clock_format")]
        protected string SettingCustomClockFormat
        {
            get => PluginConfig.Instance.custom_clock_format;
            set { PluginConfig.Instance.custom_clock_format = value; }
        }

        [UIValue("use_custom_format")]
        protected bool SettingUseCustomClockFormat
        {
            get => PluginConfig.Instance.use_custom_clock_format;
            set { PluginConfig.Instance.use_custom_clock_format = value; }
        }

        [UIValue("clock_pos_x")]
        protected float SettingClockPosX
        {
            get => PluginConfig.Instance.clock_pos.x;
            set { l_currentClockPos.x = value; ChangeClockPos(); }
        }

        [UIValue("clock_pos_y")]
        protected float SettingClockPosY
        {
            get => PluginConfig.Instance.clock_pos.y;
            set { l_currentClockPos.y = value; ChangeClockPos(); }
        }

        [UIValue("clock_pos_z")]
        protected float SettingClockPosZ
        {
            get => PluginConfig.Instance.clock_pos.z;
            set { l_currentClockPos.z = value; ChangeClockPos(); }
        }

        [UIValue("clock_rot_x")]
        protected float SettingClockRosX
        {
            get => PluginConfig.Instance.clock_rot.x;
            set { l_currentClockRot.x = value; ChangeClockRot(); }
        }

        [UIValue("clock_rot_y")]
        protected float SettingClockRosY
        {
            get => PluginConfig.Instance.clock_rot.y;
            set { l_currentClockRot.y = value; ChangeClockRot(); }
        }

        [UIValue("clock_rot_z")]
        protected float SettingClockRosZ
        {
            get => PluginConfig.Instance.clock_rot.z;
            set { l_currentClockRot.z = value; ChangeClockRot(); }
        }

        [UIValue("clockDeltaX")]
        protected float SettingClockDeltaX
        {
            get => PluginConfig.Instance.clock_sizeDelta.x;
            set { l_sizeDelta.x = value; ChangeSizeDelta();  }
        }

        [UIValue("clockDeltaY")]
        protected float SettingClockDeltaY
        {
            get => PluginConfig.Instance.clock_sizeDelta.y;
            set { l_sizeDelta.y = value; ChangeSizeDelta(); }
        }

        [UIValue("clock_glow")]
        protected float SettingClockGlow
        {
            get => PluginConfig.Instance.clock_glow_intensity;
            set => PluginConfig.Instance.clock_glow_intensity = value;
        }

        [UIValue("clockFontSize")]
        protected int SettingClockFontSize
        {
            get => PluginConfig.Instance.clock_fontSize;
            set => PluginConfig.Instance.clock_fontSize = value;
        }

        [UIValue("use_gradient")]
        protected bool SettingUseGradient
        {
            get => PluginConfig.Instance.use_gradient_clock;
            set => PluginConfig.Instance.use_gradient_clock = value;
        }

        [UIValue("gradient_color_1")]
        protected Color SettingGradientColor1
        {
            get => PluginConfig.Instance.gradient_color_1;
            set => PluginConfig.Instance.gradient_color_1 = value;
        }

        [UIValue("gradient_color_2")]
        protected Color SettingGradientColor2
        {
            get => PluginConfig.Instance.gradient_color_2;
            set => PluginConfig.Instance.gradient_color_2 = value;
        }

        [UIValue("gradientHorizontal")]
        protected bool settingGradientHorizontal
        {
            get => PluginConfig.Instance.m_clockGradientHorizontal;
            set => PluginConfig.Instance.m_clockGradientHorizontal = value;
        }

        /*[UIValue("avaibleFonts")]
        protected List<string> l_avaibleFonts = GetFontsNames();

        [UIValue("selectedFont")]
        protected string SettingSelectedFont
        {
            get => Plugin.CurrentConfig.m_clockFont;
            set => Plugin.CurrentConfig.m_clockFont = value;
        }*/
    }
}
