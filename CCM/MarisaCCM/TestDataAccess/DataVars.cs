using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marisa.TestDataAccess
{
    public class DadosPessoais
    {
        public string CPF { get; set; }
        public string DataNascimento { get; set; }
        public string NomeMae { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string TipoResidencia { get; set; }
        public string TelefoneResidencial { get; set; }
        public string MensagemRetorno { get; set; }
    }
}

//public class DadosResidenciais
//{
//    public string Cep { get; set; }
//    public string Estado { get; set; }
//    public string Cidade { get; set; }
//    public string Bairro { get; set; }
//    public string Rua { get; set; }
//    public string Numero { get; set; }
//    public string TipoResidencia { get; set; }
//    public string TelefoneResidencial { get; set; }
//}

public class DadosComerciais
    {
        public string ClasseProfissional { get; set; }
        public string Atividades { get; set; }
        public string RendaMensal { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string TipoTelefone { get; set; }
        public string TelefoneComercial { get; set; }
        public string Ramal { get; set; }
}


    public class Outros
    {
        public string VencimentoMelhorDia { get; set; }
       // public string DesejaReceberFaturaEmail { get; set; }
        public string ServicoSMS { get; set; }
        public string TarifaOverlimit { get; set; }
        public string ContaBonus { get; set; }
        public string CelularBonus { get; set; }
        public string Campanha { get; set; }

    }
    public class SemMensagemRetorno
{
    public string CPF { get; set; }
    public string DataNascimento { get; set; }
    public string MensagemRetorno { get; set; }
    public int total { get; set; }
}

    public class LoginSite
    {       
        public string Tipo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string UF { get; set; }

    }

    public class ValeTroca
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string VTroca { get; set; }      

    }


    public class Cupons
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Cupom { get; set; }

    }


    public class CVisa
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
        public string Vmes { get; set; }
        public string Vano { get; set; }
        public string Cvv { get; set; }
    }


    public class CMastercard
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
        public string Vmes { get; set; }
        public string Vano { get; set; }
        public string Cvv { get; set; }
    }


    public class CAmex
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
        public string Vmes { get; set; }
        public string Vano { get; set; }
        public string Cvv { get; set; }
    }


    public class CDinners
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
        public string Vmes { get; set; }
        public string Vano { get; set; }
        public string Cvv { get; set; }
    }


    public class CElo
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
        public string Vmes { get; set; }
        public string Vano { get; set; }
        public string Cvv { get; set; }
    }



    public class CCM
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
    }


    public class CCMItau
    {
        public string Ctipo { get; set; }
        public string Cnum { get; set; }
        public string Cnome { get; set; }
        public string Cpf { get; set; }
    }


    public class Devolucao
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string NumPedido { get; set; }
        public string Status { get; set; }
        public string ID { get; set; }
        public string Loja { get; set; }
        
    }





