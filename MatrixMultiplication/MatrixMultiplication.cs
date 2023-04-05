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
            int[,] M1_11 = new int[n, n];
            int[,] M1_12 = new int[n, n];
            int[,] M1_21 = new int[n, n];
            int[,] M1_22 = new int[n, n];
            //11 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M1_11[i, j] = M1[i, j];
            //12 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M1_12[i, j] = M1[i, j + n];
            //21 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M1_21[i, j] = M1[i + n, j];
            //22 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M1_22[i, j] = M1[i + n, j + n];

            //divide M2 into 4 Matrices
            int[,] M2_11 = new int[n, n];
            int[,] M2_12 = new int[n, n];
            int[,] M2_21 = new int[n, n];
            int[,] M2_22 = new int[n, n];
            //11 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M2_11[i, j] = M2[i, j];
            //12 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M2_12[i, j] = M2[i, j + n];
            //21 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M2_21[i, j] = M2[i + n, j];
            //22 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M2_22[i, j] = M2[i + n, j + n];

            //create 4 parts of resresult Matrix M3
            //int[,] M3_11 = add(MatrixMultiply(M1_11, M2_11, n), MatrixMultiply(M1_12, M2_21, n), n);
            //int[,] M3_12 = add(MatrixMultiply(M1_11, M2_12, n), MatrixMultiply(M1_12, M2_22, n), n);
            //int[,] M3_21 = add(MatrixMultiply(M1_21, M2_11, n), MatrixMultiply(M1_22, M2_21, n), n);
            //int[,] M3_22 = add(MatrixMultiply(M1_21, M2_12, n), MatrixMultiply(M1_22, M2_22, n), n);

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

            int[,] P1 = MatrixMultiply(M1_11, sub(M2_12, M2_22, n), n);
            int[,] P2 = MatrixMultiply(add(M1_11, M1_12, n), M2_22, n);
            int[,] P3 = MatrixMultiply(add(M1_21, M1_22, n), M2_11, n);
            int[,] P4 = MatrixMultiply(M1_22, sub(M2_21, M2_11, n), n);
            int[,] P5 = MatrixMultiply(add(M1_11, M1_22, n), add(M2_11, M2_22, n), n);
            int[,] P6 = MatrixMultiply(sub(M1_12, M1_22, n), add(M2_21, M2_22, n), n);
            int[,] P7 = MatrixMultiply(sub(M1_11, M1_21, n), add(M2_11, M2_12, n), n);


            int[,] M3_11 = sub(add(P5, P4, n), add(P2, P6, n), n);
            int[,] M3_12 = add(P1, P2, n);
            int[,] M3_21 = add(P3, P4, n);
            int[,] M3_22 = sub(add(P5, P1, n), sub(P3, P7, n), n);


            //assemble result Matrix M3

            //11 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i, j] = M3_11[i, j];
            //12 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i, j + n] = M3_12[i, j];

            //21 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i + n, j] = M3_21[i, j];
            //22 quarter
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    M3[i + n, j + n] = M3_22[i, j];

            return M3;
        }

        static public int[,] add(int[,] M1, int[,] M2, int N)
        {
            int[,] M3 = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    M3[i, j] = M1[i, j] + M2[i, j];
                }
            }
            return M3;
        }
        static public int[,] sub(int[,] M1, int[,] M2, int N)
        {
            int[,] M3 = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    M3[i, j] = M1[i, j] - M2[i, j];
                }
            }
            return M3;
        }

        #endregion
    }
}
