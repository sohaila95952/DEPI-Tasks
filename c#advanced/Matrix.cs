using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class Matrix
    {
        int[,] matrix ;
        public int this[int row,int col] {
            get {
            return matrix[row,col];
            }
            set {
                matrix[row,col] = value;
            } }
        public Matrix(int[,] Matrix) {
            matrix=Matrix;
        }
    }
}
