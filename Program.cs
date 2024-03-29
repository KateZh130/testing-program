﻿using System;
using System.Windows.Forms;

namespace testing_program
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DatabaseClass databaseclass = new DatabaseClass();
            databaseclass.Connect_to_database();
            Application.Run(new RegistrationForm(databaseclass));
        }
    }
}
