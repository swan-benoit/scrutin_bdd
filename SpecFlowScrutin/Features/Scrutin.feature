Feature: Scrutin
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator]($projectname$/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@candidateDeclaration
Scenario: Start a scrutin
	Given I am a administrator 
	And I have a list of candidate
	When  I start a scrutin
	Then the scrutin start
	
@candidateDeclaration
Scenario: Cannot start a scrutin when list user < 2
	Given I am a administrator
	And I have a list of candidate with one candidate
	When  I start a scrutin
	Then I receive a message "Scrutin must have at least 2 candidates"
	
@candidateDeclaration
	Scenario: Cannot start a scrutin when list user contain administrator
		Given I am a administrator 
		And I have a list of candidate with the administrator in it
		When  I start a scrutin
		Then I receive a message "Administrator cannot be also candidate"
	
@candidateList
Scenario: Obtain candidate list
	Given I am a user
	When i request the candidate list
	Then the result is a candidate list
	
@vote
Scenario: Vote
	Given I am a user
	When i select a candidate
	Then i get notify that my vote is a success
	
@voteAgain
Scenario: cannot vote
	Given I am a user
	When i select a candidate
	And i select a candidate 
	Then i get notify that my vote is unsuccessful