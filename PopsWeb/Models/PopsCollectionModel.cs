using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data;

namespace PopsWeb.Models
{
    public class PopsCollectionModel
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage = "The collection must have a name!")]
        [StringLength (50)]
        public string collection_name { get; set; }

        [Required (ErrorMessage = "The collection must have a Description!")]
        [StringLength (50)]
        public string collection_description { get; set; }
    }

    public class PopsCollectionDB
    {
        public void create (PopsCollectionModel novo)
        {
            string sql = @"INSERT INTO pops_collections(collection_name, collection_description)
                        VALUES (@collection_name, @collection_description)";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@collection_name", SqlDbType = SqlDbType.VarChar,Value=novo.collection_name},
                new SqlParameter(){ParameterName="@collection_description", SqlDbType = SqlDbType.VarChar,Value=novo.collection_description},
            };
            DB.Instance.executaComando (sql, parametros);
        }

        public List<PopsCollectionModel> list ()
        {
            string sql = "SELECT * FROM pops_collections";
            DataTable registos = DB.Instance.devolveConsulta (sql);
            List<PopsCollectionModel> lista = new List<PopsCollectionModel> ();

            foreach (DataRow data in registos.Rows)
            {
                PopsCollectionModel novo = new PopsCollectionModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.collection_name = data[1].ToString ();
                novo.collection_description = data[2].ToString ();
                lista.Add (novo);
            }
            return lista;
        }

        public List<PopsCollectionModel> list (int id)
        {
            string sql = "SELECT * FROM pops_collections where id like @id";

            List<PopsCollectionModel> lista = new List<PopsCollectionModel> ();

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id", SqlDbType =SqlDbType.Int,Value=id},
            };

            DataTable registos = DB.Instance.devolveConsulta (sql, parametros);

            foreach (DataRow data in registos.Rows)
            {
                PopsCollectionModel novo = new PopsCollectionModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.collection_name = data[1].ToString ();
                novo.collection_description = data[2].ToString ();
                lista.Add (novo);
            }
            return lista;
        }


        public void update (PopsCollectionModel novo)
        {
            string sql = @"UPDATE pops_collections SET collection_name = @collection_name, collection_description=@collection_description WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@collection_name", SqlDbType = SqlDbType.VarChar,Value=novo.collection_name},
                new SqlParameter(){ParameterName="@collection_description", SqlDbType = SqlDbType.VarChar,Value=novo.collection_description},
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