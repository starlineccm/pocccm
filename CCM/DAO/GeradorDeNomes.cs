using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// //////////////////////////////////////////////////////////////////////////////////////////
/// ///       ///         //////    ///////       ///  ////////  ///    ///////  ///        //
/// //  ////////////  /////////  //  //////  //// ///  ////////  ///  //  /////  ///  ////////
/// //       ///////  ////////  ////  /////  //  ////  ////////  ///  ////  ///  ///        //
/// ///////  ///////  ///////          ////     /////  ////////  ///  //////  /  ///  ////////
/// //      ////////  //////  ////////  ///  //  ////       ///  ///  ////////   ///        //
/// //////////////////////////////////////////////////////////////////////////////////////////

public class GeradorDeNomes
{
    Connection con = new Connection();

    public GeradorDeNomes()
    {
    }
    public string GerarNome()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("SELECT ALUNOME FROM");
        con.sql.AppendLine("( SELECT ALUNOME FROM alunos");
        con.sql.AppendLine("ORDER BY dbms_random.value )");
        con.sql.AppendLine("WHERE rownum = 1");

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.ALUNOME = con.dr["ALUNOME"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
        return ALUNOME;
    }


    public string ALUNOME { get; set; }
}