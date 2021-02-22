using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SGC_Basic_____project_
{
    public partial class F_NovoUsuario : Form
    {
        public F_NovoUsuario()
        {
            InitializeComponent();
        }

        private void F_NovoUsuario_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_nome.Clear();
            txt_userName.Clear();
            txt_senha.Clear();
            cbo_status.Text = "";
            n_NivelAcesso.Value = 1;
            txt_nome.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Usuario usuario = new Usuario();
            usuario.nome = txt_nome.Text;
            usuario.username = txt_userName.Text;
            usuario.senha = txt_senha.Text;
            usuario.status = cbo_status.Text;
            usuario.nivel = Convert.ToInt32(Math.Round(n_NivelAcesso.Value,0));
            Banco.NovoUsuario(usuario);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            txt_nome.Clear();
            txt_userName.Clear();
            txt_senha.Clear();
            cbo_status.Text = "";
            n_NivelAcesso.Value = 1;
            txt_nome.Focus();
            
        }

        private void btn_addFoto_Click(object sender, EventArgs e)
        {
            
        }
    }
}
