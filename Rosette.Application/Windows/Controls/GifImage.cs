using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Rosette.Windows.Controls
{
    //From http://stackoverflow.com/questions/210922/how-do-i-get-an-animated-gif-to-work-in-wpf
    public class GifImage : Image
    {
        public static readonly DependencyProperty FrameIndexProperty = DependencyProperty.Register("FrameIndex", typeof(int), typeof(GifImage), new UIPropertyMetadata(0, new PropertyChangedCallback(ChangingFrameIndex)));

        public Uri Uri { get; set; }

        private GifBitmapDecoder gifDecoder;
        private Int32Animation animation;
        private bool animationIsWorking = false;

        public int FrameIndex
        {
            get { return (int)GetValue(FrameIndexProperty); }
            set { SetValue(FrameIndexProperty, value); }
        }

        public GifImage()
        {
            Initialized += new EventHandler(GifImage_Initialized);
        }

        private void GifImage_Initialized(object sender, EventArgs e)
        {
 	        gifDecoder = new GifBitmapDecoder(Uri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            animation = new Int32Animation(0, gifDecoder.Frames.Count - 1, new Duration(new TimeSpan(0, 0, 0, gifDecoder.Frames.Count / 10, (int)((gifDecoder.Frames.Count / 10.0 - gifDecoder.Frames.Count / 10) * 1000))));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Source = gifDecoder.Frames[0];
        }

        private static void ChangingFrameIndex(DependencyObject obj, DependencyPropertyChangedEventArgs ev)
        {
            GifImage ob = obj as GifImage;
            ob.Source = ob.gifDecoder.Frames[(int)ev.NewValue];
            ob.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (!animationIsWorking)
            {
                BeginAnimation(FrameIndexProperty, animation);
                animationIsWorking = true;
            }
        }
    }
}
