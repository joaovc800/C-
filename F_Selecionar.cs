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
    public partial class F_Selecionar : Form
    {
        F_novoCliente formNovoCliente;
        public F_Selecionar(F_novoCliente f)
        {
            InitializeComponent();
            formNovoCliente = f;
        }

        private void F_Selecionar_Load(object sender, EventArgs e)
        {
            string queryTurmas = String.Format(@"
                SELECT
                    tbt.N_IDTURMA as 'ID',
                    tbt.T_DSCTURMA as 'Planos',
                    tbh.T_DSCHORARIO as 'Horarío',
                    tbp.T_NOMEPROFESSOR as 'Vendedor',
                    tbt.N_MAXALUNOS as 'Max.Clientes',
                    (   SELECT
                         count(N_IDALUNO)
                        FROM
                            tb_alunos as tba
                        WHERE
                            tba.N_IDTURMA = tbt.N_IDTURMA and T_STATUS = 'A' 
                    ) as 'Qtde. Clientes'
                FROM
                    tb_turmas as tbt
                INNER JOIN
                    tb_professores as tbp on tbp.N_IDPROFESSOR = tbt.N_IDPROFESSOR
                INNER JOIN
                    tb_horarios as tbh on tbh.N_IDHORARIO = tbt.N_IDHORARIO
            ");
            dgv_selecionar.DataSource = Banco.dql(queryTurmas);
        }

        private void dgv_selecionar_DoubleClick(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int maxAlunos = 0;
            int qtde = 0;

            maxAlunos = Int32.Parse(dgv.SelectedRows[0].Cells[4].Value.ToString());
            qtde = Int32.Parse(dgv.SelectedRows[0].Cells[5].Value.ToString());
            if(qtde >= maxAlunos)
            {
                MessageBox.Show("Esse plano não está disponivel");
            }
            else
            {
                formNovoCliente.tb_turma.Text = dgv.Rows[dgv.SelectedRows[0].Index].Cells[1].Value.ToString();
                formNovoCliente.tb_turma.Tag = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();
                Close();

            }
        }
    }
}
