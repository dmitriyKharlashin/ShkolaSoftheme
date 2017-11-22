using System;

namespace Lottery
{
    abstract class AbstractDraw
    {
        protected PredictedNumbers _predictedNumbers = new PredictedNumbers();
        protected RandomNumbers _randomNumbers = new RandomNumbers();
        protected int _drawResult = 0;

        public void PopulateTicket() {
            do
            {
                try
                {
                    if (_predictedNumbers.Length() == (int)ENumbers.NUMBERS_COUNT)
                    {
                        break;
                    }
                    Console.WriteLine("Enter unique number from {0}-{1}:", (int)ENumbers.MIN_VALUE, (int)ENumbers.MAX_VALUE);
                    var number = Int32.Parse(Console.ReadLine().Trim(' '));
                    _predictedNumbers[_predictedNumbers.Length()] = number;
                    //lotteryNumbers.Add(number);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            while (true);
        }

        public abstract void CheckDrawResult();
        public abstract void DisplayDrawResults();

    }
}
