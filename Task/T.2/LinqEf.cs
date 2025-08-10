
// 1. Lista pracowników z zespołu ".NET" z urlopem w 2019
var employeesWithVacation2019 = 
    from e in context.Employees
    join t in context.Teams on e.TeamId equals t.Id
    where t.Name == ".NET"
    join v in context.Vacations on e.Id equals v.EmployeeId
    where v.DateSince.Year == 2019 || v.DateUntil.Year == 2019
    select e;

var distinctEmployees = employeesWithVacation2019.Distinct().ToList();


// 2. Lista pracowników z liczbą dni urlopowych zużytych w bieżącym roku
var currentYear = DateTime.Now.Year;
var today = DateTime.Today;

var result = from e in context.Employees
             join v in context.Vacations on e.Id equals v.EmployeeId into ev
             from v in ev.DefaultIfEmpty()
             where v == null || (v.DateSince.Year == currentYear || v.DateUntil.Year == currentYear)
             group v by e into g
             select new
             {
                 Employee = g.Key,
                 DaysUsed = g.Sum(v => v == null ? 0 : 
                                   (v.DateUntil < today ? 
                                      (v.DateUntil - v.DateSince).TotalDays + 1 : 0))
             };

var list = result.ToList();


// 3.Lista zespołów, w których pracownicy nie mieli żadnego urlopu w 2019
var teamsWithNoVacation2019 = from t in context.Teams
                             where !context.Vacations.Any(v => 
                                  context.Employees.Any(e => e.TeamId == t.Id && e.Id == v.EmployeeId) &&
                                  (v.DateSince.Year == 2019 || v.DateUntil.Year == 2019))
                             select t;

var teamsList = teamsWithNoVacation2019.ToList();