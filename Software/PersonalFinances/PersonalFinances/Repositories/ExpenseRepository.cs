using DBLayer;
using PersonalFinances.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonalFinances.Repositories
{
    public class ExpenseRepository
    {
        public static Expense GetExpense(int id)
        {
            Expense expense = null; 
            string sql = $"SELECT * FROM Expenses WHERE ID = {id}";
            DB.OpenConnection(); 
            var reader = DB.GetDataReader(sql); 
            if (reader.HasRows) { 
                reader.Read(); 
                expense = CreateObject(reader); 
                reader.Close(); 
            }
            DB.CloseConnection(); 
            return expense;
        }

        public static List<Expense> GetExpenses()
        {
            List<Expense> expenses = new List<Expense>();
            DB.OpenConnection();
            var reader = DB.GetDataReader($"SELECT * FROM dbo.Expenses");
            while (reader.Read())
                {
                    Expense expense = CreateObject(reader);
                    expenses.Add(expense);
                }
            reader.Close();
            DB.CloseConnection();
            return expenses;
        }
        private static Expense CreateObject(SqlDataReader reader)
        {
            int id = int.Parse(reader["ID"].ToString());

            int idExpenses = int.Parse(reader["ID_Expense"].ToString());
            var expenseId = ExpenseCategoryRepository.GetExpenseCategory(idExpenses);

            int idUsers = int.Parse(reader["ID_User"].ToString());
            var user = UserRepository.GetUser(idUsers);

            float amount = float.Parse(reader["Amount"].ToString());
            string comment = reader["Comment"].ToString();

            var expense = new Expense
            {
                ID = id,
                ID_Expense = expenseId,
                ID_User = user,
                Amount = amount,
                Comment = comment
            };

            return expense;
        }
        public static void SaveExpense(User user, ExpenseCategory expenseCategory, string amount, string comment)
        {
            DB.OpenConnection();
            string sql = $"INSERT INTO Expenses (ID_Expense, ID_User, Amount, Comment) VALUES " +
                $"({expenseCategory.ID_ExpenseCategory}, {user.Id}, {amount}, '{comment}')";
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        public static void ChangeExpense(int id, ExpenseCategory expenseCategory, string amount, string comment)
        {
            DB.OpenConnection();
            string sql = $"UPDATE Expenses SET ID_Expense = {expenseCategory.ID_ExpenseCategory}, Amount = {amount}, Comment = '{comment}' WHERE ID = {id};";
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        public static List<Expense> SearchExpenses(int search)
        {
            var expenses = new List<Expense>();
            string sql = $"SELECT * FROM Expenses WHERE ID_Expense = {search}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);

            while (reader.Read())
            {
                Expense expense = CreateObject(reader);
                expenses.Add(expense);
            }
            reader.Close();
            DB.CloseConnection();

            return expenses;
        }


    }
}
