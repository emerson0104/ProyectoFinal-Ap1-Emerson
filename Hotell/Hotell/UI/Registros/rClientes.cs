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
    public partial class rClientes : Form
    {
        private int id;
        public rClientes(int id)
        {
            this.id = id;
            InitializeComponent();
        }
        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            NombrestextBox.Text = string.Empty;
            ApellidostextBox.Text = string.Empty;
            CelularmaskedTextBox.Text = string.Empty;
            DirecciontextBox.Text = string.Empty;
            EmailtextBox.Text = string.Empty;
            MyerrorProvider.Clear();
        }
        private void LlenaCampo(Clientes c)
        {
            IdnumericUpDown.Value = c.ClienteId;
            NombrestextBox.Text = c.Nombres;
            CelularmaskedTextBox.Text = c.Celular;
            ApellidostextBox.Text = c.Apellidos;
            DirecciontextBox.Text = c.Direccion;
            EmailtextBox.Text = c.Email;
        }
        private Clientes LlenaClase()
        {
            Clientes c = new Clientes();
            c.ClienteId = Convert.ToInt32(IdnumericUpDown.Value);
            c.Nombres = NombrestextBox.Text;
            c.Celular = CelularmaskedTextBox.Text;
            c.Apellidos = ApellidostextBox.Text;
            c.Direccion = DirecciontextBox.Text;
            c.Email = EmailtextBox.Text;
            c.UsuarioId = id;
            return c;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Clientes> Repositorio = new RepositorioBase<Clientes>();
            Clientes c = Repositorio.Buscar((int)IdnumericUpDown.Value);
            return (c != null);
        }
        public static bool RepetirNombre(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Cliente.Any(p => p.Nombres.Equals(descripcion)))
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
        public static bool RepetirApellido(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Cliente.Any(p => p.Apellidos.Equals(descripcion)))
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
     
        public static bool RepetirEmail(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Cliente.Any(p => p.Email.Equals(descripcion)))
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
        public static bool RepetirCelular(string descripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Cliente.Any(p => p.Celular.Equals(descripcion)))
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
        private bool ValidarRepeticion()
        {
            bool paso = true;
            MyerrorProvider.Clear();

            if (RepetirNombre(NombrestextBox.Text) || RepetirCelular(CelularmaskedTextBox.Text) || RepetirEmail(EmailtextBox.Text))
            {
                MessageBox.Show("Ya existe uno igual,Escriba otro.", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                paso = false;
            }
            return paso;
        }
        private bool Validar()
        {
            bool paso = true;
            MyerrorProvider.Clear();

            if (string.IsNullOrWhiteSpace(NombrestextBox.Text))
            {
                MyerrorProvider.SetError(NombrestextBox, "No puede Estar vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(DirecciontextBox.Text))
            {
                MyerrorProvider.SetError(DirecciontextBox, "No puede Estar vacio.");
                paso = false;
            }
            if (!CelularmaskedTextBox.MaskCompleted)
            {
                MyerrorProvider.SetError(CelularmaskedTextBox, "No puede Estar vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(EmailtextBox.Text))
            {
                MyerrorProvider.SetError(EmailtextBox, "No puede Estar vacio.");
                paso = false;
            }
            
            return paso;
        }
        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Clientes> Repositorio = new RepositorioBase<Clientes>();
            Clientes c = new Clientes();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);
            Limpiar();

            c = Repositorio.Buscar(id);
            if (c != null)
                LlenaCampo(c);
            else
                MessageBox.Show("No encontrado.");
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Clientes> Repositorio = new RepositorioBase<Clientes>();
            Clientes c = new Clientes();
            bool paso = false;
            if (!Validar())
                return;

            c = LlenaClase();


            if (IdnumericUpDown.Value == 0)
            {
                if (!ValidarRepeticion())
                    return;
                paso = Repositorio.Guardar(c);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede guardar.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = Repositorio.Modificar(c);
            }

            if (paso)
                MessageBox.Show("Guardado", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {

            RepositorioBase<Clientes> Repositorio = new RepositorioBase<Clientes>();

            MyerrorProvider.Clear();

            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            if (Repositorio.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                MyerrorProvider.SetError(IdnumericUpDown, "No existe.");
        }

        private void NombrestextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void ApellidostextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
    }
    
}

