using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Repair
{
    public partial class Othet : Form
    {
        public Othet()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BuildChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveChartToFile();
        }

        private void BuildChart()
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";
            string query = "SELECT MONTH(Assignment_Date) AS Month, COUNT(*) AS NumberOfRequests FROM Request_History GROUP BY MONTH(Assignment_Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Создание и настройка диаграммы
                Chart chart = new Chart();
                chart.Width = 500;
                chart.Height = 300;
                chart.ChartAreas.Add(new ChartArea());
                Series series = new Series();
                series.ChartType = SeriesChartType.Column;
                chart.Series.Add(series);

                foreach (DataRow row in dataTable.Rows)
                {
                    string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((int)row["Month"]);
                    series.Points.AddXY(monthName, Convert.ToInt32(row["NumberOfRequests"]));
                }
                foreach (DataPoint point in series.Points)
                {
                    point.Color = Color.FromArgb(229, 166, 121);
                }

                // Определение положения диаграммы на форме
                int formCenterX = (this.Width - chart.Width) / 2;
                int formCenterY = (this.Height - chart.Height) / 2;
                chart.Left = formCenterX;
                chart.Top = formCenterY;

                Controls.Add(chart);
            }
        }

        private void SaveChartToFile()
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                saveDialog.Title = "Save Chart Image";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveDialog.FileName;
                }
            }
        }
    }
}
