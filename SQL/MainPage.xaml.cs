using SQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SQL
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Persona person = new Persona
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Correo = txtCorreo.Text,
                };
                await App.SQLiteDB.SavePersonaAsync(person);
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEdad.Text = "";
                txtCorreo.Text = "";
                await DisplayAlert("Registro", "Ingresado correctamente", "Ok");
                LimpiarControles();
                llenarDatos();


            }
            else
            {
                await DisplayAlert("Error","Ingresar todos los datos","Ok");
            }
        }
        public async void llenarDatos()
        {
            var personaList = await App.SQLiteDB.GetAllPersonaAsync();
            if (personaList != null)
            {
                lstPersonas.ItemsSource = personaList;
            }
        }
        public bool validarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellido.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdPersona.Text))
            {
                Persona persona = new Persona()
                {
                    IdPersona = Convert.ToInt32(txtIdPersona.Text),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                    Correo = txtCorreo.Text,
                };
                await App.SQLiteDB.SavePersonaAsync(persona);
                await DisplayAlert("Registro", "Se ingreso de manera exitosa", "Ok");

                LimpiarControles();
                txtIdPersona.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                llenarDatos();
            }
        }

        private async void lstPersonas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Persona)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtNombre.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdPersona.ToString()))
            {
                var persona = await App.SQLiteDB.GetPersonaByIdAsync(obj.IdPersona);
                if(persona != null)
                {
                    txtIdPersona.Text = persona.IdPersona.ToString();
                    txtNombre.Text = persona.Nombre;
                    txtApellido.Text = persona.Apellido;
                    txtEdad.Text = persona.Edad.ToString();
                    txtCorreo.Text = persona.Correo;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var persona = await App.SQLiteDB.GetPersonaByIdAsync(Convert.ToInt32(txtIdPersona.Text));
            if(persona != null)
            {
                bool answer = await DisplayAlert("Confirmar", "¿Está seguro de que desea eliminar este registro?", "Sí", "No");

                if (answer)
                {
                    // Eliminar el registro
                    await App.SQLiteDB.DeletePersonaAsync(persona);
                    await DisplayAlert("Registro", "Se elimino correctamente", "Ok");
                    LimpiarControles();
                    llenarDatos();
                    txtIdPersona.IsVisible = false;
                    btnActualizar.IsVisible = false;
                    btnEliminar.IsVisible = false;
                    btnRegistrar.IsVisible = true;
                }
                
            }
        }

        public void LimpiarControles()
        {
            txtIdPersona.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            txtCorreo.Text = "";
        }
    }
}
