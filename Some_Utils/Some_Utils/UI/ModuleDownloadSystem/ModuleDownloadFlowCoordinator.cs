using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using HMUI;

namespace Some_Utils.UI.ModuleDownloadSystem
{
    class ModuleDownloadFlowCoordinator : FlowCoordinator
    {

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (!firstActivation)
                return;

            SetTitle("Download modules");
            showBackButton = true;
            
        }

    }
}
