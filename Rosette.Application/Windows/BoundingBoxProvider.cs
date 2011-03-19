using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Rosette.Data;

namespace Rosette.Windows
{
    /// <summary>Manager for a draggable bounding box on a canvas.</summary>
    public class BoundingBoxProvider
    {
        /// <summary>Gets the number of units the mouse must move before the dragging starts.</summary>
        private const int threshold = 3;

        private bool isDrawing;
        private Point startPoint;

        private Border boundingBox;

        /// <summary>Delegate to be called when the bounding box is released. Provides the screen-bounds of the area selected.</summary>
        public event Action<Bounds> BoxReleased;

        /// <summary>Action to call when the mouse button is pressed on the correct canvas.</summary>
        public void MouseDown(MouseEventArgs e, Canvas boundingBoxCanvas)
        {
            isDrawing = true;
            startPoint = e.GetPosition(boundingBoxCanvas);
        }

        /// <summary>Action to call when the mouse moves on the correct canvas.</summary>
        public void MouseMove(MouseEventArgs e, Canvas boundingBoxCanvas)
        {
            if (!isDrawing)
                return;

            var position = e.GetPosition(boundingBoxCanvas);
            if (boundingBox == null && startPoint.DistanceTo(position) > threshold)
            {
                boundingBox = new Border();
                boundingBox.BorderBrush = Brushes.White;
                boundingBox.BorderThickness = new Thickness(3);
                boundingBox.Width = Math.Max(3, position.X - startPoint.X);
                boundingBox.Height = Math.Max(3, position.Y - startPoint.Y);
                Canvas.SetLeft(boundingBox, startPoint.X);
                Canvas.SetTop(boundingBox, startPoint.Y);
                boundingBoxCanvas.Children.Add(boundingBox);
            }
            else if (boundingBox != null)
            {
                boundingBox.Width = Math.Max(3, position.X - startPoint.X);
                boundingBox.Height = Math.Max(3, position.Y - startPoint.Y);
            }
        }

        /// <summary>Action to call when the mouse releases on the correct canvas.</summary>
        public void MouseUp(MouseEventArgs e, Canvas boundingBoxCanvas)
        {
            if (!isDrawing)
                return;
            isDrawing = false;
            if (boundingBox == null)
                return;

            boundingBoxCanvas.Children.Remove(boundingBox);

            var screenBounds = new Bounds(
                new Bound(Canvas.GetLeft(boundingBox), Canvas.GetLeft(boundingBox) + boundingBox.Width),
                new Bound(Canvas.GetTop(boundingBox), Canvas.GetTop(boundingBox) + boundingBox.Height));

            if (BoxReleased != null)
                BoxReleased(screenBounds);

            boundingBox = null;
        }
    }
}
