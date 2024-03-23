using PersonalFinances.Models;
using PersonalFinances.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinances
{
    public partial class FrmLogin : Form
    {
        public static User LoggedUser { get; set; }
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Niste unijeli sve podatke! Pokušajte ponovno", "PersonalFinances", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                LoggedUser = UserRepository.GetUser(txtUsername.Text);
                if (LoggedUser != null && LoggedUser.CheckPassword(txtPassword.Text))
                {
                    MessageBox.Show("Uspješno ste se prijavili! Dobrodošli u PersonalFinaces", "PersonalFinaces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmExpenses frmExpenses = new FrmExpenses();
                    Hide();
                    frmExpenses.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Unijeli ste pogrešne podatke! Pokušajte ponovno", "PersonalFinances", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
