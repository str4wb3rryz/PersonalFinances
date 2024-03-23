using DBLayer;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PersonalFinances
{
    public partial class FrmExpenses : Form
    {
        public FrmExpenses()
        {
            InitializeComponent();
        }

        private void FrmExpenses_Load(object sender, EventArgs e)
        {
            ShowExpenses();
            lblName.Text = FrmLogin.LoggedUser.FirstName;
        }

        private void ShowExpenses() {
            List<Expense> expenses = ExpenseRepository.GetExpenses();
            dgvExpenses.DataSource = expenses;

            dgvExpenses.Columns["ID"].Visible = false;
            dgvExpenses.Columns["ID_Expense"].DisplayIndex = 0;
            dgvExpenses.Columns["ID_User"].Visible = false;
            dgvExpenses.Columns["Amount"].DisplayIndex = 1;
            dgvExpenses.Columns["Comment"].DisplayIndex = 2;
            dgvExpenses.Columns["ID_Expense"].HeaderText = "Vrsta troška";
            dgvExpenses.Columns["Amount"].HeaderText = "Iznos";
            dgvExpenses.Columns["Comment"].HeaderText = "Komentar";
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            FrmNewExpense frmNewExpense = new FrmNewExpense();
            Hide();
            frmNewExpense.ShowDialog();
            Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            FrmLoadFromFile frmLoadFromFile = new FrmLoadFromFile();
            Hide();
            frmLoadFromFile.ShowDialog();
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                Expense selectedExpense = dgvExpenses.CurrentRow.DataBoundItem as Expense;
                string sql = $"DELETE FROM Expenses WHERE Id=" + selectedExpense.ID;

                DB.OpenConnection();
                DB.ExecuteCommand(sql);
                DB.CloseConnection();

                MessageBox.Show("Trošak je izbrisan", "PersonalFinances", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmExpenses frmExpenses = new FrmExpenses();
                Hide();
                frmExpenses.ShowDialog();
                Close();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            FrmNewCategory frmNewCategory = new FrmNewCategory();
            Hide();
            frmNewCategory.ShowDialog();
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowExpenses();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvExpenses.Refresh();
            int id = int.Parse(txtSearch.Text);
            ShowExpensesSearch(id);
        }

        private void ShowExpensesSearch(int id)
        {
            var expenses = ExpenseRepository.SearchExpenses(id);
            dgvExpenses.DataSource = expenses;

            dgvExpenses.Columns["ID"].Visible = false;
            dgvExpenses.Columns["ID_Expense"].DisplayIndex = 0;
            dgvExpenses.Columns["ID_User"].Visible = false;
            dgvExpenses.Columns["Amount"].DisplayIndex = 1;
            dgvExpenses.Columns["Comment"].DisplayIndex = 2;
            dgvExpenses.Columns["ID_Expense"].HeaderText = "Vrsta troška";
            dgvExpenses.Columns["Amount"].HeaderText = "Iznos";
            dgvExpenses.Columns["Comment"].HeaderText = "Komentar";
        }

        //funkcije koje jos nisu u potpunosti napravljene

        private void btnChange_Click(object sender, EventArgs e)
        {
            /*Expense selectedExpense = dgvExpenses.CurrentRow.DataBoundItem as Expense;
            FrmModifyExpense frmModifyExpense = new FrmModifyExpense(selectedExpense);
            Hide();
            frmModifyExpense.ShowDialog();
            Close();*/
            MessageBox.Show("Funkcija jos nije implementirana", "Work In Progress", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void btnExpensesReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funkcija jos nije implementirana", "Work In Progress", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        
    }
}
