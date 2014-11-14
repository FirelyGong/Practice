using System;

namespace Practice.DataBase.Storage
{
    public class Pointer : IComparable<Pointer>, IEquatable<Pointer>
    {
        public Pointer()
            : this(0, 0)
        {
        }

        public Pointer(long position, int size)
        {
            Position = position;
            Size = size;
        }

        public long Position { get; set; }

        public int Size { get; set; }

        public int CompareTo(Pointer other)
        {
            return Position.CompareTo(other.Position);
        }

        public bool Equals(Pointer other)
        {
            if (Position == other.Position && Size == other.Size)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return String.Format("(Position, Size) = ({0}, {1})", Position, Size);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pointer)
            {
                return Equals((Pointer)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() ^ Size.GetHashCode();
        }

        public static bool operator ==(Pointer pointer1, Pointer pointer2)
        {
            if (ReferenceEquals(pointer1, null) && ReferenceEquals(pointer2, null))
            {
                return true;
            }
            
            if (ReferenceEquals(pointer1, null) || ReferenceEquals(pointer2, null))
            {
                return false;
            }

            return pointer1.Equals(pointer2);
        }

        public static bool operator !=(Pointer pointer1, Pointer pointer2)
        {
            return !(pointer1 == pointer2);
        }

        public static Pointer operator +(Pointer pointer, long offset)
        {
            return new Pointer
            {
                Position = pointer.Position + offset,
                Size = pointer.Size
            };
        }

        /// <summary>
        /// Checking whether the pointer is invalid.
        /// </summary>
        public bool IsNull
        {
            get
            {
                return Size == 0;
            }
        }

        /// <summary>
        /// Returns index of the block after fragment.
        /// </summary>
        public long PositionPlusSize
        {
            get { return checked(Position + Size); }
        }
    }
}
