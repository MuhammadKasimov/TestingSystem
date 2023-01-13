using System;

namespace TestingSystem.Service.Exceptions
{
    public class TestingSystemException : Exception
    {
        public int Code { get; set; }
        public TestingSystemException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
