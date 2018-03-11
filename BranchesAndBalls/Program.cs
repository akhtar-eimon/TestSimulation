using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchesAndBalls
{
    class Program
    {
        static void Main(string[] args)
        {
            // res is just to store the results when run multiple times  
            Dictionary<string, int> res = new Dictionary<string, int>();
            int TOTAL_RUN = 1000; // if multiple run is intended
            Console.WriteLine("Enter depth:");
            int depth = int.Parse(Console.ReadLine());
            for (int i = 0; i < TOTAL_RUN; i++)
            {
                Branch myRootBranch = new Branch();
                myRootBranch.Initialise(depth);
                string predictedContainer = myRootBranch.GetPredictedContainer();
                string actualContainer  =   myRootBranch.Run();
                if (predictedContainer.CompareTo(actualContainer) == 0)
                    Console.WriteLine("Prediction matched: Predicted->{0} Actual->{1}", predictedContainer, actualContainer);
                else
                    Console.WriteLine("Prediction didn't match: Predicted->{0} Actual->{1}", predictedContainer, actualContainer);
                if (res.ContainsKey(actualContainer))
                    res[actualContainer]++;
                else
                    res[actualContainer] = 1;
            }
            return ;
        }
    }
}
