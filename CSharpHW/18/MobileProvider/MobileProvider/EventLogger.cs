using System;

namespace MobileProvider
{
    public class EventLogger : ILogger
    {
        public void AddMessageEvent(int status, string partisipant1, string partisipant2)
        {
            switch (status)
            {
                case (int) LoggerStatusTypes.Success:
                    ShowLog(status, PrepareSmsSuccessLog(partisipant1, partisipant2));
                    break;
                default:
                    ShowLog(status, PrepareSmsErrorLog(partisipant1, partisipant2));
                    break;
            }
        }

        public void AddCallEvent(int status, string partisipant1, string partisipant2)
        {
            switch (status)
            {
                case (int)LoggerStatusTypes.Success:
                    ShowLog(status, PrepareCallSuccessLog(partisipant1, partisipant2));
                    break;
                default:
                    ShowLog(status, PrepareCallErrorLog(partisipant1, partisipant2));
                    break;
            }
        }

        private string PrepareSmsSuccessLog(string partisipant1, string partisipant2)
        {
            return $"Message: from {partisipant1} to {partisipant2} was successfully sent";
        }

        private string PrepareCallSuccessLog(string partisipant1, string partisipant2)
        {
            return $"Call: from {partisipant1} to {partisipant2} was successfully started";
        }

        private string PrepareSmsErrorLog(string partisipant1, string partisipant2)
        {
            return $"Message: from {partisipant1} to {partisipant2} was not delivered!";
        }

        private string PrepareCallErrorLog(string partisipant1, string partisipant2)
        {
            return $"Call: from {partisipant1} to {partisipant2} was not started!";
        }

        private void ShowLog(int status, string message)
        {
            Console.ForegroundColor = status == (int) LoggerStatusTypes.Success ?
                (ConsoleColor) LoggerColorTypes.Info :
                (ConsoleColor) LoggerColorTypes.Alert;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}