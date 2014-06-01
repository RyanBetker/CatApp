using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows8_MyFirstApp;

namespace CatApp.Tests
{
    [TestClass]
    public class ElementMovementCalculatorTest
    {
        [TestMethod]
        public void TestCalcMoveRightToLeft()
        {
            var movementCalc = new ElementMovementCalculator(PositionWhenElementIsOnRightSide(), PositionWhenElementIsOnLeft(), ElementWidth());
            double newLeftPosition = 0;

            while (movementCalc.CalculateOneMovement(-10, out newLeftPosition))
            {

            }

            Assert.AreEqual(PositionWhenElementIsOnLeft(), newLeftPosition);
        }
        [TestMethod]
        public void TestCalcMoveLeftToRight()
        {
            var movementCalc = new ElementMovementCalculator(PositionWhenElementIsOnLeft(), RightSideOfScreen(), ElementWidth());
            double newLeftPosition = 0;

            while (movementCalc.CalculateOneMovement(10, out newLeftPosition))
            {

            }

            Assert.AreEqual(PositionWhenElementIsOnRightSide(), newLeftPosition);
        }

        private static int PositionWhenElementIsOnLeft()
        {
            return 0;
        }

        private static int PositionWhenElementIsOnRightSide()
        {
            return RightSideOfScreen() - ElementWidth();
        }


        private static int ElementWidth()
        {
            return 300;
        }

        private static int RightSideOfScreen()
        {
            return 1000;
        }
    }
}
