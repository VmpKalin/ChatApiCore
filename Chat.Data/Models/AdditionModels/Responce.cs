using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.AdditionModels
{
    public class Responce<T>
    {
        public T Data { get; set; }

        public Error Error { get; set; }
    }
}
