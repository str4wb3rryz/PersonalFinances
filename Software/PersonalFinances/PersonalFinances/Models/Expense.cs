using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public ExpenseCategory ID_Expense { get; set; }
        public User ID_User { get; set; }    
        public float Amount { get; set; }
        public string Comment { get; set; }

    }
}
