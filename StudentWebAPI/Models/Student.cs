namespace StudentWebAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required int Grade { get; set; }
        public required string Gender { get; set; }
    }
}
