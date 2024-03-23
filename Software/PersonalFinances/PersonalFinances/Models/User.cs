using PersonalFinances.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PersonalFinances.Models
{
    public class User : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool CheckPassword(string password)
        {
            return Password == password;
        }
        public void AddNewExpense(ExpenseCategory expense, string amount, string comment)
        {
                ExpenseRepository.SaveExpense(this, expense, amount, comment); 
        }
        public void AddNewExpenseFromFile(string expense, string user, string amount, string comment)
        {
                DataManager.SaveExpenseFromFile(expense, user, amount, comment);
        }
        public void AddNewCategory(int id, string name)
        {
            ExpenseCategoryRepository.NewCategory(id, name);
        }

        public void UpdateExpense(int id, ExpenseCategory expense, string amount, string comment)
        {
            ExpenseRepository.ChangeExpense(id, expense, amount, comment);
        }
    }
}
