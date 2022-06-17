using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Module_GameTweaker.Configuration;

namespace Module_GameTweaker.HarmonyPatches
{
    [HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.LoadFromCurrentVersion))]
    public class EnvironmentSettingsFixPatch
    {
        /// <summary>
        /// This code is run before the original code in MethodToPatch is run.
        /// </summary>
        /// <param name="__instance">The instance of ClassToPatch</param>
        ///     added three _ to the beginning to reference it in the patch. Adding ref means we can change it.</param>
        static void Postfix(PlayerDataFileManagerSO __instance, ref PlayerData __result, ref EnvironmentTypeSO ____normalEnvironmentType, ref EnvironmentTypeSO ____a360DegreesEnvironmentType, ref EnvironmentsListSO ____allEnvironmentInfos)
        {
            __result.overrideEnvironmentSettings.overrideEnvironments = PluginConfig.Instance.m_overrideEnvironmentSettings;
            if (PluginConfig.Instance.m_normalEnvironmentTypeSerializeName != null)
            {
                __result.overrideEnvironmentSettings.SetEnvironmentInfoForType(____normalEnvironmentType, ____allEnvironmentInfos.GetEnvironmentInfoBySerializedName(PluginConfig.Instance.m_normalEnvironmentTypeSerializeName));
            }

            if (PluginConfig.Instance.m_360EnvironmentTypeSerializeName != null)
            {
                __result.overrideEnvironmentSettings.SetEnvironmentInfoForType(____a360DegreesEnvironmentType, ____allEnvironmentInfos.GetEnvironmentInfoBySerializedName(PluginConfig.Instance.m_360EnvironmentTypeSerializeName));
            }
        }
    }

    [HarmonyPatch(typeof(PlayerDataFileManagerSO), nameof(PlayerDataFileManagerSO.Save))]
    public class EnvironmentSettingsFixPatchSave
    {
        static void Postfix(PlayerDataFileManagerSO __instance, PlayerData playerData, ref EnvironmentTypeSO ____normalEnvironmentType, ref EnvironmentTypeSO ____a360DegreesEnvironmentType)
        {

            PluginConfig.Instance.m_overrideEnvironmentSettings = playerData.overrideEnvironmentSettings.overrideEnvironments;
            PluginConfig.Instance.m_normalEnvironmentTypeSerializeName = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(____normalEnvironmentType).serializedName;
            PluginConfig.Instance.m_360EnvironmentTypeSerializeName = playerData.overrideEnvironmentSettings.GetOverrideEnvironmentInfoForType(____a360DegreesEnvironmentType).serializedName;

        }
    }
}
