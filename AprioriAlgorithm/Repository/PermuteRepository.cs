using AprioriAlgorithm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriAlgorithm.Repository
{
    public class PermuteRepository
    {
        public static List<List<string>> FindAllPermutes(string[] products, double supportCount, int level)
        {
            List<List<string>> temp = new List<List<string>>();

            foreach (var permutation in PermuteService.Permute(products.ToList(), level))
            {
                List<string> temp2 = new List<string>();
                foreach (string i in permutation.Distinct())
                {
                    temp2.Add(i);
                }
                temp.Add(temp2);
            }

            return temp;
        }
    }
}
