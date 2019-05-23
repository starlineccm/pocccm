using System;
using System.Collections.Generic;
using System.IO;
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
public class Incrementador
{

    public string IncrementadorTitulo()
    {
        // string path = "C:\\Kroton\\QA\\Robo-Concurso\\Concurso\\global.txt";

        string path = "C:\\Kroton\\Regressao2015\\TestesAutomatizadosOlimpo\\features\\AtualizaRosaliJustino\\Robo-Concurso\\Concurso\\global.txt";

        StreamReader sr = new StreamReader(path);
        string var = sr.ReadLine();

        string titulo = var.Substring(0, 24);
        int cont = int.Parse(var.Substring(24).Trim());
        cont = cont + 1;
        sr.Close();

        StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII);
        sw.WriteLine(titulo + " " + cont.ToString());
        sw.Close();

        StreamReader sr2 = new StreamReader(path, Encoding.ASCII);
        titulo = sr2.ReadLine();
        sr2.Close();

        return titulo;

    }

}