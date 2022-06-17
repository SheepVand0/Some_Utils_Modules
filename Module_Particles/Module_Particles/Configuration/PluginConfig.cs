
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using Newtonsoft.Json;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace Module_Particles.Configuration
{ 
    class PluginConfig
    {
        public static PluginConfig Instance { get; set; }


        [JsonProperty("sparklesCount")]
        public virtual int m_sparklesCount { get; set; } = 100;
        [JsonProperty("explosionParticlesCount")]
        public virtual int m_explosionParticlesCount { get; set; } = 100;
        [JsonProperty("saberSpeedMultiplier")]
        public virtual float m_saberSpeedMultiplier { get; set; } = 1.0f;
        [JsonProperty("lifeTimeMultiplier")]
        public virtual float m_lifeTimeMultiplier { get; set; } = 1.0f;
        [JsonProperty("overrideDefaultParticleColor")]
        public virtual bool m_overrideDefaultParticlesColor { get; set; } = false;
        [JsonProperty("overrideParticleColor")]
        public virtual Color m_overrideParticleColor { get; set; } = Color.white;
        [JsonProperty("useRandomParticleColor")]
        public virtual bool m_useRandomParticleColor { get; set; } = false;
        [JsonProperty("particleDirectionX")]
        public virtual bool m_multiplyParticleDirectionX { get; set; } = false;
        [JsonProperty("particleDirectionY")]
        public virtual bool m_multiplyParticleDirectionY { get; set; } = false;


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
