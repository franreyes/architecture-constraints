using System.Collections.Generic;
using BirthdayGreetingsKata2.Domain;

namespace BirthdayGreetingsKata2.Infrastructure.Repositories;

public class FileEmployeesRepository : IEmployeesRepository
{
    private readonly string _path;

    public FileEmployeesRepository(string path)
    {
        _path = path;
    }

    public List<Employee> GetAll()
    {
        var employeesFile = EmployeesFile.LoadFrom(_path);
        return employeesFile.ExtractEmployees();
    }
}