namespace BirthdayGreetingsKata2.Business;

public class Greeting
{
    public string Header { get; }
    public string Content { get; }

    private Greeting(string header, string content)
    {
        Header = header;
        Content = content;
    }

    public static Greeting ForBirthdayOf(Employee employee)
    {
        var content = $"Happy Birthday, dear {employee.FirstName}!";
        var header = "Happy Birthday!";
        return new Greeting(header, content);
    }
}