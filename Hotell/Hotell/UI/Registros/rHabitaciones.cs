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
    public partial class rHabitaciones : Form
    {
        private int id;
        public rHabitaciones(int id)
        {
            this.id = id;
            InitializeComponent();
        }
        private void Limpiar()
        {

            IdnumericUpDown.Value = 0;
            NumerotextBox.Text = string.Empty;
            TipocomboBox.Text = string.Empty;
            DescripciontextBox.Text = string.Empty;
            CamasnumericUpDown.Value = 0;
            PreciotextBox.Text = string.Empty;
            EstadocomboBox.Text = string.Empty;


        }
        private void LlenaCampo(Habitaciones h)
        {
            IdnumericUpDown.Value = h.HabitacionId;
            NumerotextBox.Text = h.Numero;
            TipocomboBox.Text = h.Tipo;
            DescripciontextBox.Text = h.Descripcion;
            CamasnumericUpDown.Value = h.Camas;
            PreciotextBox.Text = Convert.ToString(h.Valor);
            EstadocomboBox.Text = h.Estado;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Habitaciones> Repositorio = new RepositorioBase<Habitaciones>();
            Habitaciones h = Repositorio.Buscar((int)IdnumericUpDown.Value);
            return (h != null);
        }
        private Habitaciones LlenaClase()
        {
            Habitaciones c = new Habitaciones();
            c.HabitacionId = Convert.ToInt32(IdnumericUpDown.Value);
            c.Numero = NumerotextBox.Text;
            c.Tipo = TipocomboBox.Text;
            c.Descripcion = DescripciontextBox.Text;
            c.Camas = Convert.ToInt32(CamasnumericUpDown.Value);
            c.Valor = Convert.ToDecimal(PreciotextBox.Text);
            c.Estado = EstadocomboBox.Text;
            c.UsuarioId = id;

            return c;

        }
        private bool Validar()
        {
            bool paso = true;
            MyErrorProvider.Clear();

            if (string.IsNullOrWhiteSpace(NumerotextBox.Text))
            {
                MyErrorProvider.SetError(NumerotextBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(TipocomboBox.Text))
            {
                MyErrorProvider.SetError(TipocomboBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(DescripciontextBox.Text))
            {
                MyErrorProvider.SetError(DescripciontextBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(PreciotextBox.Text))
            {
                MyErrorProvider.SetError(PreciotextBox, "No puede ser vacio.");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(EstadocomboBox.Text))
            {
                MyErrorProvider.SetError(EstadocomboBox, "No puede ser vacio.");
                paso = false;
            }
            return paso;
        }
        public static bool Repetir(string descripcion)
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

            if (Repetir(NumerotextBox.Text))
            {
                MyErrorProvider.SetError(NumerotextBox, "No se debe repetir los Numeros de la habitacion.");
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
            RepositorioBase<Habitaciones> Repositorio = new RepositorioBase<Habitaciones>();
            Habitaciones h = new Habitaciones();
            bool paso = false;
            if (!Validar())
                return;

            h = LlenaClase();


            if (IdnumericUpDown.Value == 0)
            {
                if (!ValidarRepetir())
                    return;
                paso = Repositorio.Guardar(h);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede guardar.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = Repositorio.Modificar(h);
            }
            if (paso)
                MessageBox.Show("Guardado", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Limpiar();
        
    }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Habitaciones> Repositorio = new RepositorioBase<Habitaciones>();

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
            RepositorioBase<Habitaciones> Repositorio = new RepositorioBase<Habitaciones>();
            Habitaciones h = new Habitaciones();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);
            Limpiar();

            h = Repositorio.Buscar(id);
            if (h != null)
                LlenaCampo(h);
            else
                MessageBox.Show("No encontrado.");
        }
    }
}
