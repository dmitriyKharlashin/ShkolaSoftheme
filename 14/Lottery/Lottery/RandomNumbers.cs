using System;

namespace Lottery
{
    class RandomNumbers : AbstractNumbers, IIndexer
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
                int current = rnd.Next((int)ENumbers.MIN_VALUE, (int)ENumbers.MAX_VALUE);
                if (Array.IndexOf(_numbers, current) == -1)
                {
                    _numbers[Length()] = current;
                }
            } while (Length() < (int)ENumbers.NUMBERS_COUNT);
        }

    }
}
