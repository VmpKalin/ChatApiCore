using System;

namespace Chat.Data.Interfaces.IEntities
{
    public interface IMessageEntity
    {
        string Data { get; set; }
        string Id { get; set; }
        DateTime Time { get; set; }
    }
}