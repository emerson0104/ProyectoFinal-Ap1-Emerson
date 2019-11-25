using Hotell.BLL;
using Hotell.DAL;
using Hotell.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotell.UI.Registros
{
    public partial class rUsuarios : Form
    {
        public rUsuarios()
        {
            InitializeComponent();
        }
        private void Limpiar()
        {

            IdnumericUpDown.Value = 0;
            NombrestextBox.Text = string.Empty;
            ApellidostextBox.Text = string.Empty;
            NombreUsuariotextBox.Text = string.Empty;
            ClavetextBox.Text = string.Empty;
            ConfirmartextBox.Text = string.Empty;
            EmailtextBox.Text = string.Empty;
            CelularmaskedTextBox1.Text = string.Empty;
            FechaCreaciondateTimePicker.Value = DateTime.Now;
            TiposcomboBox.Text = string.Empty;
            TotalVendidotextBox.Text = string.Empty;
            MyErrorProvider.Clear();

        }

        private Usuarios LlenaClase()
        {
            Usuarios usuarios = new Usuarios();
            usuarios.UsuarioId = Convert.ToInt32(IdnumericUpDown.Value);
            usuarios.Nombres = NombrestextBox.Text;
            usuarios.Apellidos = ApellidostextBox.Text;
            usuarios.Usuario = NombreUsuariotextBox.Text;
            usuarios.Clave = ClavetextBox.Text;
            usuarios.NivelAcceso = TiposcomboBox.Text;
            usuarios.TotalVentas = Convert.ToDecimal(TotalVendidotextBox.Text);
            usuarios.Email = EmailtextBox.Text;
            usuarios.FechaCreacion = FechaCreaciondateTimePicker.Value;
            usuarios.Celular = CelularmaskedTextBox1.Text;

            return usuarios;
        }


        private void LlenaCampo(Usuarios usuarios)
        {
            IdnumericUpDown.Value = usuarios.UsuarioId;
            NombrestextBox.Text = usuarios.Nombres;
            ApellidostextBox.Text = usuarios.Apellidos;
            NombreUsuariotextBox.Text = usuarios.Usuario;
            TiposcomboBox.Text = usuarios.NivelAcceso;
            TotalVendidotextBox.Text = Convert.ToString(usuarios.TotalVentas);
            ClavetextBox.Text = usuarios.Clave;
            ConfirmartextBox.Text = usuarios.Clave;
            EmailtextBox.Text = usuarios.Email;
            FechaCreaciondateTimePicker.Value = usuarios.FechaCreacion;
            CelularmaskedTextBox1.Text = usuarios.Celular;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Usuarios> Repositorio = new RepositorioBase<Usuarios>();
            Usuarios usuarios = Repositorio.Buscar((int)IdnumericUpDown.Value);
            return (usuarios != null);
        }
        public static bool RepetirUser(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Usuarios.Any(p => p.Usuario.Equals(descripcion)))
                {
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        private bool ValidarRepetir()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (RepetirUser(NombreUsuariotextBox.Text))
            {
                MyErrorProvider.SetError(NombreUsuariotextBox, "No se debe repetir los usuarios.");
                paso = false;
            }

            return paso;
        }
        private bool Validar()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (string.IsNullOrWhiteSpace(NombrestextBox.Text))
            {
                MyErrorProvider.SetError(NombrestextBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(ApellidostextBox.Text))
            {
                MyErrorProvider.SetError(ApellidostextBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(NombreUsuariotextBox.Text))
            {
                MyErrorProvider.SetError(NombreUsuariotextBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(ClavetextBox.Text))
            {
                MyErrorProvider.SetError(ClavetextBox, "No puede ser vacio.");
                paso = false;
            }
            //  if ((CelularmaskedTextBox1.MaskCompleted))
          //   {
           //    MyErrorProvider.SetError(CelularmaskedTextBox1, "No puede ser vacio.");
           //   paso = false;
           //   }
            if (ConfirmartextBox.Text != ClavetextBox.Text)
            {
                MyErrorProvider.SetError(ConfirmartextBox, "La clave no coincide.");
                paso = false;
            }
            return paso;
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> Repositorio = new RepositorioBase<Usuarios>();
            Usuarios usuarios = new Usuarios();

            bool paso = false;

            if (!Validar())
                return;

            usuarios = LlenaClase();

            if (IdnumericUpDown.Value == 0)
            {
                if (!ValidarRepetir())
                    return;
                paso = Repositorio.Guardar(usuarios);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede guardar.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = Repositorio.Modificar(usuarios);
            }

            if (paso)
                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> Repositorio = new RepositorioBase<Usuarios>();

            MyErrorProvider.Clear();

            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            if (Repositorio.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                MyErrorProvider.SetError(IdnumericUpDown, "No existe.");
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> Repositorio = new RepositorioBase<Usuarios>();
            Usuarios usuarios = new Usuarios();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);
            Limpiar();

            usuarios = Repositorio.Buscar(id);
            if (usuarios != null)
                LlenaCampo(usuarios);
            else
                MessageBox.Show("No encontrado.");
        }
    }
    }

