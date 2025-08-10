public class EmployeeHierarchyService() : IEmployeeHierarchyService
{
    private List<EmployeeStructure> _structure = [];

    public List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees)
    {
        var employeeDict = employees.ToDictionary(e => e.Id);

        foreach (var employee in employees)
        {
            AddSuperiors(employee.Id, employee.SuperiorId, 1, employeeDict);
        }

        return _structure;
    }

    private void AddSuperiors(int employeeId, int? superiorId, int rank, Dictionary<int, Employee> employeeDict)
    {
        if (!superiorId.HasValue)
            return;

        _structure.Add(new EmployeeStructure
        {
            EmployeeId = employeeId,
            SuperiorId = superiorId.Value,
            Rank = rank
        });

        if (employeeDict.TryGetValue(superiorId.Value, out var superior))
        {
            AddSuperiors(employeeId, superior.SuperiorId, rank + 1, employeeDict);
        }
    }

    public int? GetSupieriorRowOfEmployee(int employeeId, int superiorId)
    {
        var relation = _structure.FirstOrDefault(s => s.EmployeeId == employeeId && s.SuperiorId == superiorId);
        return relation?.Rank;
    }
}
