using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGC_Basic_____project_
{
    public partial class F_GestaoUsuarios : Form
    {
        public F_GestaoUsuarios()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgv_usuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void F_GestaoUsuarios_Load(object sender, EventArgs e)
        {
            dgv_usuarios.DataSource = Banco.ObterUsuariosIdNome();
            dgv_usuarios.Columns[0].Width = 100;
            dgv_usuarios.Columns[1].Width = 192;
        }

        private void dgv_usuarios_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int contlinhas = dgv.SelectedRows.Count;
            if(contlinhas > 0)
            {
                DataTable dt = new DataTable();
                string vid = dgv.SelectedRows[0].Cells[0].Value.ToString();
                dt= Banco.ObterDadosUsuario(vid);
                txt_id.Text = dt.Rows[0].Field <Int64> ("N_IDUSUARIO").ToString();
                txt_nome.Text = dt.Rows[0].Field<string>("T_NOMEUSUARIO").ToString();
                txt_userName.Text = dt.Rows[0].Field<string>("T_USERNAME").ToString();
                txt_senha.Text = dt.Rows[0].Field<string>("T_SENHAUSUARIO").ToString();
                cbo_status.Text = dt.Rows[0].Field<string>("T_STATUSUSUARIO").ToString();
                n_NivelAcesso.Value = dt.Rows[0].Field<Int64>("N_NIVELUSUARIO");
            }
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            F_NovoUsuario f_NovoUsuario = new F_NovoUsuario();
            f_NovoUsuario.ShowDialog();
            dgv_usuarios.DataSource = Banco.ObterUsuariosIdNome();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            int linha = dgv_usuarios.SelectedRows[0].Index;
            Usuario u = new Usuario();
            u.N_IDUSUARIO = Convert.ToInt32(txt_id.Text);
            u.nome = txt_nome.Text;
            u.username = txt_nome.Text;
            u.username = txt_userName.Text;
            u.senha = txt_senha.Text;
            u.status = cbo_status.Text;
            u.nivel = Convert.ToInt32(Math.Round(n_NivelAcesso.Value));
            Banco.AtualizarUsuario(u);
            //dgv_usuarios.DataSource = Banco.ObterUsuariosIdNome();
            //dgv_usuarios.CurrentCell = dgv_usuarios[0, linha];
            //dgv_usuarios[0, linha].Value = txt_id.Text;
            dgv_usuarios[1, linha].Value = txt_nome.Text;
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Deseja mesmo excluir os dados?", "Excluir?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Banco.Deletarusuario(txt_id.Text);
                dgv_usuarios.Rows.Remove(dgv_usuarios.CurrentRow);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (txt_senha.UseSystemPasswordChar == true)
            {
                txt_senha.UseSystemPasswordChar = false;

            }
            else
            {
                if (txt_senha.UseSystemPasswordChar == false)
                {
                    txt_senha.UseSystemPasswordChar = true;

                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
