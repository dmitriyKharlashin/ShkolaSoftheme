namespace MobileProvider
{
    public interface ILogger
    {
        void AddMessageEvent(int status, int partisipant1, int partisipant2);

        void AddCallEvent(int status, int partisipant1, int partisipant2);

    }
}