using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace PopsWeb.Models
{
    public class UsersPopsModel
    {
        [Key]
        public int id { get; set; }
        public int id_user { get; set; }
        public int id_pop { get; set; }
    }

    public class UsersPopsDB
    {
        public void create (UsersPopsModel novo)
        {
            string sql = @"INSERT INTO users_pops(id_user, id_pop)
                        VALUES (@id_user, @id_pop)";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id_user", SqlDbType = SqlDbType.Int,Value=novo.id_pop},
                new SqlParameter(){ParameterName="@id_pop", SqlDbType = SqlDbType.Int,Value=novo.id_user},
            };
            DB.Instance.executaComando (sql, parametros);
        }

        public List<UsersPopsModel> list ()
        {
            string sql = "SELECT * FROM users_pops";
            DataTable registos = DB.Instance.devolveConsulta (sql);
            List<UsersPopsModel> lista = new List<UsersPopsModel> ();
            foreach (DataRow data in registos.Rows)
            {
                UsersPopsModel novo = new UsersPopsModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.id_user = int.Parse (data[1].ToString ());
                novo.id_pop = int.Parse (data[2].ToString ());
                lista.Add (novo);
            }
            return lista;
        }

        public List<UsersPopsModel> list (int id)
        {
            string sql = "SELECT * FROM users_pops where id like @id";

            List<UsersPopsModel> lista = new List<UsersPopsModel> ();

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id", SqlDbType =SqlDbType.Int,Value=id},
            };

            DataTable registos = DB.Instance.devolveConsulta (sql, parametros);

            foreach (DataRow data in registos.Rows)
            {
                UsersPopsModel novo = new UsersPopsModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.id_user = int.Parse (data[1].ToString ());
                novo.id_pop = int.Parse (data[2].ToString ());
                lista.Add (novo);
            }
            return lista;
        }


        public void update (UsersPopsModel novo)
        {
            string sql = @"UPDATE pops_collections SET id_user = @id_user, id_pop=@id_pop WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter () {ParameterName="@id_user", SqlDbType = SqlDbType.Int,Value=novo.id_user},
                new SqlParameter () {ParameterName="@id_pop", SqlDbType = SqlDbType.Int,Value=novo.id_pop},
                new SqlParameter () {ParameterName="@id", SqlDbType = SqlDbType.Int,Value=novo.id},
            };

            DB.Instance.executaComando (sql, parametros);
        }
        //delete
        public void delete (int id)
        {
            string sql = "DELETE FROM pops_collections WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id",
                    SqlDbType =SqlDbType.Int,Value=id}
            };
            DB.Instance.executaComando (sql, parametros);
        }       
    }
}