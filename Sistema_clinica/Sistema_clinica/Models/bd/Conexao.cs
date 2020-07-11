using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Sistema_clinica.Models.bd
{
    public class Conexao
    {
        MySqlConnection con = new MySqlConnection();

        public Conexao()
        {
            //inserir a string de conexão
            con.ConnectionString = @"server=localhost;user id=niceface;pwd='1234567';database=db_niceface;Connect Timeout=28800;Command Timeout=2880";
        }

        public MySqlConnection Conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public void Desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}