using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mero_Kharcha.Models
{
    public class Debt
    {
        public int DebtID { get; set; }
        public string DebtTitle { get; set; }
        public string DebtAmount { get; set; }
        public DateTime DebtDueDate { get; set; }
        public string DebtSource { get; set; }
        public string DebtNotes { get; set; }
        public string DebtStatus { get; set; }
    }
}
