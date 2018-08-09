using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.AdditionModels
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string description)
        {
            ErrorDescription = description;
            ErrorCode = 500;
        }

        public Error(int errorCode, string description)
        {
            ErrorDescription = description;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>The error description.</value>

        public string ErrorDescription { get; set; }

        public override string ToString()
        {
            return $"Status code: {ErrorCode}, \nDescription:{ErrorDescription}\n";
        }
    }
}
