using System;
using BirthdayGreetingsKata2.Business;

namespace BirthdayGreetingsKata2.Infrastructure.Repositories;

public class DateRepresentation
{
    private const string DateFormat = "yyyy/MM/dd";
    private readonly string _dateAsString;

    public DateRepresentation(string dateAsString)
    {
        _dateAsString = dateAsString;
    }

    public OurDate ToDate()
    {
        return new OurDate(
            DateTime.ParseExact(_dateAsString, DateFormat, null)
        );
    }
}