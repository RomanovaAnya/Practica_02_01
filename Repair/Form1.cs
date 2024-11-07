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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string selectedTable { get; private set; } = "";

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    selectedTable = "Mechanics";
                    break;
                case 1:
                    selectedTable = "Clients";
                    break;
                case 2:
                    selectedTable = "Repair_Requests";
                    break;
                case 3:
                    selectedTable = "Request_History";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(selectedTable))
            {
                LoadDataFromDatabase(selectedTable);
            }
        }

        private void LoadDataFromDatabase(string tableName)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";
            string query = $"SELECT * FROM {tableName}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button2_Click(object sender, EventArgs e) //добавление
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                if (selectedTable == "Mechanics")
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }
                if (selectedTable == "Clients")
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }
                if (selectedTable == "Repair_Requests")
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }
                if (selectedTable == "Request_History")
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e) //обновления
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT * FROM {selectedTable}";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable); 
                adapter.Update(dataTable); 
            }
        }

        private void button4_Click(object sender, EventArgs e) //удаление
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    var selectedRow = dataGridView1.Rows[selectedRowIndex];

                    string primaryKeyColumn = "";
                    switch (selectedTable)
                    {
                        case "Mechanics":
                            primaryKeyColumn = "ID_mechanic";
                            break;
                        case "Clients":
                            primaryKeyColumn = "ID_client";
                            break;
                        case "Repair_Requests":
                            primaryKeyColumn = "Request_Number";
                            break;
                        case "Request_History":
                            primaryKeyColumn = "Request_Number";
                            break;
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(primaryKeyColumn))
                    {
                        int idToDelete = Convert.ToInt32(selectedRow.Cells[primaryKeyColumn].Value);
                        string deleteQuery = $"DELETE FROM {selectedTable} WHERE {primaryKeyColumn} = @ID";

                        SqlCommand command = new SqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@ID", idToDelete);
                        command.ExecuteNonQuery();

                        dataGridView1.Rows.RemoveAt(selectedRowIndex);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Authorization newForm = new Authorization();
            newForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Othet newForm4 = new Othet();
            newForm4.Show();
        }

        // Метод для установки выбранного значения
        public void SetComboBoxSelection(int index)
        {
            if (index == 0)
            {
                selectedTable = "Mechanics";
            }
            else if (index == 1)
            {
                selectedTable = "Clients";
            }
            if (index == 2)
            {
                selectedTable = "Repair_Requests";
            }
        }
    }
}
