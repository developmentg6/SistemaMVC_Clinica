using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_clinica.Models.bd
{
    public class AgendaBD
    {
        public String mensagem = ""; //para salvar mensagens de erro
        MySqlCommand cmd = new MySqlCommand();
        Conexao con = new Conexao();
        MySqlDataReader dr; //DataReader, para ler e salvar info do banco
        List<Agenda> Lista = new List<Agenda>();

        public List<Agenda> ListaAgenda(int id = 0, string sta = null)
        {
            if (id == 0)
            {
                cmd.CommandText = "SELECT * FROM agenda_completa";
            }
            else if (sta != null){
                cmd.CommandText = "SELECT * FROM agenda_completa WHERE id_cliente = @id AND estado = @status";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", sta);
            }

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Agenda agenda = new Agenda();
                    AtribuirAgenda(agenda, dr);
                    Lista.Add(agenda);
                }
                con.Desconectar();

            }
            catch (MySqlException)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!";
            }

            return Lista;
        }

        public Agenda AtribuirAgenda(Agenda agenda, MySqlDataReader dr)
        {
            agenda.Id = int.Parse(dr["id_agenda"].ToString());
            agenda.Data_Hora = DateTime.Parse(dr["data_hora"].ToString());
            agenda.Estado = dr["estado"].ToString();
            agenda.Pago = char.Parse(dr["agend_pago"].ToString());
            agenda.Id_sessao = int.Parse(dr["id_sessao"].ToString());
            agenda.Id_cliente = int.Parse(dr["id_cliente"].ToString());
            agenda.Nome_cliente = dr["cliente"].ToString();
            agenda.Cpf_cliente = dr["cpf"].ToString();
            agenda.Id_procedimento = int.Parse(dr["id_procedimento"].ToString());
            agenda.Nome_procedimento = dr["procedimento"].ToString();
            agenda.Id_funcionario = int.Parse(dr["id_funcionario"].ToString());
            agenda.Nome_funcionario = dr["esteticista"].ToString();

            return agenda;
        }

        public Agenda Buscar(int id)
        {
            Agenda agenda = new Agenda();
            cmd.CommandText = "select * from agenda_completa where id_agenda = @id";
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AtribuirAgenda(agenda, dr);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }

            return agenda;
        }

        public void Cadastrar(Agenda agenda)
        {
            cmd.CommandText = "insert into agenda (data_hora, estado, agend_pago, id_sessao) values(@data_hora, @estado, @agend_pago, @id_sessao)";
            cmd.Parameters.AddWithValue("@data_hora", agenda.Data_Hora);
            cmd.Parameters.AddWithValue("@estado", agenda.Estado);
            cmd.Parameters.AddWithValue("@agend_pago", agenda.Pago);
            cmd.Parameters.AddWithValue("@id_sessao", agenda.Id_sessao);

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


        public void Editar(Agenda agenda)
        {

            cmd.CommandText = "update agenda set data_hora = @data_hora, estado = @estado, agend_pago = @agend_pago, id_sessao = @id_sessao where id_agenda = @id";
            cmd.Parameters.AddWithValue("@data_hora", agenda.Data_Hora);
            cmd.Parameters.AddWithValue("@estado", agenda.Estado);
            cmd.Parameters.AddWithValue("@agend_pago", agenda.Pago);
            cmd.Parameters.AddWithValue("@id_sessao", agenda.Id_sessao);
            cmd.Parameters.AddWithValue("@id", agenda.Id);

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

            cmd.CommandText = "delete from agenda where id_agenda = @id";
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

        public List<Agenda> FiltrarNomeCliente(string nome)
        {
            string nomeAdaptado = '%' + nome + '%';
            cmd.CommandText = "select * from agenda_completa where cliente like @nome";
            cmd.Parameters.AddWithValue("@nome", nomeAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Agenda agenda = new Agenda();
                    AtribuirAgenda(agenda, dr);
                    Lista.Add(agenda);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public List<Agenda> FiltrarCpfCliente(string cpf)
        {
            string cpfAdaptado = '%' + cpf + '%';
            cmd.CommandText = "select * from agenda_completa where cpf like @cpf";
            cmd.Parameters.AddWithValue("@cpf", cpfAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Agenda agenda = new Agenda();
                    AtribuirAgenda(agenda, dr);
                    Lista.Add(agenda);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public List<Agenda> FiltrarProcedimento(string procedimento, int id = 0, string sta = null)
        {
            string procAdaptado = '%' + procedimento + '%';

            if (id == 0)
            {
                cmd.CommandText = "select * from agenda_completa where procedimento like @proc";
                cmd.Parameters.AddWithValue("@proc", procAdaptado);
            }
            else if (sta == "realizado")
            {
                cmd.CommandText = "SELECT * FROM agenda_completa WHERE procedimento like @proc AND id_cliente = @id AND estado = @status";
                cmd.Parameters.AddWithValue("@proc", procAdaptado);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", sta);
            }
            
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Agenda agenda = new Agenda();
                    AtribuirAgenda(agenda, dr);
                    Lista.Add(agenda);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public List<Agenda> FiltrarFuncionario(string funcionario)
        {
            string funcAdaptado = '%' + funcionario + '%';
            cmd.CommandText = "select * from agenda_completa where esteticista like @func";
            cmd.Parameters.AddWithValue("@func", funcAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Agenda agenda = new Agenda();
                    AtribuirAgenda(agenda, dr);
                    Lista.Add(agenda);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public List<Agenda> FiltrarEstado(string estado)
        {
            string estAdaptado = '%' + estado + '%';
            cmd.CommandText = "select * from agenda_completa where estado like @estado";
            cmd.Parameters.AddWithValue("@estado", estAdaptado);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Agenda agenda = new Agenda();
                    AtribuirAgenda(agenda, dr);
                    Lista.Add(agenda);
                }
                con.Desconectar();

            }
            catch (MySqlException ex)
            {
                this.mensagem = "ERRO COM BANCO DE DADOS!" + ex;
            }
            return Lista;
        }

        public bool DataOcupada(Agenda agenda)
        {
            bool existe = false;

            cmd.CommandText = "select * from agenda_completa where data_hora = @data_hora AND id_sessao != @sessao ";
            cmd.Parameters.AddWithValue("@data_hora", agenda.Data_Hora);
            cmd.Parameters.AddWithValue("@sessao", agenda.Id_sessao);

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