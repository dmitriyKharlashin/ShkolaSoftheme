using System;

namespace Lottery
{
    class RandomNumbers : AbstractNumbers
    {

        public int this[int index]
        {
            get
            {
                return _numbers[index];
            }
        }

         public RandomNumbers()
        {
            Random rnd = new Random();
            do
            {
                int current = rnd.Next((int)NUMBERTYPES.MIN_VALUE, (int)NUMBERTYPES.MAX_VALUE);
                if (Array.IndexOf(_numbers, current) == -1)
                {
                    _numbers[Length()] = current;
                }
            } while (Length() < (int)NUMBERTYPES.NUMBERS_COUNT);
        }

    }
}
