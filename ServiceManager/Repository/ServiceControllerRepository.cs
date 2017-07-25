using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace ServiceManager.Repository
{
    /// <summary>
    /// This Repository contains all related functions to carry on the service controller.
    /// <para>Requires System.ServiceProcess</para>
    /// <para>Developer: Sourav Barua</para>
    /// </summary>
    class ServiceControllerRepository
    {
        private ServiceController sc;
        public ServiceControllerRepository(String name)
        {
            sc = new ServiceController(name);

        }


        public String getStatus()
        {
            return sc.Status.ToString();
        }
        /// <summary>
        /// Starts the service referred by the sc variable
        /// </summary>
        /// <param name="timeoutMilliseconds"> Waiting timeout (milliseconds) for the status</param>
        /// <param name="status">out variable for recieving the status</param>
        /// <remarks>Developed By Sourav Barua</remarks>
        public void Start(int timeoutMilliseconds, out String status)
        {
            //ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds); //fix a timeout Span for waiting for the status

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                if (sc.Status == ServiceControllerStatus.Running)
                    status = "running";
                else status = this.getStatus();

            }
            catch
            {
                status = this.getStatus();
            }
        }
        /// <summary>
        /// Stops the service referred by the sc variable
        /// </summary>
        /// <param name="timeoutMilliseconds"> Waiting timeout (milliseconds) for the status</param>
        /// <param name="status">out variable for recieving the status</param>
        /// <remarks>Developed By Sourav Barua</remarks>
        public void Stop(int timeoutMilliseconds, out String status)
        {
            //ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                sc.Stop(); 
                sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                if (sc.Status==ServiceControllerStatus.Stopped)
                    status = "stopped";
                else status = this.getStatus();

            }
            catch
            {
                status = this.getStatus();
            }
        }

        /// <summary>
        /// Restarts the service referred by the sc variable
        /// </summary>
        /// <param name="timeoutMilliseconds"> Waiting timeout (milliseconds) for the status</param>
        /// <param name="status">out variable for recieving the status</param>
        /// <remarks>Developed By Sourav Barua </remarks>
        public void Restart(int timeoutMilliseconds, out String status)
        {
            //ServiceController sc = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                status = "SQL Server Restarted";
            }
            catch
            {
                status = this.getStatus();
            }
        }
        /// <summary>
        /// This returns all the services of this PC.
        /// </summary>
        /// <param name="status">The filterization will happen based on this parameter</param>
        public List<ServiceController> getAllServices (String status="all"){
            ServiceController[] allServices = ServiceController.GetServices();
            List<ServiceController> filteredServices = new List<ServiceController>();
           
            foreach(ServiceController service in allServices){
                if(status=="on")
                {
                    if(service.Status == ServiceControllerStatus.Running)
                        filteredServices.Add(service);
                }
                else if(status=="off")
                {
                    if(service.Status == ServiceControllerStatus.Stopped)
                        filteredServices.Add(service);
                }
                else if(status=="all")
                {
                        filteredServices.Add(service);
                }

            }
            return filteredServices;

            
        }





    }
}
