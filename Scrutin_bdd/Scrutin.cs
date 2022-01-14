using System.Data;

namespace Scrutin_bdd;

public class Scrutin
{
    private List<Candidate> candidates {get;}
    private Dictionary<Candidate, int> votes { get;}
    private int totalVote { get;}
}