using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game
{
    [Serializable()]
    public class Time : IEquatable<Time>, IComparer<Time> , ISerializable
    {
        private int seconds;
        private int minutes;
        private int hours;
        private int days;
        private int years;

        public int Seconds { get => seconds;}
        public int Minutes { get => minutes;}
        public int Hours { get => hours;}
        public int Days { get => days;}
        public int Years { get => years;}

        public Time()
        {
            this.seconds = 0;
            this.minutes = 0;
            this.hours = 0;
            this.days = 0;
            this.years = 0;
        }
        public Time(int seconds) : this()
        {
            this.seconds = seconds;
        }
        public Time(int seconds, int minutes) : this(seconds)
        {
            this.minutes = minutes;
        }
        public Time(int seconds, int minutes, int hours) : this(seconds, minutes)
        {
            this.hours = hours;
        }
        public Time(int seconds, int minutes, int hours, int days) : this(seconds, minutes, hours)
        {
            this.days = days;
        }
        public Time(int seconds, int minutes, int hours, int days, int years) : this(seconds, minutes, hours, days)
        {
            this.years = years;
        }
        public Time(SerializationInfo info, StreamingContext context)
        {
            this.seconds = (Int32)info.GetValue("Seconds", typeof(Int32));
            this.minutes = (Int32)info.GetValue("Minutes", typeof(Int32));
            this.hours = (Int32)info.GetValue("Hours", typeof(Int32));
            this.days = (Int32)info.GetValue("Days", typeof(Int32));
            this.years = (Int32)info.GetValue("Years", typeof(Int32));

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Seconds", this.seconds);
            info.AddValue("Minutes", this.minutes);
            info.AddValue("Hours", this.hours);
            info.AddValue("Days", this.days);
            info.AddValue("Years", this.years);

        }
        public override string ToString()
        {
            return string.Format("S:{0} M:{1} H:{2} D:{3} Y:{4}", this.seconds, this.minutes, this.hours, this.days, this.years);
        }
        public int Compare(Time time1, Time time2)
        {
            int sec1 = Time.TotalSeconds(time1);
            int sec2 = Time.TotalSeconds(time2);
            if (sec1 > sec2)
                return 1;
            if (sec1 < sec2)
                return -1;
            else
                return 0;
        }
        public bool Equals(Time other)
        {
            int secT = Time.TotalSeconds(this);
            int otherSecT = Time.TotalSeconds(other);
            if (secT != otherSecT) return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Time other = (Time)obj;
            int result = this.Compare(this, other);
            if (result == 0) { return true; }
            return false;
        }
        public override int GetHashCode()
        {
            return this.seconds.GetHashCode() ^ this.minutes.GetHashCode() ^ this.hours.GetHashCode() ^ this.days.GetHashCode() ^ this.years.GetHashCode();
        }
        public static bool operator == (Time time1, Time time2)
        {
            int result = time1.Compare(time1, time2);
            if (result == 0) { return true; }
            return false;
        }
        public static bool operator != (Time time1, Time time2)
        {
            int result = time1.Compare(time1, time2);
            if (result == 0) { return false; }
            return true;
        }
        public static bool operator > (Time time1, Time time2)
        {
            int result = time1.Compare(time1, time2);
            if (result == 1) { return true; }
            return false;
        }
        public static bool operator < (Time time1, Time time2)
        {
            int result = time1.Compare(time1, time2);
            if (result == -1) { return true; }
            return false;
        }
        public static bool operator <= (Time time1, Time time2)
        {
            int result = time1.Compare(time1, time2);
            if (result == -1 || result == 0) { return true; }
            return false;
        }
        public static bool operator >= (Time time1, Time time2)
        {
            int result = time1.Compare(time1, time2);
            if (result == 1 || result == 0) { return true; }
            return false;
        }
        public static Time operator + (Time time1, Time time2)
        {
            int secT1 = Time.TotalSeconds(time1);
            int secT2 = Time.TotalSeconds(time2);
            int totalSec = secT1 + secT2;
            return Time.SecondsToTime(totalSec);
        }
        public static Time operator - (Time time1, Time time2)
        {
            int secT1 = Time.TotalSeconds(time1);
            int secT2 = Time.TotalSeconds(time2);
            int totalSec = secT1 - secT2;
            return Time.SecondsToTime(totalSec);
        }
        public void Add(Time other)
        {
            int secT = Time.TotalSeconds(this);
            int otherSecT = Time.TotalSeconds(other);
            int totalSec = secT + otherSecT;
            this.SetTime(Time.SecondsToTime(totalSec));

        }
        public void Minus(Time other)
        {
            int secT = Time.TotalSeconds(this);
            int otherSecT = Time.TotalSeconds(other);
            int totalSec = secT - otherSecT;
            this.SetTime(Time.SecondsToTime(totalSec));

        }
        public static Time SecondsToTime(int secT)
        {
            int yearT = secT / (365 * 24 * 60 * 60);
            int yearR = secT % (365 * 24 * 60 * 60);
            int dayT = yearR / (24 * 60 * 60);
            int dayR = yearR % (24 * 60 * 60);
            int hourT = dayR / (60 * 60);
            int hourR = dayR % (60 * 60);
            int minT = hourR / 60;
            int minR = hourR % 60;
            secT = minR;

            return new Time(secT, minT, hourT, dayT, yearT);
        }
        public static int TotalSeconds(Time time)
        {
            int secInYears = time.years * 365 * 24 * 60 * 60;
            int secInDays = time.days * 24 * 60 * 60;
            int secInHours = time.hours * 60 * 60;
            int secInMinutes = time.minutes * 60;
            return time.seconds + secInMinutes + secInHours + secInDays + secInYears;
        }
        public void SetTime(Time other)
        {
            this.seconds = other.seconds;
            this.minutes = other.minutes;
            this.hours = other.hours;
            this.days = other.days;
            this.years = other.years;
        }


    }
        
}
