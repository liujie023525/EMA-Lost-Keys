#region References
#endregion

namespace Polycom.RMX2000.EMALostKeys
{
    internal enum LanguageNames
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

    internal static class LanguageHelper
    {
        #region Fields and Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        internal static string GetLanguageDisplayName(LanguageNames languageName)
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