#region References
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
        private static List<char> _englishChars = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        private static List<string> _allowedKeys = null;
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
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name.Equals("Compile")
                        && reader.AttributeCount >= 1
                        && !reader[0].Contains(".designer.cs"))
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

                    if (codeLine.Contains("UIManager.TranslationManager.GetTranslation")
                        || !codeLine.Replace(" ", String.Empty).Contains(".Text=")
                        || codeLine.TrimEnd().StartsWith("//"))
                    {
                        continue;
                    }

                    content = codeLine.Split("=".ToCharArray())[1].TrimEnd(";".ToCharArray());

                    if (String.IsNullOrEmpty(content)
                        || content.Trim().Equals("\"\"", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    if (!content.Trim().StartsWith("\"") && content.Contains("\""))
                    {
                        content = content.Substring(content.IndexOf('\"')).Substring(0, content.Substring(content.IndexOf('\"')).LastIndexOf('\"') + 1);
                    }

                    if (!content.Contains("\"") || content.Contains("+"))
                    {
                        //TODO

                        continue;
                    }
                    else if (!ContainEnglishCharacter(content))
                    {
                        continue;
                    }

                    content = content.TrimEnd("; \t".ToCharArray());

                    if (!ValidateKeyExist(content)
                        && !missingKeys.Contains(content)
                        && !_allowedKeys.Contains(content.Trim().TrimStart("\"".ToCharArray()).TrimEnd("\"".ToCharArray())))
                    {
                        missingKeys.Add(content.Trim());
                    }
                }

                return missingKeys;
            }
        }

        public static void InitializeEnglishKeys(string emaDirectoryName)
        {
            string languageFoldPath = String.Format("{0}\\EMA.UI\\Configuration\\Languages\\English", emaDirectoryName).Replace("\\EMA.UI\\EMA.UI\\", "\\EMA.UI\\");

            _englishKeys = new List<List<string>>();

            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\DialogsStringsEnglish.xml"));
            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\EnumConfigSetStringsEnglish.xml"));
            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\InternalConfigSetStringsEnglish.xml"));
            _englishKeys.Add(TranslationManager.GetTranslationKeys(languageFoldPath + "\\MessageDialogStringsEnglish.xml"));
        }

        public static void InitializeAllowedKeys(string emaDirectoryName)
        {
            string allowedKeyFilePath = String.Format("{0}\\EMA.UI\\Configuration\\AllowdedStrings.xml", emaDirectoryName).Replace("\\EMA.UI\\EMA.UI\\", "\\EMA.UI\\");

            using (XmlReader reader = XmlReader.Create(allowedKeyFilePath))
            {
                _allowedKeys = new List<string>();

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name.Equals("LegalString", StringComparison.InvariantCulture)
                        && reader.AttributeCount == 1
                        && !String.IsNullOrEmpty(reader[0])
                        && !_allowedKeys.Contains(reader[0]))
                    {
                        _allowedKeys.Add(reader[0].Trim());
                    }
                }

                _allowedKeys.Sort();
            }
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

            key = key.TrimEnd("; \t".ToCharArray()).Replace("\"", String.Empty).Trim();

            foreach (List<string> englishKeyList in _englishKeys)
            {
                if (englishKeyList.Contains(key))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainEnglishCharacter(string content)
        {
            if (String.IsNullOrEmpty(content) || String.IsNullOrEmpty(content.Trim()))
            {
                return false;
            }

            content = content.ToUpper();

            foreach (char c in content)
            {
                if (_englishChars.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Delegates and Events
        #endregion
    }
}