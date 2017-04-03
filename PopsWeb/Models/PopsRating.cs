using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;

namespace PopsWeb.Models
{
    public class PopsRating
    {
        [Key]
        public int id { get; set; }
        public int id_user { get; set; }
        public int id_pop { get; set; }

        [MaxLength (5)]
        [MinLength (1)]
        public int rating_pos { get; set; } //Suppose to be pop but typo

        public string username;
        public string pop_name;
    }

    public class PopsRatingDB
    {
        public void addRating (PopsRating novo)
        {
            string sql = @"INSERT INTO pops_rating(id_user, id_pop, rating_pos)
                        VALUES (@id_user, @id_pop, @rating_pop)";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id_user", SqlDbType = SqlDbType.Int,Value=novo.id_user},
                new SqlParameter(){ParameterName="@id_pop", SqlDbType = SqlDbType.Int,Value=novo.id_pop},
                new SqlParameter(){ParameterName="@rating_pop", SqlDbType = SqlDbType.Int,Value=novo.rating_pos},
            };
            DB.Instance.executaComando (sql, parametros);
        }

        public List<PopsRating> list ()
        {
            string sql = "select id, id_user, id_pop, rating_pos, users.username, pops.pop_name from users inner join pops_rating on users.id = pops_rating.id_user inner join pops on pops_rating.id_pop = pops.id";

            DataTable registos = DB.Instance.devolveConsulta (sql);
            List<PopsRating> lista = new List<PopsRating> ();

            foreach (DataRow data in registos.Rows)
            {
                PopsRating novo = new PopsRating ();
                novo.id = int.Parse (data[0].ToString ());
                novo.id_user = int.Parse (data[1].ToString ());
                novo.id_pop = int.Parse (data[2].ToString ());
                novo.rating_pos = int.Parse (data[3].ToString ());
                novo.username = data[4].ToString ();
                novo.pop_name = data[5].ToString ();
                lista.Add (novo);
            }
            return lista;
        }

        public float avg_rating (int pop_id)
        {
            string sql = "select avg(rating_pos) as rating from pops_rating where id_pop = @pop_id";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@pop_id", SqlDbType = SqlDbType.Int,Value=pop_id},
            };

            DataTable registos = DB.Instance.devolveConsulta (sql, parametros);
            List<PopsRating> lista = new List<PopsRating> ();

            foreach (DataRow data in registos.Rows)
                return float.Parse (data[0].ToString ());
            return 0.0f;
        }

        public int create (PopsModel novo)
        {
            string sql = @"INSERT INTO pops(pop_name, pop_description, pop_collection_id, price)
                        VALUES (@pop_name,@pop_description,@pop_collection_id,@price);SELECT cast(scope_identity() as int);";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@pop_name", SqlDbType = SqlDbType.VarChar,Value=novo.pop_name},
                new SqlParameter(){ParameterName="@pop_description", SqlDbType = SqlDbType.VarChar,Value=novo.pop_description},
                new SqlParameter(){ParameterName="@pop_collection_id", SqlDbType = SqlDbType.VarChar,Value=novo.pop_collection_id},
                 new SqlParameter(){ParameterName="@price", SqlDbType = SqlDbType.Int,Value=novo.price},
            };
            return (int)DB.Instance.executaScalar (sql, parametros);
        }
    }
}