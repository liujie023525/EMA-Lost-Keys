#region References
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
#endregion

namespace Polycom.RMX2000.TranslationManager.TranslationAnalyzer
{
    public static class TranslateManager
    {
        #region Fields and Properties
        public static readonly string[] TranslationNodeKeyLevels = new string[] { "StringConfiguration", "Translations", "Language", "String" };
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        public static bool ValidateTranslationFile(string filePath)
        {
            try
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    throw new TranslationException("Please select a translation file firstly.");
                }
                else if (!File.Exists(filePath))
                {
                    throw new TranslationException(String.Format("{0} doesn't exist, please select one firstly.", filePath));
                }

                FileInfo file = new FileInfo(filePath);

                if (!file.Extension.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new TranslationException("Translation file must be a XML file.");
                }

                bool isValidTranslationFile = false;

                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    for (int nodeIndex = 0; nodeIndex < TranslationNodeKeyLevels.Length; nodeIndex++)
                    {
                        if (!reader.Read())
                        {
                            isValidTranslationFile = false;

                            break;
                        }

                        if (reader.NodeType != XmlNodeType.Element)
                        {
                            --nodeIndex;

                            continue;
                        }

                        if (!reader.Name.Equals(TranslationNodeKeyLevels[nodeIndex], StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValidTranslationFile = false;

                            break;
                        }

                        isValidTranslationFile = true;
                    }
                }

                if (!isValidTranslationFile)
                {
                    throw new TranslationException("The selected file is not a valid translation file of EMA.");
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public static List<string> GetMissingKeys(string englishFilePath, LanguageNames languageName)
        {
            if (languageName == LanguageNames.English)
            {
                return null;
            }
            if (!ValidateTranslationFile(englishFilePath))
            {
                return null;
            }

            string validatingFilePath = englishFilePath.Replace("English", languageName.ToString());

            if (!ValidateTranslationFile(validatingFilePath))
            {
                return null;
            }

            List<string> originalKeys = GetTranslationKeys(englishFilePath);
            List<string> validatingKeys = GetTranslationKeys(validatingFilePath);
            List<string> missingKeys = new List<string>();

            if (validatingKeys == null || validatingKeys.Count == 0)
            {
                return originalKeys;
            }
            else
            {
                foreach (string key in originalKeys)
                {
                    if (!validatingKeys.Contains(key))
                    {
                        missingKeys.Add(key);
                    }
                }
            }

            return missingKeys;
        }

        public static List<string> GetTranslationKeys(string filePath)
        {
            if (!ValidateTranslationFile(filePath))
            {
                return null;
            }

            using (XmlReader reader = XmlReader.Create(filePath))
            {
                List<string> keys = new List<string>();

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name.Equals("String", StringComparison.InvariantCulture)
                        && reader.AttributeCount == 2
                        && !String.IsNullOrEmpty(reader[0]))
                    {
                        keys.Add(reader[0]);
                    }
                }

                return keys;
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods

        public static void Export(Dictionary<string, List<string>> missingKeyDictionary, string filePath)
        {
            try
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    throw new IOException("File name could not be empty.");
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (File.Create(filePath))
                {
                }

                foreach (KeyValuePair<string, List<string>> missingKeys in missingKeyDictionary)
                {
                    if (missingKeys.Key.Equals(LanguageNames.English.ToString())
                        || missingKeys.Value == null
                        || missingKeys.Value.Count == 0)
                    {
                        continue;
                    }

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(String.Format("======== {0} ========", LanguageHelper.GetLanguageDisplayName(missingKeys.Key)));

                        foreach (string missingKey in missingKeys.Value)
                        {
                            writer.WriteLine(missingKey);
                        }

                        writer.WriteLine(String.Format("======== Totally {0} missing keys ========", missingKeys.Value.Count));
                        writer.WriteLine();
                        writer.WriteLine();

                        writer.Flush();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Delegates and Events
        #endregion
    }
}