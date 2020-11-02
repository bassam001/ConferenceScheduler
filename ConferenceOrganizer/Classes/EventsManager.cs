using ConferenceOrganizer.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;

using Newtonsoft.Json;

namespace ConferenceOrganizer
{
    public class EventsManager
    {
        public List<Talk> AllTalksToJson { get; set; }
        public List<Talk> AllTalks { get; set; }
        public List<Session> MasterSessions { get; set; }
        public List<Track> Tracks { get; set; }
        public EventsManager(List<Talk> allTalks, List<Session> sessions)
        {
            this.AllTalks = allTalks;
            this.MasterSessions = sessions;
            Tracks = new List<Track>();

            Track FirstTrack = new Track(MasterSessions);
           
            Tracks.Add(FirstTrack);
            AllTalksToJson = new List<Talk>();

        }

        internal void CreateEvent()
        {
            foreach (Talk aTalk in AllTalks)
            {
                bool isAdded = false;
                foreach (Track aTrack in Tracks)
                {
                    isAdded = aTrack.AddTalk(aTalk);
                    if (isAdded)
                        break;
                }

                //als de talk niet wordt toegevoegd, maak een nieuw track en voeg die talk toe
                if (!isAdded)
                    CreateNewTrackAndAddTalk(aTalk);
            }
        }

        //nieuw track komt met dezelfde sessions die al werden gedefinieerd in prograam (morning en afternoon)
        private void CreateNewTrackAndAddTalk(Talk aTalk)
        {
            Track newTrack = new Track(MasterSessions);
            
            Tracks.Add(newTrack); 
            newTrack.AddTalk(aTalk);
        }


        public void CreateJsonFile()
        { }
        public void PrintTracks()
        {
            int indx = 1;
            foreach (Track aTrack in Tracks)
            {

                Console.WriteLine("Track " + indx);
                Console.WriteLine();
                foreach (Session aSession in aTrack.TrackSessions)
                {
                    foreach (Talk aTalk in aSession.SessionTalks)
                    {
                        AllTalksToJson.Add(aTalk);
                        Console.WriteLine(aTalk);
                        
                    }
                        

                }

                indx++;
                Console.WriteLine();
               

            }

            string json = JsonConvert.SerializeObject(AllTalksToJson.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"D:\\ConferenceJson", json);

        }

    }
}
