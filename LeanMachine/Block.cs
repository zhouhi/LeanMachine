using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanMachine
{
    public class Block
    {
        public Block(int capacity, Queue queue)
        {
            Queue = queue;
            Capacity = capacity;
        }

        public Queue Queue { get; set; }

        public int Capacity { get; set; }

        public int GetCycleTime()
        {
            return Queue.Value/Capacity + 1;
        }

    }
}
