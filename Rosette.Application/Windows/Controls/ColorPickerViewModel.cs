using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Rosette.Extensions;
using Color = Rosette.Data.Color;

namespace Rosette.Windows.Controls
{
    public class ColorPickerViewModel : ViewModel
    {
        /// <summary>Gets the color palette for choosing a color.</summary>
        public WriteableBitmap ColorPalette { get; set; }

        /// <summary>Gets or sets the left position of the palette cursor.</summary>
        public double PalettePositionLeft 
        { 
            get { return Saturation * 256.0; } 
            set { Saturation = (float)(value / 256).Clamp(0, 1); } 
        }

        /// <summary>Gets or sets the top position of the palette cursor.</summary>
        public double PalettePositionTop 
        { 
            get { return 256 - (Value * 256.0); } 
            set { Value = (float)((256 - value) / 256).Clamp(0, 1); } 
        }

        /// <summary>Gets or sets the top position of the hue chooser.</summary>
        public double HueChooserTop 
        { 
            get { return 256 - (Hue * 256.0) / 360.0; } 
            set { Hue = (int)Math.Round(((256 - value) * 360.0) / 256.0); } 
        }

        /// <summary>Gets the width of the palette cursor.</summary>
        public double PaletteCursorWidth { get { return 10; } }

        /// <summary>Gets the height of the palette cursor.</summary>
        public double PaletteCursorHeight { get { return 10; } }

        private int hue;
        /// <summary>Gets or sets the hue of the current color.</summary>
        public int Hue
        {
            get { return hue; }
            set
            {
                hue = value;
                OnPropertyChanged(x => Hue);
                OnPropertyChanged(x => CurrentColor);
                OnPropertyChanged(x => HueChooserTop);
                UpdateColorPalette();
            }
        }

        private float saturation;
        /// <summary>Gets or sets the saturation of the current color.</summary>
        public float Saturation
        {
            get { return saturation; }
            set 
            { 
                saturation = value;
                OnPropertyChanged(x => Saturation);
                OnPropertyChanged(x => CurrentColor);
                OnPropertyChanged(x => PalettePositionLeft);
            }
        }

        private float value;
        /// <summary>Gets or sets the value of the current color.</summary>
        public float Value
        {
            get { return value; }
            set 
            {
                this.value = value;
                OnPropertyChanged(x => Value);
                OnPropertyChanged(x => CurrentColor);
                OnPropertyChanged(x => PalettePositionTop);
            }
        }

        /// <summary>Gets the current color chosen.</summary>
        public Color CurrentColor { get { return Color.FromHsv(Hue, Saturation, Value); } }

        private bool paletteMouseIsDown = false;
        private bool hueMouseIsDown = false;

        private MainWindowViewModel mainWindowViewModel;

        /// <summary>Gets or sets the command for when the OK button is pressed.</summary>
        public ICommand OKCommand { get; set; }

        /// <summary>Creates a new Color Picker ViewModel.</summary>
        public ColorPickerViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            OKCommand = new Rosette.Windows.Commands.ColorPicker.OKCommand(this);

            ColorPalette = new WriteableBitmap(256, 256, 96, 96, System.Windows.Media.PixelFormats.Rgb24, null);
            Hue = 0;
            Saturation = 1;
            Value = 1;
        }

        /// <summary>Updates the color palette to the specified hue.</summary>
        private void UpdateColorPalette()
        {
            ColorPalette.WritePixels(new Int32Rect(0, 0, 256, 256), GetColorPalette(Hue), 256 * 3, 0);
            OnPropertyChanged(x => ColorPalette);
        }

        /// <summary>Gets a color palette for a hue.</summary>
        private byte[] GetColorPalette(int hue)
        {
            var buffer = new byte[(256 * 256) * 3];
            for (var y = 0; y < 256; y++)
            {
                for (var x = 0; x < 256; x++)
                {
                    var xa = x * 3;
                    var color = Color.FromHsv(hue, x / 255f, (255 - y) / 255f);
                    buffer[(y * (256 * 3)) + xa] = color.R;
                    buffer[(y * (256 * 3)) + (xa + 1)] = color.G;
                    buffer[(y * (256 * 3)) + (xa + 2)] = color.B;
                }
            }

            return buffer;
        }

        public void CloseAndOK()
        {
            mainWindowViewModel.CloseColorPickerAndOK();
        }

        #region UI Events

        public void UpdatePalettePosition(double mouseLeft, double mouseTop)
        {
            PalettePositionTop = mouseTop - PaletteCursorHeight / 2;
            PalettePositionLeft = mouseLeft - PaletteCursorWidth / 2;
        }

        public void OnPaletteMouseDown(Point point)
        {
            paletteMouseIsDown = true;
            UpdatePalettePosition(point.X, point.Y);
        }

        public void OnPaletteMouseMove(Point point)
        {
            if (paletteMouseIsDown)
                UpdatePalettePosition(point.X, point.Y);
        }

        public void OnPaletteMouseUp(Point point)
        {
            paletteMouseIsDown = false;
        }

        public void UpdateHuePosition(double top)
        {
            HueChooserTop = top;
        }

        public void OnHueMouseDown(double top)
        {
            hueMouseIsDown = true;
            UpdateHuePosition(top);
        }

        public void OnHueMouseMove(double top)
        {
            if (hueMouseIsDown)
                UpdateHuePosition(top);
        }

        public void OnHueMouseUp(double top)
        {
            hueMouseIsDown = false;
        }

        #endregion
    }
}
