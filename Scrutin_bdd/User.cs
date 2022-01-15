namespace Scrutin_bdd;

public class User
{
    private Guid Id { get;}
    private string Name { get; }
    
    public User(string name)
    {
        Id = Guid.NewGuid(); 
        Name = name;
    } 
    
}
