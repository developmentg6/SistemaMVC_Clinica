using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models.bd
{
    public class UsuarioBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr;
        bool loginValido = false;

        public bool verificarLogin(String usuario, String senha)
        {
            cmd.CommandText = "select * from login where usuario = @usuario and senha = @senha";
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@senha", senha);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) //se tiver alguma linha, ou seja, se tiver encontrado uma combinação (tem no banco)
                {
                    loginValido = true;
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return loginValido; //retorna o bool dizendo se pode entrar (True) ou não (False)
        }

    }
}