using System;
using Xunit;
using CodeSample_DiceGame;
using TestToolsToXunitProxy;

namespace CodeSample_DiceGameTests
{
    public class ScorerTests
    {

        public ScorerTests()
        {
            this.scorer = new Scorer();
        }

        Scorer scorer;

        #region Tests for Score function
        /// <summary>
        /// Tests for Score function
        /// </summary>

        [Fact]
        public void Score_BlankCategory()
        {
            Exception ex = Xunit.Assert.Throws<ArgumentException>(() => scorer.Score("", new[] { 1, 1, 1, 1, 1 }));
        }

        [Fact]
        public void Score_InvalidCategory()
        {
            Exception ex = Xunit.Assert.Throws<ArgumentException>(() => scorer.Score("invalidCategory", new[] { 1, 1, 1, 1, 1 }));
        }

        [Fact]
        public void Score_Ones_Lowercase()
        {
            Xunit.Assert.Equal(5, scorer.Score("ones", new[] { 1, 1, 1, 1, 1 }));
        }

        [Fact]
        public void Score_Ones_Uppercase()
        {
            Xunit.Assert.Equal(5, scorer.Score("ONES", new[] { 1, 1, 1, 1, 1 }));
        }

        [Fact]
        public void Score_Ones_Score3()
        {
            Xunit.Assert.Equal(3, scorer.Score("Ones", new[] { 3, 3, 1, 1, 1 }));
        }

        [Fact]
        public void Score_ThreeOfAKind_Score27()
        {
            Xunit.Assert.Equal(27, scorer.Score("ThreeOfAKind", new[] { 3, 3, 7, 7, 7 }));
        }

        [Fact]
        public void Score_FourOfAKind_Score24()
        {
            Xunit.Assert.Equal(24, scorer.Score("FourOfAKind", new[] { 4, 8, 4, 4, 4 }));
        }

        [Fact]
        public void Score_AllOfAKind_Score0()
        {
            Xunit.Assert.Equal(0, scorer.Score("allofakind", new[] { 4, 3, 4, 4, 4 }));
        }

        [Fact]
        public void Score_AllOfAKind_Score50()
        {
            Xunit.Assert.Equal(50, scorer.Score("allofakind", new[] { 4, 4, 4, 4, 4 }));
        }

        [Fact]
        public void Score_NoneOfAKind_Score0()
        {
            Xunit.Assert.Equal(0, scorer.Score("noneofakind", new[] { 4, 3, 4, 4, 4 }));
        }

        [Fact]
        public void Score_NoneOfAKind_Score40()
        {
            Xunit.Assert.Equal(40, scorer.Score("noneofakind", new[] { 4, 5, 3, 2, 1 }));
        }

        [Fact]
        public void Score_FullHouse_Score0()
        {
            Xunit.Assert.Equal(0, scorer.Score("FullHouse", new[] { 5, 5, 6, 6, 8 }));
        }

        [Fact]
        public void Score_FullHouse_Score25()
        {
            Xunit.Assert.Equal(25, scorer.Score("FullHouse", new[] { 5, 5, 6, 6, 5 }));
        }

        [Fact]
        public void Score_SmallStraight_NonConsecutive_Score30()
        {
            Xunit.Assert.Equal(30, scorer.Score("SmallStraight", new[] { 6, 5, 8, 8, 7 }));
        }

        [Fact]
        public void Score_SmallStraight_ThreeConsecutive_Score0()
        {
            Xunit.Assert.Equal(0, scorer.Score("SmallStraight", new[] { 5, 5, 6, 7, 5 }));
        }

        [Fact]
        public void Score_SmallStraight_Consecutive_Score30()
        {
            Xunit.Assert.Equal(30, scorer.Score("SmallStraight", new[] { 5, 2, 3, 4, 5 }));
        }

        [Fact]
        public void Score_LargeStraight_OnlyASmallStraight_Score0()
        {
            Xunit.Assert.Equal(0, scorer.Score("largestraight", new[] { 8, 7, 6, 5, 5 }));
        }

        [Fact]
        public void Score_LargeStraight_NonConsecutive_Score40()
        {
            Xunit.Assert.Equal(40, scorer.Score("largestraight", new[] { 6, 5, 8, 4, 7 }));
        }

        [Fact]
        public void Score_LargeStraight_Consecutive_Score40()
        {
            Xunit.Assert.Equal(40, scorer.Score("largestraight", new[] { 1, 2, 3, 4, 5 }));
        }

        [Fact]
        public void Score_Chance_Score27()
        {
            Xunit.Assert.Equal(27, scorer.Score("Chance", new[] { 4, 5, 5, 5, 8 }));
        }
        #endregion Tests for Score function

