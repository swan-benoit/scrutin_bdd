namespace Scrutin_bdd;

public interface WinningStrategy
{   
    public string GetWinner(Dictionary<User, Tuple<int, List<User>>> dictionary);
}

class AbsoluteMajorityWinningStrategy : WinningStrategy
{
    public string GetWinner(Dictionary<User, Tuple<int, List<User>>> dictionary)
    {
        return (from keyValue in dictionary orderby keyValue.Value.Item1 descending select keyValue.Key).First().Name;
    }
    
}

