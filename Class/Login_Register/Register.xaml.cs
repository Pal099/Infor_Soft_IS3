﻿using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Infor_Soft_WPF.Class.Login_Register; // Ajusta el namespace a tu proyecto

namespace Infor_Soft_WPF.View
{
    public partial class Page1 : Window
    {
        public Page1()
        {
            InitializeComponent();
        }

        // Validar que solo se puedan ingresar números en txtMatricula
        private void txtMatricula_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // No números
            return !regex.IsMatch(text);
        }

        // Evento del botón Registrar
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string matriculaText = txtMatricula.Text.Trim();
            string password = txtPassword.Password;

            // Validaciones simples
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(matriculaText) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(matriculaText, out int matricula))
            {
                MessageBox.Show("La matrícula debe ser un número válido.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Instanciar clase para registro y llamar método
            RegisterUser registerUser = new RegisterUser();

            int nuevoIdUsuario = registerUser.RegistrarUsuario(usuario, matricula, correo, password);

            if (nuevoIdUsuario != -1)
            {
                MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow dashboard = new MainWindow(usuario, nuevoIdUsuario);  // ✅ Pasás el ID y el usuario
                dashboard.Show();
                ClearForm();
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario ya existe o hubo un error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

         private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Close(); // Cierra la ventana actual
        }

        // Método para limpiar formulario después de registro
        private void ClearForm()
        {
            txtUsuario.Clear();
            txtCorreo.Clear();
            txtMatricula.Clear();
            txtPassword.Clear();
        }

       
    }

}
