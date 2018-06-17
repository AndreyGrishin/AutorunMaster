using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AutorunMaster.Model.Entities;
using AutorunMaster.Model.Enums;
using AutorunMaster.Model.Tools;
using Microsoft.Win32;

namespace AutorunMaster.Model
{
    public class RegistryAutorunManager : IAutorunManager
    {
        private const string RegistryNodePath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string RegistryNodePath6432 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run";


        /// <summary>
        /// Returns the list of <see cref="AutorunObject"/> objects that have been added through Registry
        /// </summary>
        public List<AutorunObject> GetAutorunObjects()
        {
            List<AutorunObject> resultObjectsList = new List<AutorunObject>();
            List<RegistryKey> registryKeys = new List<RegistryKey>();

            RegistryKey currentUserRegistryKey = Registry.CurrentUser.OpenSubKey(RegistryNodePath, true);
            RegistryKey currentUserRegistryKey6432 = Registry.CurrentUser.OpenSubKey(RegistryNodePath6432, true);
            RegistryKey localMachineRegistryKey = Registry.LocalMachine.OpenSubKey(RegistryNodePath, true);
            RegistryKey localMachineRegistryKey6432 = Registry.LocalMachine.OpenSubKey(RegistryNodePath6432, true);

            registryKeys.Add(currentUserRegistryKey);
            registryKeys.Add(currentUserRegistryKey6432);
            registryKeys.Add(localMachineRegistryKey);
            registryKeys.Add(localMachineRegistryKey6432);

            registryKeys.ForEach(LoadObjects);

            return resultObjectsList;

            void LoadObjects(RegistryKey regKey)
            {
                if (regKey == null) return;

                foreach (var keyName in regKey.GetValueNames())
                {
                    var subkey = regKey.GetValue(keyName);
                    string firstPattern = "(\"(?<path>.*?)\"\\s)(?<arguments>.*)";
                    string secondPattern = "((?<path>.*)\\s(?<arguments>.*))";

                    string filePath = string.Empty;
                    string arguments = string.Empty;
                    bool isAutorunObject;

                    // Checking for Pattern Matching
                    if (File.Exists(subkey.ToString()))
                    {
                        filePath = subkey.ToString();
                        isAutorunObject = true;
                    }
                    else if (CheckIsMatch(firstPattern, subkey.ToString(), out filePath, out arguments))
                    {
                        isAutorunObject = true;
                    }
                    else
                    {
                        isAutorunObject = CheckIsMatch(secondPattern, subkey.ToString(), out filePath, out arguments);
                    }

                    if (!isAutorunObject) return;

                    var autorunObject = AutorunObjectHelper.GetAutorunObject(filePath, arguments, AutorunTypes.Registry);

                    if(autorunObject != null)
                    {
                        resultObjectsList.Add(autorunObject);
                    }
                }
            }
        }     

        /// <summary>
        /// Performs a path pattern matching test
        /// </summary>
        /// <param name="regexPattern">The compliance test pattern</param>
        /// <param name="input">Verification string</param>
        /// <param name="filePath">If pattern matching success set new file name</param>
        /// <param name="arguments">If pattern matching success set new arguments</param>
        /// <returns></returns>
        private static bool CheckIsMatch(string regexPattern, string input, out string filePath, out string arguments)
        {
            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            filePath = string.Empty;
            arguments = string.Empty;

            for (var match = regex.Match(input); match.Success;)
            {
                filePath = match.Groups["path"].Value;
                arguments = match.Groups["arguments"].Value;
                return true;
            }

            return false;
        }
    }
}
