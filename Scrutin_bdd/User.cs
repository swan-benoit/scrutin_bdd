namespace Scrutin_bdd;

public class User
{
    private Guid Id { get;}
    public string Name { get; }
    
    public User(string name)
    {
        Id = Guid.NewGuid(); 
        Name = name;
    } 
    
}
