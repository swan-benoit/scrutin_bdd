namespace Scrutin_bdd;

public interface WinningStrategy
{   
    public string GetWinner(Dictionary<User, Tuple<int, List<User>>> dictionary);
    public string GetResult(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote);
}

class AbsoluteMajorityWinningStrategy : WinningStrategy
{
    public string GetWinner(Dictionary<User, Tuple<int, List<User>>> dictionary)
    {
        return (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue.Key).First().Name;
    }

    public string GetResult(Dictionary<User, Tuple<int, List<User>>> dictionary, int totalVote)
    {
        
        return (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue)
            .Aggregate("", (acc, pair) => acc + ' ' + pair.Key.Name + ": " + (Convert.ToDouble(pair.Value.Item1) / Convert.ToDouble(totalVote) * 100) + '%');
    }
// nbVoteUser / nombreTotalDevente *  100
// 

}

public enum WinningStrategyEnum
{
    AbsoluteMajority
}

