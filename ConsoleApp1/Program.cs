using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string str1 = "editing";
        string str2 = "distance";

        var result = EditDist(str1, str2);


        foreach (var item in result.Item2)
        {
            Console.WriteLine($"{item.Item1}   {item.Item2}");
        }
    }

    static (int, List<(char, char)>) EditDist(string a, string b)
    {
        int[,] matrix = new int[a.Length + 1, b.Length + 1];

        int EditDistTd(int i, int j)
        {
            if (matrix[i, j] == 0)
            {
                if (i == 0)
                {
                    matrix[i, j] = j;
                }
                else if (j == 0)
                {
                    matrix[i, j] = i;
                }
                else
                {
                    int ins = EditDistTd(i, j - 1) + 1;
                    int delete = EditDistTd(i - 1, j) + 1;
                    int sub = EditDistTd(i - 1, j - 1) + (a[i - 1] != b[j - 1] ? 1 : 0);
                    matrix[i, j] = Math.Min(Math.Min(ins, delete), sub);
                }
            }
            return matrix[i, j];
        }

        void EditDistBu()
        {
            for (int i = 0; i <= a.Length; i++)
            {
                matrix[i, 0] = i;
            }
            for (int j = 1; j <= b.Length; j++)
            {
                matrix[0, j] = j;
            }
            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int c = a[i - 1] != b[j - 1] ? 1 : 0;
                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + c);
                }
            }
        }

        List<(char, char)> Restore()
        {
            List<(char, char)> result = new List<(char, char)>();
            int i = a.Length, j = b.Length;
            while (i != 0 || j != 0)
            {
                if (i != 0 && matrix[i, j] == matrix[i - 1, j] + 1)
                {
                    result.Add((a[i - 1], '-'));
                    i--;
                }
                else if (j != 0 && matrix[i, j] == matrix[i, j - 1] + 1)
                {
                    result.Add(('-', b[j - 1]));
                    j--;
                }
                else if (matrix[i, j] == matrix[i - 1, j - 1] + (a[i - 1] != b[j - 1] ? 1 : 0))
                {
                    result.Add((a[i - 1], b[j - 1]));
                    i--;
                    j--;
                }
            }
            result.Reverse();
            return result;
        }

        EditDistTd(a.Length, b.Length);
        EditDistBu();
        var solution = Restore();

        return (matrix[a.Length, b.Length], solution);
    }
}
