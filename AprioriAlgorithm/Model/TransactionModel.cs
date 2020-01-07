using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriAlgorithm.Model
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public string[] Products { get; set; }
        public string TransactionDate { get; set; }
    }
}
