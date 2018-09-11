﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.AdditionModels
{
    public class Response<T>
    {
        public T Data { get; set; }

        public Error Error { get; set; }
    }
}
