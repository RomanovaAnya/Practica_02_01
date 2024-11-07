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
    public partial class HistoryApplications : Form
    {
        private int mechanicID;   
        private string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";
        public HistoryApplications(int id)
        {
            InitializeComponent();
            mechanicID = id;
        }

        private void HistoryApplications_Load(object sender, EventArgs e)
        {
            LoadRequestHistory();
        }

        private void LoadRequestHistory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Request_History WHERE ID_mechanic = @mechanicID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@mechanicID", mechanicID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string requestNumber = textBox4.Text;
            string executionStage = textBox1.Text;
            string mechanicComments = textBox2.Text;
            string completionDate = textBox3.Text;
            string assignmentDate = textBox5.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Request_History SET Assignment_Date = @AssignmentDate, Execution_Stage = @ExecutionStage, Mechanic_Comments = @MechanicComments, Completion_Date = @CompletionDate WHERE Request_Number = @RequestNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RequestNumber", requestNumber);
                command.Parameters.AddWithValue("@AssignmentDate", assignmentDate);  
                command.Parameters.AddWithValue("@ExecutionStage", executionStage);
                command.Parameters.AddWithValue("@MechanicComments", mechanicComments);
                command.Parameters.AddWithValue("@CompletionDate", completionDate);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Данные успешно изменены.");
                    LoadRequestHistory(); 
                }  
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2 form2 = new Form2(mechanicID);
            form2.Show();
        }
    }
}
