﻿using Hotell.BLL;
using Hotell.Entidades;
using Hotell.UI.Reportes;
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
    public partial class cHabitaciones : Form
    {
        private List<Habitaciones> Lista;
        public cHabitaciones()
        {
            InitializeComponent();
        }

        private void Consultarbutton_Click(object sender, EventArgs e)
        {
            var listado = new List<Habitaciones>();
            RepositorioBase<Habitaciones> r = new RepositorioBase<Habitaciones>();

           
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
                                listado = r.GetList(p => p.HabitacionId == id);
                            }
                            break;

                        case "Numero":
                            listado = r.GetList(p => p.Numero.Contains(CriteriotextBox.Text));
                            break;

                        case "Descripion":
                            listado = r.GetList(p => p.Descripcion.Contains(CriteriotextBox.Text));
                            break;
                        case "Tipo":
                            listado = r.GetList(p => p.Tipo.Contains(CriteriotextBox.Text));
                            break;
                        case "Estado":

                            listado = r.GetList(p => p.Estado.Contains(CriteriotextBox.Text));
                            break;


                        case "Camas":
                            int pe;
                            if (!int.TryParse(CriteriotextBox.Text, out pe))
                            {
                                MessageBox.Show("Solo numeros.");
                            }
                            else
                            {
                                int i = Convert.ToInt32(CriteriotextBox.Text);
                                listado = r.GetList(p => p.Camas == i);
                            }
                            break;
                    }
                    //     listado = listado.Where(c => c.Fecha.Date >= DesdedateTimePicker.Value.Date && c.Fecha.Date <= HastadateTimePicker.Value.Date).ToList();
               
                {
                    if (FiltrocomboBox.Text == string.Empty)
                    {
                        MessageBox.Show("El filtro no puede estar vacio.");
                    }
                    else
                       if ((string)FiltrocomboBox.Text != "Todo")
                    {
                        if (CriteriotextBox.Text == string.Empty)
                        {
                            MessageBox.Show("Al seleccionar uno de ID, Nombres, Cedula, Telefono o Direccion necesita escribir algo en el criterio.");
                        }
                    }
                    listado = r.GetList(p => true);
                    //  listado = listado.Where(c => c.Fecha.Date >= DesdedateTimePicker.Value.Date && c.Fecha.Date <= HastadateTimePicker.Value.Date).ToList();
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
                                listado = r.GetList(p => p.HabitacionId == id);
                            }
                            break;

                        case "Numero":
                            listado = r.GetList(p => p.Numero.Contains(CriteriotextBox.Text));
                            break;

                        case "Descripcion":
                            listado = r.GetList(p => p.Descripcion.Contains(CriteriotextBox.Text));
                            break;
                        case "Tipo":
                            listado = r.GetList(p => p.Tipo.Contains(CriteriotextBox.Text));
                            break;
                        case "Estado":
                            listado = r.GetList(p => p.Estado.Contains(CriteriotextBox.Text));
                            break;
                        case "Camas":
                            int pe;
                            if (!int.TryParse(CriteriotextBox.Text, out pe))
                            {
                                MessageBox.Show("Solo numeros.");
                            }
                            else
                            {
                                int i = Convert.ToInt32(CriteriotextBox.Text);
                                listado = r.GetList(p => p.Camas == i);
                            }
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
                       if ((string)FiltrocomboBox.Text != "Todo")
                    {
                        if (CriteriotextBox.Text == string.Empty)
                        {
                            MessageBox.Show("Al seleccionar uno de ID, Nombres, Cedula, Telefono o Direccion necesita escribir algo en el criterio.");
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

        private void Imprimirbutton_Click(object sender, EventArgs e)
        {
            if (ConsultadataGridView.RowCount == 0)
            {
                MessageBox.Show("No se puede imprimir");
                return;
            }
            else
            {
           HabitacionesReporte reporte = new HabitacionesReporte(Lista);
              reporte.ShowDialog();
            }
        }

        private void CHabitaciones_Load(object sender, EventArgs e)
        {

        }
    }
}

      
