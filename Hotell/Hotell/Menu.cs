using Hotell.UI.Consultas;
using Hotell.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotell
{
    public partial class Menu : Form
    {
        public int id { get; set; }
        public Menu()
        {
            this.id = id;
            InitializeComponent();
        }

        private void ClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rClientes us = new rClientes(id);
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }

        private void HabitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rHabitaciones us = new rHabitaciones(id);
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }

        private void ClienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cClientes us = new cClientes();
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }

        private void ReservacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rReservas us = new rReservas(id);
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }

        private void HabitacionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cHabitaciones us = new cHabitaciones();
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }

        private void UsuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cUsuarios us = new cUsuarios();
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }

        private void ReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cReservas us = new cReservas();
            us.MdiParent = this;
            us.StartPosition = FormStartPosition.CenterScreen;
            us.Show();
        }
    }
}
