using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Data
{
    public class DataMatrix
    {
        public double[,] Values { get; set; }

        public DataMatrix()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="N">Кол. строк    матрицы.</param>
        /// <param name="M">Кол. столбцов матрицы.</param>
        public DataMatrix(int N, int M)
        {
            Values = Support.AllocateMD(N, M, double.NaN);
        }
    }
}
