using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileProvider
{
    [Serializable]
    public class CallLogger : ILogger
    {
        private readonly List<CallLog> _eventsList = new List<CallLog>();

        public List<CallLog> EventsList
        {
            get { return _eventsList; }
        }

        public void AddCallEvent(LoggerStatusTypes status, int sender, int reciever)
        {
            EventsList?.Add(new CallLog(status, sender, reciever, ActivityTypes.Call));
        }

        public void AddMessageEvent(LoggerStatusTypes status, int sender, int reciever)
        {
            EventsList?.Add(new CallLog(status, sender, reciever, ActivityTypes.Message));
        }

        public void ShowTopSenderList()
        {
            ShowTopSenderList(5);
        }

        public void ShowTopSenderList(int limit)
        {
            var topList = EventsList
                .GroupBy(p => p.Sender)
                .Select(g => new { Sender = g.Key, Events = g.Select(p => p), Sum = g.Sum(p => p.ActivityType == ActivityTypes.Call ? 1 : 0.5) })
                .OrderByDescending(g => g.Sum)
                .Take(limit)
                .ToArray();

            Console.WriteLine();
            Console.WriteLine("Top sender rate:");
            foreach (var listItem in topList)
            {
                Console.WriteLine($"Sender {listItem.Sender} - {listItem.Sum}");
            }
            Console.WriteLine();
        }
        public void ShowTopRecieverList()
        {
            ShowTopRecieverList(5);
        }

        public void ShowTopRecieverList(int limit)
        {
            var topList = EventsList
                .GroupBy(p => p.Reciever)
                .Select(g => new { Reciever = g.Key, Sum = g.Sum(p => p.ActivityType == ActivityTypes.Call ? 1 : 0.5) })
                .OrderByDescending(g => g.Sum)
                .Take(limit)
                .ToArray();

            Console.WriteLine();
            Console.WriteLine("Top reciever rate:");
            foreach (var listItem in topList)
            {
                Console.WriteLine($"Reciever {listItem.Reciever} - {listItem.Sum}");
            }
            Console.WriteLine();
        }
    }
}