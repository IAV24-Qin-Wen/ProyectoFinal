using Pada1.BBCore.Framework;
using Pada1.BBCore;

namespace BBCore.Conditions
{
    /// <summary>
    /// </summary>
    [Condition("IAV24/CheckInt")]
    public class CheckInt : ConditionBase
    {
        ///<value>Input First Boolean Parameter.</value>
        [InParam("valueA")]
        [Help("First value to be compared")]
        public int valueA;

        ///<value>Input Second Boolean Parameter.</value>
        [InParam("valueB")]
        [Help("Second value to be compared")]
        public int valueB;

        /// <summary>
        /// Checks whether two booleans have the same value.
        /// </summary>
        /// <returns>the value of compare first boolean with the second boolean.</returns>
		public override bool Check()
        {
            return valueA == valueB;
        }
    }
}