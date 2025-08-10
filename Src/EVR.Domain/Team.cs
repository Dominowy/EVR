namespace EVR.Domain
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; } = [];
    }
}
