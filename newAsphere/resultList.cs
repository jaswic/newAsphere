using System;

namespace newAsphere
{
    public class ResultList<T1, T2> : List<Tuple<T1, T2>>
    {
        public ResultList()
        {
        }

        public void Add(T1 item, T2 item)
        {
            Add(new Tuple<T1, T2>(item1, item2));
        }
    }

}