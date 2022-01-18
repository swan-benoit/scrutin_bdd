namespace Scrutin_bdd;

public interface AdminStrategy
{
    public Scrutin Scrutin { get; set; }
    public String closeScrutin(Guid userId);
}

class DefaultAdminStrategy : AdminStrategy
{
    public Scrutin Scrutin { get; set; }

    public string closeScrutin(Guid userId)
    {
        return "Seulement l'administrateur peut fermer le scrutin";
    }
    
}

class AdminScrutinStrategy : AdminStrategy
{
    public Scrutin Scrutin { get; set; }

    public AdminScrutinStrategy(Scrutin scrutin)
    {
        Scrutin = scrutin;

    }
    public string closeScrutin(Guid userId)
    {
       return Scrutin.next(userId.ToString())
           ? "Le scrutin est fermé"
           : "Je ne suis pas administrateur de ce scrutin";
    }
    
    
    
}