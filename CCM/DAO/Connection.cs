using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;

/// //////////////////////////////////////////////////////////////////////////////////////////
/// ///       ///         //////    ///////       ///  ////////  ///    ///////  ///        //
/// //  ////////////  /////////  //  //////  //// ///  ////////  ///  //  /////  ///  ////////
/// //       ///////  ////////  ////  /////  //  ////  ////////  ///  ////  ///  ///        //
/// ///////  ///////  ///////          ////     /////  ////////  ///  //////  /  ///  ////////
/// //      ////////  //////  ////////  ///  //  ////       ///  ///  ////////   ///        //
/// //////////////////////////////////////////////////////////////////////////////////////////

public class Connection
{
    //string conexao = @"Data Source=BDPRODTS_X4;Persist Security Info=True;User ID=RAFAELAMORETTI;Password=teste;Unicode=True";
    //string conexao = @"Data Source=BD2FUNC05;Persist Security Info=True;User ID=RAFAELAMORETTI;Password=teste;Unicode=True";
    string conexao = @"Data Source=CCM_HOMOLOG;Persist Security Info=True;User ID=STARLINE;Password=Central_Starline18;Unicode=True";

    private OracleConnection conn;

    public StringBuilder sql = new StringBuilder();

    public List<OracleParameter> listaParametros = new List<OracleParameter>();

    public OracleDataReader dr;

    public OracleCommand cmd;


    public Connection()
    {
        this.conn = new OracleConnection(conexao);
    }

    public void Close()
    {
        if (this.conn.State != ConnectionState.Closed)
        {
            this.conn.Close();
        }
    }

    public void clear()
    {
        this.sql.Clear();
        this.listaParametros.Clear();
    }

    public void carregaDataReader()
    {
        try
        {
            this.open();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = this.sql.ToString();
            for (int i = 0; i < this.listaParametros.Count; i++)
            {
                this.cmd.Parameters.Add(this.listaParametros[i]);
            }
            this.dr = this.cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }

    }

    public OracleConnection open()
    {
        if (this.conn.State == ConnectionState.Closed)
        {
            try
            {
                this.conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
        return this.conn;
    }

    public void executa()
    {
        try
        {
            this.open();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }

        this.cmd = this.conn.CreateCommand();
        this.cmd.CommandText = this.sql.ToString();
        for (int i = 0; i < this.listaParametros.Count; i++)
        {
            this.cmd.Parameters.Add(listaParametros[i]);
        }

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }
    }
}