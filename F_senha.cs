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
    public partial class F_senha : Form
    {
        DataTable dt = new DataTable();
        public F_senha()
        {
            InitializeComponent();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (tb_senha.UseSystemPasswordChar == true)
            {
                tb_senha.UseSystemPasswordChar = false;

            }
            else
            {
                if (tb_senha.UseSystemPasswordChar == false)
                {
                    tb_senha.UseSystemPasswordChar = true;

                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_senha2.UseSystemPasswordChar == true)
            {
                tb_senha2.UseSystemPasswordChar = false;

            }
            else
            {
                if (tb_senha2.UseSystemPasswordChar == false)
                {
                    tb_senha2.UseSystemPasswordChar = true;

                }
            }
        }

        private void F_senha_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || tb_senha.Text== "" || tb_senha2.Text == "")
            {
                MessageBox.Show("Campo vazio, todos os campos devem ser preenchidos", "campo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(tb_senha.Text == tb_senha2.Text)
            {
                string checar = String.Format(@"
            UPDATE 
                tb_usuarios
            SET
                T_SENHAUSUARIO='{0}'
            WHERE
                T_USERNAME = '{1}'
            ", tb_senha.Text, textBox1.Text);
                Banco.dml(checar);
                MessageBox.Show("Senha alterada com louvor", "Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Senhas não corresponsentes \n Digite novamente","Senha",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
