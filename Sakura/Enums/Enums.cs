using System;
using System.Collections.Generic;
using System.Text;

namespace FERHRI.Sakura.Meta
{
    /// <summary>
    /// Перечисления на основе справочников баз данных технологии Viaware.Sakura (ОДПП, ДВНИГМИ).
    /// </summary>
    public class Enums
    {



        public enum QCL : byte
        {
            /// <summary>
            /// Забраковано синоптиком.
            /// </summary>
            BAD_SYNOP = 72,
            /// <summary>
            /// Забраковано автоматом.
            /// </summary>
            BAD_AUTO = 96,
            /// <summary>
            /// Контроль не проводился.
            /// </summary>
            ZERO = 120,
            /// <summary>
            /// Одобрено автоматом.
            /// </summary>
            GOOD_AUTO = 144,
            /// <summary>
            /// Одобрено синоптиком.
            /// </summary>
            GOOD_SYNOP = 168
        }
        /// <summary>
        /// From HmdStn data base table.
        /// </summary>
        public enum ParamHMII
        {
            /// <summary>
            /// Расчётная величина заданных обеспеченностей произвольного ряда, данные которого задаются в атрибутах расчёта.
            /// </summary>
            ANY_PROB = 2050,
            /// <summary>
            /// Максимальная скорость ветра повторяемостью 1 раз в T лет (10 мин, годовые максимумы, сроки)
            /// </summary>
            Wind10_MAX_PTY = 1,
            /// <summary>
            /// Максимальная скорость ветра повторяемостью 1 раз в T лет (10 мин, месячные максимумы, помесячно, сроки)
            /// </summary>
            Wind10_MAX_PTM = 3,
            /// <summary>
            /// Максимальная скорость ветра повторяемостью 1 раз в T лет (сутки-порыв, годовые, месячные и др. максимумы)
            /// </summary>
            Wind1_MAX_PT = 42,
            /// <summary>
            /// Скорость ветра различной обеспеченности (для ветра за 10-ти мин интервал усреднения)
            /// </summary>
            Wind10_PT = 4,
            /// <summary>
            /// Скорость ветра различной обеспеченности (для порывов ветра)
            /// </summary>
            Wind1_PT = 5,
            /// <summary>
            /// Температура наиболее холодной пятидневки обеспеченностей 0,98 и 0,92
            /// </summary>
            Ta_5YMin = 9,
            /// <summary>
            /// Температура наиболее холодных суток обеспеченностей 0,98 и 0,92
            /// </summary>
            Ta_1YMin = 41,
            /// <summary>
            /// RR: Суточный максимум осадков обеспеченностью 1,2,10% (мм)
            /// </summary>
            RR_1YMax = 23,
            /// <summary>
            /// Snow: Расчетная толщина снежного покрова
            /// </summary>
            Snow_10YMaxP = 35

        }

        public enum StationType : long
        {
            HMStation = 1, //	Гидрометстанция	ГМС
            HydrologyPost = 2, //	Гидрологический пост	ГП
            SeaPost = 3, //	Морской пост	МП
            AutoMeteoComplex = 5, //	Автоматическая метеорологический комплекс	АМК
            AutoHydrologyComplex = 6, //	Автоматическая гидрологический комплекс	АГК
            WindDetector = 7, //	Датчик измерения скорости и направления ветра	WND
            AutoSeaPost = 8, //	Автоматизированный морской пост	АМП   
            Ship = 9
        }

