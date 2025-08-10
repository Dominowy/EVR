using NUnit.Framework;
using System;
using System.Collections.Generic;

public class EmployeeServiceTest
{
    [Test]
    public void employee_can_request_vacation()
    {
        var employee = new Employee { Id = 1 };
        var vacations = new List<Vacation>();
        var vacationPackage = new VacationPackage { GrantedDays = 10 };

        var canRequest = IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);

        Assert.IsTrue(canRequest);
    }

    [Test]
    public void employee_cant_request_vacation()
    {
        var employee = new Employee { Id = 1 };
        var vacations = new List<Vacation>
        {
            new Vacation
            {
                EmployeeId = 1,
                DateSince = DateTime.Now.AddDays(-10),
                DateUntil = DateTime.Now.AddDays(-1),
                NumberOfHours = 80,
                IsPartialVacation = false
            }
        };
        var vacationPackage = new VacationPackage { GrantedDays = 5 };

        var canRequest = IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);

        Assert.IsFalse(canRequest);
    }

}