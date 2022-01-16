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
    private Dictionary<
        User,
        Tuple<int, List<User>>
    > Votes { get;}
    private int TotalVote { get; set; }
    private bool IsOpen { get; set; }
    private Guid Id { get;}
    private User _administator;
    private WinningStrategy _winningStrategy;

    public User Administrator
    {
        get => _administator;
        set => _administator = value;
    }
    public static List<Scrutin> Instances = new ();
    private Scrutin(List<User> candidates, User administrator, WinningStrategy winningStrategy)
    {
        _winningStrategy = winningStrategy;
        Id = Guid.NewGuid();
        Candidates = candidates;
        Administrator = administrator;
        Administrator.AdminStrategy = new AdminScrutinStrategy(this);
        IsOpen = true;
        Votes = InitVotes();
    }

    private Dictionary<User, Tuple<int, List<User>>> InitVotes()
    {
        return Candidates.Select(candidate => new KeyValuePair<User,Tuple<int, List<User>>>(
                candidate,
                Tuple.Create(0, new List<User>())))
            .ToDictionary(x=>x.Key, x=>x.Value);
    }

    public static string CreateScrutin(List<User> candidates, User administrator, WinningStrategyEnum winningStrategy = WinningStrategyEnum.AbsoluteMajority)
    {
        WinningStrategy winnerStrategyInstance;

        switch (winningStrategy)
        {
            case WinningStrategyEnum.AbsoluteMajority: winnerStrategyInstance = new AbsoluteMajorityWinningStrategy();
                break;
            default: return "strategy inconnu";
        }
        if (candidates.Count < 2)
        {
            return "Scrutin must have at least 2 candidates";
        }

        if (candidates.Contains(administrator))
        {
            return "Administrator cannot be also candidate";
        }

        var scrutin = new Scrutin(candidates, administrator, winnerStrategyInstance);
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
        addVote(candidate, voter);
        return "A voté";
    }

    private void addVote(User candidate, User voter)
    {
        TotalVote++;
        var candidatTupple = Votes[candidate];
        var newVoteCount = candidatTupple.Item1 + 1;
        var ListVoter = candidatTupple.Item2;
        ListVoter.Add(voter);
        Votes[candidate] = Tuple.Create(newVoteCount, ListVoter);
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

    public static Scrutin GetScrutin(String id)
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
    public String GetWinner()
    {
        var winner = _winningStrategy.GetWinner(Votes, TotalVote);
        return winner == null ? "Il n'y a aucun gagnant": "Le gagnant est " + winner;
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

    public string GetResult()
    {
        var lesResultatsSont = "Les resultats sont: " + _winningStrategy.GetResult(Votes, TotalVote);
        return lesResultatsSont;
    }

    public bool IsClosed()
    {
        return !IsOpen;
    }
}