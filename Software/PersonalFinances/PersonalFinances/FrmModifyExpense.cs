using PersonalFinances.Models;
using PersonalFinances.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalFinances
{
    public partial class FrmModifyExpense : Form
    {
        private Expense expense;
        public Expense SelectedExpense { get => expense; set => expense = value; }
        public FrmModifyExpense(Expense selectedExpense)
        {
            InitializeComponent();
            SelectedExpense = selectedExpense;
        }

        private void FrmModifyExpense_Load(object sender, EventArgs e)
        {
            var expenseCategories = ExpenseCategoryRepository.GetExpenseCategories();
            cboExpenseCategories.DataSource = expenseCategories;
        }

        private void cboExpenseCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentExpenseCategory = cboExpenseCategories.SelectedItem as ExpenseCategory;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmExpenses frmExpenses = new FrmExpenses();
            Hide();
            frmExpenses.ShowDialog();
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //var expense = cboExpenseCategories.SelectedItem as ExpenseCategory;
            //var expense = cboExpenseCategories.GetItemText(cboExpenseCategories.SelectedItem);
            //var expense = (ExpenseCategory)cboExpenseCategories.SelectedItem;
            var expense = cboExpenseCategories.SelectedItem as ExpenseCategory;
            var user = FrmLogin.LoggedUser;
            string comment = txtComment.Text;
            string amountInput = txtAmount.Text;
            var id = SelectedExpense.ID;

            if (float.TryParse(amountInput, out float amount))
            {
                string amountString = Convert.ToString(amount);
                user.UpdateExpense(id, expense, amountString, comment);
                MessageBox.Show("Novi trošak je uspješno unesen", "PersonalFInances", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmExpenses frmExpenses = new FrmExpenses();
                Hide();
                frmExpenses.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Unesite točne podatke", "PersonalFInances", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
