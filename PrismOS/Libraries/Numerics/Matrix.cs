using System.Collections.Generic;

namespace PrismOS.Libraries.Numerics
{
    public class Matrix<T>
    {
        public Matrix(int X, int Y)
        {
            for (int I = 0; I < X; I++)
            {
                M.Add(new());
                for (int Y2 = 0; Y2 < Y; Y2++)
                {
                    M[I].Add(default);
                }
            }
        }

        public List<List<T>> M = new(new List<List<T>>());
    }
}