using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAPractice
{
    public static class Log
    {
        private static List<string> log = new List<string>();

        public static void Write(string entry)
        {
            log.Add(entry);
        }

        public static string GetLastNString(int n)
        {
            string[] entries = GetLastN(n);
            string result = "";

            int i = 0;
            int t = entries.Length;
            while (i < t)
            {
                result += entries[i] + "\n";
                i++;
            }

            return result;
        }

        public static string[] GetLastN(int n)
        {
            string[] entries = new string[n];

            if (n > log.Count)
                n = log.Count;

            int t = log.Count;
            while (n > 0)
            {
                entries[n - 1] = log[t - n];
                n--;
            }

            return entries;
        }
    }
}
