using System;
using System.Collections.Generic;
using BirthdayGreetingsKata2.Infrastructure;

namespace BirthdayGreetingsKata2.Domain;

public class BirthdayService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IGreetingsSender _greetingsSender;

    public BirthdayService(IEmployeesRepository employeesRepository, IGreetingsSender greetingsSender)
    {
        _employeesRepository = employeesRepository;
        _greetingsSender = greetingsSender;
        var a = new DateTime();
    }

    public void SendGreetings(OurDate date)
    {
        _greetingsSender.Send(GreetingMessagesFor(EmployeesHavingBirthday(date)));
    }

    private static List<GreetingMessage> GreetingMessagesFor(IEnumerable<Employee> employees)
    {
        return GreetingMessage.GenerateForSome(employees);
    }

    private IEnumerable<Employee> EmployeesHavingBirthday(OurDate today)
    {
        return _employeesRepository.GetAll()
            .FindAll(employee => employee.IsBirthday(today));
    }
}