        public enum EntityType : long
        {
            Action = 5,
            LevelType = 4,
            Parameter = 1,
            Station = 2,
            TimePeriod = 6
        }
        /// <summary>
        /// Кодовая форма данных БД ГИС Метео.
        /// </summary>
        public enum CodeForm : long
        {
            SATEM_A = 1,
            TEMP_A = 2,
            TEMP_B = 3,
            TEMP_C = 4,
            TEMP_D = 5,
            PILOT_A = 8,
            GRID = 12,
            CLIMAT = 13,
            SYNOP = 16,
            RADOB = 19,
            SHIP = 21,
            PILOT_SHIP_A = 23,
            TEMP_SHIP_A = 27,
            TEMP_SHIP_B = 28,
            TEMP_SHIP_C = 29,
            TEMP_SHIP_D = 30,
            CODAR = 33,
            TESAC = 35,
            SATOB = 40,
            KH_15_HYDROWATER = 44,
            KH_24_HYDROSNOW = 45,
            KPA_4_AIR = 78,
            KH_02_MORE = 83,
            KH_21m_AGRODECADE = 89,
            KH_21m_AGRODAYLY = 90,
            KH_21m_AGRODECADESH = 91,
            KH_21m_AGRODAYLYSH = 92,
            KH_13_RHOB = 94,
            KPA_4_MONTH = 99,
            METAR = 145,
            SPECI = 146,
            TAF = 147,
            KH_19_DECADE = 148,
            GRIB = 151,
            BUOY = 152,
            BATHY = 153,
            SIGMET = 154,
            AMDAR = 155
        }
        /// <summary>
        /// Единицы измерения.
        /// </summary>
        public enum Unit
        {
            UNKNOWN = 1, MMpH, GradC
        }
        /// <summary>
        /// Сущности БД SakuraAnalog (таблица SakuraAnalog..Object).
        /// </summary>
        public enum SakuraAnalogObject
        {
            Analog0 = 1,
            Analog1 = 2,
            Fcs0 = 4,
            Fcs0Selected = 9,
            Fcs1 = 5,
            FcsEstim0 = 7
        }
        /// <summary>
        /// Типы сообщений БД SakuraAnalog (таблица SakuraAnalog..MessageType).
        /// </summary>
        public enum SakuraAnalogMessageType
        {
            Error = 3,//	Ошибка
            Info = 1,//	Информация
            Warning = 2//	Предупреждение
        }
        /// <summary>
        /// Математические (статистические и др.) переменные.
        /// </summary>
        public enum MathVar
        {
            ABSDEV = 7,
            ANOM = 12,
            AVG = 1,
            AVG_TEND = 82,
            COUNT = 87,
            DEV2 = 9,
            DEVAVG = 28,
            MAX = 2,
            MIN = 3,
            P_KQQQ = 84,
            P_s_KQQQ = 85,
            P_st_KQQQ = 86,
            R = 5,
            S_FCS = 49,
            /// <summary>
            /// Отношение СКО прогнозов к СКО от среднего за период прогноза
            /// </summary>
            S_SIGMA_F = 48,
            /// <summary>
            /// Отношение СКО прогнозов к СКО факта ЗА ПЕРИОД ПРОГНОЗА от заданной нормы 
            /// (норма не за период прогноза)
            /// РД РД 52.27.284-91, стр. 117, ф. 99,100
            /// </summary>
            S_SIGMA_7 = 56,
            STD = 4,
            STD_NM1_TEND = 81,
            STD_NM1 = 83,
            /// <summary>
            /// Обеспеченность для аномалий затем осреднение по по региону
            /// </summary>
            P86TAMONTH = 14,//	Обеспеченность для аномалий затем осреднение по по региону(стр. 12 "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат")
            /// <summary>
            /// Ro - оправд-ть аномалии сезонной Та по знаку 
            /// </summary>
            RO82TASEASON = 15,//Параметр Ro - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
            /// <summary>
            /// Q - погрешность аномалии сезонной Та по величине 
            /// </summary>
            Q82TASEASON = 17,//Параметр Q - погрешность аномалии сезонной Та по величине (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
            /// <summary>
            /// 
            /// </summary>
            P82TASEASON = 18,//Параметр P - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
            /// <summary>
            /// 
            /// </summary>
            P82RRMONTH = 20,//Параметр P - показатель качества прогноза месячной суммы осадков (стр. 19, ф. 4  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
            /// <summary>
            /// Разность оценки качества месячных прогнозов осадков (P82RRMONTH) и оценки климатического прогноза
            /// </summary>
            P82RR_minus_P_CLM = 68,//Разность оценки качества месячных прогнозов осадков (P82RRMONTH) и оценки климатического прогноза
            /// <summary>
            /// Отношение оценки качества месячных прогнозов осадков (P82RRMONTH) к оценке климатического прогноза
            /// </summary>
            P82RR_P_CLM = 66,//Отношение оценки качества месячных прогнозов осадков (P82RRMONTH) к оценке климатического прогноза
            /// <summary>
            /// Оценка оправдываемости прогноза Та
            /// </summary>
            P02TAHOUR = 23,//Оценка оправдываемости прогноза Та  (стр. 21,  "Наставление по крат. прог., 2002,Спб, ГМИздат").
            /// <summary>
            /// Оценка прогноза  аномалий ср. мес. Та по классам
            /// </summary>
            P86TAMONTH_cls = 25,//Оценка прогноза  аномалий ср. мес. Та по классам (стр. 15 "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат")
            /// <summary>
            /// Параметр P - оправд-ть аномалии месячной Та
            /// </summary>
            P82TAMONTH = 26,//Параметр P - оправд-ть аномалии месячной Та (стр.14,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
            /// <summary>
            /// Отношение оценки качества месячных прогнозов температуры (P86TAMONTH) к оценке климатического прогноза
            /// </summary>
            P86TA_P_CLM = 69,//Отношение оценки качества месячных прогнозов температуры (P86TAMONTH) к оценке климатического прогноза
            /// <summary>
            /// Разность оценки качества месячных прогнозов температуры (P86TAMONTH) и оценки климатического прогноза
            /// </summary>
            P86TA_minus_P_CLM = 71,
            VALUE = 27,
            /// <summary>
            /// Максимальное абсолютное отклонение двух рядов
            /// </summary>
            ABSDEVMAX = 88,
            /// <summary>
            /// Минимальное абсолютное отклонение двух рядов
            /// </summary>
            ABSDEVMIN = 89,
            /// <summary>
            /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 2 м/с и более
            /// </summary>
            PV2Pr = 29,//Pv2%	Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 2 м/с и более (по «Методическими указаниями» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.). MATLAB
            /// <summary>
            /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 5 м/с и более
            /// </summary>
            PV5Pr = 30,//Pv2%	Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 5 м/с и более (по «Методическими указаниями» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.). MATLAB
            /// <summary>
            /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 10 м/с и более
            /// </summary>
            PV10Pr = 31,//Pv2%	Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 10 м/с и более (по «Методическими указаниями» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.). MATLAB
            /// <summary>
            /// Оценка направления ветра (Наставление по службе прог., 1981)
            /// </summary>
            SIGMA_WDIR_SIN = 41,//	Оценка направления ветра (Наставление по службе прог., 1981)
            /// <summary>
            /// Оценка направления ветра  0 - 30 grad
            /// </summary>
            SIGMA0_WDIR_RD = 42,//	Оценка направления ветра  0 - 30 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
            /// <summary>
            /// Оценка направления ветра  31 - 60 grad 
            /// </summary>
            SIGMA31_WDIR_RD = 43,//Оценка направления ветра  31 - 60 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
            /// <summary>
            /// Оценка направления ветра  61 - 90 grad 
            /// </summary>
            SIGMA61_WDIR_RD = 44,//Оценка направления ветра  61 - 90 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
            /// <summary>
            /// Оценка направления ветра  > 90 grad 
            /// </summary>
            SIGMA91_WDIR_RD = 45//Оценка направления ветра  > 90 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
        }
        /// <summary>
        /// Порядок байт.
        /// </summary>
        public enum ByteOrder { LITTLE_ENDIAN = 0, BIG_ENDIAN = 1 }
        /// <summary>
        /// Тип набора часов (дня).
        /// </summary>
        public enum HoursType
        {
            /// <summary>
            /// Все доступные часы (дня).
            /// </summary>
            AllAvailable,
            /// <summary>
            /// Каждый час - отдельно.
            /// </summary>
            EachHourSeparatly,
            /// <summary>
            /// Только часы 0 и 12.
            /// </summary>
            Only_0_12,
            /// <summary>
            /// Только часы 0,6,12,18.
            /// </summary>
            Only_0_6_12_18

        }
        /// <summary>
        /// Тип выборки.
        /// </summary>
        public enum UdepDep { Udep = 0, Dep = 1 }
        /// <summary>
        /// Тип формата, способа хранения файлов.
        /// TODO: DELME! Use DbList identification.
        /// </summary>
        //public enum FileStoreType
        //{
        //    /// <summary>
        //    /// Cистема хранения файлов GFS в отделе Ураевского в 2011
        //    /// </summary>
        //    OMA_GFS = 1
        //}
        /// <summary>
        /// Типы, виды волнения в соответствие с таблицами ТГМ-1.
        /// </summary>
        public enum WaveType
        {
            WIND_WAVE = 1,
            SWELL,
            SWELL_DEATH,
            WIND_SWELL,
            SWELL_SWELL,
            SWELL_WIND,
            RIPS,
            CALM
        }
        /// <summary>
        /// Атмосферные явления, на море?.
        /// </summary>
        public enum PhenomAtm
        {
            /// <summary>
            /// Туман/туманы
            /// </summary>
            FOG = 1,
            /// <summary>
            /// Метель/вьюга
            /// </summary>
            BLIZZARD = 2,
            /// <summary>
            /// Снег ливневой
            /// </summary>
            SNOW_RAINFALL = 3,
            /// <summary>
            /// Пыльная буря
            /// </summary>
            DUST_STORM = 4,
            /// <summary>
            /// Снежная мгла
            /// </summary>
            SNOWY_HAZE = 5,
            /// <summary>
            /// Мгла
            /// </summary>
            HAZE = 6,
            /// <summary>
            /// Дождь
            /// </summary>
            RAIN = 7,
            /// <summary>
            /// Снег крупа
            /// </summary>
            SNOW_GRAIN = 8,
            /// <summary>
            /// Метель низовая
            /// </summary>
            BLIZZARD_BLOWING = 9,
            /// <summary>
            /// Морось
            /// </summary>
            DRIZZLE = 10,
            /// <summary>
            /// Дымка
            /// </summary>
            SMOKE = 11,
            /// <summary>
            /// Парение моря
            /// </summary>
            SOARING_SEA = 12
        }
        /// <summary>
        /// Получить тип часов (дня) по русскому наименованию.
        /// </summary>
        /// <param name="hours">наименование типа часов (дня).</param>
        /// <returns>Тип часов.</returns>
        static public HoursType HoursTypeEnumByName(string hours)
        {
            switch (hours)
            {
                case "отдельно":
                    return HoursType.EachHourSeparatly;
                case "-1":
                    return HoursType.AllAvailable;
                case "0;12":
                    return HoursType.Only_0_12;
                case "0;6;12;18":
                    return HoursType.Only_0_6_12_18;
                default:
                    throw new Exception("switch (hours) : " + hours);
            }
        }
        /// <summary>
        /// Получить русское наименование по типу часов (дня).
        /// </summary>
        /// <param name="hours">Тип часов (дня).</param>
        /// <returns>Наименование типа часов.</returns>
        static public string HoursType2String(HoursType hoursType)
        {
            switch (hoursType)
            {
                case Enums.HoursType.EachHourSeparatly:
                    return "отдельно";
                case HoursType.AllAvailable:
                    return "-1";
                case HoursType.Only_0_12:
                    return "0;12";
                case HoursType.Only_0_6_12_18:
                    return "0;6;12;18";
                default:
                    throw new Exception("switch (hoursType) : " + hoursType);
            }
        }
        /// <summary>
        /// Получить массив типов сортировки по массиву типов действй над параметром.
        /// </summary>
        /// <param name="hours">Тип действия.</param>
        /// <returns>Тип сортировки действия.</returns>
        public static Sort[] EnumSortTypeByAction(Enums.Action[] action)
        {
            Sort[] ret = new Sort[action.Length];
            for (int i = 0; i < action.Length; i++)
            {
                ret[i] = Enums.EnumSortTypeByName(action[i]);
            }
            return ret;
        }
        /// <summary>
        /// Получить тип сортировки по действию над параметром.
        /// </summary>
        /// <param name="hours">Тип действия.</param>
        /// <returns>Тип сортировки действия.</returns>
        public static Sort EnumSortTypeByName(Enums.Action dbHmdicActionId)
        {
            switch (dbHmdicActionId)
            {
                case Enums.Action.CRR:
                    return Sort.DESC;
                case Enums.Action.EUCLID:
                    return Sort.ASC;
                default:
                    throw new Exception("switch (dbHmdicActionId): " + dbHmdicActionId);
            }
        }
        /// <summary>
        /// Получить наименование временного периода (рус) по его типу.
        /// </summary>
        /// <param name="timePeriod">Тип временного периода.</param>
        /// <returns>Наименование временного периода.</returns>
        public static string ToStringRus(Enums.TimePeriod timePeriod)
        {
            switch (timePeriod)
            {
                case Enums.TimePeriod.MINUTE:
                    return "МИНУТА";
                case Enums.TimePeriod.MONTH:
                    return "MONTH";
                case Enums.TimePeriod.HOUR:
                    return "ЧАС";
                case Enums.TimePeriod.PENTADE:
                    return "ПЕНТАДА";
                case Enums.TimePeriod.DECADE:
                    return "ДЕКАДА";
                case Enums.TimePeriod.SEASON:
                    return "СЕЗОН";
                case Enums.TimePeriod.SEASON_ES2:
                    return "СЕЗОН_ЕСР2";
                case Enums.TimePeriod.SEASON_JFM:
                    return "СЕЗОН_ЯФМ";
                case Enums.TimePeriod.PERIOD_D_M:
                    return "ПЕРИОД_ДМ";
                case Enums.TimePeriod.PERIOD_M_J:
                    return "ПЕРИОД_МИ";
                case Enums.TimePeriod.PERIOD_J_S:
                    return "ПЕРИОД_ИС";
                case Enums.TimePeriod.YEAR_HALF:
                    return "ПОЛУГОДИЕ";
                case Enums.TimePeriod.YEAR:
                    return "ГОД";
                case Enums.TimePeriod.PERIOD_N_A:
                    return "ПЕРИОД_НА";
            }
            return null;
        }
        /// <summary>
        /// Получить тип временного периода по его наименованию (рус).
        /// </summary>
        /// <param name="timePeriod">Наименование временного периода.</param>
        /// <returns>Тип временного периода.</returns>
        public static TimePeriod? TimePeriodByName(string name)
        {
            switch (name)
            {
                case "MONTH":
                    return Enums.TimePeriod.MONTH;
                case "PENTADE":
                    return Enums.TimePeriod.PENTADE;
            }
            return null;
        }
        /// <summary>
        /// Типы сортировки.
        /// </summary>
        public enum Sort
        {
            /// <summary>
            /// Biggest the best
            /// </summary>
            DESC = 1,
            /// <summary>
            /// Smallest the best
            /// </summary>
            ASC = 2
        }
        /// <summary>
        /// SQL action as INSERT, UPDATE, DELETE, SELECT, VIEWONLY.
        /// </summary>
        public enum SqlAction { INSERT, UPDATE, DELETE, SELECT, VIEWONLY }
        /// <summary>
        /// Типы временных периодов. Соответствуют словарю БД Hmdic..TimePeriod.
        /// </summary>
        public enum TimePeriod : long
        {
            ALL = 43,
            DAY = 19,
            DAY366 = 56,
            DAYOFYEAR = 25,
            HOUR = 5,
            HOUR2 = 57,
            HOUR4 = 62,
            HOUR8 = 59,
            MONTH = 2,
            MINUTE = 6,
            MINHOUR0 = 50,
            MINHOUR15 = 51,
            MINHOUR30 = 53,
            PENTADE = 24,
            PENTADE1 = 55,
            DECADE = 4,
            DECADE1 = 58,
            SEASON = 8,
            SEASON_ES2 = 22,
            SEASON_JFM = 7,
            SECOND = 21,
            PERIOD_A_A = 20,
            PERIOD_D_M = 11,
            PERIOD_M_J = 15,
            PERIOD_J_M = 26,
            PERIOD_J_S = 16,
            YEAR_HALF = 18,
            YEAR = 9,
            PERIOD_N_A = 42,
            SEASONJFMR = 28, //	Скользящие сезоны DJF-JFM-...  [1-12]
            GYDRO_YEAR = 29,//	ГидроГод	1 ноя пред. календ. года - 31 окт тек.  
            GYDRO_SEASON = 34,//	ГидроСезон	зим XI-III, весен IV-V, летне-осен VI-X
            GYDRO_ICE = 37,//	ГидроЛёд	Зимний период-за даты с призн "лёд"
            /// <summary>
            /// ГидроНеЛёд	Период-за даты без признака "лёд"
            /// </summary>
            GYDRO_NO_ICE = 40,
            /// <summary>
            /// WYSIWYG
            /// </summary>
            UNKNOWN = 60,
            /// <summary>
            /// пер.с авг. пред. года по июль тек.
            /// </summary>
            PERIOD_A_L = 83,
            /// <summary>
            /// с ноября по март
            /// </summary>
            ZIMA = 64,
            /// <summary>
            /// c апреля по октябрь
            /// </summary>
            PER_AP_OK = 84,
            /// <summary>
            /// пер.лед.(1)с 1 по 3,пер.нав.(2)с4по12
            /// </summary>
            PER_J_MR = 85,
            /// <summary>
            /// Период июнь-ноябрь, навигация ОМ
            /// </summary>
            PERIOD_J_N = 61,
            /// <summary>
            /// Скользящие через 1 сутки недели (7 дней)
            /// </summary>
            WEEK1 = 86,
            /// <summary>
            /// Период с апреля по январь
            /// </summary>
            PERIOD_A_J = 87,
            /// <summary>
            /// Период с мая по декабрь
            /// </summary>
            PERIOD_J_A = 88
        }
        /// <summary>
        /// Действия. Соответствуют словарю Hmdic..Action.
        /// </summary>
        public enum Action : long
        {
            AAO = 1,
            ACCUMUL = 74,
            ANOM = 2,
            AO = 3,
            ASTD = 4,
            AVG = 5,
            Beam = 6,
            BLINOVA = 7,
            BLOCK = 8,
            CLASS = 9,
            COS = 10,
            CRR = 11,
            EOF = 12,
            EOFANM = 13,
            EUCLID = 87,
            GIRS = 14,
            HILL = 15,
            HOLE = 16,
            ITI = 17,
            KAC = 18,
            LAHI = 19,
            MIN = 75,
            MAX = 76,
            N34 = 20,
            NAO = 21,
            PDO = 22,
            PLANET = 23,
            GRAVG = 24,
            SIN = 25,
            SOI = 26,
            Square = 27,
            STD = 28,
            STD_ALATAVG = 29,
            TNI = 30,
            VALUE = 31,
            WANG = 32,
            ANG = 34,
            RGS = 35,
            CDA = 36,
            EOFG = 37,
            GRDX = 38,
            OT51 = 40,
            GRDY = 41,
            DIFF = 42,
            DIVIDE = 94,
            SUM = 43,
            SUMMULTIPLY = 95,
            GRDXY = 44,
            HI = 45,
            LA = 46,
            GRAS = 47,
            LAPLAS = 49,
            ESTIMATE = 50,
            NEURO = 51,
            EXPORT = 52,
            IMPORT = 53,
            FORECAST = 55,
            SUM2 = 56,
            AAMP = 57,
            MULTYPL = 58,
            GRDMOD = 59,
            CDR500_ZM = 60,
            TIMETEN = 61,
            OT81 = 62,
            TREND = 63,
            TIDE = 80,
            UPDATE = 65,
            /// <summary>
            /// raw data (сырые)
            /// </summary>
            RAW = 69,
            /// <summary>
            /// data after magnetic inclination correction
            /// </summary>
            ROT = 70,
            /// <summary>
            /// data after application of tide-killer filter 
            /// </summary>
            FIL = 71,
            /// <summary>
            /// data after correction of temperature and conductivity drifts, and removal salinity spikes 
            /// </summary>
            PRS = 72,
            /// <summary>
            /// Таблица повторяемостей (кол. случаев)
            /// </summary>
            TPROBQ = 73,
            /// <summary>
            /// WYSIWYG
            /// </summary>
            UNKNOWN = 93,
            /// <summary>
            /// Интерполировать
            /// </summary>
            INTERPOLATE = 96,
            /// <summary>
            /// Корень квадратный
            /// </summary>
            SQRT = 81,
            /// <summary>
            /// Климатические расчёты, в частности, для станционных данных. Тип расчётов task_attr.id = CLM_PARAM.
            /// </summary>
            CLM = 79,
            CALC = 91,
            /// <summary>
            /// Расчет обеспеченностей
            /// </summary>
            PROBABILITY = 98,
            /// <summary>
            /// Расчет повторяемостей в процентах
            /// </summary>
            FREQUENCY = 99,
            /// <summary>
            /// Оправдываемость знака величины
            /// </summary>
            RO_ZERO = 100,
            /// <summary>
            /// Дата устойчивого перехода параметра через предел (превышение)
            /// </summary>
            DATE_S_LIMIT_STAB = 102,
            /// <summary>
            /// Дата устойчивого перехода параметра через предел (когда параметр стал ниже)
            /// </summary>
            DATE_F_LIMIT_STAB = 105,
            /// <summary>
            /// Кол-во дней периода, когда параметр устойчиво превышает заданный предел
            /// </summary>
            PERIOD_LIMIT_STATE = 106,
            /// <summary>
            /// Даты начала, окончания устойчивого перехода параметра перез предел и продолжительность этого периода
            /// </summary>
            TRANS_DATE_STATE = 107,
            /// <summary>
            /// Минимальная из средних значений скользящих пятидневок
            /// </summary>
            MIN_5DAY_AVG = 108,
            /// <summary>
            /// Расчетная температура воздуха
            /// </summary>
            CALC_TA = 109,
            /// <summary>
            /// Расчетная зимняя вентиляционная температура воздуха
            /// </summary>
            CALC_TA_WINTER_VENT = 110,
            /// <summary>
            /// Средняя температрура отопительного периода
            /// </summary>
            TA_AVG_HEAT_PERIOD = 111,
            /// <summary>
            /// Амплитуда
            /// </summary>
            AMPL = 112,
            /// <summary>
            /// Среднее значение параметра за период устойчивого перехода через предел
            /// </summary>
            AVG_PARAM_PERIOD_LIMIT_STATE = 113,
            /// <summary>
            /// Значения параметра относительно заданного предела
            /// </summary>
            VALUE_LIMIT = 114,
            /// <summary>
            /// Глубина проникновения 0 в почву
            /// </summary>
            TA0SOIL = 115,
            /// <summary>
            /// Даты периода выше или ниже заданного предела
            /// </summary>
            DATE_SF_LIMIT = 116,
            /// <summary>
            /// Расчетная глубина промерзания почвы
            /// </summary>
            CALC_SOIL_FREEZING = 117,
            /// <summary>
            /// Среднее, без учета величин=0
            /// </summary>
            AVG_NOT_0 = 118,
            /// <summary>
            /// Пасмурный день
            /// </summary>
            PASM_DAY = 119,
            /// <summary>
            /// Ясный день
            /// </summary>
            YSN_DAY = 120,
            /// <summary>
            /// Суммарная продолжительность АЯ (атмосферного явления)
            /// </summary>
            DUR_SUM_AP = 121,
            /// <summary>
            /// Количество элементов параметра относительно заданного предела
            /// </summary>
            COUNT_LIM = 122,
            /// <summary>
            /// Преобразование кода горизонтальной видимости к значению в метрах
            /// </summary>
            VV_KOD_TO_METERS = 123,
            /// <summary>
            /// Расчет повторяемостей элементов параметра относительно заданного предела
            /// </summary>
            FREQ_LIM = 124,
            /// <summary>
            /// Дейсвтие в классах
            /// </summary>
            ACT_IN_PILE = 125,
            /// <summary>
            /// несколько простых Action
            /// </summary>
            SIMPLE_ACTION_ARR = 127,
            /// <summary>
            /// Первая дата в заданном периоде, когда параметр выше или ниже заданного предела
            /// </summary>
            DATE_FIRST_LIM = 126,/// <summary>
                                 /// Последняя дата в заданном периоде, когда параметр выше или ниже заданного предела
                                 /// </summary>
            DATE_LAST_LIM = 128,
            /// <summary>
            /// Дата образования устойчивого снежного покрова
            /// </summary>
            DATE_SNOW_STABLE_START = 129,
            /// <summary>
            /// Дата разрушения устойчивого снежного покрова
            /// </summary>
            DATE_SNOW_STABLE_FINISH = 130,
            /// <summary>
            /// Наличие явления
            /// </summary>
            IS_PHENOM = 131,
            /// <summary>
            /// Продолжительность явления в день с явлением
            /// </summary>
            DUR_AP_IN_DAY = 133,
            /// <summary>
            /// Непрерывная продолжительность явления
            /// </summary>
            DUR_AP = 134,
            /// <summary>
            /// Атмосферное обледенение
            /// </summary>
            ATM_ICING = 135,
            /// <summary>
            /// Климатические расчеты по региону для данных типа FIELD
            /// </summary>
            GRCLM = 136,
            /// <summary>
            /// Стандартное отклонение от среднего в регионе
            /// </summary>
            GRSTD = 137,
            /// <summary>
            /// Критерий Нэша-Сатклифа
            /// </summary>
            R_NS = 138,
            /// <summary>
            /// Расчет относительной влажности
            /// </summary>
            RH = 139,
            /// <summary>
            /// Среднее взятое с указанными весами
            /// </summary>
            AVG_WEIGHT = 140
        }
        /// <summary>
        /// Пространственная характеристика данных. Соответствует словарю Hmdic..dic_common для id_parent = 7.
        /// </summary>
        public enum Space : long
        {
            FIELDTW = 2,
            CRRMTX = 3,
            FIELD = 4,
            STATION = 5,
            PUNCT = 6
        }
        /// <summary>
        /// Базы данных технологии Viaware.Sakura.
        /// </summary>
        public enum DbList : long
        {
            /// <summary>
            /// Метод ОДПП-Свинухова со старыми нормами и регрессией.
            /// </summary>
            A_LRF_LRFD75 = 146,
            AHK = 129,
            FERHRI = 133,

