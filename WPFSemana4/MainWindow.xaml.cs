﻿using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;


namespace WPFSemana4
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("Data Source=LAB707-08\\SQLEXPRESS;Initial Catalog=Semana4;Integrated Security=True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Forma Conectada Procedimiento Almacenado
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "USP_GetUser";

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;

            parameter1.Value = "";
            parameter1.ParameterName = "@LastName";

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;

            parameter2.Value = "";
            parameter2.ParameterName = "@FirstName";

            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);


            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person()
                {
                    PersonID = dataReader["PersonID"].ToString(),
                    Lastname = dataReader["LastName"].ToString(),
                    Firstname = dataReader["FirtsName"].ToString(),
                   // FullName = string.Concat(dataReader["FirtsName"].ToString(), " ",
                  //  dataReader["LastName"].ToString())
                });
            }

            connection.Close();
            dgvPeople.ItemsSource = people;

        }
    }
}
