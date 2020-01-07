using AprioriAlgorithm.Model;
using AprioriAlgorithm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprioriAlgorithm.Service
{
    public class TransactionService
    {
        public static List<TransactionModel> DataCollect(DataGridViewRowCollection rows)
        {
            List<TransactionModel> temp = new List<TransactionModel>();

            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells[0].Value != null)
                {
                    TransactionModel model = new TransactionModel();
                    model.TransactionId = Convert.ToInt32(row.Cells["TransactionId"].Value);
                    model.Products = row.Cells["Products"].Value.ToString().Split(new[] { ", " }, StringSplitOptions.None);
                    model.TransactionDate = row.Cells["TransactionDate"].ToString();
                    temp.Add(model);
                }
            }

            return temp;
        }

        public static List<ProductList> GetProductList(List<TransactionModel> transactions, string[] products, double supportThresholdCount, int i)
        {
            List<ProductList> productsPermuted = new List<ProductList>();
            List<List<string>> permutes = PermuteRepository.FindAllPermutes(products, supportThresholdCount, i);

            foreach (List<string> productList in permutes)
            {
                int productCount = 0;

                foreach (string[] transactionList in transactions.Select(x => x.Products))
                {
                    int counter = 0;

                    foreach (string product in productList)
                    {
                        if (transactionList.ToList().Any(x => x == product))
                        {
                            counter++;
                        }
                    }

                    if (productList.Count == counter)
                    {
                        productCount++;
                    }
                }

                if (supportThresholdCount <= productCount)
                {
                    productsPermuted.Add(new ProductList(productList, productCount));
                }
            }

            return productsPermuted;
        }
    }
}
