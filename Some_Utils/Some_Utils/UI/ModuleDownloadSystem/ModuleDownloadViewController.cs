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
using IPA.Utilities;
using System.IO;
using System.Threading.Tasks;
using BS_Utils.Utilities;
using Hive.Versioning;

namespace Some_Utils.UI.ModuleDownloadSystem
{
    class ModuleDownloadUI
    {
        protected string moduleName { get; set; }

        private string m_textWhenModuleIsAlreadyinstalled = "Redownload";

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
                m_moduleDownload.SetButtonText(m_textWhenModuleIsAlreadyinstalled);
                m_moduleAlreadyInstalled = true;
            }
        }

        [UIAction("downloadModule")]
        protected void downloadModule()
        {
            
            using (var client = new WebClient())
            {
                if (!Directory.Exists("./IPA/Pending/Plugins"))
                {
                    Directory.CreateDirectory("./IPA/Pending/Plugins");
                }
                client.DownloadFileAsync(new System.Uri(m_url), "./IPA/Pending/Plugins/" + m_name);
                client.DownloadFileCompleted += ModuleFinishDownload;
                client.DownloadProgressChanged += ModuleDownloadProgressChange;
                m_moduleDownload.SetButtonText("Downloading...");
            }
                
            
        }

        private void ModuleDownloadProgressChange(object sender, DownloadProgressChangedEventArgs e)
        {
            m_moduleDownload.SetButtonText("Downloading : " + e.ProgressPercentage.ToString());
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
                m_moduleDownload.SetButtonText(m_textWhenModuleIsAlreadyinstalled);
            } else
            {
                m_moduleDownload.SetButtonText("Download");
            }
        }
    }

    struct PluginVersion
    {
        public string m_pluginId;
        public Hive.Versioning.Version m_pluginVersion;

        public PluginVersion(string p_pluginId, Hive.Versioning.Version p_pluginVersion)
        {
            m_pluginId = p_pluginId;
            m_pluginVersion = p_pluginVersion;
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
        [UIComponent("updateText")] TextMeshProUGUI m_updateText;

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

        [UIAction("checkUpdates")]
        protected void checkUpdates()
        {

            using (var l_client = new WebClient())
            {
                l_client.DownloadFileAsync(new System.Uri("https://raw.githubusercontent.com/SheepVand0/Some_Utils_Modules/UpdateBranch/Finished_Modules/VersionsList.txt"), "./SomeUtils_UpdateFile.txt");
                l_client.DownloadFileCompleted += DownloadUpdateFileCompleted;
            }

        }

        private void DownloadUpdateFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (!File.Exists("./SomeUtils_UpdateFile.txt"))
            {
                m_updateText.gameObject.SetActive(true);
                m_updateText.text = "Error during getting update file";
                return;
            }
            try
            {
                using (var l_reader = File.OpenText("./SomeUtils_UpdateFile.txt"))
                {

                    Plugin.Log.Info("Checking valids modules versions");

                    List<string> l_lines = new List<string>();
                    string l_currentLine = "";

                    while ((l_currentLine = l_reader.ReadLine()) != null)
                    {
                        l_lines.Add(l_currentLine);
                    }

                    //------------------------------------
                    List<string> l_notUpdatedPlugins = new List<string>();
                    for (int l_i = 0; l_i < l_lines.Count; l_i++)
                    {
                        var l_splited = l_lines[l_i].Split(';');
                        PluginVersion l_version = new PluginVersion();
                        PluginMetadata l_currentPlugin;

                        l_version.m_pluginId = l_splited[0];
                        
                        if ((l_currentPlugin = PluginManager.GetPluginFromId(l_splited[0])) != null)
                        {
                            l_version.m_pluginVersion = new Hive.Versioning.Version(l_splited[1]);

                            if (l_version.m_pluginVersion.ToString() != l_currentPlugin.HVersion.ToString())
                            {
                                Plugin.Log.Info(l_currentPlugin.Id+" not updated, adding");
                                l_notUpdatedPlugins.Add(l_currentPlugin.Id);
                            } else
                            {
                                Plugin.Log.Info(l_currentPlugin.Id+" updated, not adding");
                            }
                        }
                    }

                    if (l_notUpdatedPlugins.Count > 0)
                    {
                        string l_returnedValue = "";

                        foreach (string l_current in l_notUpdatedPlugins)
                        {
                            l_returnedValue = l_returnedValue + l_current + ", ";
                        }

                        m_updateText.gameObject.SetActive(true);
                        m_updateText.text = l_returnedValue + " need to be update";

                    }
                    else
                    {
                        m_updateText.gameObject.SetActive(true);
                        m_updateText.text = "All plugins are updated";
                    }

                    File.Delete("./SomeUtils_UpdateFile.txt");

                }
            } catch (Exception l_e)
            {
                Plugin.Log.Error(l_e);
            }
        }

        public void UpdateView()
        {
            m_loadingLayout.gameObject.SetActive(m_postParseLoadingLayoutVisible);
            m_errorText.gameObject.SetActive(m_postParseErrorTextVisible);
        }

    }
}
