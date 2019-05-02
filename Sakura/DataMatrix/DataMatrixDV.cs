using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Data
{
    public class DataMatrixDV
    {
        public List<DateTime> RowHeaders { get; set; }
        public List<object> ColumnHeaders { get; set; }
        public double[/*row*/,/*column*/] Values { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="rowHeaders">Заголовки строк матрицы.</param>
        /// <param name="M">Кол. столбцов матрицы.</param>
        public DataMatrixDV(List<DateTime> rowHeaders, int M)
        //: base(rowHeaders.Count, M)
        {
            RowHeaders = rowHeaders.OrderBy(x => x).ToList();
            ColumnHeaders = new List<object>();
            for (int i = 0; i < M; i++)
            {
                ColumnHeaders.Add(null);
            }
        }
    }
}
