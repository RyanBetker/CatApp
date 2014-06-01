using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windows8_MyFirstApp
{
   public class ElementMovementCalculator
    {
        private double endPositionX;
        private double leftPosition;
        private double elementWidth;


        public ElementMovementCalculator(double leftPosition, double endPositionX, double elementWidth)
        {
            this.leftPosition = leftPosition;
            this.endPositionX = endPositionX;
            this.elementWidth = elementWidth;
        }


        /// <summary>
        /// Job is to calculate the movement to have SRP and easier testing - not actually do the control movement
        /// </summary>
        /// <param name="newLeftPosition"></param>
        /// <returns></returns>
        public bool CalculateOneMovement(int movementSize, out double newLeftPosition)
        {
            newLeftPosition = leftPosition;
            //Control position shouldn't be settable by this class for SRP: control.Margin.Left 

            leftPosition += movementSize;

            //bounds check
            bool canMove = movementSize > 0 ? IsRightBoundsOk() : IsLeftBoundsOk();
            if (canMove)
            {
                newLeftPosition = leftPosition;
            }

            return canMove;
        }

        private bool IsLeftBoundsOk()
        {
            return leftPosition >= endPositionX;
        }

        private bool IsRightBoundsOk()
        {
            return leftPosition + elementWidth <= endPositionX;
        }
    }
}
