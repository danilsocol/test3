using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace test3
{
    public partial class Form1 : Form
    {
        string[] TextTable{ get; set; }
        string[,] Table { get; set; }
        int countRow { get; set; }
        DataGridViewColumn[] columns { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
             TextTable = File.ReadAllLines("testTable.txt");
             countRow = TextTable.Length;
             Table = new string[countRow, TextTable[0].Split(";").Length];

            for (int i = 0; i < TextTable[0].Split(";").Length; i++)
            {
                comboBox1.Items.Add(TextTable[0].Split(";")[i]);
            }

            for (int i = 0; i < countRow; i++)
            {
                string[] row = TextTable[i].Split(";");

                for (int j = 0; j < Table.Length / countRow; j++)
                {
                    Table[i, j] = row[j];
                }
            }

             columns = new DataGridViewColumn[TextTable[0].Split(";").Length];
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new DataGridViewColumn();
                columns[i].HeaderText = TextTable[0].Split(";")[i];
                columns[i].ReadOnly = true;
                columns[i].CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(columns[i]);
            }

            for (int i = 0; i < countRow - 1; i++)
            {
                dataGridView1.Rows.Add();
            }

            for (int i = 1; i < countRow; i++)
            {
                string[] row = TextTable[i].Split(";");

                for (int j = 0; j < columns.Length; j++)
                {
                    dataGridView1.Rows[i-1].Cells[j].Value = Table[i, j];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            string sortingValue = comboBox1.Text;
            if (comboBox2.Text == "По возрастанию")
            {
                SortInAscending = true;
            }
            else
            {
                SortInAscending = false;
            }
            string text2 = comboBox2.Text;
            string[] columnForSorting = new string[TextTable.Length - 1];
            int numColumnSortingValue = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string[] row = TextTable[i].Split(";");

                for (int j = 0; j < Table.Length / TextTable.Length; j++)
                {
                    Table[i, j] = row[j];

                    if (Table[i, j] == sortingValue && i == 0)
                    {
                        numColumnSortingValue = j;
                    }
                    else if (j == numColumnSortingValue && i != 0)
                    {
                        columnForSorting[i - 1] = Table[i, j];
                    }
                }
            }
            SelectSort(columnForSorting);
            SaveTable();
            Console.WriteLine();
        }


        void SaveTable()
        {
            for (int i = 1; i < countRow; i++)
            {
                string str = "";
                for (int j = 0; j < columns.Length; j++)
                {
                    Table[i, j] = dataGridView1[j, i-1].Value.ToString();
                    str += $"{dataGridView1[j, i-1].Value.ToString()};";
                }
                TextTable[i] = str;
            }
        }

        static bool SortInAscending = true;
        //static bool FirstSort = true;
        public  void SelectSort(string[] columnForSorting)
        {
            bool IsDigit = columnForSorting[0].Length == columnForSorting[0].Where(c => char.IsDigit(c)).Count();

            if (IsDigit && !SortInAscending)
            {
                ReMergeSort(columnForSorting);
                SortDescending(columnForSorting);
            }
            else if (IsDigit && SortInAscending)
            {
                MergeSort(columnForSorting);
                SortAscending(columnForSorting);
            }
            else if (!IsDigit && SortInAscending)
            {
                MergeSortWord(columnForSorting);
                SortAlphabetically(columnForSorting);
            }
            else
            {
                ReMergeSortWord(columnForSorting);
                SortReAlphabetically(columnForSorting);
            }
        }

        public void SortAscending(string[] arr)
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

                        DataGridViewRow row1 = dataGridView1.Rows[i];
                        DataGridViewRow row2 = dataGridView1.Rows[j];

                        dataGridView1.Rows.Remove(row2);
                        dataGridView1.Rows.Remove(row1);

                        dataGridView1.Rows.Insert(i, row2);
                        dataGridView1.Rows.Insert(j, row1);
                    }
                }
            }
        }

        public void SortDescending(string[] arr)
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


                        DataGridViewRow row1 = dataGridView1.Rows[i];
                        DataGridViewRow row2 = dataGridView1.Rows[j];

                        dataGridView1.Rows.Remove(row2);
                        dataGridView1.Rows.Remove(row1);

                        dataGridView1.Rows.Insert(i, row2);
                        dataGridView1.Rows.Insert(j, row1);
                    }
                }
            }
        }

        public  void SortAlphabetically(string[] ch)
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


                        DataGridViewRow row1 = dataGridView1.Rows[i];
                        DataGridViewRow row2 = dataGridView1.Rows[j];

                        dataGridView1.Rows.Remove(row2);
                        dataGridView1.Rows.Remove(row1);

                        dataGridView1.Rows.Insert(i, row2);
                        dataGridView1.Rows.Insert(j, row1);
                    }
                }
            }
            SortInAscending = false;
        }
        public  void SortReAlphabetically(string[] ch)
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

                        DataGridViewRow row1 = dataGridView1.Rows[i];
                        DataGridViewRow row2 = dataGridView1.Rows[j];

                        dataGridView1.Rows.Remove(row2);
                        dataGridView1.Rows.Remove(row1);

                        dataGridView1.Rows.Insert(i, row2);
                        dataGridView1.Rows.Insert(j, row1);
                    }
                }
            }
            SortInAscending = true;
        }
        int deptр = 0;
        int Ddeptp = 0;
         string[] MergeSort(string[] massive)
        {
            textBox1.Text += $"{deptр}) ";
            
            for (int i = 0; i < massive.Length; i++)
            {
                textBox1.Text += $"{massive[i]} ";
            }
            textBox1.Text += Environment.NewLine;

            if (massive.Length == 1)
            {
              //  deptр--;
                return massive;
            }
                
            int mid_point = massive.Length / 2;
            deptр++;

            string[] mass1 = MergeSort(massive.Take(mid_point).ToArray());
            string[] mass2 = MergeSort(massive.Skip(mid_point).ToArray());
            deptр--;
            Ddeptp = deptр;
            return Merge(mass1, mass2);
        }

         string[] Merge(string[] mass1, string[] mass2)
        {
            int a = 0, b = 0;
            string[] merged = new string[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                {
                    if (Convert.ToInt32(mass1[a]) > Convert.ToInt32(mass2[b])) //
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }
                else
                {
                    if (b < mass2.Length)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }

            }
            textBox1.Text += $"---{Ddeptp}) ";

            for (int i = 0; i < merged.Length; i++)
            {
                textBox1.Text += $"{merged[i]} ";
            }
            textBox1.Text += Environment.NewLine;
            Ddeptp--;

            return merged;
        }
        
         string[] ReMergeSort(string[] massive)
        {
            textBox1.Text += $"{deptр}) ";

            for (int i = 0; i < massive.Length; i++)
            {
                textBox1.Text += $"{massive[i]} ";
            }
            textBox1.Text += Environment.NewLine;

            if (massive.Length == 1)
            {
                //  deptр--;
                return massive;
            }

            int mid_point = massive.Length / 2;
            deptр++;

            string[] mass1 = ReMergeSort(massive.Take(mid_point).ToArray());
            string[] mass2 = ReMergeSort(massive.Skip(mid_point).ToArray());
            deptр--;
            return ReMerge(mass1, mass2);
        }

         string[] ReMerge(string[] mass1, string[] mass2)
        {
            int a = 0, b = 0;
            string[] merged = new string[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                {
                    if (Convert.ToInt32(mass1[a]) < Convert.ToInt32(mass2[b])) //
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }
                else
                {
                    if (b < mass2.Length)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }

            }
            textBox1.Text += $"{deptр}) ";

            for (int i = 0; i < merged.Length; i++)
            {
                textBox1.Text += $"{merged[i]} ";
            }
            textBox1.Text += Environment.NewLine;

            return merged;
        }


        string[] MergeSortWord(string[] massive)
        {
            textBox1.Text += $"{deptр}) ";

            for (int i = 0; i < massive.Length; i++)
            {
                textBox1.Text += $"{massive[i]} ";
            }
            textBox1.Text += Environment.NewLine;

            if (massive.Length == 1)
            {
                //  deptр--;
                return massive;
            }

            int mid_point = massive.Length / 2;
            deptр++;

            string[] mass1 = MergeSortWord(massive.Take(mid_point).ToArray());
            string[] mass2 = MergeSortWord(massive.Skip(mid_point).ToArray());
            deptр--;
            Ddeptp = deptр;
            return MergeWord(mass1, mass2);
        }

        string[] MergeWord(string[] mass1, string[] mass2)
        {
            int a = 0, b = 0;
            string[] merged = new string[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                {
                    if ((int)mass1[a][0] > (int)mass2[b][0]) //
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }
                else
                {
                    if (b < mass2.Length)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }

            }
            textBox1.Text += $"---{Ddeptp}) ";

            for (int i = 0; i < merged.Length; i++)
            {
                textBox1.Text += $"{merged[i]} ";
            }
            textBox1.Text += Environment.NewLine;
            Ddeptp--;

            return merged;
        }

        string[] ReMergeSortWord(string[] massive)
        {
            textBox1.Text += $"{deptр}) ";

            for (int i = 0; i < massive.Length; i++)
            {
                textBox1.Text += $"{massive[i]} ";
            }
            textBox1.Text += Environment.NewLine;

            if (massive.Length == 1)
            {
                //  deptр--;
                return massive;
            }

            int mid_point = massive.Length / 2;
            deptр++;

            string[] mass1 = ReMergeSortWord(massive.Take(mid_point).ToArray());
            string[] mass2 = ReMergeSortWord(massive.Skip(mid_point).ToArray());
            deptр--;
            Ddeptp = deptр;
            return ReMergeWord(mass1, mass2);
        }

        string[] ReMergeWord(string[] mass1, string[] mass2)
        {
            int a = 0, b = 0;
            string[] merged = new string[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                if (b < mass2.Length && a < mass1.Length)
                {
                    if ((int)mass1[a][0] < (int)mass2[b][0]) //
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }
                else
                {
                    if (b < mass2.Length)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
                }

            }
            textBox1.Text += $"---{Ddeptp}) ";

            for (int i = 0; i < merged.Length; i++)
            {
                textBox1.Text += $"{merged[i]} ";
            }
            textBox1.Text += Environment.NewLine;
            Ddeptp--;

            return merged;
        }
    }
}
