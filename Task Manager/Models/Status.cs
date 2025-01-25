namespace Task_Manager.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Duty> Duties { get; set; }
    }
}
