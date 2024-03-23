using DBLayer;
using PersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PersonalFinances
{
    public partial class FrmLoadFromFile : Form
    {
        public FrmLoadFromFile()
        {
            InitializeComponent();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog upload = new OpenFileDialog();
            upload.Filter = "CSV Files (*.csv)|*.csv";
            upload.ShowDialog();
            if(upload.FileName != "")
            {
                if(upload.FileName.EndsWith(".csv"))
                {
                    DataTable expenses = new DataTable();
                    expenses = GetDataFromCSVFile(upload.FileName);
                    txtFile.Text = upload.FileName;
                    if(expenses.Rows != null && expenses.Rows.ToString() != String.Empty)
                    {
                        dgvData.DataSource = expenses;
                    }
                }
            }
        }

        private DataTable GetDataFromCSVFile(string csvFilePath)
        {
            DataTable csvData = new DataTable();
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                string line = reader.ReadLine();
                string[] headers = line.Split(',');

                foreach (string header in headers)
                {
                    csvData.Columns.Add(header);
                }

                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    csvData.Rows.Add(fields);
                }
            }
            return csvData;
        }

       private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dgvNewData = (DataTable)(dgvData.DataSource);
                var user = FrmLogin.LoggedUser;

                foreach (DataRow dr in dgvNewData.Rows)
                {
                    string expense = Convert.ToString(dr["ID_Expense"]);
                    string userID = Convert.ToString(dr["ID_User"]);
                    string amount = Convert.ToString(dr["Amount"]);
                    string comment = Convert.ToString(dr["Comment"]);

                    user.AddNewExpenseFromFile(expense, userID, amount, comment);
                }
            } catch
            {
                MessageBox.Show("Novi trošak je uspješno unesen", "PersonalFInances", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FrmExpenses frmExpenses = new FrmExpenses();
            Hide();
            frmExpenses.ShowDialog();
            Close();

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
