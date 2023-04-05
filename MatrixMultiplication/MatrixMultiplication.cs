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
            int n = N / 2;

            //////////////////////////////////////////////////////////////////
            /// a = M1_11
            /// b = M1_12
            /// c = M1_21
            /// d = M1_22
            /// -
            /// e = M2_11
            /// f = M2_12
            /// g = M2_21
            /// h = M2_22
            /// 

            //divide M1 into 4 Matrices
            int[,] a = new int[n, n];
            int[,] b = new int[n, n];
            int[,] c = new int[n, n];
            int[,] d = new int[n, n];
            divideMatrix(M1, ref a, ref b, ref c, ref d, n);
            //divide M2 into 4 Matrices
            int[,] e = new int[n, n];
            int[,] f = new int[n, n];
            int[,] g = new int[n, n];
            int[,] h = new int[n, n];
            divideMatrix(M2, ref e, ref f, ref g, ref h, n);
            //result 
            int[,] r = new int[n, n];
            int[,] s = new int[n, n];
            int[,] t = new int[n, n];
            int[,] u = new int[n, n];

            if (N <= 8)
            {
                //create 4 parts of resresult Matrix M3 (brute force)
                r = add(MatrixMultiply(a, e, n), MatrixMultiply(b, g, n), n);
                s = add(MatrixMultiply(a, f, n), MatrixMultiply(b, h, n), n);
                t = add(MatrixMultiply(c, e, n), MatrixMultiply(d, g, n), n);
                u = add(MatrixMultiply(c, f, n), MatrixMultiply(d, h, n), n);
            }
            else
            {
                int[,] P1 = MatrixMultiply(a, sub(f, h, n), n);
                int[,] P2 = MatrixMultiply(add(a, b, n), h, n);
                int[,] P3 = MatrixMultiply(add(c, d, n), e, n);
                int[,] P4 = MatrixMultiply(d, sub(g, e, n), n);
                int[,] P5 = MatrixMultiply(add(a, d, n), add(e, h, n), n);
                int[,] P6 = MatrixMultiply(sub(b, d, n), add(g, h, n), n);
                int[,] P7 = MatrixMultiply(sub(a, c, n), add(e, f, n), n);
                /// -
                /// r = M3_11
                /// s = M3_12
                /// t = M3_21
                /// u = M3_22
                /// 
                r = add(add(P5, P6, n), sub(P4, P2, n), n);
                s = add(P1, P2, n);
                t = add(P3, P4, n);
                u = sub(add(P5, P1, n), add(P3, P7, n), n);
            }
            //assemble result Matrix M3
            assembleMatrix(ref M3, r, s, t, u, n);

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
