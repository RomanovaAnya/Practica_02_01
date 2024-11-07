using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair
{
    public partial class Form3 : Form
    {
        private int loggedInClientID;
        public Form3(int clientID)
        {
            InitializeComponent();
            loggedInClientID = clientID;
            LoadClientInfo();
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            string query = "SELECT Request_Number, Type_of_Appliance, Appliance_Model, Problem_Description, Request_Status " +
                           "FROM Repair_Requests " +
                           $"WHERE ID_client = @ID_client";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_client", loggedInClientID);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
        }

        private void LoadClientInfo()
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            string query = "SELECT Full_Name, Phone_Number, Address FROM Clients WHERE ID_client = @ID_client";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_client", loggedInClientID);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string fullName = reader.GetString(0); 
                        string phone = reader.GetString(1);
                        string address = reader.GetString(2); 

                        label5.Text = fullName;
                        label6.Text = phone; 
                        label7.Text = address;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Applications applicationsForm = new Applications(loggedInClientID);
            applicationsForm.FormClosed += (s, args) => { LoadDataGridView(); };
            applicationsForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization newForm = new Authorization();
            newForm.Show();
        }
    }
}
