namespace MobileProvider
{
    public interface ILogger
    {
        void AddMessageEvent(LoggerStatusTypes status, int sender, int reciever);

        void AddCallEvent(LoggerStatusTypes status, int sender, int partisipant2);

    }
}