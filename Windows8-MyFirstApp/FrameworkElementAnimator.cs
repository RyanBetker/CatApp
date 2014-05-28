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
            
        public FrameworkElementAnimator(FrameworkElement control)
        {
            this.control = control;
        }

        internal async void MoveToRightBorder(int movementSize)
        {
            CurrentDirection = Direction.Right;
            var maxWidth = Window.Current.Bounds.Width;

            //reset the token
            tokenSource = new CancellationTokenSource();

            var task = Task.Run(() =>  MoveTo(movementSize, maxWidth, tokenSource.Token), tokenSource.Token);
            await task;
        }

        private async Task MoveTo(int movementSize, double maxWidth, CancellationToken cancellationToken)
        {
            tokenSource.Cancel();//cancel any existing stuff
            
            continueMoving = false;
            //Give the other thread time to stop
            await Task.Delay(TimeSpan.FromMilliseconds(10));

            continueMoving = true;//reset the flag so we can start moving

            do
            {
                var newMargin = control.Margin;
                newMargin.Left += movementSize;

                //Have we pushed the image all the way right?
                if (cancellationToken.IsCancellationRequested == false && newMargin.Left + control.Width < maxWidth)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1));

                    control.Margin = newMargin;
                }
                else
                {
                    break;
                }
            } while (true);
        }

        public Direction CurrentDirection { get; set; }

        internal async void MoveToLeftBorder(int movementSize)
        {
            CurrentDirection = Direction.Left;
            //reset the token
            tokenSource = new CancellationTokenSource();
            var task = Task.Run(() =>
                MoveTo(-movementSize, 0, tokenSource.Token), tokenSource.Token);

            await task;
        }
    }
}
