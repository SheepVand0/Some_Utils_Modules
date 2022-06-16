using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using TMPro;
using BS_Utils.Utilities;
using UnityEngine.UI;
using Clock;
using Clock.Configuration;
using Plugin = Clock.Plugin;

namespace Clock.UI
{
    class ClockUI
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);

        static readonly ClockUI instance = new ClockUI();

        public static RectTransform clockRectTransform;

        public static GameObject clock;

        static Canvas clock_canvas;

        static GameObject clock_text_gameObject;

        static TextMeshProUGUI clock_text;

        static string GameplaySceneName = "StandardGameplay";

        static string CurrentSceneName = "";
        public Shader ZFixTextShader { get; private set; }

        ClockUI()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        }

        private void SceneManager_sceneUnloaded(UnityEngine.SceneManagement.Scene arg0)
        {
            if (CurrentSceneName == GameplaySceneName && arg0.name == GameplaySceneName)
                CurrentSceneName = "";
        }

        private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
        {
            CurrentSceneName = arg0.name;
        }

        public static void Create()
        {
            Plugin.Log.Info("Creating Clock");
            //Canvas
            clock = new GameObject("Clock");
            clock_canvas = clock.AddComponent<Canvas>();
            clock_canvas.name = "ClockCanvas";
            clock_canvas.renderMode = RenderMode.WorldSpace;
            clock_canvas.scaleFactor = 100.0f;

            clock.AddComponent<ClockManager>();
            clock.GetComponent<ClockManager>().Init(instance);

            clock.AddComponent<CanvasScaler>().scaleFactor = 1.0f;
            clock.AddComponent<GraphicRaycaster>();
            //Text
            clock_text_gameObject = new GameObject("ClockText");
            clock_text_gameObject.transform.parent = clock.transform;

            clock_text = clock_text_gameObject.AddComponent<TextMeshProUGUI>();
            /*TMP_FontAsset ArialFont = Resources.GetBuiltinResource(typeof(TMP_FontAsset), "Arial.ttf") as TMP_FontAsset;
            clock_text.font = ArialFont;
            clock_text.material = ArialFont.material;*/
            clock_text.text = (PluginConfig.Instance.use_custom_clock_format) ? PluginConfig.Instance.custom_clock_format : PluginConfig.Instance.clock_format;
            clock_text.alignment = TextAlignmentOptions.Midline;
            clock_text.fontSize = 1;


            //Text pos
            clockRectTransform = clock_text.GetComponent<RectTransform>();
            clockRectTransform.localPosition = Vector3.zero;
            clockRectTransform.sizeDelta = PluginConfig.Instance.clock_sizeDelta;
            clockRectTransform.rect.Set(0, 0, 4, 2);

            clock.gameObject.transform.localScale = 0.1f * Vector3.one;

            GameObject.DontDestroyOnLoad(clock);
            GameObject.DontDestroyOnLoad(clock_text_gameObject);

        }

        public static void UpdateClockSettings()
        {
            instance.UpdateClockSettings_();
        }

        public void UpdateClockSettings_()
        {

            clock.gameObject.GetComponent<Canvas>().enabled = PluginConfig.Instance.clock_enabled;
            clock.transform.localPosition = PluginConfig.Instance.clock_pos;
            clock.transform.localRotation = Quaternion.Euler(PluginConfig.Instance.clock_rot);
            clockRectTransform.sizeDelta = PluginConfig.Instance.clock_sizeDelta;
            clock_text.fontSize = PluginConfig.Instance.clock_fontSize;
            float glowIntensity = PluginConfig.Instance.clock_glow_intensity;
            Font l_arialFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            clock_text.material = l_arialFont.material;
            if (!PluginConfig.Instance.use_gradient_clock)
            {
                clock_text.enableVertexGradient = false;
                clock_text.color = PluginConfig.Instance.clock_color.ColorWithAlpha(glowIntensity);
            } else
            {
                clock_text.enableVertexGradient = true;
                clock_text.color = Color.white;
                if (PluginConfig.Instance.m_clockGradientHorizontal)
                {
                    clock_text.colorGradient = new VertexGradient(PluginConfig.Instance.gradient_color_1, PluginConfig.Instance.gradient_color_2, PluginConfig.Instance.gradient_color_1, PluginConfig.Instance.gradient_color_2);
                } else
                {
                    clock_text.colorGradient = new VertexGradient(PluginConfig.Instance.gradient_color_1, PluginConfig.Instance.gradient_color_1, PluginConfig.Instance.gradient_color_2, PluginConfig.Instance.gradient_color_2);
                }
            }

            
            /*clock_text.font = l_arialFont;
            clock_text.material = l_arialFont.material;*/

            /*Font l_resultFont;
            Utils.Utils.GetAllFontsInFolder().TryGetValue(Plugin.CurrentConfig.m_clockFont, out l_resultFont);
            if( l_resultFont != null)
            {
               clock_text.font = l_resultFont;
               clock_text.material = l_resultFont.material;
            } else
            {
               Font l_arialFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
               clock_text.font = l_arialFont;
               clock_text.material = l_arialFont.material;
            }*/

        }

        public string GetClockFormat()
        {
            if (!PluginConfig.Instance.use_custom_clock_format)
                return PluginConfig.Instance.clock_format;
            else
                return PluginConfig.Instance.custom_clock_format;
        }

        public void UpdateClock(OptimizedDateTime currentDate)
        {
            if (Plugin.m_currentSceneName != "GameCore")
                UpdateClockSettings_();

            if (clock_text.text != "")
            {
                string newDate = GetClockFormat();
                newDate = newDate.Replace("yy", currentDate.m_Year.ToString("0000"));
                newDate = newDate.Replace("mm", currentDate.m_Month.ToString("00"));
                newDate = newDate.Replace("dd", currentDate.m_Day.ToString("00"));
                newDate = newDate.Replace("hh", currentDate.m_Hours.ToString("00"));
                newDate = newDate.Replace("mn", currentDate.m_Minutes.ToString("00"));
                newDate = newDate.Replace("ss",
                    (currentDate.m_Seconds > 60) ? 0.ToString() :
                    currentDate.m_Seconds.ToString("00"));
                
                clock_text.text = newDate;
            } else
            {
                Plugin.Log.Info("Text of clock is null");
            }
        }
    }
}
