using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonalFinances.Models
{
    public class ExpenseCategory
    {
        public int ID_ExpenseCategory { get; set; }
        public string ExpenseType { get; set; }
        public override string ToString()
        {
            return ExpenseType;
        }
    }
}
