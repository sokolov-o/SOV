using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Meta
{
    /// <summary>
    /// Sample:
    /// 
    /// DateTimeVia date = new DateTimeVia(DateTime.FromBinary(DateSF[0].ToBinary()), timePeriod, timeStep);
    /// date.DateS = DateSF[0];
    /// date.DateF = DateSF[1];
    /// do
    /// {
    ///    Console.WriteLine(date.Date);
    /// } while (date.NextTimePeriod());
    /// </summary>
    public class DateTimeVia
    {
        public DateTime Date { get; set; }
        public Enums.TimePeriod TimePeriod { get; set; }
        public Enums.TimePeriod TimeStep { get; set; }
        /// <summary>
        /// Все если null или Count == 0.
        /// </summary>
        public List<int> TimePeriodIncluded { get; set; }
        public DateTime? DateS { get; set; }
        public DateTime? DateF { get; set; }

        public DateTimeVia(DateTime Date, Enums.TimePeriod TimePeriod, Enums.TimePeriod TimeStep)
        {
            this.Date = DateTime.FromBinary(Date.ToBinary());
            this.TimePeriod = TimePeriod;
            this.TimeStep = TimeStep;
        }
        public int Year
        {
            get
            {
                return GetYear(Date, TimePeriod);
            }
        }
        public bool IsTimePeriodIncluded()
        {
            if (TimePeriodIncluded != null && TimePeriodIncluded.Count > 0 && !TimePeriodIncluded.Exists(x => x == this.TimeNum))
                return false;
            return true;
        }
        /// <summary>
        /// С контролем границ периода.
        /// С контролем номеров временных интервалов: если интервал не нужен, то добавляется ещё интервал etc...
        /// </summary>
        /// <returns>false, если нарушена нижняя или верхняя граница периода [DateS;DateF]</returns>
        public bool NextTimePeriod()
        {
            AddTimePeriod(1);
            if ((DateS.HasValue && Date < DateS) || (DateF.HasValue && Date > DateF))
                return false;
            if (!IsTimePeriodIncluded())
                return NextTimePeriod();
            return true;
        }
        public void AddTimePeriod(int addQ)
        {
            Date = AddTimePeriod(Date, TimeStep, addQ);
        }
        public int TimeNum
        {
            get
            {
                return GetTimeNum(Date, TimePeriod);
            }
        }
        /**
         * Получить дату начала временного периода указанного года.
         * @param year год, для которого требуется дата начала периода
         * @param timeId тип временного периода
         * @param timeNum номер временного периода
         * @return Дата начала временного периода указанного года.
         */
        public static DateTime GetDateTimeStart(int year, int timeNum, Enums.TimePeriod timeId)
        {
            if (timeNum < 1 || timeNum > GetTimeNumMax(timeId)) throw new Exception("WRONG input timeNum: " + timeNum + ", " + timeId);

            switch (timeId)
            {
                case Enums.TimePeriod.DAYOFYEAR:
                    return (new DateTime(year, 1, 1)).AddDays(timeNum - 1);
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
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
                    Math.DivRem(timeNum, 3, out  rem);
                    Math.DivRem(timeNum - 1, 3, out  rem1);
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
                default:
                    throw new Exception("UNKNOWN timeId=" + timeId);
            }
        }
        public static DateTime GetDateTimeStart(DateTime date, Enums.TimePeriod timeId)
        {
            int year = GetYear(date, timeId);
            int timeNum = GetTimeNum(date, timeId);
            return GetDateTimeStart(year, timeNum, timeId);
        }

        public static int GetYear(DateTime date, Enums.TimePeriod timePeriod)
        {
            switch (timePeriod)
            {
                case Enums.TimePeriod.SEASON:
                case Enums.TimePeriod.PERIOD_D_M:
                case Enums.TimePeriod.SEASON_ES2:
                case Enums.TimePeriod.SEASONJFMR:
                    if (date.Month == 12)
                    {
                        return date.Year + 1;
                    } break;
                case Enums.TimePeriod.GYDRO_YEAR:
                case Enums.TimePeriod.GYDRO_SEASON:
                case Enums.TimePeriod.GYDRO_ICE:
                case Enums.TimePeriod.GYDRO_NO_ICE:
                case Enums.TimePeriod.PERIOD_N_A:
                    if (date.Month == 12 || date.Month == 11)
                    {
                        return date.Year + 1;
                    } break;
            }
            return date.Year;
        }
        /// <summary>
        /// В году.
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <returns></returns>
        public static int GetTimeNumMax(Enums.TimePeriod timePeriod)
        {
            switch (timePeriod)
            {
                case Enums.TimePeriod.MINUTE: return 60;
                case Enums.TimePeriod.MONTH: return 12;
                case Enums.TimePeriod.HOUR: return 24;
                case Enums.TimePeriod.PENTADE: return 72;
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
                    return 366;
                case Enums.TimePeriod.DECADE: return 36;
                case Enums.TimePeriod.SEASON: return 4;
                case Enums.TimePeriod.SEASON_ES2: return 6;
                case Enums.TimePeriod.SEASON_JFM: return 4;
                case Enums.TimePeriod.PERIOD_D_M: return 1;
                case Enums.TimePeriod.PERIOD_M_J: return 1;
                case Enums.TimePeriod.PERIOD_J_S: return 1;
                case Enums.TimePeriod.YEAR_HALF: return 2;
                case Enums.TimePeriod.YEAR: return 1;
                case Enums.TimePeriod.PERIOD_N_A: return 1;
            }
            return -1;
        }
        public static int GetTimeNum(DateTime date, Enums.TimePeriod timePeriod)
        {
            switch (timePeriod)
            {
                case Enums.TimePeriod.YEAR:
                    return 1;
                case Enums.TimePeriod.PENTADE1:
                case Enums.TimePeriod.DECADE1:
                case Enums.TimePeriod.DAY366:
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
            }
            return -1;
        }
        /// <summary>
        ///Сдвиг текущей даты.
        ///</summary>
        ///<param name="ttype">Тип временного интервала сдвижки.</param>
        ///<param name="time_in">Номера временных интервалов типа ttime_add, включаемых в выборку при сдвигах move_next.</param>
        ///<param name="addQ">Количество временных интервалов типа ttime_add, добавляемых при сдвигах move_next.</param>
        static public DateTime AddTimePeriod(DateTime date, Enums.TimePeriod timePeriod, int addQ)
        {
            switch (timePeriod)
            {
                case Enums.TimePeriod.SECOND: return date.AddSeconds(addQ);
                case Enums.TimePeriod.HOUR: return date.AddHours(addQ);
                case Enums.TimePeriod.DAY: return date.AddDays(addQ);
                case Enums.TimePeriod.MONTH: return date.AddMonths(addQ);
                case Enums.TimePeriod.YEAR: return date.AddYears(addQ);
                case Enums.TimePeriod.PENTADE:
                case Enums.TimePeriod.DECADE:
                case Enums.TimePeriod.DAY366:
                case Enums.TimePeriod.SEASON_ES2:
                case Enums.TimePeriod.SEASON:
                    int year = GetYear(date, timePeriod);
                    int timeNumMax = GetTimeNumMax(timePeriod);
                    if (timeNumMax == -1)
                        throw new Exception("Unknown timeNumMax for timeId=" + timePeriod);
                    int timeNum = GetTimeNum(date, timePeriod);
                    if (timeNum == -1)
                        throw new Exception("Unknown timeNum for timeId=" + timePeriod + " and date = " + date);

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
                    return GetDateTimeStart(year, timeNum, timePeriod);

                default: throw new SystemException("UNKNOWN TimePeriod");
            }
        }
    }
}
