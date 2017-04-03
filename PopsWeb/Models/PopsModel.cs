using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace PopsWeb.Models
{
    public class PopsModel
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage = "The figurine must have a name!")]
        [StringLength (50)]
        public string pop_name { get; set; }

        [Required (ErrorMessage = "The figurine must have a Description!")]
        [StringLength (50)]
        public string pop_description { get; set; }

        public int? pop_collection_id { get; set; }

        public string pop_collection_name { get; set; }

        [DataType (DataType.Currency)]
        public decimal price { get; set; }
    }

    public class PopsDB
    {
        public void adicionarPops (PopsModel novo)
        {
            string sql = @"INSERT INTO pops(pop_name, pop_description, pop_collection_id, price)
                        VALUES (@pop_name,@pop_description,pop_collection,@price)";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@pop_name", SqlDbType = SqlDbType.VarChar,Value=novo.pop_name},
                new SqlParameter(){ParameterName="@email", SqlDbType = SqlDbType.VarChar,Value=novo.pop_description},
                new SqlParameter(){ParameterName="@pop_collection_id", SqlDbType = SqlDbType.VarChar,Value=novo.pop_collection_id},
                 new SqlParameter(){ParameterName="@price", SqlDbType = SqlDbType.Int,Value=novo.price},
            };
            DB.Instance.executaComando (sql, parametros);
        }

        public List<PopsModel> list ()
        {
            string sql = "SELECT pops.id, pop_name, pop_description, pop_collection_id, price, pops_collections.collection_name FROM pops inner join pops_collections on pops.pop_collection_id = pops_collections.id";

            DataTable registos = DB.Instance.devolveConsulta (sql);
            List<PopsModel> lista = new List<PopsModel> ();
            foreach (DataRow data in registos.Rows)
            {
                PopsModel novo = new PopsModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.pop_name = data[1].ToString ();
                novo.pop_description = data[2].ToString ();
                novo.pop_collection_id = int.Parse (data[3].ToString ());
                novo.price = decimal.Parse (data[4].ToString ());
                novo.pop_collection_name = data[5].ToString ();
                lista.Add (novo);
            }
            return lista;
        }

        public List<PopsModel> list_groupby_collection ()
        {
            string sql = "SELECT * FROM pops group by pop_collection_id";
            DataTable registos = DB.Instance.devolveConsulta (sql);
            List<PopsModel> lista = new List<PopsModel> ();
            foreach (DataRow data in registos.Rows)
            {
                PopsModel novo = new PopsModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.pop_name = data[1].ToString ();
                novo.pop_description = data[2].ToString ();
                novo.pop_collection_id = int.Parse (data[3].ToString ());
                novo.price = decimal.Parse (data[4].ToString ());
                lista.Add (novo);
            }
            return lista;
        }

        public List<PopsModel> list (int id)
        {
            string sql = "SELECT pops.id, pop_name, pop_description, pop_collection_id, price, pops_collections.collection_name FROM pops inner join pops_collections on pops.pop_collection_id = pops_collections.id  where pops.id like @id";

            List<PopsModel> lista = new List<PopsModel> ();

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id", SqlDbType =SqlDbType.Int,Value=id},
            };

            DataTable registos = DB.Instance.devolveConsulta (sql, parametros);

            foreach (DataRow data in registos.Rows)
            {
                PopsModel novo = new PopsModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.pop_name = data[1].ToString ();
                novo.pop_description = data[2].ToString ();
                novo.pop_collection_id = int.Parse (data[3].ToString ());
                novo.price = decimal.Parse (data[4].ToString ());
                novo.pop_collection_name = data[5].ToString ();
                lista.Add (novo);
            }
            return lista;
        }


        public void update (PopsModel novo)
        {
            string sql = @"UPDATE pops SET pop_name = @pop_name, pop_description=@pop_description, pop_collection_id = @pop_collection_id, price=@price WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@pop_name", SqlDbType = SqlDbType.VarChar,Value=novo.pop_name},
                new SqlParameter(){ParameterName="@pop_description", SqlDbType = SqlDbType.VarChar,Value=novo.pop_description},
                new SqlParameter(){ParameterName="@pop_collection_id", SqlDbType = SqlDbType.VarChar,Value=novo.pop_collection_id},
                new SqlParameter(){ParameterName="@price", SqlDbType = SqlDbType.Int,Value=novo.price},
                new SqlParameter () {ParameterName="@id", SqlDbType = SqlDbType.Int,Value=novo.id},
            };
            DB.Instance.executaComando (sql, parametros);
        }
        //delete
        public void delete (int id)
        {
            string sql = "DELETE FROM pops WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id",
                    SqlDbType =SqlDbType.Int,Value=id}
            };
            DB.Instance.executaComando (sql, parametros);
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