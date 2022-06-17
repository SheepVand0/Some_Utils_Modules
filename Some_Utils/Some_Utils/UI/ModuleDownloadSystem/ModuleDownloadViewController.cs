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
                downloadText = "Delete";
            }
        }

        [UIAction("downloadModule")]
        protected void downloadModule()
        {
            if (m_moduleAlreadyInstalled)
            {
                File.Delete("./" + m_name);
                m_moduleAlreadyInstalled = false;
            }
            else
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new System.Uri(m_url), "./"+m_name);
                }

            }
        }

        public void UpdateText()
        {
            moduleName = m_name;
        }
    }

    [HotReload(RelativePathToLayout = @"ModuleDownloadViewController.bsml")]
    [ViewDefinition("Some_Utils.UI.ModuleDownloadSystem.ModuleDownloadViewController.bsml")]
    internal class ModuleDownloadViewController : BSMLAutomaticViewController
    {
        List<ModuleDownloadUI> downloadableModules = new List<ModuleDownloadUI>() { new ModuleDownloadUI("None", "Undefined","None") };

        protected bool indicatorActive { get { return true; } set { } }

        protected string refreshingPercentage { get { return "%"; } set { } }

        public void Awake()
        {
            
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            string l_dir = "C:/SomeUtils_Modules";
            string l_fileName = "modules_list.txt";
            if (!Directory.Exists(l_dir))
            {
                Directory.CreateDirectory(l_dir);
            }

            if (File.Exists(l_dir+"/"+l_fileName))
            {
                File.Delete(l_dir+"/"+l_fileName);
            }

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new System.Uri("https://raw.githubusercontent.com/SheepVand0/Some_Utils_Modules/main/Finished_Modules/FinishedModules.txt"), "C:\\Modules\\"+l_fileName);
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }

            

            //using (var l_reader = File.OpenText(""))
            // Code to run after BSML finishe
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            refreshingPercentage = e.ProgressPercentage.ToString() + "%";
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Plugin.Log.Info("Module list refreshing finished");
            string l_dir = "C:/SomeUtils_Modules";
            string l_fileName = "modules_list.txt";
            using (var l_reader = File.OpenText(l_dir + "/"+l_fileName))
            {
                List<string> l_lines = new List<string>();
                string l_currentLine;
                while ((l_currentLine = l_reader.ReadLine()) != null)
                {
                    l_lines.Add(l_currentLine);
                }

                downloadableModules = new List<ModuleDownloadUI>();

                foreach (var l_line in l_lines)
                {
                    var l_splited = l_line.Split(';');

                    //On the FinishedModules.txt file in Github; Params : Dll name;File Url;Module Id(Same as the id in manifest.json of the module);
                    downloadableModules.Add(new ModuleDownloadUI(l_splited[0], l_splited[1], l_splited[2]));
                }

                foreach (ModuleDownloadUI l_current in downloadableModules)
                {
                    l_current.UpdateText();
                }
            }
            if (File.Exists(l_dir+"/"+l_fileName))
            {

            }
            
            indicatorActive = false;
        }
    }
}
