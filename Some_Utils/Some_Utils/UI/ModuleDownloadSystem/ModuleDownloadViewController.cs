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
        protected string moduleName { get; set; }

        protected string downloadText { get { return "Download"; } set { } }

        [UIComponent("moduleName")] TextMeshProUGUI m_moduleName = null;
        [UIComponent("downloadButton")] Button m_moduleDownload = null;

        string m_name;
        string m_url;
        string m_moduleId;
        bool m_moduleAlreadyInstalled;
        public ModuleDownloadUI(string p_name, string p_url, string p_moduleId)
        {
            m_name = p_name;
            m_url = p_url;
            m_moduleId = p_moduleId;
     
        }

        [UIAction("#post-parse")]
        protected void postparse()
        {
            m_moduleName.text = m_moduleId;
            var l_pluginInstalled = PluginManager.GetPluginFromId(m_moduleId);
            if (l_pluginInstalled != null)
            {
                m_moduleDownload.SetButtonText("Delete");
                m_moduleAlreadyInstalled = true;
            }
        }

        [UIAction("downloadModule")]
        protected void downloadModule()
        {
            
            if (m_moduleAlreadyInstalled)
            {
                File.Delete("./Plugins/" + m_name);
                m_moduleAlreadyInstalled = false;
                m_moduleDownload.SetButtonText("Download");
            }
            else
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new System.Uri(m_url), "./Plugins/"+m_name);
                    client.DownloadFileCompleted += ModuleFinishDownload;
                    client.DownloadProgressChanged += ModuleDownloadProgressChange;
                    m_moduleDownload.SetButtonText("Downloading...");
                }
                
            }
        }

        private void ModuleDownloadProgressChange(object sender, DownloadProgressChangedEventArgs e)
        {
            m_moduleDownload.SetButtonText("Downloading " + e.ProgressPercentage.ToString());
        }

        public bool CheckIfModuleDownloaded()
        {
            return File.Exists("./Plugins/" + m_name);
        }

        private void ModuleFinishDownload(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            m_moduleAlreadyInstalled=CheckIfModuleDownloaded();
            if (m_moduleAlreadyInstalled)
            {
                m_moduleDownload.SetButtonText("Delete");
            } else
            {
                m_moduleDownload.SetButtonText("Download");
            }
        }
    }

    [HotReload(RelativePathToLayout = @"ModuleDownloadViewController.bsml")]
    [ViewDefinition("Some_Utils.UI.ModuleDownloadSystem.ModuleDownloadViewController.bsml")]
    internal class ModuleDownloadViewController : BSMLAutomaticViewController
    {
        
        public ModuleDownloadViewController()
        {
            
        }

        [UIComponent("ErrorText")] TextMeshProUGUI m_errorText = null;
        [UIComponent("loadingLayout")] VerticalLayoutGroup m_loadingLayout = null;

        public List<ModuleDownloadUI> downloadableModules = new List<ModuleDownloadUI>() { new ModuleDownloadUI("None", "Undefined", "None") };

        public bool m_postParseLoadingLayoutVisible = true;
        public bool m_postParseErrorTextVisible = false;

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            UpdateView();   
            //using (var l_reader = File.OpenText(""))
            // Code to run after BSML finishe
        }

        public void UpdateView()
        {
            m_loadingLayout.gameObject.SetActive(m_postParseLoadingLayoutVisible);
            m_errorText.gameObject.SetActive(m_postParseErrorTextVisible);
        }

    }
}
