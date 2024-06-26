using SQLite;

namespace eramos_Semana5;
[Table("Person")]
public class Person
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [MaxLength(25), Unique]
    public string Name { get; set; } = null!;
}