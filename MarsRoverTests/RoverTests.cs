using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MarsRover.Tests
{
    [TestClass()]
    public class RoverTests
    {

        [TestMethod()]
        public void ExecuteCommandsListNullTest()
        {
            //If the list is null the rover should not move and no exceptions should be thrown
            try
            {
                string rID = "Curiosity";
                Point rPosition = new Point(2, 0);
                var lz = new LandingZone(5, 5);
                lz.updateRoverPosition(rID, rPosition);
                Rover r1 = new Rover(lz, rID, 1, rPosition, new North(lz, rID), null);       
                r1.ExecuteCommands();
                Assert.AreEqual(rPosition, r1.Position);
                Assert.AreEqual<string>("N", r1.getRoverDirectionString());
            }
            catch (Exception ex) { Assert.Fail("Test Failed - unhandled exception: " + ex.Message); }   
        }

        [TestMethod()]
        public void ExecuteCommandsListEmptyTest()
        {
            //If the list is empty the rover should not move and no exceptions should be thrown
            try
            {
                string rID = "Curiosity";
                Point rPosition = new Point(2, 0);
                var lz = new LandingZone(5, 5);
                lz.updateRoverPosition(rID, rPosition);
                Rover r1 = new Rover(lz, rID, 1, rPosition, new North(lz, rID), new LinkedList<ICommand>());
                r1.ExecuteCommands();
                Assert.AreEqual(rPosition, r1.Position);
                Assert.AreEqual<string>("N", r1.getRoverDirectionString());
            }
            catch (Exception ex) { Assert.Fail("Test Failed - unhandled exception: " + ex.Message); }
        }

        [TestMethod()]
        public void ExecuteCommandsListItemNullTest()
        {
            //If a list item is null the item should be skipped and execution continued with the next valid command and no exceptions should be thrown
            try
            {
                var lz = new LandingZone(5, 5);
                lz.updateRoverPosition("Curiosity", new Point(2, 0));
                LinkedList<ICommand> cmdList = new LinkedList<ICommand>();
                ICommand nullCMD = null;
                cmdList.AddLast(new MoveForwardCommand());
                cmdList.AddLast(nullCMD);
                cmdList.AddLast(new MoveForwardCommand());
                Rover r1 = new Rover(lz, "Curiosity", 1, new Point(2, 0), new North(lz, "Curiosity"), cmdList);
                r1.ExecuteCommands();
                Assert.AreEqual(new Point(2,2), r1.Position);
                Assert.AreEqual<string>("N", r1.getRoverDirectionString());
            }
            catch (Exception ex) { Assert.Fail("Test Failed - unhandled exception: " + ex.Message); }
        }

        [TestMethod()]
        public void MoveForwardNorthTest()
        {
            var lz = new LandingZone(5, 5);
            string rID = "Curiosity";
            Point rPosition = new Point(2, 0);
            Rover r1 = new Rover(lz, rID, 1, rPosition, new North(lz, rID), null);
            lz.updateRoverPosition(rID, rPosition);
            r1.MoveForward();
            Assert.AreEqual(new Point(2, 1), r1.Position);
        }

        [TestMethod()]
        public void MoveForwardEastTest()
        {
            var lz = new LandingZone(5, 5);
            string rID = "Curiosity";
            Point rPosition = new Point(2, 0);
            Rover r1 = new Rover(lz, rID, 1, rPosition, new East(lz, rID), null);
            lz.updateRoverPosition(rID, rPosition);
            r1.MoveForward();
            Assert.AreEqual(new Point(3, 0), r1.Position);
        }

        [TestMethod()]
        public void MoveForwardSouthTest()
        {
            var lz = new LandingZone(5, 5);
            string rID = "Curiosity";
            Point rPosition = new Point(2, 1);
            Rover r1 = new Rover(lz, rID, 1, rPosition, new South(lz, rID), null);
            lz.updateRoverPosition(rID, rPosition);
            r1.MoveForward();
            Assert.AreEqual(new Point(2, 0), r1.Position);
        }

        [TestMethod()]
        public void MoveForwardWestTest()
        {
            var lz = new LandingZone(5, 5);
            string rID = "Curiosity";
            Point rPosition = new Point(2, 0);
            Rover r1 = new Rover(lz, rID, 1, rPosition, new West(lz, rID), null);
            lz.updateRoverPosition(rID, rPosition);
            r1.MoveForward();
            Assert.AreEqual(new Point(1, 0), r1.Position);
        }


        [TestMethod()]
        public void MoveForwardOffEdgeTest()
        {
            //if an invalid move is detected then we should skip it and notify the user and continue to the next command
            var lz = new LandingZone(5, 5);
            string rID = "Curiosity";
            Point rPosition = new Point(2, 0);
            Rover r1 = new Rover(lz, rID, 1, rPosition, new South(lz, rID), null);
            lz.updateRoverPosition(rID, rPosition);
            r1.MoveForward();
            Assert.AreEqual(new Point(2, 0), r1.Position);
        }

        [TestMethod()]
        public void MoveForwardIntoRoverTest()
        {
            //if an invalid move is detected then we should skip it and notify the user and continue to the next command
            var lz = new LandingZone(5, 5);
            Rover r1 = new Rover(lz, "Curiosity", 1, new Point(2, 0), new West(lz, "Curiosity"), null);
            lz.updateRoverPosition("Curiosity", new Point(2, 0));
            Rover r2 = new Rover(lz, "Opportunity", 1, new Point(1, 0), new West(lz, "Opportunity"), null);
            lz.updateRoverPosition("Opportunity", new Point(1, 0));
            r1.MoveForward();
            Assert.AreEqual(new Point(2, 0), r1.Position);
            r1.SpinRight();
            r1.MoveForward();
            Assert.AreEqual(new Point(2, 1), r1.Position);
        }

        [TestMethod()]
        public void SpinLeftTest()
        {
            Rover r1 = new Rover(null, "", 1, null, new North(null, ""), null);
            r1.SpinLeft();
            Assert.AreEqual("W", r1.getRoverDirectionString());
        }

        [TestMethod()]
        public void SpinRightTest()
        {
            Rover r1 = new Rover(null, "", 1, null, new North(null, ""), null);
            r1.SpinRight();
            Assert.AreEqual("E", r1.getRoverDirectionString());
        }

        [TestMethod()]
        public void printRoverStartPositionTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Rover r1 = new Rover(null, "1", 1, new Point(2,8), new East(null, ""), null);
                r1.printRoverStartPosition();
                Assert.AreEqual<string>("Rover 1 - Start Position: 2 8 E", sw.ToString().Trim());
                Console.SetOut(Console.Out);
            }
        }

        [TestMethod()]
        public void printRoverFinalPositionTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Rover r1 = new Rover(null, "2", 1, new Point(7, 0), new South(null, ""), null);
                r1.printRoverFinalPosition();
                Assert.AreEqual<string>("Rover 2 - Final Position: 7 0 S", sw.ToString().Trim());
                Console.SetOut(Console.Out);
            }
        }


        [TestMethod()]
        public void printRoverPositionTest()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Rover r1 = new Rover(null, "", 1, new Point(5, 2), new West(null, ""), null);
                r1.printRoverPosition();
                Assert.AreEqual<string>("5 2 W", sw.ToString().Trim());
                Console.SetOut(Console.Out);
            }
        }


        [TestMethod()]
        public void getRoverDirectionStringTest()
        {
            Rover r1 = new Rover(null, "", 1, null, new North(null, ""), null);
            Assert.AreEqual<string>("N", r1.getRoverDirectionString());
            r1.SpinRight();
            Assert.AreEqual<string>("E", r1.getRoverDirectionString());
            r1.SpinRight();
            Assert.AreEqual<string>("S", r1.getRoverDirectionString());
            r1.SpinRight();
            Assert.AreEqual<string>("W", r1.getRoverDirectionString());
        }
    }
}