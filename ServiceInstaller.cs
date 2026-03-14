using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace winRubish_TempDataServiceWeeklyDeletor
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceInstaller _serviceInstaller;
        private ServiceProcessInstaller _serviceProcessInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            _serviceInstaller = new ServiceInstaller();
            _serviceInstaller.ServiceName = "WRDD";
            _serviceInstaller.StartType = ServiceStartMode.Manual;
            _serviceInstaller.DisplayName = "Cleaning Rubbish Data From System.";
            _serviceInstaller.Description = "Deleting All Temp Files / Corrupted Unused Files / Rubbish Data / Old System Files From The Entire PC Every 7 Days";

            _serviceProcessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.LocalSystem
            };

            Installers.Add(_serviceProcessInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
