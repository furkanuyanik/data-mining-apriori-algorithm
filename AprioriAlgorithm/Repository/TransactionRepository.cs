using AprioriAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriAlgorithm.Repository
{
    public class TransactionRepository
    {
        public static string[] FindAllByProductNameDistinct(List<TransactionModel> transactions)
        {
            List<string> temp = new List<string>();

            foreach (TransactionModel transaction in transactions)
            {
                foreach (string product in transaction.Products)
                {
                    if (product != null && product.Trim() != "")
                    {
                        if (!temp.Any(x => x == product))
                        {
                            temp.Add(product);
                        }
                    }
                }
            }

            return temp.ToArray();
        }
    }
}
