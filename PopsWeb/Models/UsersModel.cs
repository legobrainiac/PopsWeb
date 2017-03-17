using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace PopsWeb.Models
{
    public class UsersModel
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage = "You must have a username!")]
        [StringLength (50)]
        [MinLength (2, ErrorMessage = "Username is too short, something common?")]
        public string username { get; set; }

        [Required (ErrorMessage = "You must have an email!")]
        [StringLength (50)]
        [MinLength (2, ErrorMessage = "Email is too short, something common?")]
        public string email { get; set; }

        [Display (Name = "Password")]
        [MinLength (5, ErrorMessage = "Password is too short, something common?")]
        [DataType (DataType.Password)]
        public string pass { get; set; }

        [DataType (DataType.Password)]
        [Display (Name = "Confirm you're password!")]
        [Compare ("pass", ErrorMessage = "Passwords do not match")]
        public string comfirmPassword { get; set; }

        public int usertype { get; set; }
    }

    public class UsersDB
    {
        public void adicionarUtilizadores (UsersModel novo)
        {
            string sql = @"INSERT INTO users(username, email,pass,usertype)
                        VALUES (@username,@email ,HASHBYTES('SHA2_512',@pass),@usertype)";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@username", SqlDbType = SqlDbType.VarChar,Value=novo.username},
                new SqlParameter(){ParameterName="@email", SqlDbType = SqlDbType.VarChar,Value=novo.email},
                new SqlParameter(){ParameterName="@pass", SqlDbType = SqlDbType.VarChar,Value=novo.pass},
                 new SqlParameter(){ParameterName="@usertype", SqlDbType = SqlDbType.Int,Value=novo.usertype},
            };
            DB.Instance.executaComando (sql, parametros);
        }

        public List<UsersModel> list ()
        {
            string sql = "SELECT * FROM users";
            DataTable registos = DB.Instance.devolveConsulta (sql);
            List<UsersModel> lista = new List<UsersModel> ();
            foreach (DataRow data in registos.Rows)
            {
                UsersModel novo = new UsersModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.username = data[1].ToString ();
                novo.email = data[3].ToString ();
                novo.pass = data[2].ToString ();
                novo.usertype = int.Parse (data[4].ToString ());
                lista.Add (novo);
            }
            return lista;
        }

        public List<UsersModel> list (int id)
        {
            string sql = "SELECT * FROM users where id like @id";

            List<UsersModel> lista = new List<UsersModel> ();

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id", SqlDbType =SqlDbType.Int,Value=id},
            };

            DataTable registos = DB.Instance.devolveConsulta (sql, parametros);

            foreach (DataRow data in registos.Rows)
            {
                UsersModel novo = new UsersModel ();
                novo.id = int.Parse (data[0].ToString ());
                novo.username = data[1].ToString ();
                novo.email = data[3].ToString ();
                novo.pass = data[2].ToString ();
                novo.usertype = int.Parse (data[4].ToString ());
                lista.Add (novo);
            }
            return lista;
        }


        public void update (UsersModel novo)
        {
            string sql = @"UPDATE users SET username = @username, usertype=@usertype, email = @email WHERE id=@id";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter () {ParameterName="@username", SqlDbType = SqlDbType.VarChar,Value=novo.username},
                new SqlParameter () {ParameterName="@email", SqlDbType = SqlDbType.VarChar,Value=novo.email},
                new SqlParameter () {ParameterName="@usertype", SqlDbType = SqlDbType.Int,Value=novo.usertype},
                new SqlParameter () {ParameterName="@id", SqlDbType = SqlDbType.Int,Value=novo.id},
            };

            DB.Instance.executaComando (sql, parametros);
        }
        //delete
        public void delete (int id)
        {
            string sql = "DELETE FROM users WHERE id=@id";
            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter(){ParameterName="@id",
                    SqlDbType =SqlDbType.Int,Value=id}
            };
            DB.Instance.executaComando (sql, parametros);
        }

        public void create (UsersModel novo)
        {
            string sql = @"insert into users(username, pass, email, usertype) values(@users,HASHBYTES('SHA2_512',@pass), @email, @usertype);";

            List<SqlParameter> parametros = new List<SqlParameter> ()
            {
                new SqlParameter () {ParameterName="@username", SqlDbType = SqlDbType.VarChar,Value=novo.username},
                new SqlParameter () {ParameterName="@pass", SqlDbType = SqlDbType.VarChar,Value=novo.email},
                new SqlParameter () {ParameterName="@email", SqlDbType = SqlDbType.VarChar,Value=novo.email},
                new SqlParameter () {ParameterName="@usertype", SqlDbType = SqlDbType.Int,Value=novo.usertype},
            };

            DB.Instance.executaComando (sql, parametros);
        }
    }
}