using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            //Result Matrix
            int[,] M3 = new int[N, N];
            //Base Case
            if (N == 2)
            {
                M3[0, 0] = M1[0, 0] * M2[0, 0] + M1[0, 1] * M2[1, 0];
                M3[0, 1] = M1[0, 0] * M2[0, 1] + M1[0, 1] * M2[1, 1];
                M3[1, 0] = M1[1, 0] * M2[0, 0] + M1[1, 1] * M2[1, 0];
                M3[1, 1] = M1[1, 0] * M2[0, 1] + M1[1, 1] * M2[1, 1];
                return M3;
            }
            //Brute Force
            else if (N <= 64)
            {
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        for (int k = 0; k < N; k++)
                            M3[i, j] += M1[i, k] * M2[k, j];

            }
            //Strassen's Method
            else
            {
                int n = N / 2;
                //Divide M1 into 4 Matrices
                int[,] M1_11 = new int[n, n];
                int[,] M1_12 = new int[n, n];
                int[,] M1_21 = new int[n, n];
                int[,] M1_22 = new int[n, n];
                divideMatrix(M1, ref M1_11, ref M1_12, ref M1_21, ref M1_22, n);
                //Divide M2 into 4 Matrices
                int[,] M2_11 = new int[n, n];
                int[,] M2_12 = new int[n, n];
                int[,] M2_21 = new int[n, n];
                int[,] M2_22 = new int[n, n];
                divideMatrix(M2, ref M2_11, ref M2_12, ref M2_21, ref M2_22, n);
                int[,] t1 = new int[n, n];
                int[,] t2 = new int[n, n];
                int[,] t3 = new int[n, n];
                int[,] t4 = new int[n, n];
                int[,] t5 = new int[n, n];
                int[,] t6 = new int[n, n];
                int[,] t7 = new int[n, n];
                //without threading
                //int[,] t1 = MatrixMultiply(M1_11, sub(M2_12, M2_22, n), n);
                //int[,] t2 = MatrixMultiply(add(M1_11, M1_12, n), M2_22, n);
                //int[,] t3 = MatrixMultiply(add(M1_21, M1_22, n), M2_11, n);
                //int[,] t4 = MatrixMultiply(M1_22, sub(M2_21, M2_11, n), n);
                //int[,] t5 = MatrixMultiply(add(M1_11, M1_22, n), add(M2_11, M2_22, n), n);
                //int[,] t6 = MatrixMultiply(sub(M1_12, M1_22, n), add(M2_21, M2_22, n), n);
                //int[,] t7 = MatrixMultiply(sub(M1_11, M1_21, n), add(M2_11, M2_12, n), n);
                //with threading
                Parallel.Invoke(() => t1 = MatrixMultiply(M1_11, sub(M2_12, M2_22, n), n),
                                () => t2 = MatrixMultiply(add(M1_11, M1_12, n), M2_22, n),
                                () => t3 = MatrixMultiply(add(M1_21, M1_22, n), M2_11, n),
                                () => t4 = MatrixMultiply(M1_22, sub(M2_21, M2_11, n), n),
                                () => t5 = MatrixMultiply(add(M1_11, M1_22, n), add(M2_11, M2_22, n), n),
                                () => t6 = MatrixMultiply(sub(M1_12, M1_22, n), add(M2_21, M2_22, n), n),
                                () => t7 = MatrixMultiply(sub(M1_11, M1_21, n), add(M2_11, M2_12, n), n));
                //Result 
                int[,] M3_11 = add(add(t5, t6, n), sub(t4, t2, n), n);
                int[,] M3_12 = add(t1, t2, n);
                int[,] M3_21 = add(t3, t4, n);
                int[,] M3_22 = sub(add(t5, t1, n), add(t3, t7, n), n);
                //Assemble result Matrix M3
                assembleMatrix(ref M3, M3_11, M3_12, M3_21, M3_22, n);
            }
            return M3;
        }
        static public int[,] add(int[,] M1, int[,] M2, int N)
        {
            int[,] M3 = new int[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    M3[i, j] = M1[i, j] + M2[i, j];
            return M3;
        }
        static public int[,] sub(int[,] M1, int[,] M2, int N)
        {
            int[,] M3 = new int[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    M3[i, j] = M1[i, j] - M2[i, j];
            return M3;
        }
        static public void divideMatrix(int[,] M, ref int[,] a, ref int[,] b, ref int[,] c, ref int[,] d, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = M[i, j];
                    b[i, j] = M[i, j + n];
                    c[i, j] = M[i + n, j];
                    d[i, j] = M[i + n, j + n];
                }
        }
        static public void assembleMatrix(ref int[,] M, int[,] r, int[,] s, int[,] t, int[,] u, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = r[i, j];
                    M[i, j + n] = s[i, j];
                    M[i + n, j] = t[i, j];
                    M[i + n, j + n] = u[i, j];
                }
        }

        #endregion
    }
}
