using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
/// //////////////////////////////////////////////////////////////////////////////////////////
/// ///       ///         //////    ///////       ///  ////////  ///    ///////  ///        //
/// //  ////////////  /////////  //  //////  //// ///  ////////  ///  //  /////  ///  ////////
/// //       ///////  ////////  ////  /////  //  ////  ////////  ///  ////  ///  ///        //
/// ///////  ///////  ///////          ////     /////  ////////  ///  //////  /  ///  ////////
/// //      ////////  //////  ////////  ///  //  ////       ///  ///  ////////   ///        //
/// //////////////////////////////////////////////////////////////////////////////////////////
public class ScreenShot
{
    private string pasta;
    private string func;

    public ScreenShot(string pasta, string func)
    {
        // TODO: Complete member initialization
        this.pasta = pasta;
        this.func = func;
    }
    public void PrintScreen()
    {
        string wpath = "C:\\Projetos\\CCM\\TestResults\\Prints\\POC\\";


        string folder = wpath + "\\" + pasta; //nome do diretorio a ser criado


        //Se o diretório não existir...

        if (!Directory.Exists(folder))
        {

            //Criamos um com o nome folder
            Directory.CreateDirectory(folder);

        }

        Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        Graphics graphics = Graphics.FromImage(printscreen as Image);
        graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

        string dataDia = DateTime.Now.Date.ToString().Substring(1, 10).Replace("/", "");
        string dataHora = DateTime.Now.ToLongTimeString().ToString().Replace(":", "");
        //printscreen.Save(wpath + "\\" + pasta + "\\" + pasta + func + "-" + dataDia.Trim() + "-" + dataHora.Trim() + ".jpg", ImageFormat.Jpeg);
        printscreen.Save(wpath + "\\" + pasta + "\\" + func + "-" + dataDia.Trim() + "-" + dataHora.Trim() + ".jpg", ImageFormat.Jpeg);

    }


}