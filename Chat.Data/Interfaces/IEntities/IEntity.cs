using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Interfaces.IEntities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
