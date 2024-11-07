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
    public partial class Applications : Form
    {
        private int loggedInClientID;
        public Applications(int clientId)
        {
            InitializeComponent();
            loggedInClientID = clientId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Repair_Requests (ID_client , Type_of_Appliance, Appliance_Model, Problem_Description, Request_Status) " +
                               "VALUES (@ID_client , @Type_of_Appliance, @Appliance_Model, @Problem_Description, @Request_Status)";

                string requestStatus = "новая";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Передаем ClientID
                    command.Parameters.AddWithValue("@ID_client ", loggedInClientID);
                    command.Parameters.AddWithValue("@Type_of_Appliance", textBox1.Text);
                    command.Parameters.AddWithValue("@Appliance_Model", textBox2.Text);
                    command.Parameters.AddWithValue("@Problem_Description", textBox3.Text);
                    command.Parameters.AddWithValue("@Request_Status", requestStatus);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Заявка успешно сохранена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Ошибка при выполнении SQL запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
