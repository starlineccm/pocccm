using System.Linq;
using System.Configuration;
using System.Data.OleDb;
using Dapper;

namespace Marisa.TestDataAccess
{
    class ExcelDataAccess
    {
        public static string TestDataFileConnection()
        {
            //var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
            var fileName = ConfigurationManager.AppSettings[@"C:\MARISA\CCM\CCM\CCM\CCM\MarisaCCM\TestDataAccess\DataVars.cs"];
            //var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", fileName);
            // var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\Users\Starline\source\repos\MarisaOmni\Marisa\Marisa\TestDataAccess.xlsx; Extended Properties=Excel 12.0;", fileName);
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", @"C:\MARISA\CCM\CCM\CCM\CCM\MarisaCCM\TestDataAccess\TestData.xlsx");
            return con;
        }




        ////public static DadosPessoais GetCPF()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [CadastroDeCliente$] where CPF is not null and MensagemRetorno is null");// ORDER BY rnd(CPF)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosPessoais GetDataNascimento()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [CadastroDeCliente$] where CPF is not null ORDER BY rnd(CPF)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}


        ////public static DadosPessoais GetNomeMae()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where NomeMae is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}

        ////public static DadosPessoais GetSexo()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where Sexo is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosPessoais GetEstadoCivil()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where EstadoCivil is not null"); // ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}

        ////public static DadosPessoais GetCelular()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where Celular is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosPessoais GetEmail()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where Email is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}

        ////public static DadosPessoais GetCep()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Cep is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}

        ////public static DadosPessoais SetMesagemRetorno(string MensagemRetorno, string CPF)
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        //var query = string.Format("update [Produtos$] set processada = 'Sim' where Sku = '{0}'", SkuName);
        ////        var query = string.Format("update [CadastroDeCliente$] set MensagemRetorno = '{0}' where CPF = '{1}'", MensagemRetorno, CPF);
        ////       var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}

        //////O Estado é preenchido altomaticamento
        //////public static DadosPessoais GetEstado()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Estado is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}
        //////A Cidade é preenchido altomaticamento
        //////public static DadosPessoais GetCidade()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Cidade is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}

        //////O Bairro é preenchido altomaticamento
        //////public static DadosPessoais GetBairro()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Bairro is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}
        //////A Rua é preenchido altomaticamento
        //////public static DadosPessoais GetRua()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Rua is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}
        ////public static DadosPessoais GetNumero()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where Numero is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosPessoais GetTipoResidencia()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where TipoResidencia is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosPessoais GetTelefoneResidencial()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosPessoais$] where TelefoneResidencial is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosPessoais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosComerciais GetClasseProfissional()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where ClasseProfissional is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosComerciais GetAtividades()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where Atividades is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosComerciais GetRendaMensal()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where RendaMensal  is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}

        ////public static DadosComerciais GetCepComercial()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where Cep is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        //////O Estado é preenchido altomaticamento
        //////public static DadosComerciais GetEstadoComercial()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where Estado is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}

        //////A Cidade é preenchido altomaticamento
        //////public static DadosComerciais GetCidadeComercial()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Cidade is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}

        //////O Bairro é preenchido altomaticamento
        //////public static DadosComerciais GetBairroComercial()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where BairroComerciais is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}
        //////A Rua é preenchido altomaticamento
        //////public static DadosComerciais GetRuaComercial()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosResidencias$] where Rua is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}
        ////public static DadosComerciais GetNumeroComercial()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where Numero is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        ////public static DadosComerciais GetTelefonComercial()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where Telefone is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
        //////public static DadosComerciais GetRamalComercial()
        //////{
        //////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        //////    {
        //////        connection.Open();
        //////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        //////        var query = string.Format("SELECT Top 1 * FROM [DadosComerciais$] where Ramal is not null");// ORDER BY rnd(NomeMae)");
        //////        var value = connection.Query<DadosComerciais>(query).FirstOrDefault();
        //////        connection.Close();
        //////        return value;
        //////    }
        //////}
        ////public static Outros GetVencimentoMelhorDia()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT Top 1 * FROM [Outros$] where VencimentoMelhorDia is not null");// ORDER BY rnd(NomeMae)");
        ////        var value = connection.Query<Outros>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////     }

        ////  }
                
        ////public static SemMensagemRetorno GetSemMensagem()
        ////{
        ////    using (var connection = new OleDbConnection(TestDataFileConnection()))
        ////    {
        ////        connection.Open();
        ////        //var query = string.Format("select * from [DataSet$] where sku='{0}'", SkuName);
        ////        var query = string.Format("SELECT COUNT(CPF) as total FROM [CadastroDeCliente$] where CPF is not null and MensagemRetorno is null");
        ////        var value = connection.Query<SemMensagemRetorno>(query).FirstOrDefault();
        ////        connection.Close();
        ////        return value;
        ////    }
        ////}
    }
 
}



