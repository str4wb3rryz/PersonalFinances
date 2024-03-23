using DBLayer;
using PersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonalFinances.Repositories
{
    public class DataManager
    {
        public static void SaveExpenseFromFile(string expenseCategory, string user, string amount, string comment)
         {
            string sql = $"INSERT INTO Expenses(ID_Expense, ID_User, Amount, Comment) VALUES ((SELECT ID_ExpenseCategory FROM ExpenseCategories WHERE ID_ExpenseCategory = {expenseCategory}), {user}, {amount}, '{comment}' )";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
         }

    }
}
