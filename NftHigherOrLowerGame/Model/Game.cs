using NftHigherOrLowerGame.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NftHigherOrLowerGame.Model
{
    public static class Game
    {
        public static GameTimer GameTime;
        public static Random rand = new();

        public static void StartTimer()
        {
            GameTime.Start();
        }

        public static void StopTimer() { }

        public static void Higher()
        {
            StartTimer();
            Debug.WriteLine("Higher");
        }

        public static void Lower()
        {
            StopTimer();
            Debug.WriteLine("Lower");
        }

        public static void RegisterTimer(GameTimer timer)
        {
            GameTime = timer;
        }
    }
}
