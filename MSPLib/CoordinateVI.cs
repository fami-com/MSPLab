using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSPLib
{
    public enum Direction
    {
        Latitude,
        Longitude
    }


    public class CoordinateVI
    {
        public Direction Direction;

        public readonly int Degrees;
        public readonly uint Minutes;
        public readonly uint Seconds;

        public CoordinateVI()
        {
            Degrees = 0;
            Minutes = 0;
            Seconds = 0;

            Direction = Direction.Latitude;
        }

        public CoordinateVI(Direction direction, int degrees, uint minutes, uint seconds)
        {

            if (direction == Direction.Latitude && (degrees < -90 || degrees > 90))
            {
                throw new ArgumentOutOfRangeException(nameof(degrees), "Degrees must be between -90 and 90 for Latitude");
            }

            if(direction == Direction.Longitude && (degrees < -180 || degrees > 180))
            {
                throw new ArgumentOutOfRangeException(nameof(degrees), "Degrees must be between -180 and 180 for Longitude");
            }


            if (minutes > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), "Minutes must be below 60");
            }


            if(seconds > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), "Seconds must be below 60");
            }

            Direction = direction;
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
        }

        public override string ToString()
        {
            char dir;
            if (Direction == Direction.Latitude)
            {
                if (Degrees < 0) dir = 'S';
                else dir = 'W';
            }
            else
            {
                if (Degrees < 0) dir = 'W';
                else dir = 'S';
            }

            return $"{Degrees:#00}°{Minutes:00}'{Seconds:00}\" {dir}";
        }

        public string ToDecimalString()
        {
            char dir;
            if (Direction == Direction.Latitude)
            {
                if (Degrees < 0) dir = 'S';
                else dir = 'W';
            }
            else
            {
                if (Degrees < 0) dir = 'W';
                else dir = 'S';
            }

            return $"{Degrees + Minutes / 60f + Seconds / 3600f}° {dir}";
        }

        public CoordinateVI Average(CoordinateVI rhs)
        {
            return Average(this, rhs);
        }

        public static CoordinateVI Average(CoordinateVI lhs, CoordinateVI rhs)
        {
            if (lhs.Direction != rhs.Direction) return null;

            return new CoordinateVI(lhs.Direction, (lhs.Degrees + rhs.Degrees) / 2, (lhs.Minutes + rhs.Minutes) / 2, (lhs.Seconds + rhs.Seconds) / 2);
        }
    }
}