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
            cliente.Id = int.Parse(dr["id_cliente"].ToString());
            cliente.Nome = dr["nome"].ToString();
            cliente.Sexo = dr["sexo"].ToString();
            cliente.Cpf = dr["cpf"].ToString();
            cliente.DataNascimento = Convert.ToDateTime(dr["data_nascimento"].ToString());
            cliente.Rua = dr["rua"].ToString();
            cliente.Numero = int.Parse(dr["numero"].ToString());
            cliente.Bairro = dr["bairro"].ToString();
            cliente.Cidade = dr["cidade"].ToString();
            cliente.Cep = dr["cep"].ToString();
            cliente.Email = dr["email"].ToString();
            cliente.Telefone = dr["telefone"].ToString();
            cliente.Profissao = dr["profissao"].ToString();
            cliente.Historico = dr["historico"].ToString();

            return cliente;
        }

        public void Cadastrar(Cliente cliente)
        {
            cmd.CommandText = "insert into cliente (nome, sexo, cpf, data_nascimento, rua, numero, bairro, cidade, cep, telefone, email, profissao, historico) values(@nome, @sexo, @cpf, @data_nascimento, @rua, @numero, @bairro, @cidade, @cep, @telefone, @email, @profissao, @historico)";
            cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
            cmd.Parameters.AddWithValue("@cpf", cliente.Cpf);
            cmd.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@rua", cliente.Rua);
            cmd.Parameters.AddWithValue("@numero", cliente.Numero);
            cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
            cmd.Parameters.AddWithValue("@cidade", cliente.Cidade);
            cmd.Parameters.AddWithValue("@cep", cliente.Cep);
            cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
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
            cmd.CommandText = "select * from cliente where id_cliente = @id";
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

            cmd.CommandText = "delete from cliente where id_cliente = @id";
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

            cmd.CommandText = "update cliente set nome = @nome, sexo = @sexo, data_nascimento = @data_nascimento, rua = @rua, numero = @numero, bairro = @bairro, cidade = @cidade, cep = @cep, telefone = @telefone, email = @email, profissao = @profissao, historico = @historico where id_cliente = @id";
            cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
            cmd.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
            cmd.Parameters.AddWithValue("@rua", cliente.Rua);
            cmd.Parameters.AddWithValue("@numero", cliente.Numero);
            cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
            cmd.Parameters.AddWithValue("@cidade", cliente.Cidade);
            cmd.Parameters.AddWithValue("@cep", cliente.Cep);
            cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
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

        public List<Cliente> FiltrarNome(string nome)
        {
            string nomeAdaptado = '%' + nome + '%';
            cmd.CommandText = "select * from cliente where nome like @nome";
            cmd.Parameters.AddWithValue("@nome", nomeAdaptado);

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
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }
        
        public List<Cliente> FiltrarCpf(string cpf)
        {
            string cpfAdaptado = '%' + cpf + '%';
            cmd.CommandText = "select * from cliente where cpf like @cpf";
            cmd.Parameters.AddWithValue("@cpf", cpfAdaptado);

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
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public bool ExisteCpf(string cpf)
        {
            Cliente cliente = new Cliente();
            bool existe = false;
            cmd.CommandText = "select * from cliente where cpf = @cpf";
            cmd.Parameters.AddWithValue("@cpf", cpf);

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

    }
}