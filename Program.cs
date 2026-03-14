using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace winRubish_TempDataServiceWeeklyDeletor
{
    internal static class Program
    {
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine("WRDD Service is running in console mode...");
                var service = new rubishDataDeletor();
                service.RunAsConsole();
                Console.WriteLine("WRDD Service is Done. Press any key to exit.");
                Console.ReadKey();
            }
            else
            {
                ServiceBase.Run(new ServiceBase[] { new rubishDataDeletor() });
            }
        }
    }
}
