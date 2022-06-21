using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using NoteShadows.Configuration;
using NoteShadows.Utils;

namespace NoteShadows.HarmonyPatches
{

    [HarmonyPatch(typeof(NoteController), "Awake")]
    internal static class SpawnShadowsOnNoteStartJump
    {
        static Sprite l_shadowTexture = BeatSaberMarkupLanguage.Utilities.FindSpriteInAssembly("NoteShadows.Textures.ShadowOpacity.png");
        [HarmonyPriority(int.MaxValue)]
        private static void Postfix(NoteController __instance, ref NoteMovement ____noteMovement, ref NoteData ____noteData)
        {

            if (!PluginConfig.Instance.show_note_shadow)
                return;

            GameObject l_currentGM = new GameObject("NoteShadow");
            SpriteRenderer l_currentMeshRend = l_currentGM.AddComponent<SpriteRenderer>();
            l_currentGM.transform.SetParent(__instance.transform, true);
            l_currentGM.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            l_currentGM.transform.localScale = Vector3.one * (0.17f * PluginConfig.Instance.note_shadow_scale);

            //l_yToRemove = __instance.transform.GetChild(0).gameObject.transform.position.y;
            l_currentGM.transform.localPosition = new Vector3(-0.67f * PluginConfig.Instance.note_shadow_scale,  l_currentGM.transform.localPosition.y, l_currentGM.transform.localPosition.z + PluginConfig.Instance.m_noteShadowOffset);
            l_currentMeshRend.sprite = l_shadowTexture;

            l_currentMeshRend.material.color = PluginConfig.Instance.note_shadow_color.ColorWithAlpha(PluginConfig.Instance.note_shadow_opacity);
            l_currentMeshRend.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive;
            l_currentMeshRend.spriteSortPoint = SpriteSortPoint.Center;

        }
    }

    [HarmonyPatch(typeof(NoteController), "HandleNoteDidStartJump")]
    static class ControlShadowsPosAfterJump
    {
        public static void Postfix(NoteController __instance)
        {
            if (!PluginConfig.Instance.show_note_shadow)
                return;

            GameObject l_shadowGm = __instance.transform.Find("NoteShadow").gameObject;
            if (l_shadowGm != null)
            {
                float l_yToRemove = 0.0f;

                switch (__instance.noteData.noteLineLayer)
                {
                    case NoteLineLayer.Base:
                        l_yToRemove = -1.0f;
                        break;
                    case NoteLineLayer.Upper:
                        l_yToRemove = -1.5f;
                        break;
                    case NoteLineLayer.Top:
                        l_yToRemove = -2.0f;
                        break;
                }

                //l_yToRemove = l_yToRemove + PluginConfig.Instance.m_noteShadowHeight;

                l_shadowGm.transform.localPosition = new Vector3(l_shadowGm.transform.localPosition.x, l_yToRemove + PluginConfig.Instance.m_noteShadowHeight , l_shadowGm.transform.localPosition.z);

            }
        }

    }
}
