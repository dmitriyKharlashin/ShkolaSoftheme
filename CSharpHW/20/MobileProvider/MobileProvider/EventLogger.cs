using System;

namespace MobileProvider
{
    public class EventLogger : ILogger
    {
        public void AddMessageEvent(int status, int sender, int reciever)
        {
            switch (status)
            {
                case (int) LoggerStatusTypes.Success:
                    ShowLog(status, PrepareSmsSuccessLog(sender, reciever));
                    break;
                default:
                    ShowLog(status, PrepareSmsErrorLog(sender, reciever));
                    break;
            }
        }

        public void AddCallEvent(int status, int sender, int partisipant2)
        {
            switch (status)
            {
                case (int)LoggerStatusTypes.Success:
                    ShowLog(status, PrepareCallSuccessLog(sender, partisipant2));
                    break;
                default:
                    ShowLog(status, PrepareCallErrorLog(sender, partisipant2));
                    break;
            }
        }

        private string PrepareSmsSuccessLog(int partisipant1, int partisipant2)
        {
            return $"Message: from {partisipant1} to {partisipant2} was successfully sent";
        }

        private string PrepareCallSuccessLog(int partisipant1, int partisipant2)
        {
            return $"Call: from {partisipant1} to {partisipant2} was successfully started";
        }

        private string PrepareSmsErrorLog(int partisipant1, int partisipant2)
        {
            return $"Message: from {partisipant1} to {partisipant2} was not delivered!";
        }

        private string PrepareCallErrorLog(int partisipant1, int partisipant2)
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