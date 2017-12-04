using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileProvider
{
    public class CallLogger : ILogger
    {
        private readonly List<Dictionary<string, string>> _eventsList = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> EventsList
        {
            get { return _eventsList; }
        }

        public void AddCallEvent(int status, string partisipant1, string partisipant2)
        {
            EventsList?.Add(new Dictionary<string, string>
            {
                { "status", status == 0 ? "success" : "error" },
                { "reciepient", partisipant2 },
                { "sender", partisipant1 },
                { "connection_type", "Call" },
            });
        }

        public void AddMessageEvent(int status, string partisipant1, string partisipant2)
        {
            EventsList?.Add(new Dictionary<string, string>
            {
                { "status", status == 0 ? "success" : "error" },
                { "reciepient", partisipant2 },
                { "sender", partisipant1 },
                { "connection_type", "Sms" },
            });
        }

        public void ShowTopSenderList()
        {
            ShowTopSenderList(5);
        }

        public void ShowTopSenderList(int limit)
        {
            KeyValuePair<string, double>[] sortedList = GetTopList("sender", limit);

            Console.WriteLine();
            Console.WriteLine("Top sender rate:");
            foreach (KeyValuePair<string, double> listItem in sortedList)
            {
                Console.WriteLine($"Sender {listItem.Key} - {listItem.Value}");
            }
            Console.WriteLine();
        }
        public void ShowTopRecieverList()
        {
            ShowTopRecieverList(5);
        }

        public void ShowTopRecieverList(int limit)
        {
            KeyValuePair<string, double>[] sortedList = GetTopList("reciepient", limit);

            Console.WriteLine();
            Console.WriteLine("Top reciepient rate:");
            foreach (KeyValuePair<string, double> listItem in sortedList)
            {
                Console.WriteLine($"Sender {listItem.Key} - {listItem.Value}");
            }
            Console.WriteLine();
        }

        public KeyValuePair<string, double>[] GetTopList(string type, int limit)
        {
            IGrouping<string, Dictionary<string, string>>[] logsList = (from eventItem in EventsList
                group eventItem by eventItem[type]).ToArray();

            Dictionary<string, double> nonSortedList = CalculateEventsRate(logsList);

            var sortedList = nonSortedList.OrderByDescending(e => e.Value).Take(limit).ToArray();

            return sortedList;
        }

        private Dictionary<string, double> CalculateEventsRate(IGrouping<string, Dictionary<string, string>>[] logsList)
        {
            var nonSortedList = new Dictionary<string, double>();

            foreach (var logsItem in logsList)
            {
                double rate = 0;
                foreach (var logsItemData in logsItem)
                {
                    if (logsItemData["connection_type"] == "Call")
                    {
                        rate += 1;
                    }
                    else
                    {
                        rate += 0.5;
                    }
                }
                nonSortedList.Add(logsItem.Key, rate);
            }

            return nonSortedList;
        }
    }
}