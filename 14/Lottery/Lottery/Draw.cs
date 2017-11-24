using System;

namespace Lottery
{
    class Draw : AbstractDraw
    {
        public override void CheckDrawResult()
        {
            for (var i = 0; i < (int)NUMBERTYPES.NUMBERS_COUNT; i++)
            {
                Console.WriteLine("Dropped number: {0}", _randomNumbers[i]);
                if (_predictedNumbers[i] == _randomNumbers[i])
                {
                    _drawResult++;
                    Console.WriteLine("Your prediction was correct!!!");
                }
                else
                {
                    Console.WriteLine("Your prediction was wrong: {0}", _predictedNumbers[i]);
                }
            }
        }
        
        public override void DisplayDrawResults()
        {
            Console.WriteLine("Your result: {0} from {1}", _drawResult, (int)NUMBERTYPES.NUMBERS_COUNT);
        }
    }
}
