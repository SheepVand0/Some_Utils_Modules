using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_GameTweaker.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Module_GameTweaker.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatLineManager),nameof(BeatLineManager.Start))]
    internal class BeatLinesPatch
    {

        public static void Prefix(BeatLine __instance)
        {

            if (PluginConfig.Instance.m_disableBeatLines)
            {
                __instance.enabled = false;
            }

        }

    }
}
