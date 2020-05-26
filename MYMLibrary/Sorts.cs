using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class Sorts
    {

        /// <summary>
        /// Sorts list Ascending by date
        /// </summary>
        /// <param name="list"></param>
        public void sortListsByDateASC(List<MeetModel> list)
        {
            MeetModel temp;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[i].getDateAndHour() < list[j].getDateAndHour())
                    {
                        temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }

    }
}
