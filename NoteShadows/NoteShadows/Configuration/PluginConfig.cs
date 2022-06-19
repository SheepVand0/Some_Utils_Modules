using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace NoteShadows.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        [JsonProperty("show_note_shadow")]
        public virtual bool show_note_shadow { get; set; } = false;
        [JsonProperty("note_shadow_scale")]
        public virtual float note_shadow_scale { get; set; } = 1.0f;
        [JsonProperty("note_shadow_color")]
        public virtual Color note_shadow_color { get; set; } = Color.black;
        [JsonProperty("note_shadow_opacity")]
        public virtual float note_shadow_opacity { get; set; } = 1.0f;
        [JsonProperty("noteShadowOffset")]
        public virtual float m_noteShadowOffset { get; set; } = 0.0f;
        [JsonProperty("noteShadowHeight")]
        public virtual float m_noteShadowHeight { get; set; } = 0.0f;

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
