using EVR.Domain;

namespace EVR.Application
{
    public interface IEmployeeHierarchyService
    {
        List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees);
        int? GetSupieriorRowOfEmployee(int employeeId, int superiorId);
    }
}