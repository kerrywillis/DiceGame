# DiceGame

This was based on these requirements:

We’d like you to build two portions of our scoring rules for a little game. It’s a simple dice game, using 5 eight-sided dice. All the scoring categories are listed below. We need you to build two methods.

The first method is a simple score method that takes the category and the roll and returns the score. An example might look like:
public int Score(string category, int[] roll) {…}

an example test case might look like:
Assert.AreEqual(25, scorer.Score(“FullHouse”, new [] {5,5,6,6,5}));

The second method will return a list categories that score the highest given a roll. An example might look like:
public string[] SuggestedCategories(int[] roll) {…}

and an example test case might look like:
CollectionAssert.AreEqual(new []{"ThreeOfAKind","Chance"}, scorer.SuggestedCategories(new []{3,3,8,8,8}));

Scoring Categories

Ones – scores the sum of all the ones
Twos – scores the sum of all the twos
Threes – scores the sum of all the threes
Fours – scores the sum of all the fours
Fives – scores the sum of all the fives
Sixes – scores the sum of all the sixes
Sevens – scores the sum of all the sevens
Eights – scores the sum of all the eights
ThreeOfAKind – scores the sum of all dice if there are 3 or more of the same die, otherwise scores 0
FourOfAKind – scores the sum of all dice if there are 4 or more of the same die, otherwise scores 0
AllOfAKind – Scores 50 if all of the dice are the same, otherwise scores 0
NoneOfAKind – Scores 40 if there are no duplicate dice, otherwise scores 0
FullHouse – Scores 25 if there are two duplicate dice of one value and three duplicate dice of a different value, otherwise scores 0
SmallStraight – Scores 30 if there are 4 or more dice in a sequence, otherwise scores 0
LargeStraight – Scores 40 if all 5 dice are in a sequence, otherwise scores 0
Chance – scores the sum of all dice
