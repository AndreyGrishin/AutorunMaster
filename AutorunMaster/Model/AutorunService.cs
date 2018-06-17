using System.Collections.Generic;
using System.Threading.Tasks;
using AutorunMaster.Model.Entities;

namespace AutorunMaster.Model
{
    public class AutorunService
    {
        public AutorunService()
        {
            
        }

        public Task<List<AutorunObject>> GetAutorunObjects()
        {
            return Task.Factory.StartNew(() =>
            {
                List<AutorunObject> resultList = new List<AutorunObject>();

                IAutorunManager startMenuManager = new StartMenuManager();
                IAutorunManager registryAutorunManager = new RegistryAutorunManager();
                IAutorunManager schedulerAutorunManager = new SchedulerAutorunManager();

                resultList.AddRange(schedulerAutorunManager.GetAutorunObjects());
                resultList.AddRange(registryAutorunManager.GetAutorunObjects());
                resultList.AddRange(startMenuManager.GetAutorunObjects());

                return resultList;
            });          
        }
    }
}
