using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Democracy3TextExtractor
{
    public partial class FormModsList : Form
    {
        public ModDetails ModSelected
        {
            get
            {
                return (ModDetails)this.comboBoxMods.SelectedItem;
            }
        }
        public FormModsList(List<ModDetails> modsList)
        {
            InitializeComponent();
            this.comboBoxMods.DataSource = modsList;
            this.comboBoxMods.DisplayMember = "DisplayName";
            this.comboBoxMods.ValueMember = "Name";
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
