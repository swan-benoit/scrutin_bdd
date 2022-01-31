namespace Scrutin_bdd;

public class Scrutin
{
    private Dictionary<int, List<User>> _candidates;

    public Dictionary<int, List<User>> Candidates
    {
        get => _candidates;
        set => _candidates = value;
    }

    private Dictionary<int, Dictionary<
        User,
        Tuple<int, List<User>>
    >> Votes { get; }

    private Dictionary<int, int> TotalVote { get; set; }
    private bool IsOpen { get; set; }
    private Guid Id { get; }
    private User _administator;
    private WinningStrategy _winningStrategy;

    public User Administrator
    {
        get => _administator;
        set => _administator = value;
    }

    public static List<Scrutin> Instances = new();
    private int _totalRound;
    public int CurrentRound = 1;
    private User _blankVoteUser = new User("blank_vote");

    private Scrutin(List<User> candidates, User administrator, WinningStrategy winningStrategy, int totalRound = 2)
    {
        candidates.Add(_blankVoteUser);
        _winningStrategy = winningStrategy;
        Id = Guid.NewGuid();
        Candidates = new Dictionary<int, List<User>>();
        Candidates.Add(1, candidates);
        Administrator = administrator;
        Administrator.AdminStrategy = new AdminScrutinStrategy(this);
        IsOpen = true;
        _totalRound = totalRound;
        Votes = new Dictionary<int, Dictionary<User, Tuple<int, List<User>>>>
        {
            [1] = initVotesTemplate(1)
        };
        TotalVote = new Dictionary<int, int> {[1] = 0};
    }

    private Dictionary<User, Tuple<int, List<User>>> initVotesTemplate(int round)
    {
        return Candidates[round].Select(candidate => new KeyValuePair<User, Tuple<int, List<User>>>(
                candidate,
                Tuple.Create(0, new List<User>())))
            .ToDictionary(x => x.Key, x => x.Value);
    }

    public static string CreateScrutin(List<User> candidates, User administrator,
        WinningStrategyEnum winningStrategy = WinningStrategyEnum.AbsoluteMajority,
        string param = "")
    {
        WinningStrategy winnerStrategyInstance;

        switch (winningStrategy)
        {
            case WinningStrategyEnum.AbsoluteMajority:
                winnerStrategyInstance = new AbsoluteMajorityWinningStrategy();
                break;
            case WinningStrategyEnum.Dictatorial:
                winnerStrategyInstance = new DictatorialWinningStrategy(new User(param));
                break;
            default: return "unknown strategy";
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
        if (!candidate.Equals(_blankVoteUser))
        {
            TotalVote[CurrentRound]++;
        }

        var candidatTupple = Votes[CurrentRound][candidate];
        var newVoteCount = candidatTupple.Item1 + 1;
        var ListVoter = candidatTupple.Item2;
        ListVoter.Add(voter);
        Votes[CurrentRound][candidate] = Tuple.Create(newVoteCount, ListVoter);
    }

    private bool hasAlreadyVote(User voter)
    {
        var alreadyVote = false;
        foreach (var candidate in Votes[CurrentRound])
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
        var winner = _winningStrategy.GetWinner(Votes[CurrentRound], TotalVote[CurrentRound]);
        return winner == null ? "Il n'y a aucun gagnant" : "Le gagnant est " + winner;
    }

    public bool next(String adminId)
    {
        if (adminId == Administrator.Id.ToString())
        {
            if (CurrentRound == _totalRound ||
                _winningStrategy.GetWinner(Votes[CurrentRound], TotalVote[CurrentRound]) != null)
            {
                IsOpen = false;
            }
            else
            {
                nextRound();
            }

            return true;
        }

        return false;
    }

    private void nextRound()
    {
        CurrentRound++;
        Candidates[CurrentRound] = _winningStrategy.GetPodium(Votes[CurrentRound - 1], TotalVote[CurrentRound - 1]);
        if (!Candidates[CurrentRound].Exists(user => user.Equals(_blankVoteUser)))
        {
            Candidates[CurrentRound].Add(_blankVoteUser);
        }

        Votes[CurrentRound] = initVotesTemplate(CurrentRound);
        TotalVote[CurrentRound] = 0;
    }


    public string GetResult()
    {
        var message = "";
        foreach (var round in Enumerable.Range(1, CurrentRound))
        {
            message += "--" + "Les resultats du tour " + round + " sont:" +
                       _winningStrategy.GetFullResults(Votes[round], TotalVote[round]) + "--";
        }

        return message;
    }

    public bool IsClosed()
    {
        return !IsOpen;
    }
}