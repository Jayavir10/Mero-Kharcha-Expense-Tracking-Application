using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mero_Kharcha.Models
{
    public class Transaction
    {
        public int TransID { get; set; }

        public string TransTitle { get; set; }

        public string TransAmount { get; set; }

        public DateTime TransDate { get; set; }

        public string TransTag { get; set; }

        public string TransNote { get; set; }

        public string TransType { get; set; }
    }
}
