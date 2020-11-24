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

        public bool verificarLogin(Usuario usuario)
        {
            cmd.CommandText = "call buscar_usuario_senha(@usuario, @senha, @tipo)";
            cmd.Parameters.AddWithValue("@usuario", usuario.Login);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@tipo", usuario.Tipo);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {
                    loginValido = true;
                    usuario.Nivel = int.Parse(dr["nivel_acesso"].ToString());
                    usuario.Id = int.Parse(dr[1].ToString());
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return loginValido; //retorna o bool dizendo se pode entrar (True) ou não (False)
        }

        public void AlterarSenha(Usuario usuario)
        {
            cmd.CommandText = "call atualizar_senha(@usuario, @senhaAntiga, @senhaNova)";
            cmd.Parameters.AddWithValue("@usuario", usuario.Login);
            cmd.Parameters.AddWithValue("@senhaAntiga", usuario.Senha);
            cmd.Parameters.AddWithValue("@senhaNova", usuario.NovaSenha);

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();                
                con.Desconectar();
            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }
        }

        public bool ExisteUsuario(string usuario)
        {
            bool existe = false;
            cmd.CommandText = "call buscar_usuario(@usuario)";
            cmd.Parameters.AddWithValue("@usuario", usuario);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    existe = true;
                }
                con.Desconectar();
            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return existe;
        }

        public void CadastrarLogin(ConfirmaCliente cliente)
        {
            cmd.CommandText = "call cad_usuario_cliente(@id, @usuario, @senha)";
            cmd.Parameters.AddWithValue("@id", cliente.Id);
            cmd.Parameters.AddWithValue("@usuario", cliente.Usuario);
            cmd.Parameters.AddWithValue("@senha", cliente.Senha);

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();
            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
        }
    }
}