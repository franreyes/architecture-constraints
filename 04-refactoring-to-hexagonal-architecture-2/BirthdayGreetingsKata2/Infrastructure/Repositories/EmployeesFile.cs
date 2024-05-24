using System;
using System.Collections.Generic;
using System.IO;
using BirthdayGreetingsKata2.Domain;

namespace BirthdayGreetingsKata2.Infrastructure.Repositories;

public class EmployeesFile
{
    private readonly StreamReader _streamReader;

    private EmployeesFile(StreamReader streamReader)
    {
        _streamReader = streamReader;
    }

    public static EmployeesFile LoadFrom(string path)
    {
        return new EmployeesFile(FileReader.ReadSkippingHeader(path));
    }

    public List<Employee> ExtractEmployees()
    {
        var employees = new List<Employee>();
        while (_streamReader.ReadLine() is { } line)
        {
            var representation = new EmployeeCsvRepresentation(line);
            employees.Add(representation.ConvertToEmployee());
        }

        _streamReader.Close();
        return employees;
    }

    private class EmployeeCsvRepresentation
    {
        private readonly string _content;
        private readonly string[] _tokens;

        public EmployeeCsvRepresentation(string content)
        {
            _content = content;
            _tokens = content.Split(", ");
        }

        public Employee ConvertToEmployee()
        {
            return new Employee(FirstName(), LastName(), BirthDate(), Email());
        }

        private string LastName()
        {
            return _tokens[0];
        }

        private string Email()
        {
            return _tokens[3];
        }

        private string FirstName()
        {
            return _tokens[1];
        }

        private OurDate BirthDate()
        {
            try
            {
                return new DateRepresentation(DateAsString()).ToDate();
            }
            catch (FormatException exception)
            {
                throw new CannotReadEmployeesException(
                    $"Badly formatted employee birth date in: '{_content}'",
                    exception
                );
            }
        }

        private string DateAsString()
        {
            return _tokens[2];
        }
    }

    private static class FileReader
    {
        public static StreamReader ReadSkippingHeader(string path)
        {
            try
            {
                return SkipHeader(ReadFile(path));
            }
            catch (IOException exception)
            {
                throw new CannotReadEmployeesException(
                    $"cannot loadFrom file = '{path}'",
                    exception
                );
            }
        }

        private static StreamReader ReadFile(string path)
        {
            var reader = new StreamReader(path);
            return reader;
        }

        private static StreamReader SkipHeader(StreamReader stream)
        {
            stream.ReadLine();
            return stream;
        }
    }
}