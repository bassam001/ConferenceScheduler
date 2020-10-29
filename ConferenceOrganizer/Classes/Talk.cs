using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer
{
  public class Talk
    {

        public static DateTime NetworkMinStart;
        public static DateTime NetworkMaxStart;
     
        public  string Title { get; set;  }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public TimeSpan TalkDuration { get; set; }
        public bool isNetworking { get; set; }
        public bool isLunchBreak { get; set; }

        public Talk (string title , int duration)
        {
            this.Title = title;
            this.Duration = duration;
            TalkDuration = new TimeSpan(0, duration, 0);
            if (Title.ToLower().Contains("networking"))
                isNetworking = true;

            if (Title.ToLower().Contains("LunchBreack"))
                isLunchBreak = true;
            

        }

        // controleer als er een Overlappe is 
        //
        internal bool OverLap(Talk aTalk)
        {
            return ((StartTime < aTalk.EndTime) && (EndTime > aTalk.StartTime));
        }

        public override string ToString()
        {
            return StartTime.ToShortTimeString() + '\t' + Title;
        }

    }
}
