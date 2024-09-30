﻿namespace GrudgeBookMvc.src.Model.Domain
{
    public class StatusParseException : Exception
    {
        public StatusParseException()
        {

        }
        public StatusParseException(string message)
            : base(message)
        {

        }
        public StatusParseException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}