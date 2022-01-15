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
	And A scrutin is open
	When i request the candidate list
	Then the result is a candidate list
	
@vote
Scenario: Vote
	Given I am a user
	And A scrutin is open
	When i select a candidate
	Then I receive a message "A voté"
	
@voteAgain
Scenario: cannot vote
	Given I am a user
	And A scrutin is open
	When i select a candidate
	And i select a candidate 
	Then I receive a message "A déjà voté"
	
	
@closeScruttin
Scenario: Close scrutin
		Given I am a administrator
		And A scrutin is open
		When I close the scrutin as a adminstrator
		Then I receive a message "Le scrutin est fermé"
		
@closeScruttinUser
Scenario: Close scrutin when i'm not administrator
		Given I am a user 
		And A scrutin is open
		When I close the scrutin as a user
		Then I receive a message "Seulement l'administrateur peut fermer le scrutin"
