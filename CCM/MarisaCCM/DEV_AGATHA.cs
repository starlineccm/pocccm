using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Microsoft.CSharp;
using System.Windows.Input;
using System.Windows.Forms;
using Starline;

//using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace FluxoVendaCartoes
{
    [TestFixture]
    public class DEV_AGATHA
    {
        private const string login = "TI_SANDRA";
        private const string senha = "Teste@1234";
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;



        [SetUp]
        public void SetupTest()
        {


            var options = new ChromeOptions();
            // options.AddArguments("--headless");
            options.AddArgument("start-maximized");
            options.EnableMobileEmulation("iPhone 6/7/8 Plus");
            options.AddArgument("window-size=414,736");
            //using (var browser = new ChromeDriver(options))
            driver = new ChromeDriver(options);


            ////options = new ChromeOptions();
            //options.AddArgument("start-maximized");
            //options.EnableMobileEmulation("iPhone 6/7/8 Plus");
            //options.AddArgument("window-size=414,736");

            //driver = new ChromeDriver(options);
            //driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Window.Maximize();
            baseURL = "https://ccmhomolog.marisa.com.br/psfsecurity/paginas/security/login/tela/login.html";
            verificationErrors = new StringBuilder();


            // driver = new ChromeDriver();
            // //options = new ChromeOptions();
            // options.AddArgument("start-maximized");
            // options.EnableMobileEmulation("iPhone 6/7/8 Plus");
            // options.AddArgument("window-size=414,736");
            //// driver = new ChromeDriver(options);
            // driver.Manage().Cookies.DeleteAllCookies();
            // driver.Manage().Window.Maximize();


            // baseURL = "http://10.10.41.171/psfsecurity/paginas/security/login/tela/login.html";
            // verificationErrors = new StringBuilder();
        }

        public void wait(By elemento)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
        }


        //{
        //public void KeyEventArgs(System.Windows.Input.KeyboardDevice keyboard, System.Windows.PresentationSource inputSource, int timestamp, System.Windows.Input.Key key);
        // }

        public void Login()//métodos
        { //Validar Seção de Login



            //Efetuar Login
            driver.FindElement(By.Id("login_username")).Clear();
            driver.FindElement(By.Id("login_username")).SendKeys(login);
            driver.FindElement(By.Id("login_password")).Clear();
            driver.FindElement(By.Id("login_password")).SendKeys(senha);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(2000);

            wait(By.Id("text-login-msg"));

        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void CT001_Cadastro_de_Clientes_Aposentados()
        {

            ////Parametros 
            //var Cadastro = Marisa.TestDataAccess.ExcelDataAccess.GetCPF();

            //var Pessoal = Marisa.TestDataAccess.ExcelDataAccess.GetEstadoCivil();

            //var Comercial = Marisa.TestDataAccess.ExcelDataAccess.GetCepComercial();

            //var Outros = Marisa.TestDataAccess.ExcelDataAccess.GetVencimentoMelhorDia();

            //var SemMensagem = Marisa.TestDataAccess.ExcelDataAccess.GetSemMensagem();

            ClsExtranet cls = new ClsExtranet();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            cls.Consulta_CPF();

            //Método de Print
            ScreenShot print = new ScreenShot("VendaCartoes", "CT001_Cadastro_de_Clientes");


            //Execução

            //Acessar Página do CCM
            driver.Navigate().GoToUrl(baseURL);


            wait(By.XPath("//button[@type='submit']"));
            wait(By.CssSelector("img.png_bg"));
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Login", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.IsTrue(IsElementPresent(By.Id("login_username")));
            Assert.IsTrue(IsElementPresent(By.Id("login_password")));
            Assert.IsTrue(IsElementPresent(By.XPath("//button[@type='submit']")));



            //Validar Tela de Login
            Assert.AreEqual("PSF Security", driver.Title);
            Assert.IsTrue(IsElementPresent(By.CssSelector("img.png_bg")));
            try
            {
                Assert.AreEqual("Portal Lojas", driver.FindElement(By.Id("tituloPagina")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            print.PrintScreen();

            //Realizar login
            Login();


            wait(By.Id("btnSearch"));
            print.PrintScreen();


            ////Alterar Filial
            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("labelFilial")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("changeFilial")).SendKeys("54 - L 054 JD / SP");
            Thread.Sleep(2000);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(2000);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(5000);


            //Validar Página Inicial
            Assert.AreEqual("PSF Security", driver.Title);

            try
            {
                Assert.AreEqual("Buscar por:\r\nCPF\r\nCARTÃO", driver.FindElement(By.Id("tipoSearch")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.IsTrue(IsElementPresent(By.Id("cpfSearch")));
            Assert.IsTrue(IsElementPresent(By.Id("btnSearch")));
            try
            {
                Assert.AreEqual("Concessao do Cartao", driver.FindElement(By.Id("menuCollapse3162")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Atendimento", driver.FindElement(By.Id("menuCollapse3164")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Cenario 1 - Cadastro de Clientes

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("26455022882");
            Thread.Sleep(2000);
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();
            Thread.Sleep(2000);



            wait(By.Id("iniciarCadastro"));


            //Validar Página Cadastro de Clientes


            Assert.IsTrue(IsElementPresent(By.Id("formPreCadastro")));

            driver.FindElement(By.Id("dataNascCad")).Click();
            driver.FindElement(By.Id("dataNascCad")).SendKeys("28/05/1974");
            driver.FindElement(By.Id("iniciarCadastro")).Click();


            //Validar Mensagem de Retorno CPF

            Thread.Sleep(8000);


            //if (this.IsElementPresent(By.Id("dialog-message")))

            //{
            //    wait(By.CssSelector("#modalContent > div.modal-body.row"));
            //    string Retorno = driver.FindElement(By.CssSelector("#modalContent > div.modal-body.row")).Text;
            //    // var setMensagemRetorno =
            //    Marisa.TestDataAccess.ExcelDataAccess.SetMesagemRetorno(Retorno, Cadastro.CPF);
            //    Thread.Sleep(2000);
            //    print.PrintScreen();

            //    //refazer o contador
            //    //int counter = 0;
            //    //int NumeroCPF = Marisa.TestDataAccess.ExcelDataAccess.GetSemMensagem().total;

            //    //while (counter < NumeroCPF) ;

            //}


            wait(By.Id("avancarDadosPessoais"));

            //Cenario 2  -- Dados pessoais


            //Validar Página Cadastro de Clientes


            Assert.IsTrue(IsElementPresent(By.Id("formDadosPessoais")));


            ////Nome da Mãe
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("nomeMaeDadosPessoais")).Click();
            //driver.FindElement(By.Id("nomeMaeDadosPessoais")).Clear();
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("nomeMaeDadosPessoais")).SendKeys("Maria Aparecida");


            //Estado Civil
            Thread.Sleep(2000);
            driver.FindElement(By.Id("estadoCivil")).Click();
            Thread.Sleep(2000);
            //new SelectElement(driver.FindElement(By.Id("estadoCivil"))).SelectByValue(Cadastro.EstadoCivil);
            driver.FindElement(By.Id("estadoCivil")).SendKeys("CASADO");// PEGAR DO BANCO
            SendKeys.SendWait("{ENTER}");



            ////Sexo       
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("sexoDadosPessoais")).Click();
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("sexoDadosPessoais")).SendKeys("MASCULINO"); // PEGAR DO BANCO
            //SendKeys.SendWait("{ENTER}");


            //Deseja Receber a Fatura por email?         
            Thread.Sleep(2000);
            driver.FindElement(By.Id("faturaEmailOutros")).Click();
            driver.FindElement(By.Id("faturaEmailOutros")).SendKeys("Não"); // PEGAR DO BANCO //


            //Email
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cliEmail")).Click();
            driver.FindElement(By.Id("cliEmail")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cliEmail")).SendKeys("starline@starline.com.br");//PEGAR NO BANCO

            Thread.Sleep(2000);

            print.PrintScreen();

            //Botão avançar
            driver.FindElement(By.Id("avancarDadosPessoais")).Click();

            //Cenario 3 -- Dados Residenciais 

            //Validar Página Dados Residenciais

            wait(By.Id("avancarDadosResidenciais"));


            Assert.IsTrue(IsElementPresent(By.Id("formDadosResidenciais")));

            //Cep
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cepDadosResidenciais")).Click();
            driver.FindElement(By.Id("cepDadosResidenciais")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cepDadosResidenciais")).SendKeys("06033080");
            Thread.Sleep(2000);
            SendKeys.SendWait("{TAB}");



            //Numero
            Thread.Sleep(2000);
            driver.FindElement(By.Id("numeroDadosResidenciais")).Click();
            driver.FindElement(By.Id("numeroDadosResidenciais")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("numeroDadosResidenciais")).SendKeys("1010");

            //Tipo Residencia
            Thread.Sleep(2000);
            driver.FindElement(By.Id("tipoResidencia")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("tipoResidencia")).SendKeys("ALUGADA"); // PEGAR DO BANCO
            SendKeys.SendWait("{ENTER}");


            //Telefone Residencia
            Thread.Sleep(2000);
            driver.FindElement(By.Id("telefoneResidencialDadosResidenciais")).Click();
            driver.FindElement(By.Id("telefoneResidencialDadosResidenciais")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("telefoneResidencialDadosResidenciais")).SendKeys("1135926538");
            Thread.Sleep(2000);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{ENTER}");



            print.PrintScreen();

            //Botão avançar
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("avancarDadosResidenciais"));

            //Cenario 4 -- Dados Comerciais 

            //Validar Página Dados Comerciais

            wait(By.Id("avancarDadosComerciais"));


            Assert.IsTrue(IsElementPresent(By.Id("formDadosComerciais")));


            //Classe Profissisional
            Thread.Sleep(2000);
            driver.FindElement(By.Id("classeProfDadosComerciais")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("classeProfDadosComerciais")).SendKeys("Aposentados"); // PEGAR DO BANCO
            SendKeys.SendWait("{ENTER}");

            //Atividade
            Thread.Sleep(2000);
            driver.FindElement(By.Id("atividadeDadosComerciais")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("atividadeDadosComerciais")).SendKeys("Aposentados"); // PEGAR DO BANCO
            SendKeys.SendWait("{ENTER}");


            //Renda Mensal
            Thread.Sleep(2000);
            driver.FindElement(By.Id("rendaMensalDadosComerciais")).Click();
            driver.FindElement(By.Id("rendaMensalDadosComerciais")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("rendaMensalDadosComerciais")).SendKeys("10000");


            ////Tempo de Empresa (ANO)
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("tempoEmpresaAnoDadosComerciais")).Click();
            //driver.FindElement(By.Id("tempoEmpresaAnoDadosComerciais")).Clear();
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("tempoEmpresaAnoDadosComerciais")).SendKeys("10");



            ////Tempo de Empresa (MES)
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("tempoEmpresaMes")).Click();
            //driver.FindElement(By.Id("tempoEmpresaMes")).Clear();
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("tempoEmpresaMes")).SendKeys("10");


            //CEP
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cepDadosComerciais")).Click();
            driver.FindElement(By.Id("cepDadosComerciais")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("cepDadosComerciais")).SendKeys("06033050");

            //Numero
            Thread.Sleep(2000);
            driver.FindElement(By.Id("numeroDadosComerciais")).Click();
            driver.FindElement(By.Id("numeroDadosComerciais")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("numeroDadosComerciais")).SendKeys("365");

            //Telefone Comercial
            Thread.Sleep(2000);
            driver.FindElement(By.Id("telefoneComercialDadosComercial")).Click();
            driver.FindElement(By.Id("telefoneComercialDadosComercial")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("telefoneComercialDadosComercial")).SendKeys("1136811452");
            Thread.Sleep(2000);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{ENTER}");

            //Botão avançar
            //Thread.Sleep(2000);
            //driver.FindElement(By.Id("avancarDadosComerciais"));

            //Cenario 5 -- Outros 

            //Validar Página Outros

            wait(By.Id("avancarOutros"));


            Assert.IsTrue(IsElementPresent(By.Id("formOutros")));

            //Vencimento Melhor Dia
            Thread.Sleep(2000);
            driver.FindElement(By.Id("vencimentoOutros")).Click();
            driver.FindElement(By.Id("vencimentoOutros")).SendKeys("10");
            Thread.Sleep(2000);

            //Confirmação de Vencimento
            SendKeys.SendWait("{ESC}");
            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");
            Thread.Sleep(2000);
            //Cobrar tarifa de overlimit


            wait(By.CssSelector("#tarifaOverlimitOutros"));

            driver.FindElement(By.CssSelector("#tarifaOverlimitOutros")).Click();
            driver.FindElement(By.CssSelector("#tarifaOverlimitOutros")).SendKeys("Não");

            //Conta Bônus?
            Thread.Sleep(2000);
            driver.FindElement(By.Id("tipoCartaoOutros")).Click();
            driver.FindElement(By.Id("tipoCartaoOutros")).SendKeys("Não");


            //Campanha
            Thread.Sleep(2000);
            driver.FindElement(By.Id("campanhaOutros")).Click();
            driver.FindElement(By.Id("campanhaOutros")).SendKeys("931 - Desc Primeira Compra Marisa Itaucard 10%");
            Thread.Sleep(2000);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{ENTER}");


            //Botão avançar
            //Thread.Sleep(2000);
            driver.FindElement(By.Id("avancarOutros")).Click();

            //Cenario 6 -- Camera 

            //Validar Página Outros

            wait(By.Id("avancarBiometriaFacial"));


            Assert.IsTrue(IsElementPresent(By.Id("fieldSetBiometria")));

            //Ligar a camera
            Thread.Sleep(2000);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{ENTER}");

            Thread.Sleep(2000);

            //Colocar a imagem
            ClassStarLine classStarLine = new ClassStarLine();
            string biometriadigital = classStarLine.GetImageBase64("C:\\MARISA\\CCM\\base6412.jpg");

            Thread.Sleep(2000);
            ((IJavaScriptExecutor)driver).ExecuteScript("acessoFlash.faceDetect('data:image/jpeg;base64," + biometriadigital + "')");

            //Aceitar a imagem
            driver.FindElement(By.CssSelector("#pnlCapturePreview > div > div.section-content.buttons > div > div.imagebutton.accept.dark")).Click();

            //String codigoPagina = driver.getPageSource();
            //assertThat(codigoPagina, Matchers.containsString("DADOS ENCONTRADOS COM SUCESSO."));
            //assertNotNull(driver.findElement(By.cssSelector("table.tmptabela")));



            driver.Quit();


        }

        ////Tirar foto
        //driver.SwitchTo();
        //    driver.FindElement(By.Id("btnCamera")).Click();
        //    Thread.Sleep(200);
        //    driver.FindElement(By.CssSelector("#pnlCapturePreview > div > div.section-content.buttons > div > div.imagebutton.accept.dark")).Click();
        //    Thread.Sleep(20000);

        //    //Botão Avançar
        //    driver.FindElement(By.Id("avancarBiometriaFacial")).Click();

        //    //Cenario 7 -- Formalização 

        //    //Validar Página Outros

        //    wait(By.Id("avancarBiometriaFacial"));


        //    Assert.IsTrue(IsElementPresent(By.Id("panelAssinaturaEletronica")));

        //    driver.Quit();


        //}




        private bool IsElementPresent(bool v)
        {
            throw new NotImplementedException();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool verify(string elementName)
        {
            try
            {
                driver = null;
                return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        private bool isElementNotPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }



        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}