            FILE_BUROVAYA = 137,

            FILE_JMASST = 22,
            FILE_JMAICEC = 111,

            FILE_GRIB = 18,
            FILE_GRIB2 = 119,
            FILE_GRIB1_ERAINT = 134,

            FILE_O_ADCP_JP1 = 84,
            FILE_O_ADCP_JP2 = 56,
            FILE_O_BUYSURF = 120,
            FILE_O_CURR_RCOD = 55,
            FILE_O_CURR_VIA = 58,
            FILE_O_CURR_VIA11 = 107,
            /// <summary>
            /// Файл с данными о течениях компании ЭКС
            /// </summary>
            FILE_O_CURR_ECS = 102,
            FILE_O_DECOMPOSED = 100,
            FILE_O_COMPOSED = 101,
            FILE_O_GOINDUD = 122,
            FILE_O_ICEZPV = 103,
            FILE_O_STN_H = 82,
            FILE_O_LEV_ODPP = 79,
            FILE_PersonaMIS = 35,
            FILE_PersonaMIP = 127,
            FILE_StnHydroDay = 34,
            /// <summary>
            /// ВНИИГМИ текстовые ежесуточные данные
            /// </summary>
            FILE_STN_DAYLY = 51,
            /// <summary>
            /// ВНИИГМИ текстовые срочные данные
            /// </summary>
            FILE_STN_HOUR = 138,
            /// <summary>
            /// Текстовый с шапкой и данными одного параметра: lon;lat;stIndex;stName;value;valueAnom etc...
            /// </summary>
            FILE_STN_XYZ = 110,
            /// <summary>
            /// Текстовый с шапкой типа: //M4311;M4312;M4056;M4202;M4400;P0001_00;P0001_07;P0001_03;P0001_01;P0001_02
            /// и данными : lon;lat;stName;value;valueAnom etc...
            /// 70.68333333;127.4;21921;Кюсюр  ;01/01/1947 00:00;-37.9;-0.382608696;-37.5173913;-46.1;-29.4
            /// 70.68333333;127.4;21921;Кюсюр  ;01/02/1947 00:00;-28.2;5.604166667;-33.80416667;-41.7;-27.1
            /// 70.68333333;127.4;21921;Кюсюр  ;01/03/1947 00:00;-21.9;4.569565217;-26.46956522;-37.3;-15.5
            /// 70.68333333;127.4;21921;Кюсюр  ;01/04/1947 00:00;-14.2;0.926086957;-15.12608696;-18.6;-10.4
            /// </summary>
            FILE_STN_ESIMO = 130,
            /// <summary>
            /// Данные прибора Infinity (течения, темп. воды) переданные ТОИ ДВО РАН в 2014.09.
            /// </summary>
            FILE_O_INFINITY = 147,
            /// <summary>
            /// Данные прибора ADCP Аргонавт, ТОИ, 2014.11 
            /// (похожи на данные FILE_O_POI_ADP250_1411)
            /// </summary>
            FILE_O_POI_2014_ARGONAUT = 150,
            /// <summary>
            /// Данные прибора ADP250, ТОИ, 2014.11 
            /// (похожи на данные FILE_O_POI_ADCP_ARGONAVT_1411)
            /// </summary>
            FILE_O_POI_2014_ADP250 = 153,
            /// <summary>
            /// Данные прибора ADP250 по уровню, ТОИ, 2014.11 
            /// </summary>
            FILE_O_POI_2014_ADP250_LEVEL = 154,
            /// <summary>
            /// Японские данные
            /// </summary>
            FILE_O_ADCP_USJ98 = 151,
            /// <summary>
            /// Данные прибора SBE26 (уровень, темп. воды, ...) переданные ТОИ ДВО РАН в 2014.09.
            /// </summary>
            FILE_O_POI_2014_SBE26 = 148,
            /// <summary>
            /// Файлы с прогнозами метода Свинухова. Типы: 1 - прогноз, 2 - правленный.
            /// </summary>
            FILE_SVN_ODPP = 109,
            FILE_XYZ = 53,
            FILES_WRF_NMM_12 = 124,
            FILES_WRF_HWRF_12 = 126,
            FILES_GFS_OMA_5deg = 125,
            FILES_GFS_OMA_025deg = 777777,
            GisMeteo = 47,

