using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair
{
    public partial class Form2 : Form
    {
        private int LoggedInMechanicID;
        public Form2(int loggedInMechanicID)
        {
            InitializeComponent();
            LoggedInMechanicID = loggedInMechanicID;
            LoadMechanictInfo();
            LoadRepairRequests();
            LoadParts();
        }

        private void LoadRepairRequests()
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Repair_Requests";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable repairRequestsTable = new DataTable();
                adapter.Fill(repairRequestsTable);

                dataGridView1.DataSource = repairRequestsTable; // Заполнение все заявки
            }
        }

        private void LoadParts()
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Parts";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable partsTable = new DataTable();
                adapter.Fill(partsTable);

                dataGridView2.DataSource = partsTable; // Заполнение запчасти
            }
        }

        private void LoadMechanictInfo()
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            string query = "SELECT Full_Name, Specialty FROM Mechanics WHERE ID_mechanic = @ID_mechanic";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_mechanic", LoggedInMechanicID);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string fullName = reader.GetString(0);
                        string specialty = reader.GetString(1);

                        label5.Text = fullName; 
                        label6.Text = specialty;   
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HistoryApplications historyForm = new HistoryApplications(LoggedInMechanicID);
            historyForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization newForm = new Authorization();
            newForm.Show();
        }
    }
}

