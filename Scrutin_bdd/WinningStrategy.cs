namespace Scrutin_bdd;

public interface WinningStrategy
{   
    public string GetFullResults(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote);
    public List<User> GetPodium(Dictionary<User, Tuple<int, List<User>>> dictionary);
    public Dictionary<User, int> GetPodiumWithResult(Dictionary<User, Tuple<int, List<User>>> dictionary);
}

class AbsoluteMajorityWinningStrategy : WinningStrategy
{
    public string GetFullResults(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote)
    {
        return (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue)
            .Aggregate("", (acc, pair) => acc + ' ' + pair.Key.Name + ": " + (Convert.ToDouble(pair.Value.Item1) / Convert.ToDouble(totalVote) * 100) + '%');
    }

    public List<User> GetPodium(Dictionary<User, Tuple<int, List<User>>> dictionary)
    {
        return GetPodiumWithResult(dictionary)
            .Select(pair => pair.Key)
            .ToList();
    }

    public Dictionary<User, int> GetPodiumWithResult(Dictionary<User, Tuple<int, List<User>>> dictionary)
    {
        var getPodiumWithResult = (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue)
            .Take(3)
            .ToDictionary(pair => pair.Key, pair => pair.Value.Item1);
        var count = getPodiumWithResult.Count();
        var beforeLast = getPodiumWithResult.ElementAt(count - 2);
        var last = getPodiumWithResult.ElementAt(count - 1);
        
        if (last.Value != beforeLast.Value)
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

