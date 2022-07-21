using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Module_GameTweaker.Configuration;

namespace Module_GameTweaker.HarmonyPatches
{
    [HarmonyPatch(typeof(MultiplayerBigAvatarAnimator), nameof(MultiplayerBigAvatarAnimator.Animate))]
    class MultiplayerPatches
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="__instance"></param>
        public static void Prefix(MultiplayerBigAvatarAnimator __instance)
        {
            if (PluginConfig.Instance.m_disablePreviewOfTheBestInMulti)
            {
                __instance.HideInstant();
            }
        }

    }

    [HarmonyPatch(typeof(MultiplayerController),nameof(MultiplayerController.ChangeState), new Type[] { typeof(MultiplayerController.State)})]
    class MultiplayerOnStateChanged
    {
        public static void Postfix(MultiplayerController __instance, ref MultiplayerController.State newState)
        {
            var l_lightWithIdManager = Resources.FindObjectsOfTypeAll<LightWithIdManager>().FirstOrDefault();

            if (newState == MultiplayerController.State.CheckingLobbyState)
            {
                for (int l_i = 0;l_i < l_lightWithIdManager.GetLightsArray().Length;l_i++)
                {
                    l_lightWithIdManager.SetColorForId(l_i, PluginConfig.Instance.m_multiLightsColor);
                }
            } else
            {
                Module_GameTweakerController l_gameController = Resources.FindObjectsOfTypeAll<Module_GameTweakerController>().FirstOrDefault();
                if (l_gameController != null)
                {
                    if (l_gameController.m_defaultMultiLightsColor == null)
                    {
                        l_gameController.m_defaultMultiLightsColor = l_lightWithIdManager.GetColorForId(0);
                    } else
                    {
                        for (int l_i = 0; l_i < l_lightWithIdManager.GetLightsArray().Length;l_i++)
                        {
                            l_lightWithIdManager.SetColorForId(l_i, l_gameController.m_defaultMultiLightsColor);
                        }
                    }
                }
            }
        }
    }
}
