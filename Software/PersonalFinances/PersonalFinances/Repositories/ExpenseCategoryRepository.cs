using DBLayer;
using PersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.Repositories
{
    public class ExpenseCategoryRepository
    {
        public static ExpenseCategory GetExpenseCategory(int id)
        {
            ExpenseCategory expenseCategory = null;
            string sql = $"SELECT * FROM ExpenseCategories WHERE ID_ExpenseCategory = {id}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                expenseCategory = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return expenseCategory;
        }
        public static List<ExpenseCategory> GetExpenseCategories()
        {
            List<ExpenseCategory> expenseCategories = new List<ExpenseCategory>();
            string sql = "SELECT * FROM ExpenseCategories";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            while (reader.Read())
            {
                ExpenseCategory expenseCategory = CreateObject(reader);
                expenseCategories.Add(expenseCategory);
            }
            reader.Close();
            DB.CloseConnection();
            return expenseCategories;
        }
        private static ExpenseCategory CreateObject(SqlDataReader reader)
        {
            int idExpenseCategory = int.Parse(reader["ID_ExpenseCategory"].ToString());
            string expenseType = reader["ExpenseType"].ToString();

            var expenseCategory = new ExpenseCategory
            {
                ID_ExpenseCategory = idExpenseCategory,
                ExpenseType = expenseType,
            };
            return expenseCategory;
        }

        public static void NewCategory(int id, string name)
        {
            DB.OpenConnection();
            string sql = $"INSERT INTO ExpenseCategories (ID_ExpenseCategory, ExpenseType) VALUES " +
                $"({id}, '{name}')";
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }
    }
}
