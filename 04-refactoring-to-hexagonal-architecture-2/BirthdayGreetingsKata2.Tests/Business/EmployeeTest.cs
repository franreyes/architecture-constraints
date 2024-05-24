using BirthdayGreetingsKata2.Business;
using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.Helpers.OurDateFactory;

namespace BirthdayGreetingsKata2.Tests.Business;

public class EmployeeTest
{
    [Test]
    public void Test_Birthday()
    {
        var employee = new Employee("foo", "bar", OurDateFromString("1990/01/31"), "a@b.c");

        Assert.That(employee.IsBirthday(OurDateFromString("2008/01/30")), Is.False, "no birthday");
        Assert.That(employee.IsBirthday(OurDateFromString("2008/01/31")), Is.True, "birthday");
    }
}