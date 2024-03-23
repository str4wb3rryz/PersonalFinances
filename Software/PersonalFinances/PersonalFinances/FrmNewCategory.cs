using PersonalFinances.Models;
using PersonalFinances.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinances
{
    public partial class FrmNewCategory : Form
    {
        public FrmNewCategory()
        {
            InitializeComponent();
        }

        private void FrmNewCategory_Load(object sender, EventArgs e)
        {
            var expenseCategories = ExpenseCategoryRepository.GetExpenseCategories();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<ExpenseCategory> categories = new List<ExpenseCategory>();
                if (txtCategoryId.Text.Length == 2 && txtCategoryId.Text[0] == '5' && txtCategoryId.Text[1] != '4' && !string.IsNullOrWhiteSpace(txtCategoryName.Text) && !CheckID(txtCategoryId.Text))
                {
                    var user = FrmLogin.LoggedUser;
                    var Id = int.Parse(txtCategoryId.Text);
                    string Name = txtCategoryName.Text;
                    user.AddNewCategory(Id, Name);
                    MessageBox.Show("Nova kategorija je uspješno unesena", "PersonalFInances", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmExpenses frmExpenses = new FrmExpenses();
                    Hide();
                    frmExpenses.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Ponovite unos podataka", "PersonalFInances", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
        }
        private bool CheckID(string text)
        {
            int id;
            if (!int.TryParse(text, out id)) return false;
            ExpenseCategory expenseCategory = ExpenseCategoryRepository.GetExpenseCategory(id);
            return expenseCategory != null;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmExpenses frmExpenses = new FrmExpenses();
            Hide();
            frmExpenses.ShowDialog();
            Close();
        }


    }
}
