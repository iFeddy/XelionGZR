using System.ServiceProcess;

namespace XelionZR
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new XelionZR.Main(),
            };
            ServiceBase.Run(ServicesToRun);

        }
    }
}
