using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Sistema_clinica.Models.bd
{
    public class ClienteBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr; //DataReader, para ler e salvar info do banco
        List<Cliente> Lista = new List<Cliente>();

        public List<Cliente> ListaClientes()
        {
            cmd.CommandText = "SELECT * FROM cliente";

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente cliente = new Cliente();
                    AtribuirCliente(cliente, dr);
                    Lista.Add(cliente);
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return Lista;
        }


        public Cliente AtribuirCliente(Cliente cliente, MySqlDataReader dr)
        {
            cliente.Id = int.Parse(dr["id"].ToString());
            cliente.Nome = dr["nome"].ToString();
            cliente.Sexo = dr["sexo"].ToString();
            cliente.Cpf = dr["cpf"].ToString();
            cliente.DataNascimento = Convert.ToDateTime(dr["data_nascimento"].ToString());
            cliente.Endereco = dr["endereco"].ToString();
            cliente.Email = dr["email"].ToString();
            cliente.Profissao = dr["profissao"].ToString();
            cliente.Historico = dr["historico"].ToString();

            return cliente;
        }

        public void Cadastrar(Cliente cliente)
        {
            cmd.CommandText = "insert into cliente (nome, sexo, cpf, data_nascimento, endereco, email, profissao, historico) values(@nome, @sexo, @cpf, @data_nascimento, @endereco, @email, @profissao, @historico)";
            cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
            cmd.Parameters.AddWithValue("@cpf", cliente.Cpf);
            cmd.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@email", cliente.Email);
            cmd.Parameters.AddWithValue("@profissao", cliente.Profissao);
            cmd.Parameters.AddWithValue("@historico", cliente.Historico);

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

        public Cliente Buscar(int id)
        {
            Cliente cliente = new Cliente();
            cmd.CommandText = "select * from cliente where ID = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AtribuirCliente(cliente, dr);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return cliente;
        }

        public void Excluir(int id)
        {

            cmd.CommandText = "delete from cliente where ID = @id";
            cmd.Parameters.AddWithValue("@id", id);

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

        public void Editar(Cliente cliente)
        {

            cmd.CommandText = "update cliente set nome = @nome, sexo = @sexo, cpf = @cpf, data_nascimento = @data_nascimento, endereco = @endereco, email = @email, profissao = @profissao, historico = @historico where id = @id";
            cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
            cmd.Parameters.AddWithValue("@cpf", cliente.Cpf);
            cmd.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@email", cliente.Email);
            cmd.Parameters.AddWithValue("@profissao", cliente.Profissao);
            cmd.Parameters.AddWithValue("@historico", cliente.Historico);
            cmd.Parameters.AddWithValue("@id", cliente.Id);

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