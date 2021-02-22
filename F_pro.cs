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
    public partial class F_pro : Form
    {
        public F_pro()
        {
            InitializeComponent();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            tb_ID.Clear();
            tb_pro.Clear();
            mtb_tel.Clear();
            tb_pro.Focus();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            string vquery;
            if (tb_ID.Text == "")
            {
                vquery = "INSERT INTO tb_professores(T_NOMEPROFESSOR,T_FONE) VALUES ('" + tb_pro.Text +"','" + mtb_tel.Text + "')";
            }
            else
            {
                vquery = "UPDATE tb_professores SET T_NOMEPROFESSOR='" + tb_pro.Text + "',T_FONE='" + mtb_tel.Text + "' WHERE N_IDPROFESSOR = " + tb_ID.Text;
            }

            Banco.dml(vquery);
            vquery = @"
                SELECT
                    N_IDPROFESSOR as 'ID',
                    T_NOMEPROFESSOR as 'Nome',
                    T_FONE  as 'Telefone'
                FROM
                    tb_professores
                ORDER BY
                    N_IDPROFESSOR
                    
            ";
            dgv_pro.DataSource = Banco.dql(vquery);
        }

        private void F_pro_Load(object sender, EventArgs e)
        {
            string vquery = @"
                SELECT
                    N_IDPROFESSOR as 'ID' ,
                    T_NOMEPROFESSOR as 'Nome' ,
                    T_FONE as 'Telefone'
                    
                FROM
                    tb_professores
                ORDER BY
                    N_IDPROFESSOR
            ";
            dgv_pro.DataSource = Banco.dql(vquery);
            dgv_pro.Columns[0].Width = 60;
            dgv_pro.Columns[1].Width = 170;
            dgv_pro.Columns[2].Width = 100;
            
        }

        private void dgv_pro_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int contlinhas = dgv.SelectedRows.Count;
            if (contlinhas > 0)
            {
                DataTable dt = new DataTable();
                string vid = dgv.SelectedRows[0].Cells[0].Value.ToString();
                string vquery = @"
                    SELECT 
                        *
                    FROM
                        tb_professores
                    WHERE
                        N_IDPROFESSOR = " + vid;
                dt = Banco.dql(vquery);
                tb_ID.Text = dt.Rows[0].Field<Int64>("N_IDPROFESSOR").ToString();
                tb_pro.Text = dt.Rows[0].Field<string>("T_NOMEPROFESSOR");
                mtb_tel.Text = dt.Rows[0].Field<string>("T_FONE");
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Deseja realmente excluir?", "Excluir", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                string vquery = "DELETE FROM tb_professores WHERE N_IDPROFESSOR=" + tb_ID.Text;
                Banco.dml(vquery);
                dgv_pro.Rows.Remove(dgv_pro.CurrentRow);
            }
        }
    }
}
