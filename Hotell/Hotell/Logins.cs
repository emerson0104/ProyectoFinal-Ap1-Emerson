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

namespace Hotell
{
    public partial class Logins : Form
    {
        public static int Usuarioid { get; set; }
        public Logins()
        {
            InitializeComponent();
        }

        private void IngresarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> repo = new RepositorioBase<Usuarios>();

            var lista = repo.GetList(p => true);
            int id = Convert.ToInt32(UsuarioComboBox.SelectedValue.ToString());

            Usuarios usuario = repo.Buscar(id);


            if (usuario.Clave == ContraseñaTextBox.Text)
            {
                Form formulario = new Menu();
                formulario.Show();
                Usuarioid = id;

                this.Hide();

                 
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Logins_Load(object sender, EventArgs e)
        {
            RepositorioBase<Usuarios> repo = new RepositorioBase<Usuarios>();

            var Lista = repo.GetList(c => true);

            UsuarioComboBox.DataSource = Lista;
            UsuarioComboBox.ValueMember = "UsuarioId";
            UsuarioComboBox.DisplayMember = "Usuario";
        }

        private void Logins_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
