using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models.bd
{
    public class ProcedimentoBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr; //DataReader, para ler e salvar info do banco
        List<Procedimento> Lista = new List<Procedimento>();

        public List<Procedimento> ListaProcedimentos()
        {
            cmd.CommandText = "SELECT * FROM procedimento";

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Procedimento proc = new Procedimento();
                    AtribuirProcedimento(proc, dr);
                    Lista.Add(proc);
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return Lista;
        }

        public Procedimento AtribuirProcedimento(Procedimento procedimento, MySqlDataReader dr)
        {
            procedimento.Id = int.Parse(dr["id_procedimento"].ToString());
            procedimento.Nome = dr["nome"].ToString();
            procedimento.Descricao = dr["descricao"].ToString();
            procedimento.Tempo_estimado = dr["tempo_estimado"].ToString();
            procedimento.Valor_proc = float.Parse(dr["valor_proc"].ToString());

            return procedimento;
        }

        public void Cadastrar(Procedimento procedimento)
        {
            cmd.CommandText = "insert into procedimento (nome, descricao, tempo_estimado, valor_proc) values(@nome, @descricao, @tempo_estimado, @valor_proc)";
            cmd.Parameters.AddWithValue("@nome", procedimento.Nome);
            cmd.Parameters.AddWithValue("@descricao", procedimento.Descricao);
            cmd.Parameters.AddWithValue("@tempo_estimado", procedimento.Tempo_estimado);
            cmd.Parameters.AddWithValue("@valor_proc", procedimento.Valor_proc);

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

        public Procedimento Buscar(int id)
        {
            Procedimento proc = new Procedimento();
            cmd.CommandText = "select * from procedimento where id_procedimento = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AtribuirProcedimento(proc, dr);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return proc;
        }

        public void Excluir(int id)
        {

            cmd.CommandText = "delete from procedimento where id_procedimento = @id";
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

        public void Editar(Procedimento procedimento)
        {

            cmd.CommandText = "update procedimento set nome = @nome, descricao = @descricao, tempo_estimado = @tempo_estimado, valor_proc = @valor_proc where id_procedimento = @id";
            cmd.Parameters.AddWithValue("@nome", procedimento.Nome);
            cmd.Parameters.AddWithValue("@descricao", procedimento.Descricao);
            cmd.Parameters.AddWithValue("@tempo_estimado", procedimento.Tempo_estimado);
            cmd.Parameters.AddWithValue("@valor_proc", procedimento.Valor_proc);
            cmd.Parameters.AddWithValue("@id", procedimento.Id);

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

        public List<Procedimento> FiltrarNome(string nome)
        {
            string nomeAdaptado = '%' + nome + '%';
            cmd.CommandText = "select * from procedimento where nome like @nome";
            cmd.Parameters.AddWithValue("@nome", nomeAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Procedimento proc = new Procedimento();
                    AtribuirProcedimento(proc, dr);
                    Lista.Add(proc);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public bool ExisteNome(string nome_procedimento)
        {
            Procedimento proc = new Procedimento();
            bool existe = false;
            cmd.CommandText = "select * from procedimento where nome = @nome";
            cmd.Parameters.AddWithValue("@nome", nome_procedimento);

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