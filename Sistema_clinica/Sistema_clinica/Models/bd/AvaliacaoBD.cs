using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models.bd
{
    public class AvaliacaoBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr; //DataReader, para ler e salvar info do banco
        List<Avaliacao> Lista = new List<Avaliacao>();

        public List<Avaliacao> ListaAvaliacao(int id = 0)
        {
            if (id == 0)
            {
                cmd.CommandText = "SELECT * FROM avaliacao_completa";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM avaliacao_completa where id_cliente = @id ";
                cmd.Parameters.AddWithValue("@id", id);
            }

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Avaliacao avaliacao = new Avaliacao();
                    AtribuirAvaliacao(avaliacao, dr);
                    Lista.Add(avaliacao);
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return Lista;
        }

        public Avaliacao AtribuirAvaliacao(Avaliacao avaliacao, MySqlDataReader dr)
        {
            avaliacao.Id = int.Parse(dr["id_avaliacao"].ToString());
            avaliacao.DataHora_avaliacao = DateTime.Parse(dr["data_hora"].ToString());
            avaliacao.Id_procedimento = int.Parse(dr["id_procedimento"].ToString());
            avaliacao.Nome_procedimento = dr["procedimento"].ToString();
            avaliacao.Id_cliente = int.Parse(dr["id_cliente"].ToString());
            avaliacao.Nome_cliente = dr["cliente"].ToString();
            avaliacao.Cpf_cliente = dr["cpf"].ToString();
            avaliacao.Obs_cliente = dr["obs_cliente"].ToString();

            return avaliacao;
        }

        public Avaliacao Buscar(int id)
        {
            Avaliacao avaliacao = new Avaliacao();
            cmd.CommandText = "select * from avaliacao_completa where id_avaliacao = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AtribuirAvaliacao(avaliacao, dr);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return avaliacao;
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

        public void Cadastrar(Avaliacao avaliacao)
        {
            cmd.CommandText = "insert into avaliacao_diagnostica (data_hora, obs_cliente, id_procedimento, id_cliente) values(@data_hora, @obs_cliente, @id_procedimento, @id_cliente)";
            cmd.Parameters.AddWithValue("@data_hora", avaliacao.DataHora_avaliacao);
            cmd.Parameters.AddWithValue("@obs_cliente", avaliacao.Obs_cliente);
            cmd.Parameters.AddWithValue("@id_procedimento", avaliacao.Id_procedimento);
            cmd.Parameters.AddWithValue("@id_cliente", avaliacao.Id_cliente);


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


        public void Editar(Avaliacao avaliacao)
        {

            cmd.CommandText = "update avaliacao_diagnostica set data_hora = @data_hora, obs_cliente = @obs_cliente, id_procedimento = @id_procedimento, id_cliente = @id_cliente where id_avaliacao = @id";
            cmd.Parameters.AddWithValue("@data_hora", avaliacao.DataHora_avaliacao);
            cmd.Parameters.AddWithValue("@obs_cliente", avaliacao.Obs_cliente);
            cmd.Parameters.AddWithValue("@id_procedimento", avaliacao.Id_procedimento);
            cmd.Parameters.AddWithValue("@id_cliente", avaliacao.Id_cliente);
            cmd.Parameters.AddWithValue("@id", avaliacao.Id);

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

            cmd.CommandText = "delete from avaliacao_diagnostica where id_avaliacao = @id";
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