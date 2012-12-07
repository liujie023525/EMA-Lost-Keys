﻿#region References
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
#endregion

namespace Polycom.RMX2000.EMALostKeys.TranslationAnalyzer
{
    public static class ProjectManager
    {
        #region Fields and Properties
        private static List<List<string>> _englishKeys = null;
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static List<string> GetCSFiles(string filePath)
        {
            if (!ValidateProjectFile(filePath))
            {
                return null;
            }

            string directoryName = new FileInfo(filePath).DirectoryName;
            List<string> csFiles = new List<string>();

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("Compile") && reader.AttributeCount >= 1)
                    {
                        csFiles.Add(String.Format("{0}\\{1}", directoryName, reader[0]));
                    }
                }
            }

            return csFiles;
        }

        public static List<string> GetMissingKeys(string csFile)
        {
            using (StreamReader reader = new StreamReader(csFile))
            {
                List<string> missingKeys = new List<string>();

                string codeLine = String.Empty;
                string content = String.Empty;

                while (!reader.EndOfStream)
                {
                    codeLine = reader.ReadLine();

                    if (codeLine.Contains("UIManager.TranslationManager.GetTranslation"))
                    {
                        continue;
                    }
                    else if (codeLine.Replace(" ", String.Empty).Contains(".Text="))
                    {
                        content = codeLine.Split("=".ToCharArray())[1].TrimEnd(";".ToCharArray());

                        if (!content.Contains("\""))
                        {
                            //TODO

                            continue;
                        }

                        if (!ValidateKeyExist(content))
                        {
                            missingKeys.Add(content);
                        }
                    }
                }

                return missingKeys;
            }
        }

        public static void InitializeEnglishKeys(string emaDirectoryName)
        {
            string languageFoldPath = String.Format("{0}\\EMA.UI\\Configuration\\Languages\\English", emaDirectoryName);

            _englishKeys = new List<List<string>>();

            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\DialogsStringsEnglish.xml"));
            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\EnumConfigSetStringsEnglish.xml"));
            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\InternalConfigSetStringsEnglish.xml"));
            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\MessageDialogStringsEnglish.xml"));
        }
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        private static bool ValidateProjectFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath) || String.IsNullOrEmpty(filePath.Trim()))
            {
                throw new Exception("UI project file path cannot be empty.");
            }

            FileInfo projectFile = new FileInfo(filePath);

            if (!projectFile.Exists)
            {
                throw new FileNotFoundException("UI project cannot be found.");
            }
            else if (!projectFile.Extension.Equals(".csproj", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Invalid UI project file.");
            }

            return true;
        }

        private static bool ValidateKeyExist(string key)
        {
            if (_englishKeys == null)
            {
                throw new TranslationException("English keys wasn't initialized.");
            }

            foreach (List<string> englishKeyList in _englishKeys)
            {
                foreach (string englishKey in englishKeyList)
                {
                    if (key.Equals(englishKey))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Delegates and Events
        #endregion
    }
}