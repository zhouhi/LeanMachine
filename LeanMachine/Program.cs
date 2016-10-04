using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var leanController = new LeanController();
            
            Console.WriteLine("Initialize the system: ");
            while (true)
            {
                Console.WriteLine("Enter the capacity: ");
                Console.WriteLine("Press 'q' to exit");
                var input = Console.ReadLine();
                if (input == "q") break;
                var capacity = Int16.Parse(input);
                leanController.AddBlock(capacity);          
                
                
            }

            leanController.PrintSystem();

            Console.WriteLine("Press Enter to go to next hop: ");
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "")
                {
                    leanController.Update();
                    leanController.PrintSystem();
                }
            }

        }


    }
}
