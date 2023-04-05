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

            //divide M1 into 4 Matrices
            int[,] a = new int[n, n];
            int[,] b = new int[n, n];
            int[,] c = new int[n, n];
            int[,] d = new int[n, n];
            //11 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = M1[i, j];
            //12 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    b[i, j] = M1[i, j + n];
            //21 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    c[i, j] = M1[i + n, j];
            //22 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    d[i, j] = M1[i + n, j + n];

            //divide M2 into 4 Matrices
            int[,] e = new int[n, n];
            int[,] f = new int[n, n];
            int[,] g = new int[n, n];
            int[,] h = new int[n, n];
            //11 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    e[i, j] = M2[i, j];
            //12 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    f[i, j] = M2[i, j + n];
            //21 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    g[i, j] = M2[i + n, j];
            //22 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    h[i, j] = M2[i + n, j + n];

            ////create 4 parts of resresult Matrix M3
            //int[,] r = add(MatrixMultiply(a, e, n), MatrixMultiply(b, g, n), n);
            //int[,] s = add(MatrixMultiply(a, f, n), MatrixMultiply(b, h, n), n);
            //int[,] t = add(MatrixMultiply(c, e, n), MatrixMultiply(d, g, n), n);
            //int[,] u = add(MatrixMultiply(c, f, n), MatrixMultiply(d, h, n), n);

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

            int[,] P1 = MatrixMultiply(a, sub(f, h, n), n);
            int[,] P2 = MatrixMultiply(add(a, b, n), h, n);
            int[,] P3 = MatrixMultiply(add(c, d, n), e, n);
            int[,] P4 = MatrixMultiply(d, sub(g, e, n), n);
            int[,] P5 = MatrixMultiply(add(a, d, n), add(e, h, n), n);
            int[,] P6 = MatrixMultiply(sub(b, d, n), add(g, h, n), n);
            int[,] P7 = MatrixMultiply(sub(a, c, n), add(e, f, n), n);

            //int[,] P1 = new int[n, n];
            //int[,] P2 = new int[n, n];
            //int[,] P3 = new int[n, n];
            //int[,] P4 = new int[n, n];
            //int[,] P5 = new int[n, n];
            //int[,] P6 = new int[n, n];
            //int[,] P7 = new int[n, n];

            //Parallel.Invoke(() => P1 = MatrixMultiply(a, sub(f, h, n), n),
            //                () => P2 = MatrixMultiply(add(a, b, n), h, n),
            //                () => P3 = MatrixMultiply(add(c, d, n), e, n),
            //                () => P4 = MatrixMultiply(d, sub(g, e, n), n),
            //                () => P5 = MatrixMultiply(add(a, d, n), add(e, h, n), n),
            //                () => P6 = MatrixMultiply(sub(b, d, n), add(g, h, n), n),
            //                () => P7 = MatrixMultiply(sub(a, c, n), add(e, f, n), n));

            /// -
            /// r = M3_11
            /// s = M3_12
            /// t = M3_21
            /// u = M3_22
            /// 

            //int[,] z = new int[n, n];
            //for (int i = 0; i < n; i++)
            //    for (int j = 0; j < n; j++)
            //        z[i, j] = 0;

            int[,] r = add(add(P5, P6, n), sub(P4, P2, n), n);
            int[,] s = add(P1, P2, n);
            int[,] t = add(P3, P4, n);
            int[,] u = sub(sub(add(P5, P1, n), P3, n), P7, n);


            //assemble result Matrix M3

            //11 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i, j] = r[i, j];
            //12 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i, j + n] = s[i, j];

            //21 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i + n, j] = t[i, j];
            //22 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i + n, j + n] = u[i, j];

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

        #endregion
    }
}
