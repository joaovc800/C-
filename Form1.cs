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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            F_carregar f_Carregar = new F_carregar();
            f_Carregar.ShowDialog();

            F_Login f_Lofin = new F_Login(this);
            f_Lofin.ShowDialog();
        }

        private void abreForm(int nivel,Form f)
        {
            if (Globais.logado)
            {
                if (Globais.nivel >= nivel)
                {
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Você não tem permissão para esse tipo de acesso");
                }
            }
            else
            {
                MessageBox.Show("É necessário ter um usuário logado!");
            }
        }
        private void logonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Login f_Login = new F_Login(this);
            f_Login.ShowDialog();
        }

        private void logoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lb_acesso.Text = "0";
            lb_nomeUsuario.Text = "---";
            pb_led_logado.Image = Properties.Resources.led_vermelho;
            Globais.nivel = 0;
            Globais.logado = false;
        }

        private void bancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //abreForm();   
        }

        private void novoUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_NovoUsuario f_NovoUsuario = new F_NovoUsuario();
            abreForm(3, f_NovoUsuario);
        }

        private void gestãoDeUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_GestaoUsuarios f_GestaoUsuarios = new F_GestaoUsuarios();
            abreForm(2, f_GestaoUsuarios);
        }

        private void novoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_novoCliente f_novoCliente = new F_novoCliente();
            abreForm(1, f_novoCliente);
        }

        private void mANUTENÇÃOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pb_led_logado_Click(object sender, EventArgs e)
        {
           
        }

        private void horáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Horarios f_Horarios = new F_Horarios();
            abreForm(2, f_Horarios);
        }

        private void professoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_pro f_pro = new F_pro();
            abreForm(2, f_pro);
        }

        private void gestãoDeTurmasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_GestaoT  f_GestaoT = new F_GestaoT();
            abreForm(3, f_GestaoT);
        }

        private void gestãoDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_GestaoC f_GestaoC = new F_GestaoC();
            abreForm(3, f_GestaoC);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString();
        }
    }
}
