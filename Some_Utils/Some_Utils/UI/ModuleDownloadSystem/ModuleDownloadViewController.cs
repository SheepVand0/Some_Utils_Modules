using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine.UI;
using IPA;
using IPA.Loader;
using System.IO;
using System.Threading.Tasks;
using BS_Utils.Utilities;

namespace Some_Utils.UI.ModuleDownloadSystem
{
    class ModuleDownloadUI
    {
        [UIComponent("moduleName")] TextMeshProUGUI m_moduleName = null;
        [UIComponent("downloadButton")] Button m_downloadModule = null;

        string m_name;
        string m_url;
        string m_moduleId;
        bool m_moduleAlreadyInstalled;
        public ModuleDownloadUI(string p_name, string p_url, string p_moduleId)
        {
            m_name = p_name;
            m_url = p_url;
            m_moduleId = p_moduleId;

            var l_pluginInstalled = PluginManager.GetPluginFromId(p_moduleId);
            if (l_pluginInstalled != null)
            {
                m_moduleAlreadyInstalled = true;
                m_downloadModule.SetButtonText("Delete");
#if DEBUG
                m_downloadModule.enabled = false;
#endif
            }
        }

        [UIAction("downloadModule")]
        protected void downloadModule()
        {
            if (m_moduleAlreadyInstalled)
            {
                File.Delete("./" + m_name);
                m_moduleAlreadyInstalled = false;
                m_downloadModule.SetButtonText("Download");
            }
            else
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(m_url, m_name);
                }

            }
        }
    }

    [HotReload(RelativePathToLayout = @"ModuleDownloadViewController.bsml")]
    [ViewDefinition("Some_Utils.UI.ModuleDownloadSystem.ModuleDownloadViewController.bsml")]
    internal class ModuleDownloadViewController : BSMLAutomaticViewController
    {
        List<ModuleDownloadUI> m_moduleDownloadList = new List<ModuleDownloadUI>();

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("","");
            }
            // Code to run after BSML finishes
        }
    }
}
