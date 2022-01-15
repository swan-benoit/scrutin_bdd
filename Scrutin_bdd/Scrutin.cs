using System.Data;
using System.Runtime.CompilerServices;

namespace Scrutin_bdd;

public class Scrutin
{
    private List<User> _candidates;

    public List<User> Candidates
    {
        get => _candidates;
        set => _candidates = value;
    }
    private Dictionary<User, Tuple<int, List<User>>> Votes { get;}
    private int TotalVote { get; set; }
    private bool IsOpen { get; set; }
    private Guid Id { get;}
    private User _administator;

    public User Administrator
    {
        get => _administator;
        set => _administator = value;
    }
    public static List<Scrutin> Instances = new ();
    private Scrutin(List<User> candidates, User administrator)
    {
        Id = Guid.NewGuid();
        Candidates = candidates;
        Administrator = administrator;
        Administrator.ScrutinStrategy = new AdminScrutinStrategy(this);
        IsOpen = true;
        Votes = new Dictionary<User, Tuple<int, List<User>>>();
        foreach (var candidate in Candidates)
        {
            Votes.Add(candidate, Tuple.Create(0, new List<User>()));
        }
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
        Instances.Add(scrutin);
        return scrutin.Id.ToString();
    }

    public string Vote(User candidate, User voter)
    {
        if (!IsOpen)
        {
            return "Le scrutin est fermé";
        }
        if (hasAlreadyVote(voter))
        {
            return "A déjà voté";
        }
        TotalVote++;
        var candidatTupple = Votes[candidate];
        var newVoteCount = candidatTupple.Item1 + 1;
        var ListVoter = candidatTupple.Item2;
        ListVoter.Add(voter);
        Votes[candidate] = Tuple.Create(newVoteCount, ListVoter);
        return "A voté";
    }

    private bool hasAlreadyVote(User voter)
    {
        var alreadyVote = false;
        foreach (var candidate in Votes)
        {
            if (candidate.Value.Item2.Contains(voter))
            {
                return true;
            }
        }
        return alreadyVote;
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

    public bool close(String adminId)
    {
        if (adminId == Administrator.Id.ToString())
        {
            IsOpen = false;
            return true;
        }
        return false;
    }
}