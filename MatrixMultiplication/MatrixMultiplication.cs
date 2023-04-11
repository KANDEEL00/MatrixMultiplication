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
            //result Matrix
            int[,] M3 = new int[N, N];
            //base case
            if (N == 2)
            {
                M3[0, 0] = M1[0, 0] * M2[0, 0] + M1[0, 1] * M2[1, 0];
                M3[0, 1] = M1[0, 0] * M2[0, 1] + M1[0, 1] * M2[1, 1];
                M3[1, 0] = M1[1, 0] * M2[0, 0] + M1[1, 1] * M2[1, 0];
                M3[1, 1] = M1[1, 0] * M2[0, 1] + M1[1, 1] * M2[1, 1];

                return M3;
            }
            else if (N <= 64)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        for (int k = 0; k < N; k++)
                        {
                            M3[i, j] += M1[i, k] * M2[k, j];
                        }
                    }
                }

            }
            else
            {
                int n = N / 2;

                //////////////////////////////////////////////////////////////////
                /// a = M1_11
                /// b = M1_12
                /// c = M1_21
                /// d = M1_22
                //divide M1 into 4 Matrices
                int[,] M1_11 = new int[n, n];
                int[,] M1_12 = new int[n, n];
                int[,] M1_21 = new int[n, n];
                int[,] M1_22 = new int[n, n];
                divideMatrix(M1, ref M1_11, ref M1_12, ref M1_21, ref M1_22, n);
                /// -
                /// e = M2_11
                /// f = M2_12
                /// g = M2_21
                /// h = M2_22
                //divide M2 into 4 Matrices
                int[,] M2_11 = new int[n, n];
                int[,] M2_12 = new int[n, n];
                int[,] M2_21 = new int[n, n];
                int[,] M2_22 = new int[n, n];
                divideMatrix(M2, ref M2_11, ref M2_12, ref M2_21, ref M2_22, n);

                int[,] P1 = new int[n, n];
                int[,] P2 = new int[n, n];
                int[,] P3 = new int[n, n];
                int[,] P4 = new int[n, n];
                int[,] P5 = new int[n, n];
                int[,] P6 = new int[n, n];
                int[,] P7 = new int[n, n];
                Parallel.Invoke(() => P1 = MatrixMultiply(M1_11, sub(M2_12, M2_22, n), n),
                                () => P2 = MatrixMultiply(add(M1_11, M1_12, n), M2_22, n),
                                () => P3 = MatrixMultiply(add(M1_21, M1_22, n), M2_11, n),
                                () => P4 = MatrixMultiply(M1_22, sub(M2_21, M2_11, n), n),
                                () => P5 = MatrixMultiply(add(M1_11, M1_22, n), add(M2_11, M2_22, n), n),
                                () => P6 = MatrixMultiply(sub(M1_12, M1_22, n), add(M2_21, M2_22, n), n),
                                () => P7 = MatrixMultiply(sub(M1_11, M1_21, n), add(M2_11, M2_12, n), n));
                /// r = M3_11
                /// s = M3_12
                /// t = M3_21
                /// u = M3_22
                /// 
                //result 
                int[,] r = add(add(P5, P6, n), sub(P4, P2, n), n);
                int[,] s = add(P1, P2, n);
                int[,] t = add(P3, P4, n);
                int[,] u = sub(add(P5, P1, n), add(P3, P7, n), n);
                //int[,] r = new int[n, n];
                //int[,] s = new int[n, n];
                //int[,] t = new int[n, n];
                //int[,] u = new int[n, n];
                //System.Threading.Tasks.Parallel.Invoke(() => r = add(add(P5, P6, n), sub(P4, P2, n), n),
                //                                       () => s = add(P1, P2, n),
                //                                       () => t = add(P3, P4, n),
                //                                       () => u = sub(add(P5, P1, n), add(P3, P7, n), n));

                //assemble result Matrix M3
                assembleMatrix(ref M3, r, s, t, u, n);

            }
            return M3;
        }

        static public int[,] add(int[,] M1, int[,] M2, int N)
        {
            int[,] M3 = new int[N, N];
            Parallel.For(1, 11, i =>
            {
                for (int j = 0; j < N; j++)
                    M3[i, j] = M1[i, j] + M2[i, j];
            });
            return M3;
        }
        static public int[,] sub(int[,] M1, int[,] M2, int N)
        {
            int[,] M3 = new int[N, N];
            Parallel.For(1, 11, i =>
            {
                for (int j = 0; j < N; j++)
                    M3[i, j] = M1[i, j] - M2[i, j];
            });
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
