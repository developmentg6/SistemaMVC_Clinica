using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models.bd
{
    public class FuncionarioBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr; //DataReader, para ler e salvar info do banco
        List<Funcionario> Lista = new List<Funcionario>();

        public List<Funcionario> ListaFuncionarios()
        {
            cmd.CommandText = "SELECT * FROM funcionario";

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Funcionario func = new Funcionario();
                    AtribuirFuncionario(func, dr);
                    Lista.Add(func);
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return Lista;
        }

        public Funcionario AtribuirFuncionario(Funcionario funcionario, MySqlDataReader dr)
        {
            funcionario.IdFuncionario = int.Parse(dr["id"].ToString());
            funcionario.Nome = dr["nome"].ToString();
            funcionario.Cargo = dr["cargo"].ToString();
            funcionario.Cpf = dr["cpf"].ToString();
            funcionario.Endereco = dr["endereco"].ToString();
            funcionario.Email = dr["email"].ToString();
            funcionario.Telefone = dr["telefone"].ToString();

            return funcionario;
        }

        public void Cadastrar(Funcionario funcionario)
        {
            cmd.CommandText = "insert into funcionario (nome, cargo, cpf, endereco, email, telefone) values(@nome, @cargo, @cpf, @endereco, @email, @telefone)";
            cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
            cmd.Parameters.AddWithValue("@cpf", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco);
            cmd.Parameters.AddWithValue("@email", funcionario.Email);
            cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);

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

        public Funcionario Buscar(int id)
        {
            Funcionario func = new Funcionario();
            cmd.CommandText = "select * from funcionario where id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AtribuirFuncionario(func, dr);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return func;
        }

        public void Excluir(int id)
        {

            cmd.CommandText = "delete from funcionario where id = @id";
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

        public void Editar(Funcionario funcionario)
        {

            cmd.CommandText = "update cliente set nome = @nome, cargo = @cargo, cpf = @cpf, endereco = @endereco, telefone = @telefone, email = @email where id = @id";
            cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
            cmd.Parameters.AddWithValue("@cpf", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco);
            cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
            cmd.Parameters.AddWithValue("@email", funcionario.Email);
            cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);

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

        public List<Funcionario> FiltrarNome(string nome)
        {
            string nomeAdaptado = '%' + nome + '%';
            cmd.CommandText = "select * from funcionario where nome like @nome";
            cmd.Parameters.AddWithValue("@nome", nomeAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Funcionario func = new Funcionario();
                    AtribuirFuncionario(func, dr);
                    Lista.Add(func);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public List<Funcionario> FiltrarCpf(string cpf)
        {
            string cpfAdaptado = '%' + cpf + '%';
            cmd.CommandText = "select * from funcionario where cpf like @cpf";
            cmd.Parameters.AddWithValue("@cpf", cpfAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Funcionario func = new Funcionario();
                    AtribuirFuncionario(func, dr);
                    Lista.Add(func);
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
            Funcionario func = new Funcionario();
            bool existe = false;
            cmd.CommandText = "select * from funcionario where cpf = @cpf";
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