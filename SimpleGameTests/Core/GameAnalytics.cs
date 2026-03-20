using System.Collections.Generic;

namespace SimpleGameTests.Core
{
    public class GameAnalytics
    {
        public List<string> Events { get; } = new List<string>();

        public void TrackEvent(string eventName)
        {
            Events.Add(eventName);
        }
    }
}