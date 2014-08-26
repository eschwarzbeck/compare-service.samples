using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CompareServer.WebAdmin
{
    public class UnityControllerFactoryException : Exception
    {
        public UnityControllerFactoryException()
        {
        }

        public UnityControllerFactoryException(string message)
            : base(message)
        {
        }

        public UnityControllerFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnityControllerFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    public class ControllerTypeNullException : UnityControllerFactoryException
    {
        public ControllerTypeNullException()
        {
        }

        public ControllerTypeNullException(string message)
            : base(message)
        {
        }

        public ControllerTypeNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ControllerTypeNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}