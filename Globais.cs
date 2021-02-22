using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC_Basic_____project_
{
    class Globais
    {
        public static string versao = "1.0";
        public static Boolean logado = false;
        public static int nivel = 0; // nivel 1 = usuário comum, nivel 2 = Gerente, nivel 3 = Gerenciador de dados e alterações
        //public static string caminho = System.Environment.CurrentDirectory;
        public static string caminho = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        public static string nomeBanco = "banco_sgc.db";
        public static string caminhoBanco = caminho+@"\banco\";
        public static string caminhoFotos = caminho + @"\foto\";

        /*
         * 
         *  Dados da Banco de dados (tb_usuarios)
        N_IDUSUARIO
        T_NOMEUSUARIO
        T_USERNAME
        T_SENHAUSUARIO
        T_STATUSUSUARIO
        N_NIVELUSUARIO
         */
    }
}
