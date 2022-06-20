using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using HMUI;
using System.Net;
using System.IO;

namespace Some_Utils.UI.ModuleDownloadSystem
{
    class ModuleDownloadFlowCoordinator : FlowCoordinator
    {

        ModuleDownloadViewController _m_downloadViewController;

        FlowCoordinator m_lastFlow;

        public void Awake()
        {
            
        }
             
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;

            SetTitle("Download modules");
            showBackButton = true;
        }

        List<ModuleDownloadUI> m_tempModulesList = new List<ModuleDownloadUI>();

        public void ShowFlow()
        {
            //Download module_list from web
            string l_dir = "C:/SomeUtils_Modules";
            string l_fileName = "modules_list.txt";
            string l_filePath = "./modules_list.txt";
            if (!Directory.Exists(l_dir))
            {
                Directory.CreateDirectory(l_dir);
            }

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new System.Uri("https://raw.githubusercontent.com/SheepVand0/Some_Utils_Modules/main/Finished_Modules/FinishedModules.txt"), l_filePath);
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }

        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            string l_dir = "C:/SomeUtils_Modules";
            string l_fileName = "modules_list.txt";
            string l_filePath = "./modules_list.txt";

            //Checking if the downlaod of modules_list finished correctly
            if (!File.Exists(l_filePath))
            {
                if (_m_downloadViewController == null)
                    _m_downloadViewController = BeatSaberUI.CreateViewController<ModuleDownloadViewController>();

                ProvideInitialViewControllers(_m_downloadViewController);
                

                _m_downloadViewController.m_postParseLoadingLayoutVisible = false;
                _m_downloadViewController.m_postParseErrorTextVisible = true;
                //m_downloadViewController.UpdateView();
                return;
            }

            //If successful, getting modules
            using (var l_reader = File.OpenText(l_filePath))
            {
                List<string> l_lines = new List<string>();
                string l_currentLine;
                while ((l_currentLine = l_reader.ReadLine()) != null)
                {
                    l_lines.Add(l_currentLine);
                }

                foreach (var l_line in l_lines)
                {
                    var l_splited = l_line.Split(';');

                    //On the FinishedModules.txt file in Github; Params : Dll name;File Url;Module Id(Same as the id in manifest.json of the module);

                    m_tempModulesList.Add(new ModuleDownloadUI(l_splited[0], l_splited[1], l_splited[2]));
                }
            }

            Plugin.Log.Info("Module list refreshing finished");


            //Creating view Controller
            if (_m_downloadViewController == null)
                _m_downloadViewController = BeatSaberUI.CreateViewController<ModuleDownloadViewController>();
            
            _m_downloadViewController.downloadableModules = m_tempModulesList;

            ProvideInitialViewControllers(_m_downloadViewController);

            _m_downloadViewController.m_postParseErrorTextVisible = false;
            _m_downloadViewController.m_postParseLoadingLayoutVisible = false;
            //m_downloadViewController.UpdateView();

            m_lastFlow = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            m_lastFlow.PresentFlowCoordinator(this, null, ViewController.AnimationDirection.Vertical, false, false);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            base.BackButtonWasPressed(topViewController);
            m_lastFlow.DismissFlowCoordinator(this);
        }

    }
}
