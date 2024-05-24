using System.Collections.Generic;

namespace BirthdayGreetingsKata2.Domain;

public interface IGreetingsSender
{
    void Send(List<GreetingMessage> messages);
}