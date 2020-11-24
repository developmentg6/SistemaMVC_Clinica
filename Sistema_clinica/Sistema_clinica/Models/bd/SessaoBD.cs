using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models.bd
{
    public class SessaoBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr; //DataReader, para ler e salvar info do banco
        List<Sessao> Lista = new List<Sessao>();

        public List<Sessao> ListaSessao(int id = 0)
        {
            if (id != 0)
            {
                cmd.CommandText = "SELECT * FROM sessao_completa WHERE id_cliente = @id AND (agendadas < quantidade)";
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                cmd.CommandText = "SELECT * FROM sessao_completa";
            }
            
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sessao sessao = new Sessao();
                    AtribuirSessao(sessao, dr);
                    Lista.Add(sessao);
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return Lista;
        }

        public Sessao AtribuirSessao(Sessao sessao, MySqlDataReader dr)
        {
            sessao.Id = int.Parse(dr["id_sessao"].ToString());
            sessao.Id_cliente = int.Parse(dr["id_cliente"].ToString());
            sessao.Nome_cliente = dr["cliente"].ToString();
            sessao.Cpf_cliente = dr["cpf"].ToString();
            sessao.Id_procedimento = int.Parse(dr["id_procedimento"].ToString());
            sessao.Nome_procedimento = dr["procedimento"].ToString();
            sessao.Id_funcionario = int.Parse(dr["id_funcionario"].ToString());
            sessao.Nome_funcionario = dr["esteticista"].ToString();
            sessao.Descricao = dr["descricao"].ToString();
            sessao.Quantidade = int.Parse(dr["quantidade"].ToString());

            sessao.Sessoes_agendadas = int.Parse(dr["agendadas"].ToString());

            return sessao;
        }

        public Sessao Buscar(int id)
        {
            Sessao sessao = new Sessao();
            cmd.CommandText = "select * from sessao_completa where id_sessao = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AtribuirSessao(sessao, dr);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return sessao;
        }

        public int BuscarClientePeloCpf(string cpf)
        {
            int id = 0;
            cmd.CommandText = "select id_cliente from cliente where cpf = @cpf";
            cmd.Parameters.AddWithValue("@cpf", cpf);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id = int.Parse(dr["id_cliente"].ToString());
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return id;
        }

        public void Cadastrar(Sessao sessao)
        {
            cmd.CommandText = "insert into sessao (id_cliente, id_procedimento, id_funcionario, descricao, quantidade) values(@id_cliente, @id_procedimento, @id_funcionario, @descricao, @quantidade)";
            cmd.Parameters.AddWithValue("@id_cliente", sessao.Id_cliente);
            cmd.Parameters.AddWithValue("@id_procedimento", sessao.Id_procedimento);
            cmd.Parameters.AddWithValue("@id_funcionario", sessao.Id_funcionario);
            cmd.Parameters.AddWithValue("@descricao", sessao.Descricao);
            cmd.Parameters.AddWithValue("@quantidade", sessao.Quantidade);


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


        public void Editar(Sessao sessao)
        {

            cmd.CommandText = "update sessao set id_cliente = @id_cliente, id_procedimento = @id_procedimento, id_funcionario = @id_funcionario, descricao = @descricao, quantidade = @quantidade where id_sessao = @id";
            cmd.Parameters.AddWithValue("@id_cliente", sessao.Id_cliente);
            cmd.Parameters.AddWithValue("@id_procedimento", sessao.Id_procedimento);
            cmd.Parameters.AddWithValue("@id_funcionario", sessao.Id_funcionario);
            cmd.Parameters.AddWithValue("@descricao", sessao.Descricao);
            cmd.Parameters.AddWithValue("@quantidade", sessao.Quantidade);
            cmd.Parameters.AddWithValue("@id", sessao.Id);

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

        public void Excluir(int id)
        {

            cmd.CommandText = "delete from sessao where id_sessao = @id";
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

    }
}