using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer
{
   public class Track
    {


       
        public   List<Session> TrackSessions { get; set; }
        public Track(List<Session> ListOfSessions )
        {

            TrackSessions = new List<Session>();
            foreach (Session aSession in ListOfSessions)
            {
                Session TrackSession = new Session(aSession);
                TrackSessions.Add(TrackSession);
            }
            
        }

      

        internal  bool AddTalk(Talk aTalk)
        {

            bool isAdded = false;
            foreach (Session aSession in TrackSessions)
            {
                isAdded = aSession.TryAddToSession(aTalk);
                if (isAdded)
                    break;
            }
            return (isAdded);
        }





      

    }




}
