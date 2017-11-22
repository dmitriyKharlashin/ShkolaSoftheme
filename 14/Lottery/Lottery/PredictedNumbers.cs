using System;

namespace Lottery
{
    class PredictedNumbers : AbstractNumbers, IIndexer
    {
        public int this[int index]
        {
            get
            {
                return _numbers[index];
            }
            set
            {
                if (value < (int)ENumbers.MIN_VALUE || value > (int)ENumbers.MAX_VALUE)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (Array.IndexOf(_numbers, value) > -1)
                {
                    throw new Exception("Please, enter unique value");
                }
                _numbers[index] = value;
            }
        }
    }
}
