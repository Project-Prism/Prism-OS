using PrismOS.Libraries.Numerics;

namespace PrismOS.Tests
{
    public static class Tester
    {
        public static bool MatrixTest()
        {
            Matrix<int> M1 = new(4096, 4096);
            for (int X = 0; X < M1.M.Count; X++)
            {
                for (int Y = 0; Y < M1.M[X].Count; Y++)
                {
                    if (M1.M[X][Y] == 0)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }
    }
}