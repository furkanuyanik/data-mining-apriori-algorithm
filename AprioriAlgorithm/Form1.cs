using AprioriAlgorithm.Model;
using AprioriAlgorithm.Repository;
using AprioriAlgorithm.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprioriAlgorithm
{
    public partial class Form1 : Form
    {
        List<string> output = new List<string>();
        List<TransactionModel> transactions;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(1, "Sugar, Tea, Bread", "07.01.2020");
            dataGridView1.Rows.Add(2, "Bread, Cheese, Olive, Pasta", "08.01.2020");
            dataGridView1.Rows.Add(3, "Sugar, Cheese, Detergent, Bread, Pasta", "08.01.2020");
            dataGridView1.Rows.Add(4, "Bread, Cheese, Tea, Pasta", "09.01.2020");
            dataGridView1.Rows.Add(5, "Cheese, Pasta, Sugar, Beer", "13.01.2020");
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            transactions = TransactionService.DataCollect(dataGridView1.Rows);

            double SupportThresholdValue = Convert.ToDouble(textSupportValue.Text) / 100;
            double SupportThresholdCount = SupportThresholdValue * transactions.Count;

            string[] products = TransactionRepository.FindAllByProductNameDistinct(transactions);

            int level = 0;
            for (int i = 0; ; i++)
            {
                level = i;

                var temp = TransactionService.GetProductList(transactions, products, SupportThresholdCount, i + 1);
                bool isPass = temp.Count == 0 ? false : true;
                if (!isPass) break;
            }

            if (level != 0)
            {
                List<ProductList> productList = TransactionService.GetProductList(transactions, products, SupportThresholdCount, level);
                foreach (ProductList productsForAnalyses in productList)
                {
                    List<ProductList> anaylses = new List<ProductList>();
                    for (int i = 1; i < level; i++)
                        anaylses.AddRange(TransactionService.GetProductList(transactions, productsForAnalyses.Products.ToArray(), 0, i));

                    for (int i = 0; i < anaylses.Count; i++)
                    {
                        string product = "", totalProduct = "";
                        for (int j = 0; j < anaylses[i].Products.Count; j++)
                        {
                            product += anaylses[i].Products[j] + ", ";
                        }
                        for (int j = 0; j < productList[0].Products.Count; j++)
                        {
                            totalProduct += productList[0].Products[j] + ", ";
                        }

                        product = product.Substring(0, product.Length - 2);
                        totalProduct = totalProduct.Substring(0, totalProduct.Length - 2);

                        double ConfidenceRate = Convert.ToDouble(productList[0].Count) / Convert.ToDouble(anaylses[i].Count) * 100;
                        output.Add(ConfidenceRate + " percent of people who want to buy " + product + " wants to buy " + totalProduct);
                    }

                    foreach (var item in output)
                    {
                        dataGridView2.Rows.Add(item);
                    }
                }
            }
            else
            {
                MessageBox.Show("Not found Confidence value!");
            }
        }
    }
}
