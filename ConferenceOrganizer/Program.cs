using ConferenceOrganizer.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using Newtonsoft.Json;

namespace ConferenceOrganizer
{
    class Program
    {
        //
       static List<Talk> ListOfAllTalks = new List<Talk>();
       static List<Session> ListOfSessions = new List<Session>();

        static void Main(string[] args) 
        {

            Talk.NetworkMinStart = new DateTime(2020, 10, 27, 16, 0, 0);
            Talk.NetworkMaxStart = new DateTime(2020, 10, 27, 17, 0, 0);
            Talk talk = new Talk("Networking Event", 15);

            ListOfAllTalks.Add(talk);
            ListOfAllTalks.Add(talk);
           


            string TalksPath = "C:\\Conference.txt";
            LoadAllTalks(TalksPath);



            CreateASession( new DateTime(2020, 10, 27, 9, 0, 0), new DateTime(2020, 10, 27, 12, 0, 0) );
            CreateASession(new DateTime(2020, 10, 27, 13, 0, 0), new DateTime(2020, 10, 27, 17, 0, 0));
            EventsManager Event1 = new EventsManager(ListOfAllTalks, ListOfSessions);
            Event1.CreateEvent();
            Event1.PrintTracks();

       



        }



        public static void LoadAllTalks(string TalksPath)
        {
          
            foreach (string value in TxtReader.ReadTxtFile(TalksPath))
            {
                Talk aTalk = new Talk(value, TxtReader.FindDuration(value));
                ListOfAllTalks.Add(aTalk);



            }

            

        }
        
        public static void CreateASession(DateTime startTime , DateTime endTime )
        {
             Session NewSession = new Session(startTime, endTime);
                ListOfSessions.Add(NewSession);

      
        }

        

    }
}
