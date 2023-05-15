using MediatR;

namespace PurchaseAPI.CQRS.Notifications
{
    public class PurchaseAddedNotification : INotification
    {
        public string? Message { get; set; }

        public PurchaseAddedNotification(string Message)
        {
            this.Message = Message;
        }
    }
}
