using System.Collections.Generic;

namespace BirthdayGreetingsKata2.Business;

public interface IGreetingsSender
{
    void Send(List<GreetingMessage> messages);
}