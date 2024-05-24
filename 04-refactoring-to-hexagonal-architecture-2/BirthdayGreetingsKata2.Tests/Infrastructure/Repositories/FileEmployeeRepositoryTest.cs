using System.Collections.Generic;
using BirthdayGreetingsKata2.Domain;
using BirthdayGreetingsKata2.Infrastructure.Repositories;
using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.helpers.OurDateFactory;


namespace BirthdayGreetingsKata2.Tests.Infrastructure.Repositories;

public class FileEmployeeRepositoryTest
{
    [Test]
    public void Get_Employees_From_File()
    {
        var employeesRepository = new FileEmployeesRepository("Application/employee_data.txt");

        var employees = employeesRepository.GetAll();

        var expectedEmployees = new List<Employee>
        {
            new("John", "Doe", OurDateFromString("1982/10/08"), "john.doe@foobar.com"),
            new("Mary", "Ann", OurDateFromString("1975/03/11"), "mary.ann@foobar.com")
        };
        Assert.That(employees, Is.EquivalentTo(expectedEmployees));
    }

    [Test]
    public void Fails_When_The_File_Does_Not_Exist()
    {
        var employeesRepository = new FileEmployeesRepository("non-existing.file");

        var exception =
            Assert.Throws<CannotReadEmployeesException>(() => employeesRepository.GetAll());

        Assert.That(exception, Has.Message.Contains("cannot loadFrom file"));
        Assert.That(exception, Has.Message.Contains("non-existing.file"));
    }

    [Test]
    public void Fails_When_The_File_Does_Not_Have_The_Necessary_Fields()
    {
        var employeesRepository =
            new FileEmployeesRepository("Infrastructure/Repositories/wrong_data__wrong-date-format.csv");

        var exception =
            Assert.Throws<CannotReadEmployeesException>(() => employeesRepository.GetAll());

        Assert.That(exception, Has.Message.Contains("Badly formatted employee birth date in"));
    }
}