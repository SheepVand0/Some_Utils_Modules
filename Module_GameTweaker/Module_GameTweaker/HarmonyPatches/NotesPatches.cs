using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Module_GameTweaker.Configuration;
using UnityEngine;

namespace Module_GameTweaker.HarmonyPatches
{
    //[HarmonyPatch(typeof(Player),"Awake")]
    class NotesPatches
    {
        public static void Prefix(NoteController __instance)
        {
            if (!PluginConfig.Instance.EditPlayerHeight) return;

            Vector3 l_NotePos = __instance.transform.localPosition;
            __instance.transform.localPosition = new Vector3(l_NotePos.x, l_NotePos.y + PluginConfig.Instance.PlayerHeightAdd, l_NotePos.z);
        }
    }
}
