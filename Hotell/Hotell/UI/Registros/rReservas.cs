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
    public partial class rReservas : Form
    {
        public List<ReservasDetalle> Detalle { get; set; }
        private int id;
        public rReservas(int id)
        {
            this.id = id;
            InitializeComponent();
            Cliente();
            Habitacion();
            
            Detalle = new List<ReservasDetalle>();
           //  CargarUsuario();
            
        }
        private void Cliente()
        {
            RepositorioBase<Clientes> db = new RepositorioBase<Clientes>();
            var listado = new List<Clientes>();
            listado = db.GetList(p => true);
            ClientecomboBox.DataSource = listado;
            ClientecomboBox.DisplayMember = "Nombres";
            ClientecomboBox.ValueMember = "ClienteId";

        }
      
      void fechaAdia()
        {
            int dia;

            Reservas r = new Reservas();
            DateTime fechai = r.FechaLlegada = FechaLlegadateTimePicker.Value.Date;
            DateTime fechaF = r.FechaSalida = FechaSalidadateTimePicker.Value.Date;
            TimeSpan T = fechaF - fechai;
            dia = T.Days;
            ///    decimal v=Convert.ToDecimal(ValortextBox.Text);
            decimal d = (decimal)dia;


            decimal res = (d);
            PreciotextBox.Text = Convert.ToString(res);



        }
        private void Habitacion()
        {
            RepositorioBase<Habitaciones> db = new RepositorioBase<Habitaciones>();
            var listado = new List<Habitaciones>();
            listado = db.GetList(p => true);
            NumerocomboBox.DataSource = listado;
            NumerocomboBox.DisplayMember = "numero";

            NumerocomboBox.ValueMember = "habitacionId";

        }
        private void CargarGrid()
        {//List<ReservasDetalle>lista
         //     DetalledataGridView.Rows.Clear();
         // foreach (var item in lista)
         //  {
         //     DetalledataGridView.Rows.Add(item.HabitacionId, item.Numero, item.Tipo, item.Descripcion, item.Precio);
         // }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = this.Detalle;

        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            ClientecomboBox.Text = null;
            ValortextBox.Text = string.Empty;
            TipotextBox.Text = string.Empty;
            PreciotextBox.Text = string.Empty;
            NumerocomboBox.Text = null;
            FechaReservadateTimePicker.Value = DateTime.Now;
            FechaLlegadateTimePicker.Value = DateTime.Now;
            FechaSalidadateTimePicker.Value = DateTime.Now;

            MontotextBox.Text = string.Empty; ;
            this.Detalle = new List<ReservasDetalle>();
            CargarGrid();
            MyErrorProvider.Clear();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Reservas> db = new RepositorioBase<Reservas>();
            Reservas re = db.Buscar((int)IdnumericUpDown.Value);
            return (re != null);
        }

        private bool Validar()
        {
            bool paso = true;
            Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
            MyErrorProvider.Clear();
            if (string.IsNullOrWhiteSpace(ClientecomboBox.Text))
            {
                MyErrorProvider.SetError(ClientecomboBox, "No puede ser vacio.");
                paso = false;
            }

            if (FechaSalidadateTimePicker.Value <= FechaReservadateTimePicker.Value)
            {
                MyErrorProvider.SetError(FechaSalidadateTimePicker, "No puede ser igual A Fecha de salida");
                paso = false;
            }

            if (Detalle.Count == 0)
            {
                MessageBox.Show("El grid esta vacio.", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                paso = false;
            }
            return paso;
        }

        private void LlenaCampo(Reservas v)
        {
            IdnumericUpDown.Value = v.ReservaId;
            ClientecomboBox.Text = Convert.ToString(v.ClienteId);
            FechaLlegadateTimePicker.Value = v.FechaLlegada;
            FechaSalidadateTimePicker.Value = v.FechaSalida;
            FechaReservadateTimePicker.Value = v.FechaReserva;
            MontotextBox.Text = Convert.ToString(v.MontroReserva);
            
            this.Detalle = v.Detalle;
            CargarGrid();
        }
        private Reservas LlenaClase()
        {
            Reservas r = new Reservas();
            ReservasDetalle rd = new ReservasDetalle();
            r.Detalle = this.Detalle;
            r.ReservaId = Convert.ToInt32(IdnumericUpDown.Value);
            r.ClienteId = Convert.ToInt32(ClientecomboBox.SelectedValue.ToString());
            r.FechaReserva = FechaReservadateTimePicker.Value;
            r.MontroReserva = Convert.ToDecimal(MontotextBox.Text);
            r.FechaLlegada = FechaLlegadateTimePicker.Value;
            r.FechaSalida = FechaSalidadateTimePicker.Value;
            r.UsuarioId = 1;



            Habitaciones d = NumerocomboBox.SelectedItem as Habitaciones;

            

            r.Detalle = this.Detalle;
            CargarGrid();
            return r;
        }
        private void Agregarbutton_Click(object sender, EventArgs e)
        {

            if (NumerocomboBox.SelectedValue != null)
            {
                int d = (int)NumerocomboBox.SelectedValue;

                foreach (var item in Detalle)
                {
                    if (d == item.HabitacionId)
                    {
                        MyErrorProvider.SetError(Agregarbutton, "La habitacion ya esta agregada");
                        return;
                    }
                }
                if (Existencia())
                    return;



                Contexto db = new Contexto();
                if (dataGridView1.DataSource != null)
                {
                    this.Detalle = (List<ReservasDetalle>)dataGridView1.DataSource;
                }
                /*  this.Detalle.Add(new ReservasDetalle(
                      reservaId :0,
                      habitacionId :NumerocomboBox.SelectedIndex,
                      numero:NumerocomboBox.Text,
                      tipo:TipotextBox.Text,
                      descripcion:DescripciontextBox.Text,
                      precio:Convert.ToDecimal(PreciotextBox.Text)
                      )
                      );*/

                Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
                if (NumerocomboBox.SelectedValue != null)
                {
                    this.Detalle.Add(new ReservasDetalle()
                    {
                        Id = 0,
                        ReservaId = (int)IdnumericUpDown.Value,
                        HabitacionId = (int)NumerocomboBox.SelectedValue,
                        Numero = NumerocomboBox.Text,
                        Tipo = TipotextBox.Text,
                        Dias = Convert.ToDecimal(PreciotextBox.Text),
                        Precio = Convert.ToDecimal(ValortextBox.Text),
                        Importe = Convert.ToDecimal(ImporteTextBox.Text)
                    });

                }
                NumerocomboBox.Text = null;
                PreciotextBox.Text = null;
                TipotextBox.Text = null;
                ValortextBox.Text = null;
                CargarGrid();
                CargaTotal();
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["ReservaId"].Visible = false;
            }

        }
        public void CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Detalle)
            {
                total += item.Precio;
            }
            MontotextBox.Text = total.ToString();
        }

      
        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            if (ReservasBLL.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                MyErrorProvider.SetError(IdnumericUpDown, "No existe.");
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Reservas v = new Reservas();
            bool paso = false;


            if (!Validar())
                return;

            v = LlenaClase();

            if (IdnumericUpDown.Value == 0)
            {
                paso = ReservasBLL.Guardar(v);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede guardar.", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = ReservasBLL.Modificar(v);
            }

            if (paso)
                MessageBox.Show("Guardado", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar", "Hotel Software", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Limpiar();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Reservas> Repositorio = new RepositorioBase<Reservas>();
            Reservas v = new Reservas();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);
            Limpiar();

            v = Repositorio.Buscar(id);
            if (v != null)
                LlenaCampo(v);
            else
                MessageBox.Show("No encontrado.");
        }

        private void Checkinbutton_Click(object sender, EventArgs e)
        {
            Reservas r = new Reservas();
            ReservasBLL.checkins(r);
        }
        private bool Existencia()
        {
            bool paso = false;
            string estado = "Ocupado";
            
            RepositorioBase<Habitaciones> repositorio = new RepositorioBase<Habitaciones>();
            Habitaciones ha = repositorio.Buscar(Convert.ToInt32(NumerocomboBox.SelectedValue));
            if (estado == ha.Estado)
            {
                MessageBox.Show("Habitacion Ocupada!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                paso = true;
            }
            
            return paso;
        }


        private void NumerocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Habitaciones p = NumerocomboBox.SelectedItem as Habitaciones;
            if (p != null)
            {
                TipotextBox.Text = p.Tipo;
                ValortextBox.Text = Convert.ToString(p.Valor);

                
            }
        }

        private void PreciotextBox_TextChanged(object sender, EventArgs e)
        {
            decimal precio = ToDecimal(PreciotextBox.Text);
            decimal dias = ToDecimal(ValortextBox.Text);
            decimal importe = precio * dias;
            ImporteTextBox.Text = importe.ToString();
        }

        private void ValortextBox_TextChanged(object sender, EventArgs e)
        {
           
          
            }

        private void FechaSalidadateTimePicker_ValueChanged(object sender, EventArgs e)
        {


            fechaAdia();

        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null)
            {
                Detalle.RemoveAt(dataGridView1.CurrentRow.Index);
                CargarGrid();
                CalcularTotal();
            }
        }
        private void CargaTotal()
        {
            List<ReservasDetalle> detalle = new List<ReservasDetalle>();

            if (dataGridView1.DataSource != null)
            {
                detalle = (List<ReservasDetalle>)dataGridView1.DataSource;
            }
            decimal Total = 0;
            foreach (var item in detalle)
            {
                Total += item.Importe;
            }
            MontotextBox.Text = Total.ToString();
        }

        private void RebajaTotal()
        {
            List<ReservasDetalle> detalle = new List<ReservasDetalle>();

            if (dataGridView1.DataSource != null)
            {
                detalle = (List<ReservasDetalle>)dataGridView1.DataSource;
            }
            decimal Total = 0;
            foreach (var item in detalle)
            {
                Total -= item.Importe;
            }
            Total *= (-1);
            MontotextBox.Text = Total.ToString();
        }

        private int ToInt(object valor)
        {
            int retorno = 0;
            int.TryParse(valor.ToString(), out retorno);

            return retorno;
        }

        private decimal ToDecimal(object valor)
        {
            decimal retorno = 0;
            decimal.TryParse(valor.ToString(), out retorno);

            return retorno;
        }
    }
    }


