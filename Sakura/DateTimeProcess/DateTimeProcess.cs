using FERHRI.Sakura.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.DateTimeProcess
{
    public class DateTimeProcess : Common.DateTimeProcess
    {
        public static int GetYear(DateTime date, int timePeriod)
        {
            switch ((Enums.TimePeriod)timePeriod)
            {
                case Enums.TimePeriod.PERIOD_A_J:
                    if (date.Month == 1)
                        return date.Year - 1;
                    break;
                case Enums.TimePeriod.SEASON:
                case Enums.TimePeriod.PERIOD_D_M:
                case Enums.TimePeriod.SEASON_ES2:
                case Enums.TimePeriod.SEASONJFMR:
                    if (date.Month == 12)
                        return date.Year + 1;
                    break;
                case Enums.TimePeriod.PERIOD_A_L:
                    if (date.Month >= 8)
                        return date.Year + 1;
                    break;
                case Enums.TimePeriod.ZIMA:
                case Enums.TimePeriod.GYDRO_YEAR:
                case Enums.TimePeriod.GYDRO_SEASON:
                //case Enums.TimePeriod.GYDRO_ICE:
                //case Enums.TimePeriod.GYDRO_NO_ICE:
                case Enums.TimePeriod.PERIOD_N_A:
                    if (date.Month == 12 || date.Month == 11)
                        return date.Year + 1;
                    else break;
                case Enums.TimePeriod.YEAR:
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
                case Enums.TimePeriod.PENTADE:
                case Enums.TimePeriod.DECADE:
                case Enums.TimePeriod.MONTH:
                case Enums.TimePeriod.PER_J_MR:
                case Enums.TimePeriod.PER_AP_OK:
                case Enums.TimePeriod.PERIOD_J_N:
                case Enums.TimePeriod.PERIOD_J_A:
                case Enums.TimePeriod.WEEK1:
                    break;
                default:
                    throw new Exception("UNKNOWN timePeriod=" + timePeriod);
            }
            return date.Year;
        }

        public static int GetTimeNum(DateTime date, int timePeriod)
        {
            switch ((Enums.TimePeriod)timePeriod)
            {

                case Enums.TimePeriod.YEAR:
                case Enums.TimePeriod.PERIOD_A_L:
                    return 1;
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
                case Enums.TimePeriod.WEEK1:
                    return date.DayOfYear + ((date.Month > 2 && !DateTime.IsLeapYear(date.Year)) ? 1 : 0);
                case Enums.TimePeriod.PENTADE:
                    int pent = (date.Day - 1) / 5;
                    return (date.Month - 1) * 6 + ((pent > 5) ? 5 : pent) + 1;
                case Enums.TimePeriod.DECADE:
                    int day = date.Day;
                    return (date.Month - 1) * 3 + ((day <= 10) ? 1 : (day <= 20) ? 2 : 3);
                case Enums.TimePeriod.MONTH:
                    return date.Month;
                case Enums.TimePeriod.SEASON_ES2:
                    switch (date.Month)
                    {
                        case 12:
                        case 1: return 1;
                        case 2:
                        case 3: return 2;
                        case 4:
                        case 5: return 3;
                        case 6:
                        case 7: return 4;
                        case 8:
                        case 9: return 5;
                        case 10:
                        case 11: return 6;
                    }
                    break;
                case Enums.TimePeriod.SEASON:
                    switch (date.Month)
                    {
                        case 12:
                        case 1:
                        case 2: return 1;
                        case 3:
                        case 4:
                        case 5: return 2;
                        case 6:
                        case 7:
                        case 8: return 3;
                        case 9:
                        case 10:
                        case 11: return 4;
                    }
                    break;
                case Enums.TimePeriod.ZIMA:
                    if (date.Month <= 3 || date.Month >= 11)
                        return 1;
                    break;
                case Enums.TimePeriod.PER_J_MR:
                    return (date.Month >= 1 && date.Month <= 3) ? 1 : 2;
                case Enums.TimePeriod.PER_AP_OK:
                    if (date.Month >= 4 && date.Month <= 10)
                        return 1;
                    break;
                case Enums.TimePeriod.PERIOD_J_N:
                    if (date.Month >= 6 && date.Month <= 11)
                        return 1;
                    break;
                case Enums.TimePeriod.PERIOD_J_A:
                    return (date.Month >= 1 && date.Month <= 4) ? 1 : 2;
                case Enums.TimePeriod.PERIOD_A_J:
                    if ((date.Month >= 4 && date.Month <= 12) || date.Month <= 1)
                        return 1;
                    break;
                case Enums.TimePeriod.PERIOD_D_M:
                    if (date.Month == 12 || date.Month <= 5)
                        return 1;
                    break;

            }
            return -1;
        }
        /**
         * Получить дату начала временного периода указанного года.
         * @param year год, для которого требуется дата начала периода
         * @param timeId тип временного периода
         * @param timeNum номер временного периода
         * @return Дата начала временного периода указанного года.
         */
        public static DateTime GetDateTimeStart(int year, int timeNum, int timeId)
        {
            if (timeNum < 1 || timeNum > GetTimeNumMax(timeId)) throw new Exception("WRONG input timeNum: " + timeNum + ", " + timeId);

            switch ((Enums.TimePeriod)timeId)
            {
                case Enums.TimePeriod.DAYOFYEAR:
                    return (new DateTime(year, 1, 1)).AddDays(timeNum - 1);
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
                case Enums.TimePeriod.WEEK1:
                    if (!DateTime.IsLeapYear(year) && timeNum == 60)
                        throw new Exception("WRONG input timeNum = 60 and not LeapYear= " + year + ", " + timeId);
                    else
                        return (new DateTime(year, 1, 1)).AddDays(timeNum - ((timeNum > 60 && !DateTime.IsLeapYear(year)) ? 2 : 1));
                case Enums.TimePeriod.PENTADE:
                    int month = timeNum / 6;
                    if (timeNum % 6 != 0)
                    {
                        month++;
                    }
                    int day = 26;
                    switch (timeNum % 6)
                    {
                        case 1:
                            day = 1;
                            break;
                        case 2:
                            day = 6;
                            break;
                        case 3:
                            day = 11;
                            break;
                        case 4:
                            day = 16;
                            break;
                        case 5:
                            day = 21;
                            break;
                    }
                    return new DateTime(year, month, day, 0, 0, 0, 0);

                case Enums.TimePeriod.DECADE:
                    int rem, rem1;
                    Math.DivRem(timeNum, 3, out rem);
                    Math.DivRem(timeNum - 1, 3, out rem1);
                    return new DateTime(
                        year,
                        (int)(timeNum / 3) + ((rem == 0) ? 0 : 1),
                        (rem1 == 0) ? 1 : (rem == 0) ? 21 : 11,
                        0, 0, 0, 0);
                case Enums.TimePeriod.MONTH:
                    return new DateTime(year, timeNum, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.SEASON_ES2:
                    if (timeNum == 1)
                    {
                        return new DateTime(year - 1, 12, 1, 0, 0, 0, 0);
                    }
                    else
                    {
                        return new DateTime(year, (timeNum - 1) * 2, 1, 0, 0, 0, 0);
                    }
                case Enums.TimePeriod.PERIOD_J_N:
                    return new DateTime(year, 6, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.PERIOD_N_A:
                    return new DateTime(year, 4, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.SEASON:
                    if (timeNum == 1)
                    {
                        return new DateTime(year - 1, 12, 1, 0, 0, 0, 0);
                    }
                    else
                    {
                        return new DateTime(year, (timeNum - 1) * 3, 1, 0, 0, 0, 0);
                    }
                case Enums.TimePeriod.YEAR:
                    return new DateTime(year, 1, 1);
                case Enums.TimePeriod.ZIMA:
                    return new DateTime(year - 1, 11, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.PER_AP_OK:
                case Enums.TimePeriod.PERIOD_A_J:
                    return new DateTime(year, 4, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.PERIOD_A_L:
                    return new DateTime(year - 1, 8, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.PER_J_MR:
                    if (timeNum == 1)
                        return new DateTime(year, 1, 1, 0, 0, 0, 0);
                    else //timenum=2
                        return new DateTime(year, 4, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.PERIOD_J_A:
                    if (timeNum == 1)
                        return new DateTime(year, 1, 1, 0, 0, 0, 0);
                    else //timenum=2
                        return new DateTime(year, 5, 1, 0, 0, 0, 0);
                case Enums.TimePeriod.PERIOD_D_M:
                    return new DateTime(year - 1, 12, 1, 0, 0, 0, 0);
                default:
                    throw new Exception("UNKNOWN timeId=" + timeId);
            }
        }
        /// <summary>
        /// ATTENSION: UNSPECIFIED KIND OF DATETIME!!!
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeId"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeStart(DateTime date, int timeId)
        {
            switch ((Enums.TimePeriod )timeId)
            {
                case Enums.TimePeriod.DAY:
                    return new DateTime(date.Year, date.Month, date.Day);
                case Enums.TimePeriod.HOUR:
                case Enums.TimePeriod.HOUR2:
                case Enums.TimePeriod.HOUR8:
                case Enums.TimePeriod.HOUR4:
                    return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
            }
            int year = GetYear(date, timeId);
            int timeNum = GetTimeNum(date, timeId);
            return GetDateTimeStart(year, timeNum, timeId);
            //return new DateTime(getDateTimeStart(year, timeNum, timeId).Ticks, date.Kind);
        }

        /// <summary>
        /// В году.
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <returns></returns>
        public static int GetTimeNumMax(int timePeriod)
        {
            switch ((Enums.TimePeriod)timePeriod)
            {
                case Enums.TimePeriod.MINUTE: return 60;
                case Enums.TimePeriod.MONTH: return 12;
                case Enums.TimePeriod.HOUR: return 24;
                case Enums.TimePeriod.PENTADE: return 72;
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
                case Enums.TimePeriod.WEEK1:
                    return 366;
                case Enums.TimePeriod.DECADE: return 36;
                case Enums.TimePeriod.SEASON: return 4;
                case Enums.TimePeriod.SEASON_ES2: return 6;
                case Enums.TimePeriod.SEASON_JFM: return 4;

                case Enums.TimePeriod.PERIOD_D_M:
                case Enums.TimePeriod.PERIOD_M_J:
                case Enums.TimePeriod.YEAR:
                case Enums.TimePeriod.PERIOD_N_A:
                case Enums.TimePeriod.PERIOD_J_S:
                case Enums.TimePeriod.PERIOD_A_L:
                case Enums.TimePeriod.ZIMA:
                case Enums.TimePeriod.PER_AP_OK:
                case Enums.TimePeriod.PERIOD_J_N:
                case Enums.TimePeriod.PERIOD_A_J:
                    return 1;
                case Enums.TimePeriod.PERIOD_J_A:
                case Enums.TimePeriod.PER_J_MR:
                    return 2;
                case Enums.TimePeriod.YEAR_HALF: return 2;
            }
            return -1;
        }

        /// <summary>
        ///Сдвиг текущей даты.
        ///</summary>
        ///<param name="ttype">Тип временного интервала сдвижки.</param>
        ///<param name="time_in">Номера временных интервалов типа ttime_add, включаемых в выборку при сдвигах move_next.</param>
        ///<param name="addQ">Количество временных интервалов типа ttime_add, добавляемых при сдвигах move_next.</param>
        static public DateTime Add(DateTime d, int timePeriod, int addQ)
        {
            switch ((Enums.TimePeriod)timePeriod)
            {
                case Enums.TimePeriod.SECOND: return d.AddSeconds(addQ);
                case Enums.TimePeriod.HOUR: return d.AddHours(addQ);
                case Enums.TimePeriod.HOUR4: return d.AddHours(6 * addQ);
                case Enums.TimePeriod.HOUR8: return d.AddHours(3 * addQ);
                case Enums.TimePeriod.HOUR2: return d.AddHours(12 * addQ);
                case Enums.TimePeriod.DAY: return d.AddDays(addQ);
                case Enums.TimePeriod.MONTH: return d.AddMonths(addQ);
                case Enums.TimePeriod.YEAR: return d.AddYears(addQ);
                case Enums.TimePeriod.PENTADE:
                case Enums.TimePeriod.DECADE:
                case Enums.TimePeriod.DAY366:
                case Enums.TimePeriod.WEEK1:
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.SEASON_ES2:
                case Enums.TimePeriod.SEASON:
                case Enums.TimePeriod.PER_J_MR:
                case Enums.TimePeriod.PERIOD_J_A:
                    int year = GetYear(d, timePeriod);
                    int timeNumMax = GetTimeNumMax(timePeriod);
                    if (timeNumMax == -1)
                        throw new Exception("Unknown timeNumMax for timeId=" + timePeriod);
                    int timeNum = GetTimeNum(d, timePeriod);
                    if (timeNum == -1)
                        throw new Exception("Unknown timeNum for timeId=" + timePeriod + " and date = " + d);

                    int yearCountAdd = (int)Math.Truncate((double)addQ / timeNumMax);
                    year += yearCountAdd;
                    addQ = addQ % timeNumMax;
                    timeNum += addQ;
                    if (timeNum < 1)
                    {
                        year--;
                        timeNum += timeNumMax;
                    }
                    else if (timeNum > timeNumMax)
                    {
                        year++;
                        timeNum -= timeNumMax;
                    }
                    if ((timePeriod == (int)Enums.TimePeriod.DAY366 || timePeriod == (int)Enums.TimePeriod.WEEK1) &&
                        !DateTime.IsLeapYear(year) && timeNum == 60)
                        timeNum++;
                    return GetDateTimeStart(year, timeNum, timePeriod);

                default: throw new SystemException("UNKNOWN TimePeriod");
            }
        }

    }
}
