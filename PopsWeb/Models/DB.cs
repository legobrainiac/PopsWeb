using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

//TODO(legobrainiac): Pesquisa dinamica de pops
//TODO(legobrainiac): Mudar mais nomes de cenas
//TODO(legobrainiac): Mais cenas de que me lembre depois 

namespace PopsWeb
{
    public class DB
    {
        private static DB instance;
        public static DB Instance
        {
            get
            {
                if (instance == null)
                    instance = new DB ();
                return instance;
            }
        }
        private string strLigacao;
        private SqlConnection ligacaoDB;
        public DB ()
        {
            //ligação à DB
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString ();
            ligacaoDB = new SqlConnection (strLigacao);
            ligacaoDB.Open ();
        }
        ~DB ()
        {
            try
            {
                ligacaoDB.Close ();
            }
            catch (Exception e)
            {
                Console.WriteLine (e.Message);
            }
        }
        #region Funções genéricas
        //devolve consulta
        public DataTable devolveConsulta (string sql)
        {
            SqlCommand comando = new SqlCommand (sql, ligacaoDB);
            DataTable registos = new DataTable ();
            SqlDataReader dados = comando.ExecuteReader ();
            registos.Load (dados);
            dados.Dispose ();
            comando.Dispose ();
            return registos;
        }
        public DataTable devolveConsulta (string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand (sql, ligacaoDB);
            DataTable registos = new DataTable ();
            comando.Parameters.AddRange (parametros.ToArray ());
            SqlDataReader dados = comando.ExecuteReader ();
            registos.Load (dados);
            dados.Dispose ();
            comando.Dispose ();
            return registos;
        }
        public DataTable devolveConsulta (string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand (sql, ligacaoDB);
            comando.Transaction = transacao;
            DataTable registos = new DataTable ();
            comando.Parameters.AddRange (parametros.ToArray ());
            SqlDataReader dados = comando.ExecuteReader ();
            registos.Load (dados);
            dados.Dispose ();
            comando.Dispose ();
            return registos;
        }
        public bool executaComando (string sql)
        {
            try
            {
                SqlCommand comando = new SqlCommand (sql, ligacaoDB);
                comando.ExecuteNonQuery ();
                comando.Dispose ();
            }
            catch (Exception erro)
            {
                return false;
            }
            return true;
        }
        public bool executaComando (string sql, List<SqlParameter> parametros)
        {
            try
            {
                SqlCommand comando = new SqlCommand (sql, ligacaoDB);
                comando.Parameters.AddRange (parametros.ToArray ());
                comando.ExecuteNonQuery ();
                comando.Dispose ();
            }
            catch (Exception erro)
            {
                Console.Write (erro.Message);
                //throw erro;
                return false;
            }
            return true;
        }

        public bool executaComando (string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            try
            {
                SqlCommand comando = new SqlCommand (sql, ligacaoDB);
                comando.Parameters.AddRange (parametros.ToArray ());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery ();
                comando.Dispose ();
            }
            catch (Exception erro)
            {
                Console.Write (erro.Message);
                return false;
            }
            return true;
        }
        public int executaScalar (string sql)
        {
            int valor = -1;
            try
            {
                SqlCommand comando = new SqlCommand (sql, ligacaoDB);
                valor = (int)comando.ExecuteScalar ();
                comando.Dispose ();
            }
            catch (Exception erro)
            {
                Console.Write (erro.Message);
                return valor;
            }
            return valor;
        }
        public int executaScalar (string sql, List<SqlParameter> parametros)
        {
            int valor = -1;
            try
            {
                SqlCommand comando = new SqlCommand (sql, ligacaoDB);
                comando.Parameters.AddRange (parametros.ToArray ());
                valor = (int)comando.ExecuteScalar ();
                comando.Dispose ();
            }
            catch (Exception erro)
            {
                Console.Write (erro.Message);
                return valor;
            }
            return valor;
        }
        #endregion
    }
}