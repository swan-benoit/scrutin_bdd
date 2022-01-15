namespace SpecFlowScrutin.Steps;

[Binding]
public sealed class ScrutinStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;

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

    [Given("the second number is (.*)")]
    public void GivenTheSecondNumberIs(int number)
    {
        //TODO: implement arrange (precondition) logic
        // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
        // To use the multiline text or the table argument of the scenario,
        // additional string/Table parameters can be defined on the step definition
        // method. 

        _scenarioContext.Pending();
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        //TODO: implement act (action) logic

        _scenarioContext.Pending();
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int result)
    {
        //TODO: implement assert (verification) logic

        _scenarioContext.Pending();
    }

    [Given(@"I am a candidate")]
    public void GivenIAmACandidate()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"I apply")]
    public void WhenIApply()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the result is a candidate list")]
    public void ThenTheResultIsACandidateList()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"I am in the candidate list")]
    public void ThenIAmInTheCandidateList()
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"I am a user")]
    public void GivenIAmAUser()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"i request the candidate list")]
    public void WhenIRequestTheCandidateList()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"i select a candidate")]
    public void WhenISelectACandidate()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"i get notify that my vote is a success")]
    public void ThenIGetNotifyThatMyVoteIsASuccess()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"i get notify that my vote is unsuccessful")]
    public void ThenIGetNotifyThatMyVoteIsUnsuccessful()
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"I have a list of candidate")]
    public void GivenIHaveAListOfCandidate()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"I start a scrutin")]
    public void WhenIStartAScrutin()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the scrutin start")]
    public void ThenTheScrutinStart()
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"I am a administrator")]
    public void GivenIAmAAdministrator()
    {
        ScenarioContext.StepIsPending();
    }
}