        #region Tests for ValidateValues function
        /// <summary>
        /// ValidateValues tests
        /// </summary>
        [Fact]
        public void ValidateValues_MinFiveValues()
        {
            Exception ex = Xunit.Assert.Throws<ArgumentException>(() => scorer.ValidateValues(new int[] { 1, 1, 1 }));
        }

        [Fact]
        public void ValidateValues_MaxFiveValues()
        {
            Exception ex = Xunit.Assert.Throws<ArgumentException>(() => scorer.ValidateValues(new int[] { 1, 1, 1, 1, 1, 1, 1 }));
        }

        [Fact]
        public void ValidateValues_EachValueInRange()
        {
            Exception ex = Xunit.Assert.Throws<ArgumentException>(() => scorer.ValidateValues(new int[] { 0, 0, 0, 0, 0 }));
        }
        #endregion Tests for ValidateValues function

        #region Tests for GetOfAKindScore function
        /// <summary>
        /// Tests for GetOfAKindScore function
        /// </summary>

        [Fact]
        public void GetOfAKindScore_ThreeOfAKind_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetOfAKindScore(3, new[] { 3, 2, 1, 1, 4 }));
        }

        [Fact]
        public void GetOfAKindScore_ThreeOfAKind_Return9()
        {
            Xunit.Assert.Equal(9, scorer.GetOfAKindScore(3, new[] { 3, 3, 1, 1, 1 }));
        }

        [Fact]
        public void GetOfAKindScore_FourOfAKind_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetOfAKindScore(4, new[] { 3, 5, 3, 1, 3 }));
        }

        [Fact]
        public void GetOfAKindScore_FourOfAKind_Return13()
        {
            Xunit.Assert.Equal(13, scorer.GetOfAKindScore(4, new[] { 3, 3, 3, 1, 3 }));
        }

        [Fact]
        public void GetOfAKindScore_AllOfAKind_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetOfAKindScore(5, new[] { 3, 3, 5, 3, 3 }));
        }

        [Fact]
        public void GetOfAKindScore_AllOfAKind_Return50()
        {
            Xunit.Assert.Equal(50, scorer.GetOfAKindScore(5, new[] { 3, 3, 3, 3, 3 }));
        }

        [Fact]
        public void GetOfAKindScore_NoneOfAKind_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetOfAKindScore(1, new[] { 3, 3, 5, 3, 3 }));
        }

        [Fact]
        public void GetOfAKindScore_NoneOfAKind_Return40()
        {
            Xunit.Assert.Equal(40, scorer.GetOfAKindScore(1, new[] { 2, 3, 1, 5, 4 }));
        }
        #endregion Tests for GetOfAKindScore function

        #region Tests for GetFullHouseScore function
        /// <summary>
        /// Tests for GetFullHouseScore function
        /// </summary>

        [Fact]
        public void GetFullHouseScore_NoMatches_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetFullHouseScore(new[] { 2, 3, 1, 5, 4 }));
        }

        [Fact]
        public void GetFullHouseScore_FourOfAKind_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetFullHouseScore(new[] { 7, 3, 7, 7, 7 }));
        }

        [Fact]
        public void GetFullHouseScore_Return25()
        {
            Xunit.Assert.Equal(25, scorer.GetFullHouseScore(new[] { 8, 8, 5, 5, 5 }));
        }
        #endregion Tests for GetFullHouseScore function

        #region Tests for GetStraightScore function
        /// <summary>
        /// Tests for GetStraightScore function
        /// </summary>

        [Fact]
        public void GetStraightScore_SmallStraight_TwoDistinctValues_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetStraightScore(CategoryEnum.Category.SmallStraight, new[] { 8, 8, 5, 5, 5 }));
        }

        [Fact]
        public void GetStraightScore_SmallStraight_ThreeConsecutive_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetStraightScore(CategoryEnum.Category.SmallStraight, new[] { 5, 5, 6, 7, 5 }));
        }

        [Fact]
        public void GetStraightScore_SmallStraight_AllSameValue_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetStraightScore(CategoryEnum.Category.SmallStraight, new[] { 5, 5, 5, 5, 5 }));
        }

        [Fact]
        public void GetStraightScore_SmallStraight_NonConsecutive_Return30()
        {
            Xunit.Assert.Equal(30, scorer.GetStraightScore(CategoryEnum.Category.SmallStraight, new[] { 6, 5, 8, 8, 7 }));
        }

        [Fact]
        public void GetStraightScore_SmallStraight_Consecutive_Return30()
        {
            Xunit.Assert.Equal(30, scorer.GetStraightScore(CategoryEnum.Category.SmallStraight, new[] { 5, 2, 3, 4, 5 }));
        }

		//for bug when passed values are 1,2,6,7,8
		[Fact]
		public void GetStraightScore_SmallStraight_PartialConsecutive_Return30()
		{
			Xunit.Assert.Equal(0, scorer.GetStraightScore(CategoryEnum.Category.SmallStraight, new[] { 1, 2, 6, 7, 8 }));
		}

        [Fact]
        public void GetStraightScore_LargeStraight_OnlyASmallStraight_Return0()
        {
            Xunit.Assert.Equal(0, scorer.GetStraightScore(CategoryEnum.Category.LargeStraight, new[] { 8, 7, 6, 5, 5 }));
        }

        [Fact]
        public void GetStraightScore_LargeStraight_NonConsecutive_Return40()
        {
            Xunit.Assert.Equal(40, scorer.GetStraightScore(CategoryEnum.Category.LargeStraight, new[] { 6, 5, 8, 4, 7 }));
        }

        [Fact]
        public void GetStraightScore_LargeStraight_Consecutive_Return40()
        {
            Xunit.Assert.Equal(40, scorer.GetStraightScore(CategoryEnum.Category.LargeStraight, new[] { 1, 2, 3, 4, 5 }));
        }

        #endregion Tests for GetStraightScore function

        #region Tests for SumOfMatching function
        /// <summary>
        /// SumOfMatching tests
        /// </summary>
        [Fact]
        public void SumMatchingDiceValues_Return5()
        {
            var sum = scorer.SumMatchingDiceValues(1, new int[] { 1, 1, 1, 1, 1 });
            Xunit.Assert.Equal(5, sum);
        }

        [Fact]
        public void SumMatchingDiceValues_NoMatchingEights_Return0()
        {
            var sum = scorer.SumMatchingDiceValues(8, new int[] { 5, 5, 5, 5, 5 });
            Xunit.Assert.Equal(0, sum);
        }
        #endregion Tests for SumOfMatching function

        #region Tests for GetCountsOfEachKind function
        /// <summary>
        /// GetCountsOfEachKind tests
        /// </summary>

        [Fact]
        public void GetOrderedDiceValuesAndCounts_5()
        {
            var countsOfEachKind = scorer.GetOrderedDiceValuesAndCounts(new int[] { 3, 1, 2, 5, 4 });
            Xunit.Assert.Equal(5, countsOfEachKind.Count);
        }

        [Fact]
        public void GetOrderedDiceValuesAndCounts_Two()
        {
            var countsOfEachKind = scorer.GetOrderedDiceValuesAndCounts(new int[] { 3, 1, 3, 1, 3 });
            Xunit.Assert.Equal(2, countsOfEachKind.Count);
        }

        [Fact]
        public void GetOrderedDiceValuesAndCounts_Three()
        {
            var countsOfEachKind = scorer.GetOrderedDiceValuesAndCounts(new int[] { 3, 1, 1, 1, 2 });
            Xunit.Assert.Equal(3, countsOfEachKind.Count);
        }

        [Fact]
        public void GetOrderedDiceValuesAndCounts_Four()
        {
            var countsOfEachKind = scorer.GetOrderedDiceValuesAndCounts(new int[] { 5, 1, 4, 3, 5 });
            Xunit.Assert.Equal(4, countsOfEachKind.Count);
        }

        [Fact]
        public void GetOrderedDiceValuesAndCounts_1()
        {
            var countsOfEachKind = scorer.GetOrderedDiceValuesAndCounts(new int[] { 3, 3, 3, 3, 3 });
            Xunit.Assert.Equal(1, countsOfEachKind.Count);
        }
		#endregion Tests for GetCountsOfEachKind function

		#region Tests for SuggestedCategories function
        //to do collection assert
		/// <summary>
		/// Tests for SuggestedCategories function
		/// </summary>

		[Fact]
		public void SuggestedCategories_AllOfAKind_Found()
		{
            CollectionAssert.AreEqual(new[] { "AllOfAKind" }, scorer.SuggestedCategories(new[] { 8, 8, 8, 8, 8 }));
		}

        [Fact]
        public void SuggestedCategories_ThreeOfAKind_Chance_Found(){
            CollectionAssert.AreEqual(new[] { "ThreeOfAKind", "Chance" }, scorer.SuggestedCategories(new[] { 3, 3, 8, 8, 8 }));
        }

		[Fact]
		public void SuggestedCategories_FullHouse_Found()
		{
			CollectionAssert.AreEqual(new[] { "FullHouse" }, scorer.SuggestedCategories(new[] { 3, 3, 1, 1, 1 }));
		}

		//for bug when passed values are 1,2,6,7,8
		[Fact]
		public void SuggestedCategories_ReturnsSmallStraight()
		{
			CollectionAssert.AreEqual(new[] { "NoneOfAKind" }, scorer.SuggestedCategories(new[] { 1, 2, 6, 7, 8 }));
		}

		#endregion Tests for SuggestedCategories function
	}
}
