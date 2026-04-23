namespace Demo.Services.Interfaces
{
    public interface IMessagePublisher
    {
        void PublishUserRegistered(object message);
    }
}
