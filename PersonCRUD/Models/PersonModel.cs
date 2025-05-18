namespace PersonCRUD.Models;

public class PersonModel
{
    public PersonModel(String name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public string Name { get; private set; }

    public void ChangeName(String name)
    {
        Name = name;
    }

    public void SetInactive()
    {
        Name = "disable";
    }
    
}