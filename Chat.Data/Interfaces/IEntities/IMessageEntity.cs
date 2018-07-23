namespace Chat.Data.Interfaces.IEntities
{
    public interface IMessageEntity
    {
        string Data { get; set; }
        string Id { get; set; }
        string SenderId { get; set; }
        long Time { get; set; }
    }
}