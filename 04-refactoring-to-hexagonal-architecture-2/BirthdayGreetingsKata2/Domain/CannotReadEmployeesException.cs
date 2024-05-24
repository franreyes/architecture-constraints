using System;

namespace BirthdayGreetingsKata2.Domain;

public class CannotReadEmployeesException : Exception
{
    public CannotReadEmployeesException(string cause, Exception exception) : base(cause, exception)
    {
    }
}