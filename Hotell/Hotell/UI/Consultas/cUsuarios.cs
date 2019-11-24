using Hotell.BLL;
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

namespace Hotell.UI.Consultas
{
    public partial class cUsuarios : Form
    {
        private List<Usuarios> Lista;
        public cUsuarios()
        {
            InitializeComponent();
        }

        private void Consultarbutton_Click(object sender, EventArgs e)
        { 

            var listado = new List<Usuarios>();
        RepositorioBase<Usuarios> r = new RepositorioBase<Usuarios>();
            if (RangocheckBox.Checked == true)
            {
                if (CriteriotextBox.Text.Trim().Length > 0)
                {
                    switch (FiltrocomboBox.Text)
                    {
                        case "Todo":
                            listado = r.GetList(p => true);
                            break;
                        case "ID":
                            int parse;
                            if (!int.TryParse(CriteriotextBox.Text, out parse))
                            {
                                MessageBox.Show("Solo numeros.");
                            }
                            else
                            {
                                int id = Convert.ToInt32(CriteriotextBox.Text);
    listado = r.GetList(p => p.UsuarioId == id);
                            }
                            break;
                        case "Nombres":
                            if (!System.Text.RegularExpressions.Regex.IsMatch(CriteriotextBox.Text, "^[a-zA-Z ]"))
                            {
                                MessageBox.Show("No numeros en los nombres.");
                            }
                            else
                            {
                                listado = r.GetList(p => p.Nombres.Contains(CriteriotextBox.Text));
                            }
                            break;

                        case "Usuario":
                            listado = r.GetList(p => p.Usuario.Contains(CriteriotextBox.Text));
                            break;
                        case "Email":
                            listado = r.GetList(p => p.Email.Contains(CriteriotextBox.Text));
                            break;
                    }
                    listado = listado.Where(c => c.FechaCreacion.Date >= DesdedateTimePicker.Value.Date && c.FechaCreacion.Date <= HastadateTimePicker.Value.Date).ToList();
                }
                else
                {
                    if (FiltrocomboBox.Text == string.Empty)
                    {
                        MessageBox.Show("El filtro no puede estar vacio.");
                    }
                    else
                       if ((string) FiltrocomboBox.Text != "Todo")
                    {
                        if (CriteriotextBox.Text == string.Empty)
                        {
                            MessageBox.Show("Al seleccionar uno de ID, Usuario, Nombres o Email necesita escribir algo en el criterio.");
                        }
                    }
                    listado = r.GetList(p => true);
                    listado = listado.Where(c => c.FechaCreacion.Date >= DesdedateTimePicker.Value.Date && c.FechaCreacion.Date <= HastadateTimePicker.Value.Date).ToList();
                }
                Lista = listado;
                ConsultadataGridView.DataSource = listado;
            }
            else
            {
                if (CriteriotextBox.Text.Trim().Length > 0)
                {
                    switch (FiltrocomboBox.Text)
                    {
                        case "Todo":
                            listado = r.GetList(p => true);
                            break;
                        case "ID":
                            int parse;
                            if (!int.TryParse(CriteriotextBox.Text, out parse))
                            {
                                MessageBox.Show("Solo numeros.");
                            }
                            else
                            {
                                int id = Convert.ToInt32(CriteriotextBox.Text);
listado = r.GetList(p => p.UsuarioId == id);
                            }
                            break;
                        case "Nombres":
                            if (!System.Text.RegularExpressions.Regex.IsMatch(CriteriotextBox.Text, "^[a-zA-Z ]"))
                            {
                                MessageBox.Show("No numeros en los nombres.");
                            }
                            else
                            {
                                listado = r.GetList(p => p.Nombres.Contains(CriteriotextBox.Text));
                            }
                            break;

                        case "Usuario":
                            listado = r.GetList(p => p.Usuario.Contains(CriteriotextBox.Text));
                            break;
                        case "Email":
                            listado = r.GetList(p => p.Email.Contains(CriteriotextBox.Text));
                            break;
                    }
                }
                else
                {
                    if (FiltrocomboBox.Text == string.Empty)
                    {
                        MessageBox.Show("El filtro no puede estar vacio.");
                    }
                    else
                       if ((string) FiltrocomboBox.Text != "Todo")
                    {
                        if (CriteriotextBox.Text == string.Empty)
                        {
                            MessageBox.Show("Al seleccionar uno de ID, Usuario, Nombres o Email necesita escribir algo en el criterio.");
                        }
                    }
                    else
                    {
                        listado = r.GetList(p => true);
                    }

                }
                Lista = listado;
                ConsultadataGridView.DataSource = listado;
            }
        }

    }
}
