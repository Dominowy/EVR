public class EmployeeService : IEmployeeService
{
    public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
    {
        return CountFreeDaysForEmployee(employee, vacations, vacationPackage) > 0;
    }

    public int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
    {
        var currentYear = DateTime.Now.Year;
        var today = DateTime.Today;

        var usedDays = vacations
            .Where(v => v.EmployeeId == employee.Id &&
                        (v.DateSince.Year == currentYear || v.DateUntil.Year == currentYear) &&
                        v.DateUntil < today)
            .Sum(v => (v.DateUntil - v.DateSince).TotalDays + 1);

        int freeDays = vacationPackage.GrantedDays - (int)usedDays;
        return freeDays < 0 ? 0 : freeDays;
    }
}