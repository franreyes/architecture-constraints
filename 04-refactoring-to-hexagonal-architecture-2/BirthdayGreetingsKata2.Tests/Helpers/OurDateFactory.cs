using System;
using BirthdayGreetingsKata2.Domain;

namespace BirthdayGreetingsKata2.Tests.helpers;

public static class OurDateFactory
{
    private const string DateFormat = "yyyy/MM/dd";
    
    public static OurDate OurDateFromString(string dateAsString) {
        return new OurDate(
            DateTime.ParseExact(dateAsString, DateFormat, null)
        );
    }
}