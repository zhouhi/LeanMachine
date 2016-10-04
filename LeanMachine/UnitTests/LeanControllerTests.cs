using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeanMachine.UnitTests
{
    [TestClass]
    public class LeanControllerTests
    {
        private LeanController leanController;

        [TestInitialize]
        public void Initialize()
        {
            leanController = new LeanController();
        }

        [TestMethod]
        public void TestAddBlock()
        {
            leanController.AddBlock(80);
            Assert.AreEqual(80, leanController.blocks[0].Capacity);
            Assert.AreEqual(0, leanController.blocks[0].Queue.Value);
        }

        [TestMethod]
        public void TestAddBlockInput()
        {
            leanController.AddBlock((int)80.8);
            Assert.AreEqual(80, leanController.blocks[0].Capacity);
            Assert.AreEqual(0, leanController.blocks[0].Queue.Value);
        }

        [TestMethod]
        public void TestUpdateThroughputWhenHopsLessThanBlocks()
        {
            leanController.AddBlock(80);
            leanController.AddBlock(60);
            leanController.AddBlock(70);
            leanController.AddBlock(80);

            Assert.AreEqual(0, leanController.currentThroughput);
        }

        [TestMethod]
        public void TestQueueSizeAfterUpdateWithVaryingBlocks()
        {
            leanController.AddBlock(80);
            leanController.AddBlock(60);
            leanController.AddBlock(70);
            leanController.AddBlock(50);

            leanController.Update();
            Assert.AreEqual(0, leanController.blocks[1].Queue.Value);

            leanController.Update();
            Assert.AreEqual(20, leanController.blocks[1].Queue.Value);
            Assert.AreEqual(0, leanController.blocks[3].Queue.Value);

            leanController.Update();
            Assert.AreEqual(40, leanController.blocks[1].Queue.Value);
            Assert.AreEqual(0, leanController.blocks[3].Queue.Value);

            leanController.Update();
            Assert.AreEqual(60, leanController.blocks[1].Queue.Value);
            Assert.AreEqual(10, leanController.blocks[3].Queue.Value);
        }

        [TestMethod]
        public void TestQueueSizeAfterUpdate()
        {
            leanController.AddBlock(80);
            leanController.AddBlock(60);
            leanController.AddBlock(70);
            leanController.AddBlock(80);
            leanController.Update();
            Assert.AreEqual(0, leanController.blocks[1].Queue.Value);
            leanController.Update();
            Assert.AreEqual(20, leanController.blocks[1].Queue.Value);
            leanController.Update();
            Assert.AreEqual(40, leanController.blocks[1].Queue.Value);
            leanController.Update();
            Assert.AreEqual(60, leanController.currentThroughput);
        }

        [TestMethod]
        public void TestThroughputAfterUpdate()
        {
            leanController.AddBlock(80);
            leanController.AddBlock(60);
            leanController.AddBlock(70);
            leanController.AddBlock(80);
            leanController.Update();
            leanController.Update();
            leanController.Update();

            leanController.Update();
            Assert.AreEqual(60, leanController.currentThroughput);

            leanController.Update();
            Assert.AreEqual(120, leanController.currentThroughput);

            leanController.Update();
            Assert.AreEqual(180, leanController.currentThroughput);
        }
    }
}
