using System.Collections.Generic;
using System.Net.Mail;
using BirthdayGreetingsKata2.Domain;
using BirthdayGreetingsKata2.Infrastructure;
using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.helpers.OurDateFactory;

namespace BirthdayGreetingsKata2.Tests.Infrastructure;

public class EmailGreetingsSenderTest
{
    private const int SmtpPort = 25;
    private const string SmtpHost = "localhost";
    private const string From = "sender@here.com";
    private List<MailMessage> _messagesSent;
    private EmailGreetingsSenderForTesting _greetingsSender;

    [SetUp]
    public void SetUp()
    {
        _messagesSent = new List<MailMessage>();
        _greetingsSender = new EmailGreetingsSenderForTesting(_messagesSent);
    }
    
    [Test]
    public void Send_Mail_When_There_Are_Greetings()
    {
        var employees = new List<Employee> {
            new("John", "Doe", OurDateFromString("1982/10/08"), "john.doe@foobar.com"),
        };
        var greetings = GreetingMessage.GenerateForSome(employees);
        
        _greetingsSender.Send(greetings);
        
        Assert.That(_messagesSent.Count, Is.EqualTo(1));
        var message = _messagesSent[0];
        Assert.That(message.Body, Is.EqualTo("Happy Birthday, dear John!"));
        Assert.That(message.Subject, Is.EqualTo("Happy Birthday!"));
        Assert.That(message.To.Count, Is.EqualTo(1));
        Assert.That(message.To[0].Address, Is.EqualTo("john.doe@foobar.com"));
    }
    
    [Test]
    public void No_Send_Mail_When_There_Are_No_Greetings()
    {
        var greetings = GreetingMessage.GenerateForSome(new List<Employee>());
        
        _greetingsSender.Send(greetings);
        
        Assert.That(_messagesSent, Is.Empty);
    }

    private class EmailGreetingsSenderForTesting: EmailGreetingsSender
    {
        private readonly List<MailMessage> _messagesSent;

        public EmailGreetingsSenderForTesting(List<MailMessage> messagesSent) : 
            base(SmtpHost, SmtpPort, From)
        {
            _messagesSent = messagesSent;
        }

        protected override void SendMessage(MailMessage msg, SmtpClient smtpClient)
        {
            _messagesSent.Add(msg);
        }
    }
}