using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SGC_Basic_____project_
{
    public partial class F_GestaoT : Form
    {
        string idSelecionado;
        int modo = 0; // 0 padrão /1 edição/2 inserção
        string vqueryDGV;
        public F_GestaoT()
        {
            InitializeComponent();
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void F_GestaoT_Load(object sender, EventArgs e)
        {
            vqueryDGV = @"
            SELECT  
            tbt.N_IDTURMA as 'ID',
            tbt.T_DSCTURMA as 'Planos',
            tbh.T_DSCHORARIO as 'Horário'
 
            FROM 
                tb_turmas as tbt
            INNER JOIN
                tb_horarios as tbh on tbh.N_IDHORARIO = tbt.N_IDHORARIO
            ";
            dgv_turmas.DataSource = Banco.dql(vqueryDGV);
            dgv_turmas.Columns[0].Width = 40;
            dgv_turmas.Columns[1].Width = 140;
            dgv_turmas.Columns[2].Width = 85;

            string vqueryPRO = @"
            SELECT  
            N_IDPROFESSOR,
            T_NOMEPROFESSOR
            
            FROM 
                tb_professores
            ORDER BY
                N_IDPROFESSOR
            ";

            cb_pro.Items.Clear();
            cb_pro.DataSource = Banco.dql(vqueryPRO);
            cb_pro.DisplayMember = "T_NOMEPROFESSOR";
            cb_pro.ValueMember = "N_IDPROFESSOR";

            // status
            Dictionary<string, string> st = new Dictionary<string, string>();
            st.Add("A", "Ativo");
            st.Add("P", "Paralizado");
            st.Add("C", "Cancelado");

            cb_status.Items.Clear();
            cb_status.DataSource = new BindingSource(st, null);
            cb_status.DisplayMember = "Value";
            cb_status.ValueMember = "key";

            //horarios

            string vqueryHorarios = @"
            SELECT  
            *   
            FROM 
                tb_horarios
            ORDER BY
               T_DSCHORARIO
            ";

            cb_horario.Items.Clear();
            cb_horario.DataSource = Banco.dql(vqueryHorarios);
            cb_horario.DisplayMember = "T_DSCHORARIO";
            cb_horario.ValueMember = "N_IDHORARIO";
        }

        private void dgv_turmas_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int contLinhas = dgv.SelectedRows.Count;
            if(contLinhas > 0)
            {
                modo = 0;
                idSelecionado = dgv_turmas.Rows[dgv_turmas.SelectedRows[0].Index].Cells[0].Value.ToString();
                string vqueryCampos = @"
                    SELECT  
                        T_DSCTURMA,
                        N_IDPROFESSOR,
                        N_MAXALUNOS,
                        N_IDHORARIO,
                        T_STATUS
                    FROM 
                        tb_turmas
                    WHERE
                        N_IDTURMA =" + idSelecionado;
                DataTable dt = Banco.dql(vqueryCampos);
                tb_dscTurma.Text = dt.Rows[0].Field<string>("T_DSCTURMA");
                cb_pro.SelectedValue = dt.Rows[0].Field<Int64>("N_IDPROFESSOR").ToString();
                n_max.Value = dt.Rows[0].Field<Int64>("N_MAXALUNOS");
                cb_status.SelectedValue = dt.Rows[0].Field<string>("T_STATUS");
                cb_horario.SelectedValue = dt.Rows[0].Field<Int64>("N_IDHORARIO");

                tb_vagas.Text = calcVagas();
            }
        }

        private string calcVagas()
        {
            string queryVagas = String.Format(@"
                    SELECT
                        count(N_IDALUNO) as 'cont'
                    FROM
                        tb_alunos
                    WHERE
                        T_STATUS = 'A' AND N_IDTURMA = {0}", idSelecionado);
            DataTable dt = Banco.dql(queryVagas);
            int vagas = Int32.Parse(Math.Round(n_max.Value, 0).ToString());
            vagas -= Int32.Parse(dt.Rows[0].Field<Int64>("cont").ToString());
            tb_vagas.Text = vagas.ToString();
            return vagas.ToString();
        }

        private void btn_nova_Click(object sender, EventArgs e)
        {
            tb_dscTurma.Clear();
            cb_pro.SelectedIndex = -1;
            n_max.Value = 0;
            cb_status.SelectedIndex = -1;
            cb_horario.SelectedIndex = -1;
            tb_dscTurma.Focus();
            modo = 2;


        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (modo!=0) {
                string queryTurma = "";
                string msg = "";
                if (modo == 1)
                {
                    msg = "Dados alterados";
                    queryTurma = String.Format(@"
                UPDATE
                    tb_turmas
                SET
                    T_DSCTURMA='{0}',
                    N_IDPROFESSOR={1},
                    N_IDHORARIO={2},
                    N_MAXALUNOS={3},
                    T_STATUS='{4}'
                WHERE
                    N_IDTURMA={5}
            ", tb_dscTurma.Text, cb_pro.SelectedValue, cb_horario.SelectedValue, Int32.Parse(Math.Round(n_max.Value, 0).ToString()), cb_status.SelectedValue, idSelecionado);
                }
                else
                {
                    msg = "Novo Plano Inserido";
                    queryTurma = String.Format(@"
                INSERT INTO
                    tb_turmas
                (T_DSCTURMA,N_IDPROFESSOR,N_IDHORARIO,N_MAXALUNOS,T_STATUS)
                VALUES('{0}',{1},{2},{3},'{4}')",tb_dscTurma.Text,cb_pro.SelectedValue,cb_horario.SelectedValue, Int32.Parse(Math.Round(n_max.Value, 0).ToString()), cb_status.SelectedValue);
               }
            int linha = dgv_turmas.SelectedRows[0].Index;
                
            Banco.dml(queryTurma);
            if(modo == 1)
                {
                    dgv_turmas[1, linha].Value = tb_dscTurma.Text;
                    dgv_turmas[2, linha].Value = cb_horario.Text;
                    tb_vagas.Text = calcVagas();
                }
                else
                {
                    dgv_turmas.DataSource = Banco.dql(vqueryDGV);
                }
            
            MessageBox.Show(msg,"Salvar",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Confirma a exclusão ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string queryExcluir = String.Format(@"
                    DELETE
                    FROM
                        tb_turmas
                    WHERE
                        N_IDTURMA={0}", idSelecionado);

                Banco.dml(queryExcluir);
                dgv_turmas.Rows.Remove(dgv_turmas.CurrentRow);
            }
        }

        private void tb_dscTurma_TextChanged(object sender, EventArgs e)
        {
            if(modo == 0)
            {
                modo = 1;
            }
            
        }

        private void cb_pro_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (modo == 0)
            {
                modo = 1;
            }
        }

        private void n_max_ValueChanged(object sender, EventArgs e)
        {
            if (modo == 0)
            {
                modo = 1;
            }
            
        }

        private void cb_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modo == 0)
            {
                modo = 1;
            }
        }

        private void cb_horario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modo == 0)
            {
                modo = 1;
            }
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            string nomeArquivo = Globais.caminho + @"\Planos.pdf";
            FileStream arquivoPDF = new FileStream(nomeArquivo, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPDF);

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Globais.caminho + @"\ico.png");
            logo.ScaleToFit(140f, 120f); //largura,altura
            logo.Alignment = Element.ALIGN_LEFT;
            //logo.SetAbsolutePosition(100f, 750f);//x,-y

            string dados = "";

            Paragraph paragrafo1 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold));
            paragrafo1.Alignment = Element.ALIGN_CENTER;
            paragrafo1.Add("Relatório de Planos \n\n");


            Paragraph paragrafo2 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold));
            paragrafo2.Alignment = Element.ALIGN_CENTER;
            paragrafo2.Add("SGC Basic ++ [Project] \n\n");

            DateTime hora = DateTime.Now;
            Paragraph paragrafo3 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8, (int)System.Drawing.FontStyle.Bold));
            paragrafo3.Alignment = Element.ALIGN_BOTTOM;
            paragrafo3.Add("\n\n\n\n\n\n\n\n\n\n\n\n" + "Data da consulta : " + hora); 

            PdfPTable tabela = new PdfPTable(3);//3 colunas
            tabela.DefaultCell.FixedHeight = 20;

            tabela.AddCell("ID Plano");
            tabela.AddCell("Planos");
            tabela.AddCell("Horário ");


            DataTable dtTurmas = Banco.dql(vqueryDGV);
            for(int i = 0; i < dtTurmas.Rows.Count; i++)
            {
                tabela.AddCell(dtTurmas.Rows[i].Field<Int64>("ID").ToString());
                tabela.AddCell(dtTurmas.Rows[i].Field<string>("Planos"));
                tabela.AddCell(dtTurmas.Rows[i].Field<string>("Horário"));
            }

            doc.Open();
            doc.Add(logo);
            doc.Add(paragrafo2);
            doc.Add(paragrafo1);
            doc.Add(tabela);
            doc.Add(paragrafo3);
            doc.Close();

            DialogResult res = MessageBox.Show("Deseja abrir o relatório?", "Relatório", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Globais.caminho + @"\Planos.pdf");
            }

        }
    }
}
