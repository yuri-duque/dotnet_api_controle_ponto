using System;

namespace Service.Utils
{
    public class Data_Utils
    {
        public bool isfinalDeSemana(DateTime _data)
        {
            if (_data.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else if (_data.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return false;
        }
    }
}
