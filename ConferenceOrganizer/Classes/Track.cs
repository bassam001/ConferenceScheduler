using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer
{
   public class Track
    {


       
        public   List<Session> TrackSessions { get; set; }

        public Track ( )
        {

            TrackSessions = new List<Session>();

        }

        internal void CopyMasterSessions(List<Session> sessions)
        {
            foreach (Session aSession in sessions)
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
