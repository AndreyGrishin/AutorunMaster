using System;
using System.Collections.Generic;
using System.IO;
using AutorunMaster.Model.Entities;
using AutorunMaster.Model.Enums;
using AutorunMaster.Model.Tools;
using IWshRuntimeLibrary;

namespace AutorunMaster.Model
{
    public class StartMenuManager : IAutorunManager
    {
        private readonly string _startupDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\Startup";

        /// <summary>
        /// Returns the list of <see cref="AutorunObject"/> objects that have been added through Start menu directory
        /// </summary>
        public List<AutorunObject> GetAutorunObjects()
        {
            List<AutorunObject> resultObjectsList = new List<AutorunObject>();

            DirectoryInfo directoryInfo = new DirectoryInfo(_startupDirectoryPath);

            if (!directoryInfo.Exists) return null;

            // If need show all files remove searchPattern string
            var files = directoryInfo.GetFiles("*.lnk");

            foreach (var file in files)
            {
                WshShell shell = new WshShell();

                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(file.FullName);

                string targetPath = link.TargetPath;
                string arguments = link.Arguments;           

                var autorunObject = AutorunObjectHelper.GetAutorunObject(targetPath, arguments, AutorunTypes.StartMenu);

                if (autorunObject != null)
                {
                    resultObjectsList.Add(autorunObject);
                }                  
            }

            return resultObjectsList;         
        }

    }
}
