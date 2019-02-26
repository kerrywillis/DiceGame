using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSample_DiceGame
{
    /// <summary>
    /// Class used to score a roll of 5 8-sided dice
    /// </summary>
    public class Scorer
    {
        #region Scorer specific functions
        /// <summary>
        /// Determine the score of a roll of 5 8-sided dice
        /// </summary>
        /// <returns>The score.</returns>
        /// <param name="category">Category.</param>
        /// <param name="diceValues">Dice values.</param>
        public int Score(string category, int[] diceValues)
        {
            //basic validation, could be avoided with enums
            ValidateValues(diceValues);

            //would noramlly expect an enum as a parameter but pushing into one here
            CategoryEnum.Category enumCategory = new CategoryEnum.Category();

            if (!Enum.TryParse(category, true, out enumCategory))
            {
                throw new ArgumentException("Invalid Category");
            }
            int enumIntValue = (int)enumCategory;

            switch (enumCategory)
            {
                case CategoryEnum.Category.Ones:
                    return SumMatchingDiceValues(1, diceValues);
				case CategoryEnum.Category.Twos:
					return SumMatchingDiceValues(2, diceValues);
				case CategoryEnum.Category.Threes:
					return SumMatchingDiceValues(3, diceValues);
				case CategoryEnum.Category.Fours:
					return SumMatchingDiceValues(4, diceValues);
				case CategoryEnum.Category.Fives:
					return SumMatchingDiceValues(5, diceValues);
				case CategoryEnum.Category.Sixes:
					return SumMatchingDiceValues(6, diceValues);
				case CategoryEnum.Category.Sevens:
					return SumMatchingDiceValues(7, diceValues);
				case CategoryEnum.Category.Eights:
					return SumMatchingDiceValues(8, diceValues);
                case CategoryEnum.Category.ThreeOfAKind:
                    return GetOfAKindScore(3, diceValues);
                case CategoryEnum.Category.FourOfAKind:
                    return GetOfAKindScore(4, diceValues);
                case CategoryEnum.Category.AllOfAKind:
                    return GetOfAKindScore(5, diceValues);
                case CategoryEnum.Category.NoneOfAKind:
                    return GetOfAKindScore(1, diceValues);
                case CategoryEnum.Category.FullHouse:
                    return GetFullHouseScore(diceValues);
                case CategoryEnum.Category.SmallStraight:
                    return GetStraightScore(enumCategory, diceValues);
                case CategoryEnum.Category.LargeStraight:
                    return GetStraightScore(enumCategory, diceValues);
                case CategoryEnum.Category.Chance:
                    return diceValues.Sum();
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Validates the values passed of the dice that were rolled.
        /// </summary>
        /// <param name="currentDiceValues">Current dice values.</param>
        public void ValidateValues(int[] currentDiceValues)
        {
            //normally would expect an enum (or array of enums) as an parameter for strong typing
            if (currentDiceValues.Count() < 5)
                throw new ArgumentException("Min of 5 dice values must be provided.");

            if (currentDiceValues.Count() > 5)
                throw new ArgumentException("Max of 5 dice values can be provided.");

            foreach (int v in currentDiceValues)
            {
                if ((v < 1) || (v > 8))
                    throw new ArgumentException("Dice values must be between 1 and 8.");
            }
        }

        /// <summary>
        /// return x of a kind scores
        /// </summary>
        /// <returns>The AK ind score.</returns>
        /// <param name="minMatching">Minimum matching.</param>
        /// <param name="diceValues">Dice values.</param>
        public int GetOfAKindScore(int minMatching, int[] diceValues)
        {
            var maxOfAKind = GetOrderedDiceValuesAndCounts(diceValues).FirstOrDefault().Value;

            if (minMatching == 5 && maxOfAKind == 5) return 50;
            else if (minMatching == 1 && maxOfAKind == 1) return 40;
            else if (minMatching > 1 && minMatching < 5 && maxOfAKind >= minMatching) return diceValues.Sum();
            else return 0;
        }

        /// <summary>
        /// Determines score for FullHouse category
        /// </summary>
        /// <returns>The house score.</returns>
        /// <param name="diceValues">Dice values.</param>
        public int GetFullHouseScore(int[] diceValues)
        {
            IDictionary<int, int> dictionaryOfCounts = GetOrderedDiceValuesAndCounts(diceValues);

            //If there are only 2 entries in dictionary and one of them has 3 values then must be full house
            if (dictionaryOfCounts.Count() == 2 && dictionaryOfCounts.FirstOrDefault().Value == 3) return 25;
            else return 0;
        }

        /// <summary>
        /// Determines score for straights
        /// </summary>
        /// <returns>The score.</returns>
        /// <param name="diceValues">Dice values.</param>
        public int GetStraightScore(CategoryEnum.Category straightType, int[] diceValues)
        {
            IDictionary<int, int> dictionaryOfCounts = GetOrderedDiceValuesAndCounts(diceValues);

            //short circuits
            //don't go any further if less than 4 distinct dice values
            if (dictionaryOfCounts.Count() < 4) return 0;
            //don't go any further if less than 5 distinct dice values and a large straight
            if (straightType == CategoryEnum.Category.LargeStraight && dictionaryOfCounts.Count() < 5) return 0;

            //sort on DiceValue rather than Count and reduce to list of Keys/DiceValues
            var listOfOrderedDiceValues = dictionaryOfCounts.Keys.OrderBy(o => o).ToList();
            int differencesGreaterThan1 = 0;
            int maxDifference = 0;
            //loop through, if difference > 1 for more than 2 than cannot be small straight
            for (var i = 1; i < listOfOrderedDiceValues.Count; i++)
            {
                int differenceCurrentAndPrevious = listOfOrderedDiceValues[i] - listOfOrderedDiceValues[i - 1];
                if (differenceCurrentAndPrevious > 1)
                {
                    differencesGreaterThan1++;

                    maxDifference = (differenceCurrentAndPrevious > maxDifference) ? differenceCurrentAndPrevious : maxDifference;
                }
            }

            if (straightType == CategoryEnum.Category.SmallStraight && differencesGreaterThan1 <= 1 && maxDifference <= 1) return 30;
            else if (straightType == CategoryEnum.Category.LargeStraight && differencesGreaterThan1 == 0) return 40;
            else return 0;
        }

        /// <summary>
        /// Sums the matching dice values based on number provided in parameter.
        /// </summary>
        /// <returns>The matching dice values.</returns>
        /// <param name="numberToSum">Number to sum.</param>
        /// <param name="diceValues">Dice values.</param>
        public int SumMatchingDiceValues(int numberToSum, int[] diceValues)
        {
            return diceValues.Where(x => x == numberToSum).Sum();
        }

        /// <summary>
        /// Gets a dictionary object that contains dice values and counts in descending order by counts.
        /// </summary>
        /// <returns>The dice values and counts in descending order by count.</returns>
        /// <param name="diceValues">Dice values.</param>
        public IDictionary<int, int> GetOrderedDiceValuesAndCounts(int[] diceValues)
        {
            return diceValues.GroupBy(x => x)
                             .Select(s => new { DiceValue = s.Key, Count = s.Count() })
                             .OrderByDescending(d => d.Count)
                             .ToDictionary(d => d.DiceValue, d => d.Count);
        }
        #endregion Scorer specific functions

        #region SuggestedCatagories specific functions
        /// <summary>
        /// Suggests the categories with the max score
        /// </summary>
        /// <returns>The categories.</returns>
        /// <param name="roll">Roll.</param>
        public string[] SuggestedCategories(int[] roll)
        {
            IDictionary<string, int> dictionaryOfScores = new Dictionary<string, int>();

            foreach (CategoryEnum.Category category in Enum.GetValues(typeof(CategoryEnum.Category)))
            {
                dictionaryOfScores.Add(category.ToString(), Score(category.ToString(), roll));
            }

            return dictionaryOfScores.Where(x => x.Value == dictionaryOfScores.Max(y => y.Value))
                                     .Select(z => z.Key).ToArray();
        }
        #endregion SuggestedCatagories specific functions

    }
}
