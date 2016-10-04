using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanMachine
{
    public class Queue
    {
        private int value;

        public Queue(int value)
        {
            Value = value;
        }
        public int Value { get; set; }

        public void Update()
        {
            Value += value;
        }

        public void InitilizeValue(int v)
        {
            value = v;
        }
    }
}
