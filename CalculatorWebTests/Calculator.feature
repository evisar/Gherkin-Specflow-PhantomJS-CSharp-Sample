Feature: Calculator


Scenario: Add two numbers
	Given I open calculator
	When I enter 50.22 
	And I press "+"
	And I enter 70.33 
	And I press "="
	Then the result should be 120.55

Scenario: Substract two numbers
	Given I open calculator
	When I enter 80.44 
	And I press "-"
	And I enter 70.33 
	And I press "="
	Then the result should be 10.11

Scenario: Multiply two numbers
	Given I open calculator
	When I enter 5.55 
	And I press "x"
	And I enter 2.0 
	And I press "="
	Then the result should be 11.10

Scenario: Divide two numbers
	Given I open calculator
	When I enter 50.22 
	And I press "/"
	And I enter 7.33 
	And I press "="
	Then the result should be 6.85

Scenario: PerformListOfOperations
	Given I open calculator
	When Calculate
	| a    | op | b    | equals |
	| 5.33 | +  | 4.67 | 10.0   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	| 3.33 | x  | 3.00 | 9.99   |
	


Scenario: PerformSetOfOperations
	Given I open calculator
	When I execute "5+3x4-2x3.33="
	Then the result should be 99.9

