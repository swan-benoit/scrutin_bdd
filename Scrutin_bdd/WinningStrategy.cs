namespace Scrutin_bdd;

public interface WinningStrategy
{
    public string? GetWinner(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote);
    public string GetFullResults(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote);
    public List<User> GetPodium(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote);

    public Dictionary<User, double> GetPodiumWithResult(Dictionary<User, Tuple<int, List<User>>> dictionary,
        int totalVote);
}

class AbsoluteMajorityWinningStrategy : WinningStrategy
{
    public string? GetWinner(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote)
    {
        return GetPodiumWithResult(dictionary, totalVote)
            .Where(pair => pair.Value > 50)
            .Select(pair => pair.Key)
            .FirstOrDefault(new User(null))
            .Name;
    }

    public string GetFullResults(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote)
    {
        return (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue)
            .Aggregate("",
                (acc, pair) => acc + ' ' + pair.Key.Name + ": " +
                               (Convert.ToDouble(pair.Value.Item1) / Convert.ToDouble(totalVote) * 100) + '%');
    }

    public List<User> GetPodium(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote)
    {
        return GetPodiumWithResult(dictionary, totalVote)
            .Select(pair => pair.Key)
            .ToList();
    }

    public Dictionary<User, double> GetPodiumWithResult(Dictionary<User, Tuple<int, List<User>>> dictionary,
        int totalVote)
    {
        var getPodiumWithResult = (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue)
            .Take(3)
            .ToDictionary(pair => pair.Key,
                pair => Math.Floor(Convert.ToDouble(pair.Value.Item1) / Convert.ToDouble(totalVote) * 100));
        var count = getPodiumWithResult.Count();
        var beforeLast = getPodiumWithResult.ElementAt(count - 2);
        var last = getPodiumWithResult.ElementAt(count - 1);

        if (!last.Value.Equals(beforeLast.Value))
        {
            getPodiumWithResult.Remove(last.Key);
        }

        return getPodiumWithResult;
    }
}

public enum WinningStrategyEnum
{
    AbsoluteMajority
}