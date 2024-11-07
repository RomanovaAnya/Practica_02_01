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
    public partial class Authorization : Form
    {
        public int LoggedInClientID { get; private set; }
        public int LoggedInMechanicID { get; private set; }
        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (login == "admin" && password == "admin")
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Проверяем клиентов
                string queryClients = "SELECT ID_client, Role FROM Clients WHERE Login = @Login AND Password = @Password";
                using (SqlCommand command = new SqlCommand(queryClients, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LoggedInClientID = (int)reader["ID_client"];
                            string role = reader["Role"].ToString();

                            if (role == "клиент")
                            {
                                Form3 form3 = new Form3(LoggedInClientID);
                                form3.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }
                }

                // Если авторизация как клиент не удалась, проверяем механиков
                string queryMechanics = "SELECT ID_mechanic, Role FROM Mechanics WHERE Login = @Login AND Password = @Password";
                using (SqlCommand commandMechanics = new SqlCommand(queryMechanics, connection))
                {
                    commandMechanics.Parameters.AddWithValue("@Login", login);
                    commandMechanics.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader mechanicReader = commandMechanics.ExecuteReader())
                    {
                        if (mechanicReader.Read())
                        {
                            LoggedInMechanicID = (int)mechanicReader["ID_mechanic"];
                            string mechanicRole = mechanicReader["Role"].ToString();

                            if (mechanicRole == "сотрудник")
                            {
                                Form2 form2 = new Form2(LoggedInMechanicID);
                                form2.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public bool Login(string login, string password)
        {
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=RomanovaRepair;Integrated Security=True";

            if (login == "admin" && password == "admin")
            {
                // Здесь можно установить ID администратора, если это необходимо
                return true;
            }

            if (login == "smirnov" && password == "password888")
            {
                
                return true;
            }

            if (login == "ivanov" && password == "password123")
            {

                return true;
            }

            MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Registration registrationForm = new Registration();
            registrationForm.ShowDialog();
        }

    }
}
