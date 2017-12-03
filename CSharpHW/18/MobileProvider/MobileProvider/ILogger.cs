namespace MobileProvider
{
    public interface ILogger
    {
        void GetMessageEvent(int status, string partisipant1, string partisipant2);

        void GetCallEvent(int status, string partisipant1, string partisipant2);


    }
}