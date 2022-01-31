Feature: Scrutin
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator]($projectname$/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

    @candidateDeclaration
    Scenario: Start a scrutin
        Given I am a administrator
        And I have a list of candidate
        When I start a scrutin
        Then the scrutin start

    @candidateDeclaration
    Scenario: Cannot start a scrutin when list user < 2
        Given I am a administrator
        And I have a list of candidate with one candidate
        When I start a scrutin
        Then I receive a message "Scrutin must have at least 2 candidates"

    @candidateDeclaration
    Scenario: Cannot start a scrutin when list user contain administrator
        Given I am a administrator
        And I have a list of candidate with the administrator in it
        When I start a scrutin
        Then I receive a message "Administrator cannot be also candidate"

    @candidateList
    Scenario: Obtain candidate list
        Given I am a user
        And A scrutin is open
        When i request the candidate list for 1 round
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

    @getWinnerResult
    Scenario: Get Winner of scrutin if there is a winner
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Swan      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Swan"

    @getWinnerResult
    Scenario: Get Winner of scrutin if there is no winner
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter  | candidate |
          | Rayane | Enzo      |
          | gerard | Swan      |
          | John   | Rayane    |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Il n'y a aucun gagnant"

    @getResult
    Scenario: Get Result of scrutin du premier tour
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Swan      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        When I close the scrutin as a adminstrator
        And I ask for the result
        Then I receive a message "--Les resultats du tour 1 sont: Swan: 60% Rayane: 20% Enzo: 20% gerard: 0% jacque: 0% blank_vote: 0%--"

    Scenario: Get the winner of scrutin du premier tour if more than 50%
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Swan      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Swan"

    Scenario: Cannot vote if 1st decipher a winner
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Swan      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        And I close the scrutin as a adminstrator
        When Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
        Then I receive a message "Le scrutin est fermé"

    Scenario: Get the winner of scrutin du premier tour if less than 50%
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Il n'y a aucun gagnant"

    Scenario: Get Result of scrutin du second tour
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Enzo      |
        When I close the scrutin as a adminstrator
        And I ask for the result
        Then I receive a multilinemessage
        """
        --Les resultats du tour 1 sont: Enzo: 40% Swan: 40% Rayane: 20% gerard: 0% jacque: 0% blank_vote: 0%----Les resultats du tour 2 sont: Enzo: 60% Swan: 40% blank_vote: 0%--
        """

    Scenario: Get winner of scrutin du second tour
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Enzo      |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Enzo"

    Scenario: On first round equality tree candidate are on second round
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
          | John      | Rayane    |
        And I close the scrutin as a adminstrator
        When i request the candidate list for 2 round
        Then the result is a candidate list
        And has 4 candidate

    Scenario: Get winner of scrutin du second tour when second round equality
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Enzo      |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Il n'y a aucun gagnant"

    Scenario: Get winner of scrutin du second tour with empty votes
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate  |
          | Geraldine | Swan       |
          | Enzo      | Enzo       |
          | Enzo      | Enzo       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | John      | Rayane     |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Enzo      |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Enzo"
        
    Scenario: Get winner of scrutin du second tour with empty votes with blank_vote win first round
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate  |
          | Geraldine | Swan       |
          | Enzo      | Enzo       |
          | Enzo      | Enzo       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | John      | Rayane     |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Enzo      |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Enzo"
        
                
    Scenario: Get winner of scrutin du second tour when more blank vote than the winner
        Given I am a user
        And A scrutin is open
        And Users vote for candidate
          | voter     | candidate  |
          | Geraldine | Swan       |
          | Enzo      | Enzo       |
          | Enzo      | Enzo       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | John      | Rayane     |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Enzo      |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Swan"
        
    Scenario: Get winner of scrutin du second tour whith dictatorial scrutin
        Given I am a user
        And A dictatorial scrutin is open for "Mobutu"
        And Users vote for candidate
          | voter     | candidate  |
          | Geraldine | Swan       |
          | Enzo      | Enzo       |
          | Enzo      | Enzo       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | John      | Rayane     |
        And I close the scrutin as a adminstrator
        And Users vote for candidate
          | voter     | candidate  |
          | Geraldine | Swan       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | Rayane    | blank_vote |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | gerard    | Swan       |
          | Enzo      | Enzo       |
          | Rayane    | Enzo       |
          | gerard    | Swan       |
          | John      | Enzo       |
        When I close the scrutin as a adminstrator
        And I ask for the winner
        Then I receive a message "Le gagnant est Mobutu"
        
    Scenario: Get Result of scrutin du second tour for dictatorial result
        Given I am a user
        And A dictatorial scrutin is open for "Mobutu"
        And Users vote for candidate
          | voter     | candidate |
          | Geraldine | Swan      |
          | Enzo      | Enzo      |
          | Rayane    | Enzo      |
          | gerard    | Swan      |
          | John      | Rayane    |
        And I close the scrutin as a adminstrator
        When I ask for the result
        Then I receive a multilinemessage
        """
        --Les resultats du tour 1 sont: Mobutu: 50% Enzo: 20% Swan: 20% Rayane: 10% gerard: 0% jacque: 0% blank_vote: 0%--
        """