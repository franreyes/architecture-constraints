using System;
using BirthdayGreetingsKata2.Domain;
using BirthdayGreetingsKata2.Infrastructure;
using BirthdayGreetingsKata2.Infrastructure.Repositories;

namespace BirthdayGreetingsKata2.Application;

public class App
{
    private const string EmployeesFilePath = "employee_data.txt";
    private const string SenderEmailAddress = "sender@here.com";
    private const string Host = "localhost";
    private const int SmtpPort = 25;

    static void Main(string[] args)
    {
        var service = new BirthdayService(
            new FileEmployeesRepository(EmployeesFilePath), new EmailGreetingsSender(Host, SmtpPort, SenderEmailAddress));
        try
        {
            var today = new OurDate(new DateTime());
            service.SendGreetings(today);
        }
        catch (Exception e)
        {
            Console.Write(e.StackTrace);
        }
    }
}