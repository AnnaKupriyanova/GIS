using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic.FileIO;

namespace GisCsv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static DataTable ReadCsv(string fileName)
        {
            DataTable data = new DataTable();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                string[] columns = line.Split('\t');

                foreach (string column in columns)
                {
                    data.Columns.Add(column);
                }

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] values = line.Split('\t');
                    data.Rows.Add(values);
                }
            }

            return data;
        }

        public static void WriteCsv(string fileName, DataTable data)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                string columnNames = string.Join("\t", data.Columns.Cast<DataColumn>().Select(column => column.ColumnName));
                writer.WriteLine(columnNames);

                foreach (DataRow row in data.Rows)
                {
                    string line = string.Join("\t", row.ItemArray);
                    writer.WriteLine(line);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"D:\VSCode\practica\second\GisCsv\GisCsv\test.txt";

            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                }

                DataTable data = ((DataView)dataGridView1.ItemsSource).Table;
                WriteCsv(fileName, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"D:\VSCode\practica\second\GisCsv\GisCsv\project.txt";

            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                }

                DataTable data = ReadCsv(fileName);
                dataGridView1.ItemsSource = data.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"D:\VSCode\practica\second\GisCsv\GisCsv\test.qgs");
        }
    }
}
