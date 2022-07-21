using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Module_GameTweaker.Configuration;
using UnityEngine;
using TMPro;

namespace Module_GameTweaker.HarmonyPatches
{

    [HarmonyPatch(typeof(ComboUIController), nameof(ComboUIController.Start))]
    class ComboPatch
    {

        public static void Prefix(ComboUIController __instance, TextMeshProUGUI ____comboText)
        {
            if (!PluginConfig.Instance.m_useComboGradient) return;

            ____comboText.enableVertexGradient = true;

            Color l_Color1 = PluginConfig.Instance.m_comboGradient1;
            Color l_Color2 = PluginConfig.Instance.m_comboGradient2;
            ____comboText.colorGradient = new VertexGradient(l_Color1, l_Color1, l_Color2, l_Color2);
        }

    }
}
