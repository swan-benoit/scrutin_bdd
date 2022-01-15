using System.Data;

namespace Scrutin_bdd;

public class Scrutin
{
    private List<User> Candidates { get; }
    private Dictionary<User, int> Votes { get;}
    private int TotalVote { get;}
    private bool IsOpen { get; }
    private Guid Id { get;}

    private User Administrator { get; }
    public static List<Scrutin> Instances = new ();
    private Scrutin(List<User> candidates, User administrator)
    {
        Id = Guid.NewGuid();
        Candidates = candidates;
        Administrator = administrator;
        
    }

    public static string CreateScrutin(List<User> candidates, User administrator)
    {
        if (candidates.Count < 2)
        {
            return "Scrutin must have at least 2 candidates";
        }

        if (candidates.Contains(administrator))
        {
            return "Administrator cannot be also candidate";
        }

        var scrutin = new Scrutin(candidates, administrator);
        Instances.Add(scrutin); ;
        return scrutin.Id.ToString();

    }

    public static Scrutin getScrutin(String id)
    {
        try
        {
            var guid = Guid.Parse(id);
            return Instances.FirstOrDefault(instance => instance.Id.Equals(guid), null);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
}