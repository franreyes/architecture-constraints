using System;

namespace BirthdayGreetingsKata2.Business;

public class CannotReadEmployeesException : Exception
{
    public CannotReadEmployeesException(string cause, Exception exception) : base(cause, exception)
    {
    }
}