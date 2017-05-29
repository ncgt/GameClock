using GrandTheftMultiplayer.Server.API;
using System;
using System.Timers;

namespace GameClock
{
    public class GameClock : Script
    {
        static double racio = 4;
        static TimeSpan FullDay = new TimeSpan(24, 0, 0);//How can I simplify this?
        TimeSpan RClock = new TimeSpan(5, 20, 00); //realTime when shutdown
        Timer updateConsole;

        public GameClock() { API.onResourceStart += API_onResourceStart; }

        public void SetTime() {
            
            RClock = RClock.Add(TimeSpan.FromSeconds(1));
            TimeSpan GClock = TimeSpan.FromSeconds((RClock.TotalSeconds * FullDay.TotalSeconds) / (FullDay.TotalSeconds / racio));
            API.setTime(GClock.Hours, GClock.Minutes);}

        private void API_onResourceStart()
        {updateConsole = new Timer(){Interval = 1000}; updateConsole.Elapsed += Elapsed_Console_Timer; updateConsole.Enabled = true;}

        private void Elapsed_Console_Timer(object sender, ElapsedEventArgs e){SetTime();}
    }
}