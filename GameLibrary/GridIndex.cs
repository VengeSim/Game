using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace GameLibrary
{
    [Serializable()]
    public struct GridIndex : IEquatable<GridIndex> , ISerializable

    {
        private int x;
        private int y;
        private int z;

        public int X { get => x;}
        public int Y { get => y;}
        public int Z { get => z;}

        //public static GridIndex North { get => new GridIndex(0, 0).GetNorth(); }
        //public static GridIndex East { get => new GridIndex(0, 0).GetEast(); }
        //public static GridIndex South { get => new GridIndex(0, 0).GetSouth(); }
        //public static GridIndex West { get => new GridIndex(0, 0).GetWest(); }

        public GridIndex(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public GridIndex(SerializationInfo info, StreamingContext context)
        {
            this.x = (Int32)info.GetValue("X", typeof(Int32));
            this.y = (Int32)info.GetValue("Y", typeof(Int32));
            this.z = (Int32)info.GetValue("Z", typeof(Int32));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", this.x);
            info.AddValue("Y", this.y);
            info.AddValue("Z", this.z);
        }

        public override string ToString()
        {

            return string.Format("[GridIndex]({0}, {1}, {2})", this.x, this.y, this.z);

        }

        public bool Equals(GridIndex other)
        {
            if (this.x != other.x) return false;
            if (this.y != other.y) return false;
            if (this.z != other.z) return false;

            return true;
        }


        //public GridIndex GetNorth() { return new GridIndex( this.x, this.y - 1); }
        //public GridIndex GetEast(){ return new GridIndex( this.x + 1, this.y); }
        //public GridIndex GetSouth(){ return new GridIndex( this.x, this.y + 1); }
        //public GridIndex GetWest() { return new GridIndex( this.x - 1, this.y); }

        private bool IsInRange(GridIndex index, int maxX, int maxY, int maxZ)
        {
            int x = index.x;
            int y = index.y;
            int z = index.z;

            if (x < 0) { return false; }
            if (y < 0) { return false; }
            if (z < 0) { return false; }
            if (x > maxX - 1) { return false; }
            if (y > maxY - 1) { return false; }
            if (z > maxY - 1) { return false; }

            return true;

        }

        // do this twice for x,y then x,z. then add z to the first...
        public static List<GridIndex> GetIndexesOnLine(GridIndex index1, GridIndex index2)
        {

            int x = index1.x;
            int y = index1.y;
            int x2 = index2.x;
            int y2 = index2.y;

            List<GridIndex> IndexList = new List<GridIndex>();

            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                IndexList.Add(new GridIndex(x, y, 0));
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
            return IndexList;
        }

        public GridIndex RotateAround(GridIndex centerPoint, RotationData rotation)
        {
            //index must be offset from the center point...
            GridIndex newIndex = rotation.Matrix * this;
            
            this.x = centerPoint.x + newIndex.X;
            this.y = centerPoint.y + newIndex.Y;
            this.z = centerPoint.z + newIndex.Z;
            return this;
        }

        public GridIndex GetOffset(GridIndex other)
        {
            return new GridIndex(this.x - other.X, this.y - other.Y, this.z - other.Z);
        }
    }
}
