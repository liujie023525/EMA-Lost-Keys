#region References
using System;
using Microsoft.Win32;
#endregion

namespace Polycom.RMX2000.EMALostKeys
{
    internal static class RegistryHelper
    {
        #region Fields and Properties
        private static string _productKey = "EMALostKeys";
        private static string _themeKey = "Theme";

        public const string DEFAULT_THEME_NAME = "DevExpress Dark Style";
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        internal static string GetTheme()
        {
            string themeName = DEFAULT_THEME_NAME;
            RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("SOFTWARE");

            if (softwareKey.OpenSubKey(_productKey) == null)
            {
                SetTheme(DEFAULT_THEME_NAME);
            }
            else
            {
                return softwareKey.OpenSubKey(_productKey).GetValue(_themeKey).ToString();
            }

            return themeName;
        }

        internal static void SetTheme(string themeName)
        {
            try
            {
                if (String.IsNullOrEmpty(themeName)
                    || (!themeName.Equals(DEFAULT_THEME_NAME)
                        && !themeName.Equals("Metropolis")
                        && !themeName.Equals("Office 2010 Blue")))
                {
                    themeName = DEFAULT_THEME_NAME;
                }

                RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);

                if (softwareKey.OpenSubKey(_productKey) == null)
                {
                    softwareKey.CreateSubKey(_productKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }

                softwareKey.OpenSubKey(_productKey, true).SetValue(_themeKey, themeName);

                softwareKey.Flush();
            }
            catch
            {
                throw;
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
