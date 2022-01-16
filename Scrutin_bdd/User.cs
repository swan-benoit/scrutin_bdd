namespace Scrutin_bdd;


public class User
{
    public Guid Id { get;}
    public string Name { get; }

    public AdminStrategy AdminStrategy;

    public Scrutin Scrutin { get; set; }
    public User(string name)
    {
        Id = Guid.NewGuid(); 
        Name = name;
        AdminStrategy = new DefaultAdminStrategy();
    }

    public  String closeScrutin()
    {
        return AdminStrategy.closeScrutin(Id);
    }

}
