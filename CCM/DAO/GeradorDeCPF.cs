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
public class GeradorDeCPF
{
    public GeradorDeCPF() { }
    public string GeraCPF()
        //public void GeraCPF()
    {
        string cpf;
        Random r = new Random();
        int a1 = r.Next(10), a2 = r.Next(10), a3 = r.Next(10),
            a4 = r.Next(10), a5 = r.Next(10), a6 = r.Next(10),
            a7 = r.Next(10), a8 = r.Next(10), a9 = r.Next(10);
        int b1 = a1 * 10,
            b2 = a2 * 9,
            b3 = a3 * 8,
            b4 = a4 * 7,
            b5 = a5 * 6,
            b6 = a6 * 5,
            b7 = a7 * 4,
            b8 = a8 * 3,
            b9 = a9 * 2;
        int DV1 = b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9;
        int T1 = DV1 / 11;
        int T2 = DV1 % 11;// Cálculo do resto da divisão
        int Primeiro_Digito_Verificador;
        if (T2 < 2)
            Primeiro_Digito_Verificador = 0;
        else
            Primeiro_Digito_Verificador = 11 - T2;

        int c1 = a1 * 11,
            c2 = a2 * 10,
            c3 = a3 * 9,
            c4 = a4 * 8,
            c5 = a5 * 7,
            c6 = a6 * 6,
            c7 = a7 * 5,
            c8 = a8 * 4,
            c9 = a9 * 3,
            c10 = Primeiro_Digito_Verificador * 2;
        int DV2 = c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + c10;
        int T3 = DV2 / 11;
        int T4 = DV2 % 11; // Cálculo do resto da divisão
        int Segundo_Digito_Verificador;
        if (T4 < 2)
            Segundo_Digito_Verificador = 0;
        else
            Segundo_Digito_Verificador = 11 - T4;
        cpf = a1.ToString() + a2.ToString() + a3.ToString() + "." +
            a4.ToString() + a5.ToString() + a6.ToString() + "." + a7.ToString() + a8.ToString() + a9.ToString() + "-"
            + Primeiro_Digito_Verificador.ToString() + Segundo_Digito_Verificador.ToString();
        return cpf;
    }
}