using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace PopsWeb.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Username is required!")]
        public string username { get; set; }

        [Required (ErrorMessage = "Password is required!")]
        [DataType (DataType.Password)]
        public string pass { get; set; }
    }

    public class LoginDB
    {

        public UsersModel validateLogin (LoginModel login)
        {
            string sql = "SELECT * FROM users WHERE username=@username AND pass=HASHBYTES('SHA2_512',@pass)";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.VarChar,Value=login.username },
                new SqlParameter() {ParameterName="@pass",SqlDbType=SqlDbType.VarChar,Value=login.pass },
            };
            DataTable dados = DB.Instance.devolveConsulta (sql, parametros);
            UsersModel utilizador = null;

            if (dados != null && dados.Rows.Count > 0)
            {
                utilizador = new UsersModel ();
                utilizador.username = dados.Rows[0][1].ToString ();
                utilizador.usertype = int.Parse (dados.Rows[0][4].ToString ());
            }
            return utilizador;
        }
    }
}
