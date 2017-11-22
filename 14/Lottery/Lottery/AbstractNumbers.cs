namespace Lottery
{
    abstract class AbstractNumbers
    {
        protected int[] _numbers = new int[(int)ENumbers.NUMBERS_COUNT];

        /*
        public void Add(int number)
        {
            _numbers[Length()] = number;
        }
        */
        protected int GetLastNotEmptyIndex()
        {
            for (var i = _numbers.Length - 1; i >= 0; i--)
            {
                if (_numbers[i] != 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public int Length()
        {
            return GetLastNotEmptyIndex() + 1;
        }
    }
}
