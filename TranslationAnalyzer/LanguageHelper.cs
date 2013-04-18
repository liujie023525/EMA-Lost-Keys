#region References
#endregion

using System;
namespace Polycom.RMX2000.TranslationManager.TranslationAnalyzer 
{
    public enum LanguageNames
    {
        English = 0,
        ChineseSimplified,
        ChineseTraditional,
        French,
        German,
        Italian,
        Japanese,
        Korean,
        Norwegian,
        PortugueseBrazilian,
        Russian,
        SpanishSouthAmerica,
        Turkish
    }

    public enum AnalyzeModes
    {
        TranslationFile = 0,
        UIProject
    }

    public static class LanguageHelper
    {
        #region Fields and Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        public static string GetLanguageDisplayName(string languageName)
        {
            LanguageNames language = LanguageNames.English;

            if (Enum.TryParse<LanguageNames>(languageName, out language))
            {
                return GetLanguageDisplayName(language);
            }
            else
            {
                return languageName;
            }
        }

        public static string GetLanguageDisplayName(LanguageNames languageName)
        {
            switch (languageName)
            {
                case LanguageNames.ChineseSimplified:
                    return "Simplified Chinese";
                case LanguageNames.ChineseTraditional:
                    return "Traditional Chinese";
                case LanguageNames.PortugueseBrazilian:
                    return "Brazilian Portuguese";
                case LanguageNames.SpanishSouthAmerica:
                    return "South America Spanish";
                default:
                    return languageName.ToString();
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Delegates and Events
        #endregion
    }
}