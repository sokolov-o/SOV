using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FERHRI.Social;
using FERHRI.Niokr;

namespace Import.FERHRISalary
{
    /// <summary>
    /// Импорт таблицы зарплаты сотрудников ДВНИГМИ за 07.2017 г.
    /// в БД Social & HIOKR.
    /// Предполагается, что БД пустые и конфликтов не будет.
    /// 
    /// Для данного алгоритма табл staff, salary_staff, salary_employee д.б. пустой, иначе могут быть дубли.
    /// 
    /// 
    ///  <строка сумм, пропускаем>                        330 950.19  		61 980.00  	84 000.00  		46 613.31  				711 620.40  		881 647.44  	4 544 907.77  	 54 538 893.24   	 71 991 339.08   
    ///Подразделение1 Подразделение2  Должность Уровень ПКГ 1	Уровень ПКГ 2	Установленный должностной оклад Коэф.повышающий по должности   Повышающий коэффициент по должности (руб)   Коэф.повышающий за выслугу лет Повышающий коэффициент за выслугу лет(руб) Коэф.персональный повышающий   Персональная повышающая надбавка(руб)  Надбавка за учёную степень(руб)    Коэф.надбавки за секретность Надбавка за секретность(руб)   Коэф.за интенсивность и высокий результат работы   Надбавка за интенсивность и высокий результат работы(руб)  Районный коэффициент    Районный коэффициент(руб)  Коэф.процентной надбавки   Процентная надбавка(руб)   Итого ФИО
    ///п/с " Ак. Ш О К А Л Ь С К И Й"	ОБЩЕСУДОВАЯ СЛУЖБА Капитан	4	4	6 468.86  	0.18	1 164.39  	0.10  	646.89  	0.00  	0.00  		0.00	0.00	0.31  	2 005.35  	0.30  	3 085.65  	0.30  	3 085.65  	16 456.78  	Табакарь КА
    ///п/с " Ак. Ш О К А Л Ь С К И Й"	ОБЩЕСУДОВАЯ СЛУЖБА Врач	3	5	4 439.42  	0.35	1 553.80  	0.00  	0.00  	0.00  	0.00  		0.00	0.00	0.10  	443.94  	0.30  	1 931.15  	0.30  	1 931.15  	10 299.45  		
    ///п/с " Ак. Ш О К А Л Ь С К И Й"	ОБЩЕСУДОВАЯ СЛУЖБА Ст пом.     4	2	6 468.86  	0.07	452.82  	0.30  	1 940.66  	0.00  	0.00  		0.00	0.00	0.27  	1 746.59  	0.30  	3 182.68  	0.30  	3 182.68  	16 974.29  	Величко НИ
    /// 
    /// OSokolov@ferhri.ru, 20171001
    /// </summary>
    class Program
    {
        static string[] COL_NAMES = new string[]
        {
            "Подразделение0","Подразделение1","Подразделение2","Должность", // 0,1,2,3
            "Уровень ПКГ 1","Уровень ПКГ 2", // 4,5
            "Установленный должностной оклад", // 6
            "Коэф. повышающий по должности","Повышающий коэффициент по должности (руб)", // 7,8
            "Коэф. повышающий за выслугу лет","Повышающий коэффициент за выслугу лет (руб)",//9,10
            "Коэф. персональный повышающий","Персональная повышающая надбавка (руб)",//11,12
            "Надбавка за учёную степень (руб)",//13
            "Коэф. надбавки за секретность","Надбавка за секретность (руб)",//14,15
            "Коэф. за интенсивность и высокий результат работы","Надбавка за интенсивность и высокий результат работы (руб)",//16,17
            "Районный коэффициент","Районный коэффициент (руб)",//18,19
            "Коэф. процентной надбавки","Процентная надбавка (руб)",//20,21
            "Итого","ФИО"//22,23
        };
        static string FILE_PATH = @"C:\Users\Admin\Documents\МЕНЕДЖ\Штатное расписание ДВНИГМИ с 01.07.2017.txt";
        static char[] SPLITTER = new char[] { '\t' };

        static int employerId = 777;
        static DateTime dateSOrg = new DateTime(1991, 1, 1);
        static DateTime dateSalary = new DateTime(2017, 07, 01);

        static FERHRI.Social.DataManager dmSocial = FERHRI.Social.DataManager.GetInstance();
        static FERHRI.Niokr.DataManager dmNiokr = FERHRI.Niokr.DataManager.GetInstance();


