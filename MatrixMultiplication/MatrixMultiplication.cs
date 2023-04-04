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

            //divide M1 into 4 Matrices
            int[,] M1_11 = new int[N / 2, N / 2];
            int[,] M1_12 = new int[N / 2, N / 2];
            int[,] M1_21 = new int[N / 2, N / 2];
            int[,] M1_22 = new int[N / 2, N / 2];
            //11 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M1_11[i, j] = M1[i, j];
            //12 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M1_12[i, j] = M1[i, j + N / 2];
            //21 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M1_21[i, j] = M1[i + N / 2, j];
            //22 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M1_22[i, j] = M1[i + N / 2, j + N / 2];

            //divide M2 into 4 Matrices
            int[,] M2_11 = new int[N / 2, N / 2];
            int[,] M2_12 = new int[N / 2, N / 2];
            int[,] M2_21 = new int[N / 2, N / 2];
            int[,] M2_22 = new int[N / 2, N / 2];
            //11 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M2_11[i, j] = M2[i, j];
            //12 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M2_12[i, j] = M2[i, j + N / 2];
            //21 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M2_21[i, j] = M2[i + N / 2, j];
            //22 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M2_22[i, j] = M2[i + N / 2, j + N / 2];

            //create 4 parts of resresult Matrix M3
            int[,] M3_11 = add(MatrixMultiply(M1_11, M2_11, N / 2), MatrixMultiply(M1_12, M2_21, N / 2), N / 2);
            int[,] M3_12 = add(MatrixMultiply(M1_11, M2_12, N / 2), MatrixMultiply(M1_12, M2_22, N / 2), N / 2);
            int[,] M3_21 = add(MatrixMultiply(M1_21, M2_11, N / 2), MatrixMultiply(M1_22, M2_21, N / 2), N / 2);
            int[,] M3_22 = add(MatrixMultiply(M1_21, M2_12, N / 2), MatrixMultiply(M1_22, M2_22, N / 2), N / 2);


            //assemble result Matrix M3

            //11 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M3[i, j] = M3_11[i, j];
            //12 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M3[i, j + N / 2] = M3_12[i, j];

            //21 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M3[i + N / 2, j] = M3_21[i, j];
            //22 quarter
            for (int i = 0; i < N / 2; i++)
                for (int j = 0; j < N / 2; j++)
                    M3[i + N / 2, j + N / 2] = M3_22[i, j];

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
