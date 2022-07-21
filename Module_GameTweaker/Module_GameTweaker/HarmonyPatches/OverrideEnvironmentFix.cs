using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Module_GameTweaker.Configuration;

namespace Module_GameTweaker.HarmonyPatches
{
    [HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.CreateDefaultOverrideEnvironmentSettings))]
    public class EnvironmentSettingsFixPatch
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        /// <param name="____normalEnvironmentType"></param>
        /// <param name="____a360DegreesEnvironmentType"></param>
        /// <param name="____allEnvironmentInfos"></param>
        public static void Postfix(PlayerDataFileManagerSO __instance, OverrideEnvironmentSettings __result, ref EnvironmentsListSO ____allEnvironmentInfos, ref EnvironmentTypeSO ____normalEnvironmentType, ref EnvironmentTypeSO ____a360DegreesEnvironmentType)
        {
            __result.overrideEnvironments = PluginConfig.Instance.m_overrideEnvironmentSettings;
            if (PluginConfig.Instance.m_normalEnvironmentTypeSerializeName != null)
            {
                __result.SetEnvironmentInfoForType(____normalEnvironmentType, ____allEnvironmentInfos.GetEnvironmentInfoBySerializedName(PluginConfig.Instance.m_normalEnvironmentTypeSerializeName));
            }

            if (PluginConfig.Instance.m_360EnvironmentTypeSerializeName != null)
            {
                __result.SetEnvironmentInfoForType(____a360DegreesEnvironmentType, ____allEnvironmentInfos.GetEnvironmentInfoBySerializedName(PluginConfig.Instance.m_360EnvironmentTypeSerializeName));
            }
        }
    }

    [HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.Save), new Type[] { typeof(PlayerData) })]
    public class EnvironmentSettingsFixPatchSave
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="playerData"></param>
        /// <param name="____normalEnvironmentType"></param>
        /// <param name="____a360DegreesEnvironmentType"></param>
        public static void Postfix(PlayerDataFileManagerSO __instance, ref PlayerData playerData, ref EnvironmentTypeSO ____normalEnvironmentType, ref EnvironmentTypeSO ____a360DegreesEnvironmentType)
        {
            PluginConfig.Instance.m_overrideEnvironmentSettings = playerData.overrideEnvironmentSettings.overrideEnvironments;
            PluginConfig.Instance.m_normalEnvironmentTypeSerializeName = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(____normalEnvironmentType).serializedName;
            PluginConfig.Instance.m_360EnvironmentTypeSerializeName = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(____a360DegreesEnvironmentType).serializedName;
        }
    }
}
