using System;
using System.Text;

namespace CodeSample_DiceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Scorer scorer = new Scorer();

            //Use this section for testing Score function
            //int result = scorer.Score("smallstraight", new[] { 6, 5, 8, 8, 7 });

			//Use this section for testing SuggestedCategories function
			var suggestedCategories = scorer.SuggestedCategories(new[] { 3, 3, 3, 4, 4 });
			var result = new StringBuilder();
			result.Append(string.Join(" ", suggestedCategories));

			Console.WriteLine("Result: " + result.ToString());
        }
    }
}
