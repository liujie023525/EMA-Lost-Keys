#region References
using System;
using System.Windows.Forms;
using DevExpress.Skins;
#endregion

namespace Polycom.RMX2000.EMALostKeys
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SkinManager.EnableFormSkins();
            SkinManager.EnableFormSkinsIfNotVista();
            SkinManager.EnableMdiFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainXtraForm());
        }
    }
}
