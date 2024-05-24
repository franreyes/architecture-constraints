using System.Collections.Generic;
using System.Linq;
using BirthdayGreetingsKata2.Application;
using BirthdayGreetingsKata2.Business;
using NSubstitute;
using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.Helpers.OurDateFactory;

namespace BirthdayGreetingsKata2.Tests.Application;

public class AcceptanceTest
{
    private BirthdayService _service;
    private IEmployeesRepository _employeesRepository;
    private IGreetingsSender _greetingsSender;

    [SetUp]
    public void SetUp()
    {
        _employeesRepository = Substitute.For<IEmployeesRepository>();
        _greetingsSender = Substitute.For<IGreetingsSender>();
        _service = new BirthdayService(_employeesRepository, _greetingsSender);
    }

    [Test]
    public void Base_Scenario()
    {
        var today = OurDateFromString("2008/10/08");
        _employeesRepository.GetAll().Returns(new List<Employee>()
        {
            new("John", "Doe", OurDateFromString("1982/10/08"), "john.doe@foobar.com"),
            new("Mary", "Ann", OurDateFromString("1975/03/11"), "mary.ann@foobar.com")
        });

        _service.SendGreetings(today);

        _greetingsSender.Received(1).Send(Arg.Is<List<GreetingMessage>>(message =>
            message.Count.Equals(1) &&
            message.First().Text().Equals("Happy Birthday, dear John!") &&
            message.First().Subject().Equals("Happy Birthday!") &&
            message.First().To().Equals("john.doe@foobar.com")
        ));
    }

    [Test]
    public void Will_Not_Send_Emails_When_Nobodies_Birthday()
    {
        var today = OurDateFromString("2008/01/01");
        _employeesRepository.GetAll().Returns(new List<Employee>
            {
                new("Mary", "Ann", OurDateFromString("1975/03/11"), "mary.ann@foobar.com")
            }
        );

        _service.SendGreetings(today);

        _greetingsSender.Received(1).Send(Arg.Is<List<GreetingMessage>>(message =>
            message.Count.Equals(0)
        ));
    }
}