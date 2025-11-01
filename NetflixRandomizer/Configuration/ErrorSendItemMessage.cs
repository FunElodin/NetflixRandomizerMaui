using CommunityToolkit.Mvvm.Messaging.Messages;

namespace NetflixRandomizer
{
    public class ErrorSendItemMessage : ValueChangedMessage<string>
    {
        public ErrorSendItemMessage(string value) : base(value)
        {
        }
    }
}
