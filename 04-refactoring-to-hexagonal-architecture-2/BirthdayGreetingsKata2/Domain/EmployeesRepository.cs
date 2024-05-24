using System.Collections.Generic;

namespace BirthdayGreetingsKata2.Domain;

public interface IEmployeesRepository
{
    List<Employee> GetAll();
}