using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using Newtonsoft.Json;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace Clock.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        [JsonProperty("clock_enabled")]
        public bool clock_enabled = true;
        [JsonProperty("clock_format")]
        public string clock_format = "dd hh:mm:ss";
        [JsonProperty("clock_color")]
        public Color clock_color = Color.white;
        [JsonProperty("clock_font")]
        public string m_clockFont = "";
        [JsonProperty("use_gradient_clock")]
        public bool use_gradient_clock = false;
        [JsonProperty("gradient_color_1")]
        public Color gradient_color_1 = Color.white;
        [JsonProperty("gradient_color_2")]
        public Color gradient_color_2 = Color.white;
        [JsonProperty("clock_glow_intensity")]
        public float clock_glow_intensity = 1.0f;
        [JsonProperty("use_custom_clock_format")]
        public bool use_custom_clock_format = true;
        [JsonProperty("custom_clock_format")]
        public string custom_clock_format = "hh:mm:ss";
        [JsonProperty("clock_pos")]
        public Vector3 clock_pos = new Vector3(0, 2.45f, 3.85f);
        [JsonProperty("clock_rot")]
        public Vector3 clock_rot = new Vector3(-10, 0, 0);
        [JsonProperty("clock_sizeDelta")]
        public Vector2 clock_sizeDelta = new Vector2(50, 50);
        [JsonProperty("clock_font_size")]
        public int clock_fontSize = 2;
        [JsonProperty("clockGradientHorizontal")]
        public bool m_clockGradientHorizontal = false;

        /*/// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }*/
    }
}