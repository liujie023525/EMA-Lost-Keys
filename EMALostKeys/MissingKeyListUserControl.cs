#region References
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
#endregion

namespace Polycom.RMX2000.TranslationManager.UI
{
    public partial class MissingKeyListUserControl : UserControl
    {
        #region Fields and Properties
        #endregion

        #region Constructors
        private MissingKeyListUserControl()
        {
            InitializeComponent();
        }

        public MissingKeyListUserControl(List<string> missingKeys)
            : this()
        {
            this.listBoxControl.Items.Clear();

            if (missingKeys == null || missingKeys.Count == 0)
            {
                return;
            }

            missingKeys.Sort();

            this.listBoxControl.Items.AddRange(missingKeys.ToArray());
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
        #endregion
    }
}
