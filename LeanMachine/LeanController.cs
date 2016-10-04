using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanMachine
{
    public class LeanController
    {
        public readonly List<Block> blocks;
        public int HopNumber { get; set; }
        public int currentThroughput;

        public LeanController()
        {
            blocks = new List<Block>();
        }

        public void AddBlock(int capacity)
        {
            blocks.Add(new Block(capacity, new Queue(0)));
        }
        
        public void Reset()
        {
            blocks.Clear();
        }

        public void UpdateThroughput()
        {
            if (HopNumber < blocks.Count)
            {
                currentThroughput += 0;
                return;
            }

            currentThroughput += blocks.Select(x => x.Capacity).Min();
        }

        public int CalculateTotalCycleTime()
        {
            return blocks.Select(x => x.GetCycleTime()).Sum();
        }      

        public void PrintSystem()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~");

            Console.WriteLine("Current cycle: " + HopNumber);
            Console.WriteLine("Print System:");
            for (int i = 0; i < blocks.Count; i++)
            {
                Console.WriteLine("Block Index: " + (i+1) + "\nBlock Capacity :" + blocks[i].Capacity);
                Console.WriteLine("Queue size: " + blocks[i].Queue.Value);
                Console.WriteLine("Block cycle time: " + blocks[i].GetCycleTime());
            }
            Console.WriteLine("Total throughput: " + currentThroughput);
            //Console.WriteLine("Total cycle time: " + CalculateTotalCycleTime());
            Console.WriteLine();
        }

        public void Update()
        {
            HopNumber++;
            UpdateThroughput();
            if (HopNumber < blocks.Count)
            {
                int index = HopNumber%blocks.Count;
                var bottleNeck = blocks.Any() ? blocks.GetRange(0, index).Select(x => x.Capacity).Min() : 0;
                blocks[index].Queue.InitilizeValue((bottleNeck - blocks[index].Capacity) < 0 ? 0 : (bottleNeck - blocks[index].Capacity));
                for (int i = 0; i < index; i++)
                {
                    blocks[i].Queue.Update();
                }

                return;
            }
            
            foreach (var block in blocks)
            {
                block.Queue.Update();
            }
        }
    }
}