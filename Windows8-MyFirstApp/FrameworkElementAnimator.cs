using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Windows8_MyFirstApp
{
    public class FrameworkElementAnimator
    {
        public enum Direction
        {
            Left,
            Right
        }

        private FrameworkElement control;
        private bool continueMoving;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public Direction CurrentDirection { get; set; }
            
        public FrameworkElementAnimator(FrameworkElement control)
        {
            this.control = control;
        }

        internal async void MoveToLeftBorder(int movementSize)
        {
            CurrentDirection = Direction.Left;

            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High,
                () => MoveTo(0, -movementSize));

        }

        internal async void MoveToRightBorder(int movementSize)
        {
            CurrentDirection = Direction.Right;
            var maxWidth = Window.Current.Bounds.Width;

            //reset the token
            //tokenSource = new CancellationTokenSource();
            //new System.ServiceModel.Dispatcher.ClientOperation();
            
            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High,
                () => MoveTo(maxWidth, movementSize));
        }

        private async Task MoveTo(double endPositionX, int movementSize)//, CancellationToken cancellationToken)
        {
            //tokenSource.Cancel();//cancel any existing stuff
            
            continueMoving = false;
            //Could try to Give the other thread time to stop
            //await Task.Delay(TimeSpan.FromMilliseconds(10));

            continueMoving = true;//reset the flag so we can start moving

            double newLeftPosition = 0;
            var moveCalc = new ElementMovementCalculator(leftPosition: control.Margin.Left, endPositionX: endPositionX, elementWidth: control.Width);

            while (moveCalc.CalculateOneMovement(movementSize, out newLeftPosition))
            {
                var newMargin = control.Margin;
                newMargin.Left = newLeftPosition;

                await Task.Delay(TimeSpan.FromMilliseconds(.5));

                control.Margin = newMargin;
            }
        }
    }
}
