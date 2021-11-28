using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace test3
{
    class Sort
    {
        static bool SortInAscending = true;
        static bool FirstSort = true;
        public static string[] ReadTable()
        {
            string[] text = File.ReadAllLines("testTable.txt");
            int countRow = text.Length;
            string[,] table = new string[countRow, text[0].Split(";").Length];

            string sortingValue = "id";
            string[] columnForSorting = new string[countRow - 1];

            int numColumnSortingValue = 0;

            for (int i = 0; i < countRow; i++)
            {
                string[] row = text[i].Split(";");

                for (int j = 0; j < table.Length / countRow; j++)
                {
                    table[i, j] = row[j];

                    if (table[i, j] == sortingValue && i == 0)
                    {
                        numColumnSortingValue = j;
                    }
                    else if (j == numColumnSortingValue && i != 0)
                    {
                        columnForSorting[i - 1] = table[i, j];
                    }
                }
            }

            return columnForSorting;
        }

       

        public static void SortAlphabetically(string[] ch)
        {
            char[] sl = new char[ch.Length];
            for (int i = 0; i < ch.Length; i++)
            {
                char[] arr = new char[ch[i].Length];
                arr = ch[i].ToCharArray();
                sl[i] = arr[0];
            }
            for (int i = 0; i < ch.Length; i++)
            {
                for (int j = i + 1; j < ch.Length; j++)
                {
                    if ((int)sl[i] > (int)sl[j])
                    {
                        string temp = ch[i];
                        ch[i] = ch[j];
                        ch[j] = temp;

                        char temp2 = sl[i];
                        sl[i] = sl[j];
                        sl[j] = temp2;
                    }
                }
            }
            SortInAscending = false;
        }
        public static void SortReAlphabetically(string[] ch)
        {
            char[] sl = new char[ch.Length];
            for (int i = 0; i < ch.Length; i++)
            {
                char[] arr = new char[ch[i].Length];
                arr = ch[i].ToCharArray();
                sl[i] = arr[0];
            }
            for (int i = 0; i < ch.Length; i++)
            {
                for (int j = i + 1; j < ch.Length; j++)
                {
                    if ((int)sl[i] < (int)sl[j])
                    {
                        string temp = ch[i];
                        ch[i] = ch[j];
                        ch[j] = temp;

                        char temp2 = sl[i];
                        sl[i] = sl[j];
                        sl[j] = temp2;
                    }
                }
            }
            SortInAscending = true;
        }


        public static void SortAscending(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (Convert.ToInt32(arr[i]) > Convert.ToInt32(arr[j]))
                    {
                        string temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

        public static void SortDescending(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (Convert.ToInt32(arr[i]) < Convert.ToInt32(arr[j]))
                    {
                        string temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

        //static void BringOut(string[,] table, int countRow)
        //{
        //    for (int i = 0; i < countRow; i++)
        //    {
        //        for (int j = 0; j < table.Length / countRow; j++)
        //        {
        //            Console.Write($"{table[i, j]} ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //static void OutResult(string[] arr)
        //{
        //    foreach (var item in arr)
        //    {
        //        Console.Write($"{item} ");
        //    }
        //}
    }
}
