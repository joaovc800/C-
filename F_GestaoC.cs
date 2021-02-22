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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SGC_Basic_____project_
{
    public partial class F_GestaoC : Form
    {
        string vqueryDGV2 = "";
        string vqueryDGV = "";
        string turmaAtual = "";
        string idSelecionado = "";
        string turma = "";
        int linha = 0;

        public F_GestaoC()
        {
            InitializeComponent();
        }

        private void F_GestaoC_Load(object sender, EventArgs e)
        {
            vqueryDGV2 = @"
                SELECT
                    N_IDALUNO as 'ID',
                    T_NOMEALUNO as 'Cliente',
                    T_STATUS as 'Status',
                    T_TELEFONE as 'Telefone',
                    N_IDTURMA as 'ID Plano'
                FROM
                    tb_alunos
            ";

            vqueryDGV = @"
                SELECT
                    N_IDALUNO as 'ID',
                    T_NOMEALUNO as 'Cliente'
                FROM
                    tb_alunos
            ";
            dgv_GestaoC.DataSource = Banco.dql(vqueryDGV);
            dgv_GestaoC.Columns[0].Width = 60;
            dgv_GestaoC.Columns[1].Width = 265;
            tb_nome.Text = dgv_GestaoC.Rows[dgv_GestaoC.SelectedRows[0].Index].Cells[1].Value.ToString();

            string vqueryTurmas = @"
                SELECT
                    N_IDTURMA,
                    ('Máximo de clientes: '|| (
                                    (N_MAXALUNOS)-(
                                                    SELECT
                                                        count(tba.N_IDALUNO)
                                                    FROM
                                                        tb_alunos as tba
                                                    WHERE
                                                        tba.T_STATUS='A' and tba.N_IDTURMA=N_IDTURMA
                                                    )
                                 ) || ' / Plano: '|| T_DSCTURMA
                    ) as 'Plano'
                    FROM
                        tb_turmas
                    ORDER BY
                         N_IDTURMA
            ";
            cb_turmas.Items.Clear();
            cb_turmas.DataSource = Banco.dql(vqueryTurmas);
            cb_turmas.DisplayMember = "Plano";
            cb_turmas.ValueMember = "N_IDTURMA";

            Dictionary<string, string> status = new Dictionary<string, string>();
            status.Add("A", "Ativo");
            status.Add("B", "Bloqueado");
            status.Add("C", "Cancelado");
            cb_status.DataSource = new BindingSource(status, null);
            cb_status.DisplayMember = "Value";
            cb_status.ValueMember = "Key";

            turma = cb_turmas.Text;
            turmaAtual = cb_turmas.Text;
            idSelecionado = dgv_GestaoC.Rows[0].Cells[0].Value.ToString();


        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma a exclusão?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (File.Exists(pb_foto.ImageLocation))
                {
                    File.Delete(pb_foto.ImageLocation);
                }
                string vqueryExcluir = String.Format(@"
                    DELETE FROM
                        tb_alunos
                    WHERE
                        N_IDALUNO ={0}
                ", idSelecionado);
                Banco.dml(vqueryExcluir);
                dgv_GestaoC.Rows.Remove(dgv_GestaoC.CurrentRow);
                pb_foto.Image = null;
                tb_nome.Clear();
                mtb_telefone.Clear();
                cb_status.Text = "";
                cb_turmas.Text = "";

            }
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgv_GestaoC_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.SelectedRows.Count > 0)
            {
                idSelecionado = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();
                tb_nome.Text = dgv_GestaoC.Rows[dgv_GestaoC.SelectedRows[0].Index].Cells[1].Value.ToString();
                idSelecionado = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();
                string vqueryCampos = String.Format(@"
                    SELECT 
                        N_IDALUNO,
                        T_NOMEALUNO,
                        T_TELEFONE,
                        T_STATUS,
                        N_IDTURMA,
                        T_FOTO
                        
                    FROM
                        tb_alunos
                    WHERE
                        N_IDALUNO = {0}", idSelecionado);
                DataTable dt = Banco.dql(vqueryCampos);
                tb_nome.Text = dt.Rows[0].Field<string>("T_NOMEALUNO");
                mtb_telefone.Text = dt.Rows[0].Field<string>("T_TELEFONE");
                cb_status.SelectedValue = dt.Rows[0].Field<string>("T_STATUS");
                cb_turmas.SelectedValue = dt.Rows[0].Field<Int64>("N_IDTURMA");
                turmaAtual = cb_turmas.Text;
                pb_foto.ImageLocation = dt.Rows[0].Field<string>("T_FOTO");
            }
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            string nomeArquivo = Globais.caminho + @"\Dados do cliente.pdf";
            FileStream arquivoPdf = new FileStream(nomeArquivo, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter escritorPDF = PdfWriter.GetInstance(doc, arquivoPdf);

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Globais.caminho + @"\ico.png");
            logo.ScaleToFit(140f, 120f); //largura,altura
            logo.Alignment = Element.ALIGN_LEFT;
            //logo.SetAbsolutePosition(100f, 750f);//x,-y

            string dados = "";

            Paragraph paragrafo1 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold));
            paragrafo1.Alignment = Element.ALIGN_CENTER;
            paragrafo1.Add("Relatório de clientes \n\n");


            Paragraph paragrafo2 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, (int)System.Drawing.FontStyle.Bold));
            paragrafo2.Alignment = Element.ALIGN_CENTER;
            paragrafo2.Add("SGC Basic ++ [Project] \n\n");

            DateTime hora = DateTime.Now;
            Paragraph paragrafo3 = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8, (int)System.Drawing.FontStyle.Bold));
            paragrafo3.Alignment = Element.ALIGN_BOTTOM;
            paragrafo3.Add("\n\n\n\n\n\n\n\n\n\n\n\n" + "Data da consulta : " + hora);

            PdfPTable tabela = new PdfPTable(4);//5 colunas
            float[] widths = new float[] {1f,4f,4f,2f};

            tabela.DefaultCell.FixedHeight = 20;
            tabela.SetWidths(widths);

            tabela.AddCell("ID");
            tabela.AddCell("Cliente");
            tabela.AddCell("Telefone ");
            tabela.AddCell("Status ");
            //tabela.AddCell("Foto ");


            DataTable dtTurmas = Banco.dql(vqueryDGV2);
            for (int i = 0; i < dtTurmas.Rows.Count; i++)
            {
                tabela.AddCell(dtTurmas.Rows[i].Field<Int64>("ID").ToString());
                tabela.AddCell(dtTurmas.Rows[i].Field<string>("Cliente"));
                tabela.AddCell(dtTurmas.Rows[i].Field<string>("Telefone"));
                tabela.AddCell(dtTurmas.Rows[i].Field<string>("Status"));
                //tabela.AddCell(dtTurmas.Rows[i].Field<string>("Foto"));
            }

            doc.Open();
            doc.Add(logo);
            doc.Add(paragrafo2);
            doc.Add(paragrafo1);
            doc.Add(tabela);
            doc.Add(paragrafo3);
            doc.Close();

            DialogResult res = MessageBox.Show("Deseja abrir o relatório?", "Relatório", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Globais.caminho + @"\Dados do cliente.pdf");
            }
        }
    }
}
