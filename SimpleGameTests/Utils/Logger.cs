using System.Collections.Generic;

namespace SimpleGameTests.Utils
{
    public class Logger
    {
        public List<string> Logs { get; } = new List<string>();

        public void Log(string message)
        {
            Logs.Add(message);
        }
    }
}