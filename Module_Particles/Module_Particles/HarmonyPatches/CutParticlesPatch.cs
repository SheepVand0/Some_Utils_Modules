using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Module_Particles.Configuration;

namespace Module_Particles.HarmonyPatches
{
    [HarmonyPatch(typeof(NoteCutParticlesEffect),nameof(NoteCutParticlesEffect.SpawnParticles), new Type[] { typeof(Vector3), typeof(Vector3), typeof(Vector3), typeof(float), typeof(Vector3), typeof(Color32), typeof(int), typeof(int), typeof(float) })]
    public class CutParticlesPatch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="cutPoint"></param>
        /// <param name="cutNormal"></param>
        /// <param name="saberDir"></param>
        /// <param name="saberSpeed"></param>
        /// <param name="noteMovementVec"></param>
        /// <param name="color"></param>
        /// <param name="sparkleParticlesCount"></param>
        /// <param name="explosionParticlesCount"></param>
        /// <param name="lifetimeMultiplier"></param>
        public static void Prefix(NoteCutParticlesEffect __instance, ref Vector3 cutPoint, ref Vector3 cutNormal, ref Vector3 saberDir, ref float saberSpeed, ref Vector3 noteMovementVec, ref Color32 color, ref int sparkleParticlesCount, ref int explosionParticlesCount, ref float lifetimeMultiplier)
        {
            saberSpeed = PluginConfig.Instance.m_saberSpeedMultiplier * saberSpeed;
            sparkleParticlesCount = PluginConfig.Instance.m_sparklesCount;
            explosionParticlesCount = PluginConfig.Instance.m_explosionParticlesCount;
            cutNormal = new Vector3(
                (PluginConfig.Instance.m_multiplyParticleDirectionX) ? cutNormal.x * saberSpeed : cutNormal.x, 
                (PluginConfig.Instance.m_multiplyParticleDirectionY) ? cutNormal.y * saberSpeed : cutNormal.y, 
                cutNormal.z);
            lifetimeMultiplier = PluginConfig.Instance.m_lifeTimeMultiplier * lifetimeMultiplier;
            if (PluginConfig.Instance.m_overrideDefaultParticlesColor)
            {

                if (!PluginConfig.Instance.m_useRandomParticleColor)
                {
                    Color l_overrideParticleColor = PluginConfig.Instance.m_overrideParticleColor;
                    color = l_overrideParticleColor;
                } else
                {
                    color = new Color32(Convert.ToByte(UnityEngine.Random.Range(0, 1)), Convert.ToByte(UnityEngine.Random.Range(0, 1)), Convert.ToByte(UnityEngine.Random.Range(0, 1)),255);
                }

            }
        }

    }


}
