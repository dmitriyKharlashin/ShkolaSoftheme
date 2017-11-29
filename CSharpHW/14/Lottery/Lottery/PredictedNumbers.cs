using System;

namespace Lottery
{
    class PredictedNumbers : AbstractNumbers
    {
        public int this[int index]
        {
            get
            {
                return _numbers[index];
            }
            set
            {
                if (value < (int)NUMBERTYPES.MIN_VALUE || value > (int)NUMBERTYPES.MAX_VALUE)
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
