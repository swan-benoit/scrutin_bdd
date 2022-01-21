using Scrutin_bdd;
using Xunit;

namespace SpecFlowScrutin.Steps;

[Binding]
public sealed class ScrutinStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;
    private User admin;
    private List<User> candidates;
    private Scrutin scrutin;
    private string message;
    private User user;

    public ScrutinStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("the first number is (.*)")]
    public void GivenTheFirstNumberIs(int number)
    {
        //TODO: implement arrange (precondition) logic
        // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
        // To use the multiline text or the table argument of the scenario,
        // additional string/Table parameters can be defined on the step definition
        // method. 

        _scenarioContext.Pending();
    }

    [Then(@"the result is a candidate list")]
    public void ThenTheResultIsACandidateList()
    {
        Assert.True(candidates.First() is User);
    }

    [Then(@"I am in the candidate list")]
    public void ThenIAmInTheCandidateList()
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"I am a user")]
    public void GivenIAmAUser()
    {
        user = new User("Marie");
    }


    [When(@"i request the candidate list for (.*) round")]
    public void WhenIRequestTheCandidateListForRound(int p0)
    {
        candidates = scrutin.Candidates[p0];
    }

    [When(@"i select a candidate")]
    public void WhenISelectACandidate()
    {
        message = scrutin.Vote(candidates.First(), user);
    }


    [Given(@"I have a list of candidate")]
    public void GivenIHaveAListOfCandidate()
    {
        candidates = new List<User>
        {
            new("gerard"),
            new("jacque"),
            new("Rayane"),
            new("Enzo"),
            new("Swan")
        };
    }

    [When(@"I start a scrutin")]
    public void WhenIStartAScrutin()
    {
        message = Scrutin.CreateScrutin(candidates, admin);
        scrutin = Scrutin.GetScrutin(message);
        if (scrutin is Scrutin)
        {
            admin = scrutin.Administrator;
        }
    }

    [Then(@"the scrutin start")]
    public void ThenTheScrutinStart()
    {
        Assert.True(scrutin != null);
        Assert.True(scrutin is Scrutin);
    }

    [Given(@"I am a administrator")]
    public void GivenIAmAAdministrator()
    {
        admin = new User("admin");
    }

    [Given(@"I have a list of candidate with one candidate")]
    public void GivenIHaveAListOfCandidateWithOneCandidate()
    {
        candidates = new List<User>
        {
            new("gerard"),
        };
    }

    [Then(@"I receive a message ""(.*)""")]
    public void ThenIReceiveAMessage(string p0)
    {
        Assert.Equal(p0, message);
    }

    [Given(@"I have a list of candidate with the administrator in it")]
    public void GivenIHaveAListOfCandidateWithTheAdministratorInIt()
    {
        candidates = new List<User>
        {
            new("gerard"),
            new("jacque"),
            new("Rayane"),
            new("Enzo"),
            new("Swan"),
            admin
        };
    }

    [Given(@"A scrutin is open")]
    public void GivenAScrutinIsOpen()
    {
        GivenIHaveAListOfCandidate();
        GivenIAmAAdministrator();
        WhenIStartAScrutin();
    }

    [When(@"I close the scrutin as a adminstrator"), Given(@"I close the scrutin as a adminstrator")]
    public void WhenICloseTheScrutin()
    {
        message = admin.closeScrutin();
    }

    [When(@"I close the scrutin as a user")]
    public void WhenICloseTheScrutinAsAUser()
    {
        message = user.closeScrutin();
    }

    [Given(@"Users vote for candidate"), When("Users vote for candidate")]
    public void GivenUsersVoteForCandidate(Table table)
    {
        foreach (var row in table.Rows)
        {
            var voter = row[0];
            var candidatName = row[1];
            var user1 = new User(voter);
            var candidat = scrutin.Candidates[1].Where(candidat => candidat.Name == candidatName).First();
            scrutin.Vote(candidat, user1);
        }
    }

    [When(@"I ask for the winner")]
    public void WhenIAskForTheWinner()
    {
        message = scrutin.GetWinner();
    }

    [When(@"I ask for the result")]
    public void WhenIAskForTheResult()
    {
        message = scrutin.GetResult();
    }

    [Then(@"I receive a multilinemessage")]
    public void ThenIReceiveAMultilinemessage(string multilineText)
    {
        ThenIReceiveAMessage(multilineText);
    }

    [Then(@"has (.*) candidate")]
    public void ThenHasCandidate(int numberCandidate)
    {
        Assert.Equal(numberCandidate, candidates.Count);
    }

    [Then(@"the scrutin is closed")]
    public void ThenTheScrutinIsClosed()
    {
        Assert.False(scrutin.IsClosed());
    }
}