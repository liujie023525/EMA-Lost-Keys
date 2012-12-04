#region References
using System;
using DevExpress.XtraSplashScreen;
using System.Windows.Forms;
#endregion

namespace Polycom.RMX2000.EMALostKeys
{
    public partial class InformationScreen : SplashScreen
    {
        #region Fields and Properties
        #endregion

        #region Constructors
        public InformationScreen()
        {
            InitializeComponent();

            this.versionLabel.Text = String.Format("V{0}", ProductVersion.TrimEnd(".0".ToCharArray()));

            Timer formCloseTimer = new Timer() { Interval = 5000 };
            formCloseTimer.Tick += new EventHandler(formCloseTimer_Tick);

            formCloseTimer.Start();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Internal Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Private Methods
        #endregion

        #region Delegates and Events
        private void formCloseTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}