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

namespace Hotell.UI.Reportes
{
    public partial class ReservaReporte : Form
    {
        public ReservaReporte(List<Reservas> cl)
        {
            InitializeComponent();
            ListadoReservas re = new ListadoReservas();
            re.SetDataSource(cl);
            crystalReportViewer1.ReportSource = re;
            crystalReportViewer1.Refresh();
        }

        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
