using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using Repair;
using System.Windows.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Form registrationForm;
        [TestInitialize]
        public void Setup()
        {
            registrationForm = new Registration();
        }


        [TestMethod]
        public void TestLoginAsAdmin_OpenForm1()
        {
            Repair.Authorization authorizationForm = new Repair.Authorization();
            string login = "admin";
            string password = "admin";

            bool loginSuccessful = authorizationForm.Login(login, password);

            Assert.IsTrue(loginSuccessful, "Вход в систему администратора должен быть успешным.");
        }

        [TestMethod]
        public void TestLoginAsClient_OpenForm2()
        {
            Repair.Authorization authorizationForm = new Repair.Authorization();
            string login = "ivanov";
            string password = "password123";

            bool loginSuccessful = authorizationForm.Login(login, password);

            Assert.IsTrue(loginSuccessful, "Вход в систему клиента должен быть успешным.");
        }

        [TestMethod]
        public void TestLoginAsEmployee_OpenForm3()
        {
            Repair.Authorization authorizationForm = new Repair.Authorization();
            string login = "smirnov";
            string password = "password888";

            bool loginSuccessful = authorizationForm.Login(login, password);

            Assert.IsTrue(loginSuccessful, "Вход в систему сотрудника должен быть успешным.");
        }

        [TestMethod]
        public void TestSetComboBoxSelection_ChangeSelectedTable()
        {
            Form1 form = new Form1();

            form.SetComboBoxSelection(0); // Установить индекс для "Mechanics"

            Assert.AreEqual("Mechanics", form.selectedTable, "После выбора должна отобразиться таблица Механиков");
        }

        [TestMethod]
        public void TestSetComboBoxSelection_ChangeToClients()
        {
            Form1 form = new Form1();

            form.SetComboBoxSelection(1); // Установить индекс для "Clients"

            Assert.AreEqual("Clients", form.selectedTable, "После выбора должна отобразиться таблица Клиентов.");
        }

        [TestMethod]
        public void TestSetComboBoxSelection_ChangeToRepairRequests()
        {
            Form1 form = new Form1();

            form.SetComboBoxSelection(2); // Установить индекс для "Repair_Requests"

            Assert.AreEqual("Repair_Requests", form.selectedTable, "После выбора должна отобразиться таблица Запросов на ремонт.");
        }

        [TestMethod]
        public void TestSetComboBoxSelection_InvalidIndex()
        {
            Form1 form = new Form1();
            string previousSelection = form.selectedTable; // Сохранить текущее значение

            form.SetComboBoxSelection(5); // Установить неверный индекс

            Assert.AreEqual(previousSelection, form.selectedTable, "При установке неправильного индекса выбор таблицы не должен измениться.");
        }

        [TestMethod]
        public void TestRegistration_WithEmptyFields_ShowsWarning()
        {
            TextBox textBoxLogin = (TextBox)registrationForm.Controls["textBox1"];
            TextBox textBoxPassword = (TextBox)registrationForm.Controls["textBox2"];
            TextBox textBoxFullName = (TextBox)registrationForm.Controls["textBox3"];
            TextBox textBoxPhoneNumber = (TextBox)registrationForm.Controls["textBox4"];
            TextBox textBoxEmail = (TextBox)registrationForm.Controls["textBox5"];
            TextBox textBoxAddress = (TextBox)registrationForm.Controls["textBox6"];

            textBoxLogin.Text = ""; // Пустой логин
            textBoxPassword.Text = "password123"; 
            textBoxFullName.Text = "Иванов Иван"; 
            textBoxPhoneNumber.Text = ""; // Пустой номер телефона
            textBoxEmail.Text = "ivanov@example.com"; 
            textBoxAddress.Text = "Москва, ул. Пушкина"; 

            string expectedMessage = "Пожалуйста, заполните все обязательные поля.";
            string actualMessage = string.Empty;

            // Act: Нажимаем кнопку регистрации
            var buttonRegister = (Button)registrationForm.Controls["button1"];
            buttonRegister.PerformClick();

            // Проверяем, что сообщение соответствует ожиданию
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void TestLogin_Failure()
        {
            Repair.Authorization authorizationForm = new Repair.Authorization();
            string invalidLogin = "wrongUser";
            string invalidPassword = "wrongPassword";

            bool loginSuccessful = authorizationForm.Login(invalidLogin, invalidPassword);

            Assert.IsFalse(loginSuccessful, "Вход в систему с неверным логином и паролем должен завершиться неудачей.");
        }

       [TestMethod]
        public void TestExitApplication()
        {
            Repair.Registration registrationForm = new Repair.Registration();

            registrationForm.ExitApplication();

            Assert.IsTrue(registrationForm.IsDisposed, "Экземпляр формы регистрации должен быть закрыт.");
        }


    }
}


