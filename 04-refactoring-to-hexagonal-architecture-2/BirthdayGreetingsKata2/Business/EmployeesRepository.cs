using System.Collections.Generic;

namespace BirthdayGreetingsKata2.Business;

public interface IEmployeesRepository
{
    List<Employee> GetAll();
}