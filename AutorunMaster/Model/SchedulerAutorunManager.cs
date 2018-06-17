using System.Collections.Generic;
using System.IO;
using AutorunMaster.Model.Entities;
using AutorunMaster.Model.Enums;
using AutorunMaster.Model.Tools;
using TaskScheduler;

namespace AutorunMaster.Model
{
    public class SchedulerAutorunManager : IAutorunManager
    {
        /// <summary>
        /// Returns the list of <see cref="AutorunObject"/> objects that have been added through TaskScheduler
        /// </summary>
        public List<AutorunObject> GetAutorunObjects()
        {
            List<AutorunObject> resultObjectsList = new List<AutorunObject>();

            TaskScheduler.TaskScheduler taskService = new TaskScheduler.TaskScheduler();
            taskService.Connect();

            ITaskFolder rootFolder = taskService.GetFolder(@"\");

            IRegisteredTaskCollection regTask = rootFolder.GetTasks(0);

            foreach (IRegisteredTask task in regTask)
            {
                ITaskDefinition taskDefinition = task.Definition;
                ITriggerCollection triggerCollection = taskDefinition.Triggers;

                foreach (ITrigger trigger in triggerCollection)
                {
                    if (trigger.Type == _TASK_TRIGGER_TYPE2.TASK_TRIGGER_BOOT ||
                        trigger.Type == _TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON)
                    {
                       IActionCollection actionCollection = taskDefinition.Actions;

                        foreach (IExecAction action in actionCollection)
                        {
                            if (action.Type != _TASK_ACTION_TYPE.TASK_ACTION_EXEC) continue;

                            var autorunObject = AutorunObjectHelper.GetAutorunObject(action.Path, action.Arguments, AutorunTypes.Scheduler);

                            if (autorunObject != null)
                            {
                                resultObjectsList.Add(autorunObject);
                            }
                        }
                    }
                }
            }

            return resultObjectsList;        
        }
    }
}
