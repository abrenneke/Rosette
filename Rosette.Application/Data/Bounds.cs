namespace Rosette.Data
{
    /// <summary>Bounding line for a single axis.</summary>
    public struct Bound
    {
        /// <summary>Gets or sets the minimum value of the bound.</summary>
        public double MinValue { get; private set; }

        /// <summary>Gets or sets the maximum value of the bound.</summary>
        public double MaxValue { get; private set; }

        /// <summary>Gets the length of the bound.</summary>
        public double Length { get { return MaxValue - MinValue; } }

        /// <summary>Creates a new Bound with minimum and maximum values.</summary>
        public Bound(double minValue, double maxValue)
            : this()
        {
            MinValue = minValue; MaxValue = maxValue;
        }

        /// <summary>Copies a Bound from another Bound.</summary>
        public Bound(Bound other) : this(other.MinValue, other.MaxValue) { }

        public override string ToString()
        {
            return "[" + MinValue + "," + MaxValue + "]";
        }
    }

    /// <summary>Bounding box for a rectangular region.</summary>
    public struct Bounds
    {
        /// <summary>Gets or sets the bound of the X axis.</summary>
        public Bound X { get; private set; }

        /// <summary>Gets or sets the bound of the Y axis.</summary>
        public Bound Y { get; private set; }

        /// <summary>Gets the width of the bounds.</summary>
        public double Width { get { return X.Length; } }

        /// <summary>Gets the height of the bounds.</summary>
        public double Height { get { return Y.Length; } }

        /// <summary>Creates a new Bounds with two Bounds.</summary>
        public Bounds(Bound x, Bound y) : this()
        {
            X = x; Y = y;
        }

        /// <summary>Copies a Bounds from another bounds.</summary>
        public Bounds(Bounds other) : this(other.X, other.Y) { }

        public override string ToString()
        {
            return "x:" + X.ToString() + " y:" + Y.ToString();
        }
    }
}