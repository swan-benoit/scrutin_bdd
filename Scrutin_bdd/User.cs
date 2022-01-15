namespace Scrutin_bdd;


public class User
{
    public Guid Id { get;}
    public string Name { get; }

    public ScrutinStrategy ScrutinStrategy;

    public Scrutin Scrutin { get; set; }
    public User(string name)
    {
        Id = Guid.NewGuid(); 
        Name = name;
        ScrutinStrategy = new DefaultScrutinStrategy();
    }

    public  String closeScrutin()
    {
        return ScrutinStrategy.closeScrutin(Id);
    }

}
