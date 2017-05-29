using GrandTheftMultiplayer.Server.API;
using System;
using System.Timers;
using System.Collections.Generic;

namespace GameClock
{
    public class GameClock : Script
    {
        static double racio = 4;
        TimeSpan Clock = new TimeSpan(0, 0, 0); //serverSidedTime (int hours, int minutes, int seconds)
        TimeSpan AddOnTime = new TimeSpan(0, 0, 1); //need to calculate this value
        Timer updateConsole;

        public GameClock() { API.onResourceStart += API_onResourceStart; }



        public KeyValuePair<TimeSpan, TimeSpan> FindMinute(double racio)
        {
            TimeSpan day = new TimeSpan(1, 0, 0, 0);
            TimeSpan minute = TimeSpan.FromSeconds(60);
            TimeSpan IntervalToAdd = new TimeSpan(0, 0, 0);
            TimeSpan TimeToAdd = new TimeSpan(0, 0, 0);
            //double duration = 60;
            do
            {
                IntervalToAdd = TimeSpan.FromSeconds(((1 * (day.TotalSeconds / racio) / day.TotalSeconds)));
                TimeToAdd = TimeSpan.FromSeconds(((1 * (day.TotalSeconds) / (day.TotalSeconds / racio))));
            } while (TimeToAdd <= minute);

            return new KeyValuePair<TimeSpan, TimeSpan>(IntervalToAdd, TimeToAdd);
        }

        private void API_onResourceStart()
        {

            API.setTime(Clock.Hours, Clock.Minutes);
            //API.consoleOutput("" + FindMinute(racio) + "   ");
            updateConsole = new Timer()
            {
                Interval = FindMinute(racio).Key.Milliseconds //milliseconds
            };
            updateConsole.Elapsed += Elapsed_Console_Timer; updateConsole.Enabled = true;
            API.consoleOutput("IntervalToAdd: " + FindMinute(racio).Key + " TimeToAdd: " + FindMinute(racio).Value);
        }

        private void Elapsed_Console_Timer(object sender, ElapsedEventArgs e)
        {
            Clock = Clock.Add(FindMinute(racio).Value);
            API.setTime(Clock.Hours, Clock.Minutes);
            API.consoleOutput("G:" + Clock);
        }
    }
}
//NCUNHA