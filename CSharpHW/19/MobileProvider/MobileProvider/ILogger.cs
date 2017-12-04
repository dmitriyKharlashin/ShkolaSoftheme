namespace MobileProvider
{
    public interface ILogger
    {
        void AddMessageEvent(int status, string partisipant1, string partisipant2);

        void AddCallEvent(int status, string partisipant1, string partisipant2);

    }
}