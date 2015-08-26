using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SpaceWars
{
    class PerformanceTimer
    {
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long lpFrequency);
    }
}
