﻿using NftHigherOrLowerGame.Components;
using System.Diagnostics;

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

        public static void StopTimer()
        {
            GameTime.Stop();
        }

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