        static void Main(string[] args)
        {
            // TASK CONTEXT MEMBERS !!!

            List<Division> divisions = new List<Division>();
            List<StaffPosition> staffposs = new List<StaffPosition>();
            List<Staff> staffs = new List<Staff>();
            List<SalaryStaff> salaryes = new List<SalaryStaff>();

            StreamReader sr = new StreamReader(FILE_PATH, Encoding.GetEncoding(1251));

            try
            {
                // READ HEADER
                int iRow = 0;
                string[] cells = sr.ReadLine().Split(SPLITTER);
                iRow++;
                cells = sr.ReadLine().Split(SPLITTER);
                for (int i = 0; i < COL_NAMES.Length; i++)
                {
                    if (cells[i] != COL_NAMES[i])
                        throw new Exception("cells[i]!= COL_NAMES[i] : " + cells[i] + "!=" + COL_NAMES[i]);
                }

                // READ BODY
                while (!sr.EndOfStream)
                {
                    cells = sr.ReadLine().Split(SPLITTER);

                    // DIVISION
                    Division div0 = GetDivision(divisions, cells[0], null);
                    Division div1 = GetDivision(divisions, cells[1], div0);
                    Division div = div1;
                    Division div2 = null;
                    if (!string.IsNullOrEmpty(cells[2].Trim()))
                    {
                        div2 = GetDivision(divisions, cells[2], div1);
                        div = div2;
                    }

                    // STAFF POSITION
                    StaffPosition staffpos = GetStaffPosition(staffposs, cells[3]);

                    // STAFF
                    Staff staff = GetStaff(div, staffpos);

                    // SALARY STAFF
                    SalaryStaff salary = new SalaryStaff()
                    {
                        Staff = staff,
                        DateS = dateSalary,
                        SumBase = double.Parse(cells[6]),
                        SumScience = string.IsNullOrEmpty(cells[13]) ? 0 : double.Parse(cells[13]),
                        Coef1Regional = double.Parse(cells[18]),
                        Coef1Personal = double.Parse(cells[20]),
                        Coef2Age = double.Parse(cells[9]),
                        Coef2Intensiv = string.IsNullOrEmpty(cells[16]) ? 0 : double.Parse(cells[16]),
                        Coef2Personal = double.Parse(cells[11]),
                        Coef2Secret = double.Parse(cells[14]),
                        Coef2StaffPosition = string.IsNullOrEmpty(cells[7]) ? 0 : double.Parse(cells[7]),
                    };
                    salary.Id = dmNiokr.SalaryStaffRepository.Insert(salary);

                    // SALARY EMPLOYEE
                    LegalEntity employee = GetEmployee(cells[23]);
                    if (employee != null)
                    {
                        dmNiokr.SalaryEmployeeRepository.Insert(new SalaryEmployee()
                        {
                            Employee = GetEmployee(cells[23]),
                            Salary = salary,
                            DateS = dateSalary,
                            Percent = 100
                        });
                    }
                }
            }
            finally
            {
                sr.Close();
            }
            Console.WriteLine("PRESS ENTER...");
            Console.ReadLine();
        }

        private static Staff GetStaff(Division div, StaffPosition staffpos)
        {
            Staff ret = new Staff() { Division = div, StaffPosition = staffpos, DateS = dateSalary };
            ret.Id = dmSocial.StaffRepository.Insert(ret);
            return ret;
        }

        static LegalEntity GetEmployee(string nameRus)
        {
            if (string.IsNullOrEmpty(nameRus)) return null;

            LegalEntity ret = dmSocial.LegalEntityRepository.SelectByNameRus(nameRus);
            if (ret == null)
            {
                ret = new LegalEntity() { NameRus = nameRus, NameRusShort = nameRus, Type = (char)Enums.ToStringLegalEntityType(Enums.LegalEntityType.Person) };
                ret.Id = dmSocial.LegalEntityRepository.Insert(ret);
            }
            return ret;
        }

        static Division GetDivision(List<Division> divisions, string nameRus, Division parent)
        {
            Division ret = divisions.FirstOrDefault(x => x.NameRus == nameRus);
            if (ret == null)
            {
                ret = dmSocial.DivisionRepository.Select(employerId, nameRus, dateSalary);
                if (ret == null)
                {
                    ret = new Division() { Employer = new LegalEntity() { Id = employerId }, NameRus = nameRus, NameRusShort = nameRus, ParentDivision = parent, DateS = dateSOrg };
                    ret.Id = dmSocial.DivisionRepository.Insert(ret);
                }
                divisions.Add(ret);
            }
            return ret;
        }
        static StaffPosition GetStaffPosition(List<StaffPosition> staffposs, string nameRus)
        {
            StaffPosition ret = staffposs.FirstOrDefault(x => x.NameRus == nameRus);
            if (ret == null)
            {
                List<StaffPosition> sps = dmSocial.StaffPositionRepository.Select(nameRus);
                if (sps.Count == 0)
                {
                    ret = new StaffPosition() { NameRus = nameRus, NameRusShort = nameRus };
                    ret.Id = dmSocial.StaffPositionRepository.Insert(ret);
                }
                else if (sps.Count == 1)
                {
                    ret = sps[0];
                }
                else
                    throw new Exception("(sps.Count != 1)");
                staffposs.Add(ret);
            }
            return ret;
        }
    }
}
