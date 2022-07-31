using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Module_GameTweaker.Configuration;
using UnityEngine;

namespace Module_GameTweaker
{
    [HarmonyPatch(typeof(TrackLaneRingsManager),"Awake")]
    internal class RingsPatches
    {

        public static void Prefix(TrackLaneRingsManager __instance, ref int ____ringCount)
        {
            ____ringCount = PluginConfig.Instance.m_ringsCount;
            /*foreach (var l_currentRing in __instance.Rings)
            {
                l_currentRing.gameObject.transform.localScale = Vector3.one * PluginConfig.Instance.m_ringsScale;
            }*/
        }

    }

    [HarmonyPatch(typeof(TrackLaneRing),nameof(TrackLaneRing.SetDestRotation), new Type[] { typeof(float), typeof(float) })]
    class RingsRotSpeedPatch
    {

        public static void Prefix(TrackLaneRing __instance,ref float destRotZ, ref float rotateSpeed)
        {

            rotateSpeed = PluginConfig.Instance.m_ringsTurnSpeed;

        }
    }

    [HarmonyPatch(typeof(TrackLaneRing),nameof(TrackLaneRing.SetPosition), new Type[] { typeof(float), typeof(float) })]
    class RingMoveSpeedPatch
    {
        public static void Prefix(TrackLaneRing __instance, ref float destPosZ, ref float moveSpeed)
        {
            moveSpeed = PluginConfig.Instance.m_ringsMoveSpeed;
        }
    }






}
