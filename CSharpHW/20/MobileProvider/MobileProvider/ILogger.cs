namespace MobileProvider
{
    public interface ILogger
    {
        void AddMessageEvent(int status, int sender, int reciever);

        void AddCallEvent(int status, int sender, int partisipant2);

    }
}