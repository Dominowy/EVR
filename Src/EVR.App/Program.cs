using EVR.Application;
using EVR.Domain;
using EVT.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace EVR.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

            IApplicationDbContext context = new ApplicationDbContext(options);

            context.Set<Employee>().AddRange(new List<Employee>
            {
                new() { Id = 1, Name = "Jan Kowalski" },
                new() { Id = 2, Name = "Kamil Nowak", SuperiorId = 1 },
                new() { Id = 3, Name = "Anna Mariacka", SuperiorId = 1 },
                new() { Id = 4, Name = "Andrzej Abacki", SuperiorId = 2 }
            });

            await context.SaveChangesAsync();

            var emplyees = await context.Set<Employee>().ToListAsync();

            var service = new EmployeeHierarchyService();
            service.FillEmployeesStructure(emplyees);

            Console.WriteLine(service.GetSupieriorRowOfEmployee(2, 1));
            Console.WriteLine(service.GetSupieriorRowOfEmployee(4, 3));
            Console.WriteLine(service.GetSupieriorRowOfEmployee(4, 1));
        }
    }
}
