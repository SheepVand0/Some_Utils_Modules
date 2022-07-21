
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using Newtonsoft.Json;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace Module_GameTweaker.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        [JsonProperty("disableBeatLines")]
        public virtual bool m_disableBeatLines { get; set; } = false;

        [JsonProperty("disableSpectrograms")]
        public virtual bool m_disableSpectrograms { get; set; } = false;

        [JsonProperty("ringScale")]
        public virtual float m_ringsScale { get; set; } = 1.0f;

        [JsonProperty("ringCount")]
        public virtual int m_ringsCount { get; set; } = 20;

        [JsonProperty("ringMoveSpeed")]
        public virtual float m_ringsMoveSpeed { get; set; } = 1.0f;

        [JsonProperty("ringTurnSpeed")]
        public virtual float m_ringsTurnSpeed { get; set; } = 1.0f;

        [JsonProperty("practiceColor")]
        public virtual Color m_practiceColor { get; set; } = Color.yellow;

        [JsonProperty("useCustomPracticeColor")]
        public virtual bool m_useCustomPracticeColor { get; set; } = false;

        [JsonProperty("overrideEnvironmentSettings")]
        public virtual bool m_overrideEnvironmentSettings { get; set; }

        [JsonProperty("normalEnvironmentTypeSerializeName")]
        public virtual string m_normalEnvironmentTypeSerializeName { get; set; } = "";
        [JsonProperty("360EnvironmentTypeSerializeName")]
        public virtual string m_360EnvironmentTypeSerializeName { get; set; } = "";
        [JsonProperty("DisableMuliplayerLights")]
        public virtual Color m_multiLightsColor { get; set; } = Color.cyan;
        [JsonProperty("DisablePreviewOfTheBestInMulti")]
        public virtual bool m_disablePreviewOfTheBestInMulti { get; set; } = false;
        [JsonProperty("beforeEditMultiColor")]
        public virtual Color m_beforeEditMultiColor { get; set; } = Color.blue;
        [JsonProperty("useComboGradient")]
        public virtual bool m_useComboGradient { get; set; } = false;
        [JsonProperty("comboGradient1")]
        public virtual Color m_comboGradient1 { get; set; } = Color.blue;
        [JsonProperty("comboGradient2")]
        public virtual Color m_comboGradient2 { get; set; } = Color.magenta;
        [JsonProperty("EditPlrHeight")]
        public virtual bool EditPlayerHeight { get; set; } = false;
        [JsonProperty("PlayerHeightAdd")]
        public virtual float PlayerHeightAdd { get; set; } = 0.0f;

        /// <summary>
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
        }
    }
}