            HCRIndeces = 163,
            HCRMonth = 162,

            HmdClimate = 123,
            Hmdic = 10,
            HmdField2Field = 28,
            HmdFirst = 12,
            HmdEOF = 23,
            HmdGribHour1 = 4,
            HmdGribHour = 20,
            HmdGribHourEra = 149,
            HmdGribMonth = 5,
            HmdFieldClimate = 72,
            HmdOceanStation = 77,
            HmdOperSYNOP = 8,
            HmdStn = 135,
            HmdStnClimate = 145,
            HmdStnDay = 19,
            HmdStnKN02Hour = 3,
            HmdStnMonth = 2,
            HmdStnTmcHour = 1,
            HmdStnHydroDay = 7,
            HmdOperKH02 = 48,
            HmdOperKH15 = 49,
            Tide = 75,
            OmaOceanOper = 9,
            OperFieldAvg = 21,
            Sakura = 11,
            SakuraAnalog = 104,
            WEB = 46,
            /// <summary>
            /// Данные наблюдений на автоматических станциях типа АМС/АМК/АГК в формате XML по http из ПУГМС.
            /// </summary>
            XML_PUGMS_AMS = 128,
            FILE_FCS_HMC = 131,
            HmdFieldFcs = 132,
            /// <summary>
            /// Формат ТММ1 согласно Метод. указаниям, 1983 г.
            /// </summary>
            FILE_STN_TMM1 = 136,
            HmdIndeces = 59,
            Amur = 188
        }
        /// <summary>
        /// Формат хранения данных (Hmdic..Format table rows).
        /// </summary>
        public enum Format : long
        {
            /// <summary>
            /// Поле данных в узлах регулярной сетки, записанное последовательно как float-значения.
            /// </summary>
            GRIDasFLOAT = 8,
            /// <summary>
            /// Relational database as scalar.
            /// </summary>
            RDB = 13,
            /// <summary>
            /// Relational database as vector.
            /// </summary>
            RDB_VEC = 14,
            /// <summary>
            /// Relational database as image (BLOB).
            /// </summary>
            //RDBIMAGE = 15,
            /// <summary>
            /// XYZ field format.
            /// </summary>
            XYZ = 16,
            /// <summary>
            /// GRIB 2 format.
            /// </summary>
            GRIB1 = 7,
            /// <summary>
            /// GRIB 2 format.
            /// </summary>
            GRIB2 = 25
        }
        /// <summary>
        /// Типы уровней.
        /// </summary>
        public enum LevelType : long
        {
            UNKNOWN = 25,
            DEPTH_BELOW_SEA_LEVEL = 21,
            ISOBARIC_LEVEL = 10,
            /// <summary>
            /// Уровень станции, земли, моря.
            /// </summary>
            LND = 1,
            /// <summary>
            /// Стандартный уровень по высоте.
            /// </summary>
            SHG = 11,
            /// <summary>
            /// Уровень моря.
            /// </summary>
            MSL = 6,
            /// <summary>
            /// Уровень моря.
            /// </summary>
            POCHVA = 18
        }
        /// <summary>
        /// Г/м параметры.
        /// </summary>
        public enum Parameter : long
        {
            UNKNOWN = 336,
            /// <summary>
            /// Геопотенциальная высота.
            /// </summary>
            GEOPOTEN_HEIGHT = 4,
            /// <summary>
            /// Ветер направление надо льдом.
            /// </summary>
            ICE_WIND_DIR = 175,
            /// <summary>
            /// Ветер скорость надо льдом.
            /// </summary>
            ICE_WIND_VELOS = 176,
            /// <summary>
            /// Темп. воздуха надо льдом.
            /// </summary>
            ICE_TA = 177,
            /// <summary>
            /// Темп. воды подо льдом.
            /// </summary>
            ICE_TW = 178,
            /// <summary>
            /// FAST - неподвижный лёд, припай (балл)
            /// </summary>
            ICE_FAST_B = 179,
            /// <summary>
            /// Припай макс. ширина
            /// </summary>
            ICE_FAST_WIDTH_MAX = 181,
            /// <summary>
            /// Припай мин. ширина
            /// </summary>
            ICE_FAST_WIDTH_MIN = 182,
            /// <summary>
            /// Припай ширина в створе (чего-то, не помню).
            /// </summary>
            ICE_FAST_WIDTH_STVOR = 183,
            /// <summary>
            /// Торосистость макс. (балл)
            /// </summary>
            ICE_FAST_TOROS_B_MAX = 184,
            /// <summary>
            /// Торосистость мин. (балл)
            /// </summary>
            ICE_FAST_TOROS_B_MIN = 185,
            /// <summary>
            /// Разрушенность (балл)
            /// </summary>
            ICE_FAST_DESTROY_B = 186,
            /// <summary>
            /// ПРИЗНАК РАЗРУШ.ПРИПАЯ,БАЛЛ
            /// </summary>
            ICE_FAST_DESTROY_Q = 187,
            /// <summary>
            /// Направление створа от ледового пункта (грд)
            /// </summary>
            ICE_PUNCT_STVOR = 360,
            /// <summary>
            /// Высота ледового пункта (м)
            /// </summary>
            ICE_PUNCT_HEIGHT = 361,
            /// <summary>
            /// Сплоченность преобладающая, балл
            /// </summary>
            ICE_SPLOCH_PREOBL = 202,
            /// <summary>
            /// Температура воздуха
            /// </summary>
            TA = 3,
            /// <summary>
            /// Точка росы (температура)
            /// </summary>
            TD = 8,
            /// <summary>
            /// Барическая тенденция (величина)
            /// </summary>
            PP = 10,
            /// <summary>
            /// Количество осадков за период
            /// </summary>
            RR = 13,
            /// <summary>
            /// Duration of period of reference for amount of precip, ending at the time of the report FM13
            /// </summary>
            tR = 417,
            /// <summary>
            /// Количество облаков CL
            /// </summary>
            NL = 46,
            /// <summary>
            /// Высота основания нижнего рода об­лаков (код)
            /// </summary>
            CL = 47,
            /// <summary>
            /// Высота основания нижнего рода об­лаков (метры)
            /// </summary>
            CL_M = 427,
            /// <summary>
            /// Тип облаков нижнего яруса
            /// </summary>
            CLL = 77,
            /// <summary>
            /// Температура почвы
            /// </summary>
            TSOIL = 56,
            /// <summary>
            /// Погода в срок наблюдения
            /// </summary>
            WW = 72,
            /// <summary>
            /// Прошедшая погода
            /// </summary>
            W = 73,
            /// <summary>
            /// Характеристика барической тенден­ции (код)
            /// </summary>
            A = 74,
            /// <summary>
            /// Признак наличия облачности ниже уровня станции (КН-01)
            /// </summary>
            //CALFA = 366,
            /// <summary>
            /// Тип облаков среднего яруса
            /// </summary>
            CM = 78,
            /// <summary>
            /// Тип облаков верхнего яруса
            /// </summary>
            CA = 79,
            /// <summary>
            /// Тип кучевых облаков. (тип облаков вертикального развития)
            /// </summary>
            CC = 126,
            /// <summary>
            /// Недостаток насыщения
            /// Дефицит упругости водяного пара
            /// </summary>
            SATD = 129,
            /// <summary>
            /// Тип слоистой, слоисто-кучевой облачности
            /// </summary>
            CCR = 135,
            /// <summary>
            /// Тип слоисто-дождевых и разорванно-дождевых облаков
            /// </summary>
            CN = 136,
            /// <summary>
            /// Состояние пов-ти почвы (код)
            /// </summary>
            LAND = 137,
            /// <summary>
            /// Температура 
            /// </summary>
            T = 153,
            /// <summary>
            /// Температура воды
            /// </summary>
            TW = 9,
            /// <summary>
            /// Температура воды мин.
            /// </summary>
            TW_MIN = 258,
            /// <summary>
            /// Температура воды макс.
            /// </summary>
            TW_MAX = 259,
            /// <summary>
            /// Температура воды средняя.
            /// </summary>
            TW_AVG = 261,
            /// <summary>
            /// Парциальное давление водяного па­ра (Упруность водяного пара)
            /// </summary>
            WATERVAPOR_UPR = 34,
            /// <summary>
            /// Относительная влажность
            /// </summary>
            RELATIVE_HUMID = 128,
            /// <summary>
            /// Давление 
            /// </summary>
            P = 2,
            /// <summary>
            /// Давление на уровне моря
            /// </summary>
            PRESS_MSL = 130,
            /// <summary>
            /// Давление на уровне станции
            /// </summary>
            PRESS_STN = 131,
            /// <summary>
            /// Расход воды
            /// </summary>
            kQQQ8 = 33,
            Qf = 354,
            Qb = 353,
            /// <summary>
            /// Общее количество облаков (N, ball)
            /// </summary>
            CLOUD_TOTAL_BALL = 45,
            /// <summary>
            /// Общее количество облаков (N, code)
            /// </summary>
            CLOUD_TOTAL_CODE = 422,
            /// <summary>
            /// Количество облаков нижнего яруса
            /// </summary>
            CLOUD_LOW_BALL = 46,
            /// <summary>
            /// Количество облаков нижнего яруса
            /// </summary>
            CLOUD_LOW_CODE = 420,
            /// <summary>
            /// Признак наличия облачн. ниже уров. станции (код)
            /// </summary>
            CLOUDNESS_LOW_STN = 428,
            /// <summary>
            /// Атм. явления, ТГМ-1
            /// </summary>
            PHENOM_ATM = 139,
            /// <summary>
            /// АЯ11 Ливневой дождь
            /// </summary>
            AP11_RAIN_LIVEN = 348,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) жижкие осадки
            /// </summary>
            AP11_RAIN_LIQUID = 305,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) твёрдые осадки
            /// </summary>
            AP11_RAIN_HARD = 306,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) мокр. снег т ливневой мокр. снег
            /// </summary>
            AP11_SNOW_LIQUID = 307,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) туман
            /// </summary>
            AP11_FOG = 308,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) изморозь
            /// </summary>
            AP11_IZMOROZ = 309,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) гололед
            /// </summary>
            AP11_GOLOLED = 351,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) пыльная буря и пыльный поземок
            /// </summary>
            AP11_PILNAYA_BURIA = 310,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) метель
            /// </summary>
            AP11_METEL = 311,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) роса
            /// </summary>
            AP11_ROSA = 350,
            /// <summary>
            /// АЯ11 (АЯ с продолжительностью) позёмок
            /// </summary>
            AP11_POZIOMOK = 349,

            /*/// <summary>
            /// АЯ град
            /// </summary>
            AP7_GRAD = 282,
            /// <summary>
            /// АЯ (наличие АЯ) поземный туман, ледяной туман
            /// </summary>
            AP7_FOG_POZEMNY = 424,
            /// <summary>
            /// АЯ7 (наличие АЯ) дымка
            /// </summary>
            AP7_DIMKA = 272,
            /// <summary>
            /// АЯ7 (наличие АЯ) иней
            /// </summary>
            AP7_INEY = 297,
            /// <summary>
            /// АЯ7 (наличие АЯ) снеж. мгла
            /// </summary>
            AP7_SNOW_MGLA = 425,
            /// <summary>
            /// АЯ7 (наличие АЯ) полярное сияние
            /// </summary>
            AP7_POLAR_SHINE = 283,
            /// <summary>
            /// АЯ7 (наличие АЯ) мираж
            /// </summary>
            AP7_MIRASH = 290,
            /// <summary>
            /// АЯ7 (наличие АЯ) гроза
            /// </summary>
            AP7_GROZA = 277,
            /// <summary>
            /// АЯ7 (наличие АЯ) мгла
            /// </summary>
            AP7_MGLA = 275,
            /// <summary>
            /// АЯ7 (наличие АЯ) смерч
            /// </summary>
            AP7_SMERCH = 302,
            /// <summary>
            /// АЯ7 (наличие АЯ) ледяные иглы
            /// </summary>
            AP7_ICE_IGLY = 301,
            /// <summary>
            /// АЯ7 (наличие АЯ) шквал
            /// </summary>
            AP7_SHKVAL = 293,
            /// <summary>
            /// АЯ7 (наличие АЯ) парение моря
            /// </summary>
            AP7_PARENIE_SEA = 295,
            /// <summary>
            /// АЯ7 (наличие АЯ) вихрь
            /// </summary>
            AP7_VIHR = 299,
      * */
            /// <summary>
            /// АЯ дополнительная информация об АЯ
            /// </summary>
            AP_ADD_INFO = 426,


            /// <summary>
            /// АЯ смерч 
            /// </summary>
            AP_SMERCH = 302,
            /// <summary>
            /// АЯ вихрь
            /// </summary>
            AP_VIHR = 299,
            /// <summary>
            /// АЯ шквал
            /// </summary>
            AP_SHKVAL = 293,
            /// <summary>
            /// АЯ мираж
            /// </summary>
            AP_MIRAZH = 290,
            /// <summary>
            /// АЯ роса
            /// </summary>
            AP_ROSA = 296,
            /// <summary>
            /// АЯ иней
            /// </summary>
            AP_INEI = 297,
            /// <summary>
            /// АЯ гололед
            /// </summary>
            AP_GOLOLED = 298,
            /// <summary>
            /// АЯ изморозь кристаллическая
            /// </summary>
            AP_IZM_KRISTAL = 300,
            /// <summary>
            /// АЯ изморозь зернистая
            /// </summary>
            AP_IZM_ZERN = 430,
            /// <summary>
            /// АЯ гололедица
            /// </summary>
            AP_GOLOLEDICA = 431,
            /// <summary>
            /// АЯ парение моря (озера, реки)
            /// </summary>
            AP_PARENIE_MORYA = 295,
            /// <summary>
            /// АЯ дымка
            /// </summary>
            AP_DIMKA = 272,
            /// <summary>
            /// АЯ туман
            /// </summary>
            AP_TUMAN = 432,
            /// <summary>
            /// АЯ туман просвечивающий
            /// </summary>
            AP_TUMAN_PROSV = 433,
            /// <summary>
            /// АЯ туман поземный
            /// </summary>
            AP_TUMAN_POZEM = 273,
            /// <summary>
            /// АЯ туман ледяной
            /// </summary>
            AP_TUMAN_LED = 434,
            /// <summary>
            /// АЯ туман ледяной просвечивающий
            /// </summary>
            AP_TUMAN_LED_PROSV = 274,
            /// <summary>
            /// АЯ туман ледяной поземный
            /// </summary>
            AP_TUMAN_LED_POZEM = 435,
            /// <summary>
            /// АЯ туман в окрестности станции
            /// </summary>
            AP_TUMAN_STN = 436,
            /// <summary>
            /// АЯ туман поземный в окрестности станции
            /// </summary>
            AP_TUMAN_POZEM_STN = 437,
            /// <summary>
            /// АЯ мгла
            /// </summary>
            AP_MGLA = 275,
            /// <summary>
            /// АЯ пыльный поземок
            /// </summary>
            AP_PILN_POZEM = 438,
            /// <summary>
            /// АЯ пыльная буря
            /// </summary>
            AP_PILN_BURYA = 294,
            /// <summary>
            /// АЯ мгла снежная
            /// </summary>
            AP_MGLA_SNEG = 425,
            /// <summary>
            /// АЯ поземок
            /// </summary>
            AP_POZEMOK = 291,
            /// <summary>
            /// АЯ метель низовая
            /// </summary>
            AP_METEL_NIZOV = 439,
            /// <summary>
            /// АЯ метель с выпадением снега
            /// </summary>
            AP_METEL_SNEG = 440,
            /// <summary>
            /// АЯ метель общая (вьюга, буран, пурга)
            /// </summary>
            AP_METEL = 441,
            /// <summary>
            /// АЯ иглы ледяные
            /// </summary>
            AP_IGLI_LED = 301,
            /// <summary>
            /// АЯ ледяной дождь
            /// </summary>
            AP_LED_DOZHD = 284,
            /// <summary>
            /// АЯ крупа ледяная
            /// </summary>
            AP_KRUPA_LED = 442,
            /// <summary>
            /// АЯ крупа снежная
            /// </summary>
            AP_KRUPA_SNEG = 443,
            /// <summary>
            /// АЯ зерна снежные
            /// </summary>
            AP_ZERNA_SNEG = 444,
            /// <summary>
            /// АЯ морось
            /// </summary>
            AP_MOROS = 280,
            /// <summary>
            /// АЯ дождь
            /// </summary>
            AP_DOZHD = 279,
            /// <summary>
            /// АЯ дождь ливневый
            /// </summary>
            AP_DOZHD_LIVN = 281,
            /// <summary>
            /// АЯ град
            /// </summary>
            AP_GRAD = 282,
            /// <summary>
            /// АЯ снег
            /// </summary>
            AP_SNEG = 445,
            /// <summary>
            /// АЯ снег ливневый
            /// </summary>
            AP_SNEG_LIVN = 446,
            /// <summary>
            /// АЯ снег мокрый
            /// </summary>
            AP_SNEG_MOKR = 285,
            /// <summary>
            /// АЯ снег ливневый мокрый
            /// </summary>
            AP_SNEG_LIVN_MOKR = 286,
            /// <summary>
            /// АЯ гроза
            /// </summary>
            AP_GROZA = 277,
            /// <summary>
            /// АЯ зарница
            /// </summary>
            AP_ZARNICA = 448,
            /// <summary>
            /// АЯ полярное сияние
            /// </summary>
            AP_POL_SIYANIE = 283,
            /// <summary>
            /// АЯ изморозь кристаллическая, изморозь зернистая
            /// </summary>
            AP_IZM_KRIST_ZER = 449,
            /// <summary>
            /// АЯ венец вокруг солнца и луны
            /// </summary>
            AP_VENEC = 450,
            /// <summary>
            /// АЯ гало вокруг солнца и луны
            /// </summary>
            AP_GALO = 451,
            /// <summary>
            /// АЯ гроза отдаленная
            /// </summary>
            AP_GROZA_OTDAL = 278,
            /// <summary>
            /// АЯ чистый воздух
            /// </summary>
            AP_CHIST_VOZDUH = 276,
            /// <summary>
            /// АЯ Гроза, Отдаленная гроза
            /// </summary>
            AP_GROZA_OTDAL_GROZA = 454,
            /// <summary>
            /// АЯ Ледяная крупа, ливневый снег, снежная крупа
            /// </summary>
            AP_LED_KR_LIVN_SNEG_SNEG_KRUPA = 456,
            /// <summary>
            /// АЯ Низовая метель, метель с выпадением снега
            /// </summary>
            AP_NIZ_METEL_METEL_SO_SNEGOM = 457,
            /// <summary>
            /// АЯ Снег, снежные зерна
            /// </summary>
            AP_SNEG_ZERNA_SNEG = 455,
            /// <summary>
            /// АЯ Твердый налет, жидкий налет
            /// </summary>
            AP_NALET = 458,
            /// <summary>
            /// АЯ бурный ветер
            /// </summary>
            AP_BURN_VETER = 292,


            /// <summary>
            /// U-компонента ветра
            /// </summary>
            UComponentWind = 154,
            /// <summary>
            /// V-компонента ветра
            /// </summary>
            VComponentWind = 155,

            UComponentCurrent = 337,
            VComponentCurrent = 338,
            /// <summary>
            /// Гориз. видимость (м or code)
            /// </summary>
            VV = 29,
            Coordinates = 341,
            /// <summary>
            /// Глубина|высота снежного покрова
            /// </summary>
            SNOW_HEIGHT = 31,
            /// <summary>
            /// Высота снежного покрова по постоянной рейке № 1 (см).
            /// </summary>
            S1 = 389,
            /// <summary>
            /// Высота снежного покрова по постоянной рейке № 2 (см).
            /// </summary>
            S2 = 390,
            /// <summary>
            /// Высота снежного покрова по постоянной рейке № 3 (см).
            /// </summary>
            S3 = 391,
            /// <summary>
            /// Степень покрытия участка снегом, cтепень покр.окрестности станции снегом
            /// </summary>
            SNOW_COVER_DEGREE = 322,
            /// <summary>
            /// Характеристика залегания снежного покрова
            /// </summary>
            SNOW_CHARACTER = 321,
            /// <summary>
            /// Продолжительность солнечного сия­ния
            /// </summary>
            SUN_DURATION = 55,
            /// <summary>
            /// Первый час, после которого светило солнце
            /// </summary>
            SUN_FIRST_HOUR = 453,
            /// <summary>
            /// Темп. пов. почвы по срочному термометру (град с точн. до десятых).
            /// </summary>
            Tp = 368,
            /// <summary>
            /// Темп. пов. почвы в срок по спирту минимального термометра (град с точн. до десятых).
            /// </summary>
            Tpc = 369,
            /// <summary>
            /// Мин. темп. пов. почвы за период меж сроками по штифту миним-го термометра (град с точн. до десятых).
            /// </summary>
            Tpm = 377,
            /// <summary>
            /// Макс. темп. пов. почвы за период между сроками по макс термометру (град с точн. до десятых).
            /// </summary>
            Tpx = 378,
            /// <summary>
            /// Темп. по макс. термометру на пов. почвы после встряхивания (град с точн. до десятых).
            /// </summary>
            Tpp = 379,
            /// <summary>
            /// Темпю воздуха в срок по "смоченному" термометру (град с точн. до десятых).
            /// </summary>
            Tacm = 262,
            /// <summary>
            /// Темп. воздуха в срок по спирту минимального термометра (град с точн. до десятых).
            /// </summary>
            Tamc = 380,
            /// <summary>
            /// Миним. темп. воздуха за период между сроками по штифту мин. термометра (град с точн. до десятых).
            /// </summary>
            Tam = 382,
            /// <summary>
            /// Макс. темп. воздуха за период между сроками по штифту макс. термометра (град с точн. до десятых).
            /// </summary>
            Tax = 383,
            /// <summary>
            /// Макс. темп. воздуха за период между сроками по штифту макс. термометра после встряхивания (град с точн. до десятых).
            /// </summary>
            Taxp = 387,
            /// <summary>
            /// Температура воздуха по термографу
            /// </summary>
            Tterm = 394,
            /// <summary>
            /// Уровень воды.
            /// </summary>
            WATER_LEVEL = 32,
            WATER_LEVEL_HOURLY = 325,
            WATER_LEVEL_AVGDAY = 260,
            WATER_LEVEL_MIN = 255,
            WATER_LEVEL_MAX = 256,
            /// <summary>
            /// Солёность воды.
            /// </summary>
            WATER_SOLENOST = 27,
            /// <summary>
            /// Условная плотность воды (ro).
            /// </summary>
            WATER_USLPLOTN_RO = 339,
            /// <summary>
            /// Условная плотность воды (sigma).
            /// </summary>
            WATER_USLPLOTN_SIGMA = 358,
            /// <summary>
            /// Волнение.
            /// </summary>
            WAVE_TYPE_CODE = 263,
            WAVE_DIR_MAIN_GRD = 264,
            WAVE_DIR_SEC_GRD = 265,
            WAVE_HEIGHT_AVG = 266,
            WAVE_HEIGHT_MAX = 267,
            WAVE_PERIOD_AVG = 268,
            WAVE_LENGTH_AVG = 269,
            /// <summary>
            /// Период ветрового волнения
            /// </summary>
            WAVE_WIND_PERIOD = 20,
            /// <summary>
            /// Высота ветровых волн
            /// </summary>
            WAVE_WIND_HEIGHT = 21,
            /// <summary>
            /// Период ветрового волнения (инструментальные)
            /// </summary>
            WAVE_WIND_PERIOD_INSTR = 403,
            /// <summary>
            /// Высота ветрового волнения (инструментальные)
            /// </summary>
            WAVE_WIND_HEIGHT_INSTR = 404,
            /// <summary>
            /// Направление зыби - первая система волн зыби
            /// </summary>
            WAVE_SWELL1_DIRECTION = 23,
            /// <summary>
            /// Период зыби - первая система волн зыби
            /// </summary>
            WAVE_SWELL1_PERIOD = 24,
            /// <summary>
            /// Высота зыби - первая система волн зыби
            /// </summary>
            WAVE_SWELL1_HEIGHT = 67,
            /// <summary>
            /// Направление зыби - вторая система волн зыби
            /// </summary>
            WAVE_SWELL2_DIRECTION = 405,
            /// <summary>
            /// Период зыби - вторая система волн зыби
            /// </summary>
            WAVE_SWELL2_PERIOD = 406,
            /// <summary>
            /// Высота зыби - вторая система волн зыби
            /// </summary>
            WAVE_SWELL2_HEIGHT = 407,
            /// <summary>
            /// Направление ветра
            /// </summary>
            WIND_DIR = 5,
            /// <summary>
            /// Скорость ветра
            /// </summary>
            WIND_VELOS = 6,
            WIND_AVG8 = 355,
            WIND_MAX8 = 356,
            WIND_ABSMAX = 357,
            /// <summary>
            /// Характеристика регулярности ветра (код)
            /// </summary>
            WIND_REGULARITY = 452,
            /// <summary>
            /// Макс. скор. ветра за 3 часа (m/s) КН-01
            /// </summary>
            WIND_MAX3 = 367,
            /// <summary>
            /// Дальность видимости поверхности моря 
            /// </summary>
            VISIB_SEASURF = 174,
            ///// <summary>
            ///// Дефицит упругости водяного пара (гПа).
            ///// </summary>
            //D = 388,
            /// <summary>
            /// Особые явления (Синоптика)
            /// </summary>
            DANGER_PHEN = 50,
            /// <summary>
            /// Indicator for source and units of wind speed. FM13
            /// </summary>
            iw = 397,
            /// <summary>
            /// Indicator for inclusion or omission of precipitation data. FM13
            /// </summary>
            ir = 398,
            /// <summary>
            /// Indicator for the type of station operation and whether group 7 is encoded in section 1.
            /// See code table 1860 of the land synoptic code.
            /// FM13
            /// </summary>
            iX = 399,
            /// <summary>
            /// - Direction of ship movement made good during the previousthree hours.
            /// </summary>
            Ds = 401,
            /// <summary>
            /// Speed of the ship made good during the previous three hours.
            /// </summary>
            Vs = 402,
            /// <summary>
            ///Is - Ice accretion code. See code table 1751
            ///Code table 1751 Is — Ice accretion on ships
            ///Code figure Description
            ///1 Icing from ocean spray.
            ///2 Icing from fog.
            ///3 Icing from spray and fog.
            ///4 Icing from rain.
            ///5 Icing from spray and rain.
            /// </summary>
            Is = 69,
            /// <summary>
            /// EsEs - Thickness of ice accretion on ship in centimeters.
            /// </summary>
            EsEs = 25,
            /// <summary>
            /// Rate of ice accretion. See code table 3551.
            ///
            ///Code table 3551 Rs — Rate of ice accretion on ships
            ///
            ///Code figure Description
            ///0 Ice not building up.
            ///1 Ice building up slowly.
            ///2 Ice building up rapidly.
            ///3 Ice melting or breaking up slowly.
            ///4 Ice melting or breaking up rapidly.
            /// </summary>
            Rs = 68,
            /// <summary>
            /// ICING + plain language - Report on ice accretion on ships. When the ice
            ///accretion on ships is reported in plain language, it is preceded by 
            ///the word ICING.
            /// </summary>
            ICINGText = 408,
            /// <summary>
            /// Concentration or arrangement ofsea ice. See code table 0639. FM13
            /// </summary>
            ci = 409,
            /// <summary>
            /// Stage of development of the sea ice at the time of the observation. See code table 3739. FM13
            /// </summary>
            Si = 410,
            /// <summary>
            /// Ice of land origin present at time of observation. See code table 0439. FM13
            /// </summary>
            bi = 411,
            /// <summary>
            /// Orientation of the principal edge of the sea ice at the time of the obs. table 0739. FM13
            /// </summary>
            Di = 413,
            /// <summary>
            /// Present ice situation and trend of conditions over preceding 3 hours. See code table 5239. FM13
            /// </summary>
            zi = 414,
            /// <summary>
            /// HwaHwaHwa - Height of instrumetal measured waves to the nearest 0.1 meters. FM13
            /// </summary>
            HwaHwaHwa = 415,
            /// <summary>
            /// Атмосферные явления
            /// </summary>
            AP = 429

        }
        public enum DbType : int
        {
            MSSQL = 1,
            FILE = 2,
            POSTGRESQL = 3
        }
        /// <summary>
        /// Атрибуты объектов технологии Viaware.Sakura - параметров, записей каталога данных, задач технологии. (Hmdic..t_attr table rows).
        /// </summary>
        public enum TaskAttr : long
        {
            DB_TYPE_ID = 232,
            _ROOT = 1,
            X_TimeShiftMax = 3,
            METRIC_LIMIT = 4,
            METRIC_LIMIT1 = 6,
            ANGA_XQMin = 7,
            ANGA_Metric = 9,
            ANGA_Metric1 = 10,
            X_TimeShiftMin = 11,
            SELECTMETHOD = 12,
            ID_GEOREG = 15,
            DATE_0_S = 16,
            DATE_IDEP_S = 19,
            DATE_IDEP_F = 20,
            METRIC = 21,
            MONTH = 22,
            PARAMS = 23,
            METRIC_ORDER = 24,
            LAG_X = 25,
            LAG_Y = 26,
            LAG_XY = 27,
            SQL_HMD = 28,
            ACTION = 29,
            DST_HMDCTL_IS_INS = 30,
            DST_HMDCTL_IS_DEL = 32,
            ACTION_SUB = 33,
            /// <summary>
            /// Допустимое кол. отсутствующих элементов данных (шт).
            /// </summary>
            COUNT_NULL = 212,
            /// <summary>
            /// Допустимое кол. отсутствующих элементов данных (%).
            /// </summary>
            COUNT_NULL_PERC = 219,
            DATE_0_F = 34,
            ACTION_TIME = 36,
            DST_HMDIC_FORMAT = 37,
            DST_CONNECTION = 38,
            DATE_ADD_Q = 39,
            DST_HMDIC_CENTER = 40,
            TIME_NUM = 42,
            Y_ID_HMD_SET = 43,
            Y_ID_HMD = 44,
            ANG_Q = 45,
            ID_TASK_PARENT = 46,
            TIME_SHIFT = 48,
            TIME_SHIFT_MIN = 49,
            TIME_SHIFT_MAX = 51,
            MATHVAR_SET = 52,
            LAG_Y_STEP = 53,
            LAG_X_STEP = 54,
            MIN_DEP_LENGTH = 55,
            MIN_UDEP_LENGTH = 56,
            COEF_A = 57,
            COEF_B = 58,
            X_MIN_Q = 59,
            TASK_RESULT_DEL = 61,
            ESTIM_TYPE_FST = 62,
            ESTIM_TYPE_FT = 63,
            ESTIM_TYPE_RST = 64,
            ESTIM_DEP = 65,
            ESTIM_UDEP = 66,
            ESTIM_ALL = 67,
            ID_PARAM = 98,
            ID_LTYPE = 99,
            ID_CTL_CLM_AVG = 125,
            ID_CTL_SET = 144,
            IS_CLM = 199,
            NER_TRAIN_PEPOCHS = 68,
            NER_TRAIN_PGOAL = 69,
            NER_TRANSFER_FCN = 70,
            NER_LERN_FCN = 71,
            NER_TRAIN_FCN = 72,
            NER_PERFOM_FCN = 73,
            NER_SPREAD = 74,
            NER_TYPE_FUNC = 75,
            NER_LAYERS_Q = 76,
            NER_LAUERS_SIZE = 77,
            NER_LERN_FCNS = 79,
            MATHVAR = 80,
            TIME_SEL_PRINCIP = 81,
            X_MAX_Q = 82,
            X_X_CRR_LIMIT = 83,
            X_CTL_ID_SET = 186,
            SRC_CONNECTION = 84,
            SRC_HMDIC_FORMAT = 86,
            SRC_HMD_ID = 185,
            FILE_NAME_CONTENT = 87,
            DESCRIPTION = 88,
            LATSTEP = 89,
            LONSTEP = 90,
            LEVEL_VALUE = 220,
            PARENT0 = 91,
            PARENT1 = 92,
            PARAM_NAME = 200,
            DATA_SOURCE = 93,
            DST_ID_DB_LIST = 95,
            SRC_ID_DB_LIST = 96,
            ST_REGION_ID = 97,
            LOG_SF = 100,
            SRC_CNN_OLE = 102,
            SRC_CNN_MSSQL = 103,
            STATION_ID = 221,
            STATION_TYPE = 228,
            DST_TABNAME = 104,
            IS_UPDATABLE = 105,
            DST_HMDCTL_ID = 106,
            PARENTS = 107,
            CLASS = 108,
            ESTIM_TYPE_RSTCLS = 110,
            WEIGHT_X_TYPE = 111,
            WEIGHT_TSHIFT = 112,
            X_PREPA_ACTION_ID = 113,
            DATE_S_CLM = 114,
            DATE_F_CLM = 115,
            COORDNUM = 116,
            ST_NAME = 117,
            X_MIN_Q_P = 118,
            X_MAX_Q_P = 119,
            TASK_EXEC_STR = 120,
            TASK_EXEC_TYPE = 121,
            FCS_METH_ADD = 122,
            PRED_METH_SELECT = 126,
            TASK_ID = 128,
            SRC_TABNAME = 129,
            DST_HMDIC_SPACE = 130,
            ID_GEOREG_SET = 131,
            DST_HMDCTL_UEXIS = 132,
            IS_TEST_ONLY = 133,
            IS_REPEAT_ACTION = 134,
            CODE_FORM = 137,

            VAL_ISO_STEPSIGN = 192,
            VAL_ISOSTEP = 187,
            VAL_LEG_STEPSIGN = 193,
            VAL_LEG_MAX = 195,
            VAL_LEG_MIN = 194,
            VAL_PRECISION = 198,
            VAL_UNIT = 196,
            VAL2REAL_ADD = 182,
            VAL2REAL_MULTYP = 184,
            VAL_NAME_ADD = 197,

            LEVEL_VEL_MULTYP = 203,
            DATE_FORMAT = 208,

            ST_NAME_VNIIGMI = 209,

            ID_GRID = 141

        }
        /// <summary>
        /// Параметры вектора, в частности, скорости течений.
        /// </summary>
        public enum CurrentForm
        {
            DIRTO_MOD = 1, UV = 2, MOD_DIRTO = 3, VALUE = 4, DIRFROM_MOD = 5
        }
        /// <summary>
        /// Положение одного отрезка (точки) относительно другого отрезка (точки).
        /// </summary>
        public enum LocationSide : int
        {
            Left, // Слева
            Right,
            Include,
            Above,
            Equial,
            Error // Если границы периодов не корректны.
        }
        /// <summary>
        /// Состояние (для отображения) экранной формы.
        /// </summary>
        public enum FormState
        {
            Ordinal = 1,
            Filter = 2,
            Insert = 3,
            Update = 4
        }
        /// <summary>
        /// Формат поля данных.
        /// </summary>
        public enum FieldFormat : long
        {
            GRID = 1,
            XYZ = 2
        }
        /// <summary>
        /// Словари для географических регионов.
        /// </summary>
        public class Geo
        {
            /// <summary>
            /// Формат строки, содержащей географическую координату точки (широту или долготу).
            /// </summary>
            public enum Formats { GRDDEC = 0, GRD_MINDEC, GRD_MIN_SEC };
            /// <summary>
            /// Наименование форматов строки, содержащей географическую координату точки (широту или долготу).
            /// </summary>
            public string[] formatNames = new string[] { "градусы с десятыми", "градусы и минуты с десятыми", "градусы, минуты, секунды" };
            /// <summary>
            /// Широта, долгота.
            /// </summary>
            public enum LatOrLon { LAT = 0, LON }
            /// <summary>
            /// Доли, части географической координаты.
            /// </summary>
            public enum GrdMinSec { GRD = 0, MIN, SEC }
            /// <summary>
            /// Географические проекции.
            /// </summary>
            public enum Projection : long
            {
                LATLON = 0, //	Latitude/Longitude Grid also called Equidistant Cylindrical or Plate Carree projection grid
                MERCATOR = 1, //Mercator Projection Grid 
                GNOMONIC = 2, //	Gnomonic Projection Grid 
                LAMBERT = 3, //	Lambert Conformal, secant or tangent, conical or bipolar (normal or oblique) Projection Grid
                GAUSS = 4, //	Gaussian Latitude/Longitude Grid
                POLAR = 5, //	Polar Stereographic Projection Grid
                LAMBERT_OBL = 13, //Oblique Lambert conformal, secant or tangent, conical or bipolar, projection 
            }
        }
        public enum DataSource : long
        {
            FERHRI_ODPP = 3
        }
        public enum Center : long
        {
            ECMF = 98,
            US_WS_NMC = 7
        }
        public enum ParamQuality : long
        {
            /// <summary>
            /// Достоверно
            /// </summary>
            DOST = 0,
            /// <summary>
            /// Достоверно и восстановлено автоматически
            /// </summary>
            DOST_VOST_AUTO = 1,
            /// <summary>
            /// Достоверно и восстановлено вручную
            /// </summary>
            DOST_VOST_HAND = 2,
            /// <summary>
            /// Забраковано
            /// </summary>
            ZABRAK = 3,
            /// <summary>
            /// Отсутствует
            /// </summary>
            ABSENT = 4,
            /// <summary>
            /// Сомнительно
            /// </summary>
            DOUBTFUL = 5,
            /// <summary>
            /// Наблюдения не проводились
            /// </summary>
            OBSERV_NO = 6,
            /// <summary>
            /// Отсутствие осадков или в данный срок наблюдения за ними не проводятся
            /// </summary>
            RR_NO = 7,
            /// <summary>
            /// Забраковано, т.к. величина не принадлежит допустимым значениям
            /// </summary>
            BRAK_BY_LIMIT = 8,
            /// <summary>
            /// Наблюдения были, явление не наблюдалось
            /// </summary>
            OBSERV_YES = 9

        }
        public enum SoilSurfaceType
        {
            OGOLENAYA = 1,
            ESTESTV_POKROV = 2
        }
        public enum SnowMeasureLocation
        {
            Reika1 = 1,
            Reika2 = 2,
            Reika3 = 3,
            /// <summary>
            /// Среднее по рейкам
            /// </summary>
            Reika123 = 4,
            SoilTermometer = 5
        }
        /// <summary>
        /// Нумератор форматов исх. файлов, который зависит от года.
        /// </summary>
        public enum DbListSubType
        {
            SUTKI_65_1 = 1023,      // c АЯ
            SUTKI_77_1 = 1011,     // c АЯ

            SUTKI_85_04_1 = 1012,
            SUTKI_85_04_2 = 1013,  //АЯ
            SUTKI_85_04_31 = 1014, // Т почвы
            SUTKI_85_04_32 = 1015, // Т почвы
            SUTKI_85_04_33 = 1016, // Т почвы

            SUTKI_02_09_1 = 1017,
            SUTKI_02_09_21 = 1018, //АЯ продолж
            SUTKI_02_09_22 = 1019, //АЯ наличие
            SUTKI_02_09_31 = 1020, // Т почвы
            SUTKI_02_09_32 = 1021, // Т почвы
            SUTKI_02_09_33 = 1022,  // Т почвы

            HOUR4_36_65_12 = 3,//ТМ1Сроки преобразованный к версии Восхода66
            HOUR4_36_65_11 = 1,//ТМ1 сроки исходный

            HOUR8_66_83_11 = 1002,//Восход исходный (каждая запись впритык к предыдущей)
            HOUR8_66_83_12 = 1006,//Восход исходный (каждая запись отдельной строкой)
            HOUR8_66_83_2 = 1009,//Восход текстовый

            HOUR8_84 = 1010//TMC бинарный
        }
        public enum DateTimeDirection
        {
            ForwardBackward = 0,
            ForwardOnly = 1,
            BackwardOnly = 2
        }
    }
}
