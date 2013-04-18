#region References
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
#endregion

namespace Polycom.RMX2000.TranslationManager.TranslationAnalyzer
{
    public static class SolutionManager
    {
        #region Fields and Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static List<string> GetXmlFiles(string filePath)
        {
            if (!ValidateSolutionFile(filePath))
            {
                return null;
            }

            string uiProjectPath = filePath.Replace("EMA.sln", @"EMA.UI\EMA.UI.csproj");

            string directoryName = new FileInfo(uiProjectPath).DirectoryName;
            List<string> xmlFiles = new List<string>();
            string xmlFilePath = String.Empty;

            using (XmlReader reader = XmlReader.Create(uiProjectPath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name.Equals("Content")
                        && reader.AttributeCount >= 1
                        && reader[0].ToLower().Contains(".xml"))
                    {
                        xmlFilePath = reader[0];

                        if (!xmlFilePath.Contains("DialogsStrings")
                            && !xmlFilePath.Contains("EnumConfigSetStrings")
                            && !xmlFilePath.Contains("InternalConfigSetStrings")
                            && !xmlFilePath.Contains("MessageDialogStrings"))
                        {
                            continue;
                        }

                        xmlFiles.Add(String.Format("{0}\\{1}", directoryName, xmlFilePath));
                    }
                }
            }

            return xmlFiles;
        }

        public static List<string> GetDuplicatedKeys(string filePath)
        {
            List<string> keys = TranslateManager.GetTranslationKeys(filePath);
            keys.Sort();

            string lastKey = String.Empty;
            List<string> duplicatedKeys = new List<string>();

            foreach (string key in keys)
            {
                if (key.Equals(lastKey) && !duplicatedKeys.Contains(key))
                {
                    duplicatedKeys.Add(key);
                }

                lastKey = key;
            }

            return duplicatedKeys;
        }
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private static bool ValidateSolutionFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath) || String.IsNullOrEmpty(filePath.Trim()))
            {
                throw new Exception("Solution file path cannot be empty.");
            }

            FileInfo projectFile = new FileInfo(filePath);

            if (!projectFile.Exists)
            {
                throw new FileNotFoundException("Solution cannot be found.");
            }
            else if (!projectFile.Extension.Equals(".sln", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Invalid solution file.");
            }

            return true;
        }
        #endregion

        #region Delegates and Events
        #endregion
    }
}