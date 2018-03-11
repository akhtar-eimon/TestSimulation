using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchesAndBalls
{
    public enum OpenGate{
        NotInitialized=0,
        Left ,
        Right
    }
    
    public class Branch
    {
        private static Random rnd = new Random();
        private static HashSet<string> Containers = new HashSet<string>();
        private static int Depth;
        private OpenGate openGate = OpenGate.NotInitialized;
        private string gateName = "";
        private Branch Left = null;
        private Branch Right = null;
        private List<int> Balls = null;
        private int CurrentDepth = 0;
        private Dictionary<string, int> ContainersWithBall = null;
        // returns a random container name as a prediction
        public string GetPredictedContainer()
        {
            string strPrdt = "";
            Random rndForPrd = new Random();
            for (int d = 0; d < Depth; d++)
                strPrdt += rndForPrd.Next(1, 3) == (int)OpenGate.Left ? "L" : "R";
            return strPrdt;
        }        

        public Branch()
        {
            Balls = new List<int>();            
        }
        public void Initialise(int depth)
        {
            Depth = depth;
            for (int i = 0; i < Math.Pow(2, Depth) - 1; i++)
                Balls.Add(i + 1);

            this.CurrentDepth = 0;
            this.openGate= (OpenGate)rnd.Next(1, 3);
            Queue<Branch> branches = new Queue<Branch>();
            branches.Enqueue(this);
            while(branches.Count>0 && branches.Peek().CurrentDepth<depth)
            {
                Branch parent = branches.Dequeue();
                parent.Left = this.CreateBranch(parent.CurrentDepth+1,parent.gateName+"L");
                parent.Right = this.CreateBranch(parent.CurrentDepth + 1, parent.gateName + "R");
                branches.Enqueue(parent.Left);
                branches.Enqueue(parent.Right);
            }
            //leaf nodes are considered as containers
            while (branches.Count > 0)
                Containers.Add(branches.Dequeue().gateName);
            return ;
        }

        

        private Branch CreateBranch(int currentDepth,string gateName)
        {
            Branch temp = new Branch();
            temp.CurrentDepth = currentDepth;
            temp.openGate = (OpenGate)rnd.Next(1,3);
            temp.gateName = gateName;
            return temp;        
        }
        
        public string Run()
        {         
            
            Queue<Branch> toBeProcessed = new Queue<Branch>();
            this.CurrentDepth = 0;
            toBeProcessed.Enqueue(this);            
            //Process balls
            while (toBeProcessed.Count > 0 && toBeProcessed.Peek().CurrentDepth<Depth)
            {
                Branch parent = toBeProcessed.Dequeue();                
                ProcessBalls(parent);                
                if (parent.Left.Balls.Count > 0)
                    toBeProcessed.Enqueue(parent.Left);
                if (parent.Right.Balls.Count > 0)
                    toBeProcessed.Enqueue(parent.Right);
            }
            //Store balls in the container
            ContainersWithBall = new Dictionary<string, int>();
            while (toBeProcessed.Count > 0)
            {
                Branch temp = toBeProcessed.Dequeue();
                ContainersWithBall[temp.gateName] = temp.Balls[0];
                Containers.Remove(temp.gateName);
            }
            // return the container name without any ball
            return Containers.SingleOrDefault();
        }

        private void ProcessBalls(Branch parent)
        {                
            foreach( int i in parent.Balls)
            {
                if (parent.openGate == OpenGate.Left)
                    parent.Left.Balls.Add(i);
                else
                    parent.Right.Balls.Add(i);
                parent.openGate = (parent.openGate == OpenGate.Left) ? OpenGate.Right : OpenGate.Left;
            }
                    
        }

       
    }
}
