using System;

namespace Rosette.Data
{
    /// <summary>Representation of a width and height.</summary>
    public struct Dimensions : IEquatable<Dimensions>
    {
        /// <summary>Gets the width of the dimensions.</summary>
        public int Width { get; private set; }

        /// <summary>Gets the height of the dimensions.</summary>
        public int Height { get; private set; }

        /// <summary>Creates a new set of dimensions with a width and height.</summary>
        public Dimensions(int width, int height) : this()
        {
            Width = width;
            Height = height;
        }

        public override int GetHashCode() { return Width.GetHashCode() ^ Height.GetHashCode(); }
        public override bool Equals(object obj) { return obj is Dimensions && Equals((Dimensions)obj); }
        public bool Equals(Dimensions other) { return Height == other.Height && Width == other.Width; }

        public static bool operator ==(Dimensions a, Dimensions b) { return a.Equals(b); }
        public static bool operator !=(Dimensions a, Dimensions b) { return !a.Equals(b); }
    }
}
