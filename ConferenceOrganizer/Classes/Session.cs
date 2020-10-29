using System;
using System.Collections.Generic;

using System.Text;

namespace ConferenceOrganizer
{
   public class Session
    {

        //Er zijn aleen twee vast sesstie type, of drie alles we LunchBreak als een sesstie beschouwd , daardoor wordt enum gebruikt (enum is een value type)
        public enum SESSION_TYPE { MORNING, AFTERNOON };
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }

        //readonly en wordt auomatisch ingevuild 
        public double Duration { get { return ((EndTime - StartTime).TotalMinutes); } }

        //readonly en wordt auomatisch ingevuild 
        public SESSION_TYPE Type { get { if (StartTime.Hour <= 12) return SESSION_TYPE.MORNING; else return SESSION_TYPE.AFTERNOON; } }
        public  List<Talk> SessionTalks { get; set; }


        public Session (DateTime startTime , DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            SessionTalks = new List<Talk>();

        }

       
        public Session(Session session)
        {
            this.StartTime = session.StartTime;
            this.EndTime = session.EndTime;
            SessionTalks = new List<Talk>();
        }


         

        internal bool TryAddToSession(Talk aTalk)
        {
            bool isAdded = false;

            if (aTalk.isNetworking)
            {
                //hadel de requst from the AddNetworkingEvent function
                isAdded = AddNetworkingEvent(aTalk);
                return isAdded;
            }
          
 
            if (IsFull())
                return isAdded;

            if (!CanFit(aTalk))
                return isAdded;


            aTalk.StartTime = StartTime;
            aTalk.EndTime = aTalk.StartTime.Add(aTalk.TalkDuration);

            //De belangrijkste hier om de overlappen te vinden, en dan de start en eind time te passen voor de atalk
            //
            foreach (Talk aSessionTalk in SessionTalks)
            {
                if (aSessionTalk.OverLap(aTalk))
                {
                    aTalk.StartTime = aSessionTalk.EndTime;
                    aTalk.EndTime = aTalk.StartTime.Add(aTalk.TalkDuration);
                }
                else
                    break;
            }

            if (aTalk.EndTime <= this.EndTime)
            {
                isAdded = true;
                SessionTalks.Add(aTalk);
                SessionTalks.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            }


            return isAdded;
        }



        private bool AddNetworkingEvent(Talk NewTalk)
        {
            bool isAdded = false;
            if ((Talk.NetworkMinStart > EndTime) || (Talk.NetworkMaxStart < StartTime))
                return isAdded;

            if (IsFull())

                return isAdded;

            if (!CanFit(NewTalk))
                return isAdded;

            NewTalk.StartTime = EndTime - NewTalk.TalkDuration;
            NewTalk.EndTime = EndTime;
            

            foreach (Talk aTalk in SessionTalks)
            {
                if (NewTalk.OverLap(aTalk))
                {
                    NewTalk.StartTime = aTalk.EndTime;
                    NewTalk.EndTime = NewTalk.StartTime.Add(NewTalk.TalkDuration);
                }
                else
                    break;
            }
            if ((NewTalk.EndTime <= EndTime) && (NewTalk.StartTime >= Talk.NetworkMinStart) && (NewTalk.StartTime <= Talk.NetworkMaxStart))
            {
                SessionTalks.Add(NewTalk);
                SessionTalks.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
                isAdded = true;
            }

            return isAdded;
        }
       


        //controleer als de sessie een talk kan bevatten 
        private bool CanFit(Talk aTalk)
        {
            double TotalTalksDuration = GetTotalTalksDuration();
            double availableDuration = Duration - TotalTalksDuration;

            return (availableDuration >= aTalk.Duration);
        }


        //controleer als de sessie full is
        private bool IsFull()
        {
            //return fals als de lijst van SessionTalks is leeg

            if (SessionTalks.Count == 0)
                return false;

            double TotalTalksDuration = GetTotalTalksDuration();
            double availableDuration = Duration - TotalTalksDuration;

            //return true als de sessie heeft niet meer tijd
            return (availableDuration <= 0);

        }


        private double GetTotalTalksDuration()
        {
            double totalTalksDuration = 0;
            foreach (Talk aTalk in SessionTalks)
            {
                totalTalksDuration += aTalk.Duration;
            }
            return totalTalksDuration;
        }


    }
}
