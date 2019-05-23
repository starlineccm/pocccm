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
using System.Windows.Input;
using System.Windows.Forms;
//using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace CCM_POC
{
    [TestFixture]
    public class TABLET_CCM
    {
        private const string login = "TI_SANDRA";
        private const string senha = "Teste@1234";
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private ChromeOptions options;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();

            options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.EnableMobileEmulation("Galaxy S5");
            options.AddArgument("window-size=380,640");
            driver = new ChromeDriver(options);
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Window.Maximize();
            baseURL = "http://10.10.41.171/psfsecurity/paginas/security/login/tela/login.html";
            verificationErrors = new StringBuilder();
        }

        public void wait(By elemento)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
        }



        public void Login()
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
        public void POC001_Adesao_Bolsa_Protegida()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC001_Adesão_Bolsa_Protegida");

            
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

           

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("");
            Thread.Sleep(2000);
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);            
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Disponíveis"
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Disponíveis")).Click();


            wait(By.XPath("(//div[@id='collapsecard.id']/div/ul/li[3]/div[3]/a/span)[2]"));
            print.PrintScreen();


            //Validar Seção Serviços Disponíveis
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            

            //Aderir Serviço Bolsa Protegida
            driver.FindElement(By.XPath("(//div[@id='collapsecard.id']/div/ul/li[3]/div[3]/a/span)[2]")).Click();
            Thread.Sleep(2000);            

            wait(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk"));
            print.PrintScreen();

            //Validar Pop-up de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Contratação de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            
            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Não", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnNoCancel")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));

            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviço contratado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Contratação", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
                                 
            driver.FindElement(By.Id("btnYesOk")).Click();

            Thread.Sleep(2000);
            print.PrintScreen();

        }


        [Test]
        public void POC002_Cancelar_Bolsa_Protegida()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC002_Cancelar_Bolsa_Protegida");


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

            

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Contratados"            
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Contratados")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a/span"));
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cancelar Serviço            
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a/span")).Click();
            wait(By.Id("motivoSolicitacaoCancelamento"));

            //Validar tela de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Cancelamento de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Motivo Solicitação do Cancelamento", driver.FindElement(By.CssSelector("label.control-label")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            print.PrintScreen();

            //Selecionar Motivo do Cancelamento
            new SelectElement(driver.FindElement(By.Id("motivoSolicitacaoCancelamento"))).SelectByText("Perdeu o interesse");
            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            //Confirmar Cancelamento de Serviço
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Cancelamento", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviço cancelado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.Id("btnYesOk")).Click();
            Thread.Sleep(2000);
            print.PrintScreen();

        }

        [Test]
        public void POC003_Adesao_Proteção_Financeira()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC003_Adesao_Proteção_Financeira");


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

         

            //Thread.Sleep(2000);

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Disponíveis"
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Disponíveis")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[2]/div[3]/a/span"));
            print.PrintScreen();


            //Validar Seção Serviços Disponíveis
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Aderir Proteção Financeira
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[2]/div[3]/a/span")).Click();

            //driver.FindElement(By.XPath("(//div[@id='collapsecard.id']/div/ul/li[2]/div[3]/a/span)[3]")).Click();
            //driver.FindElement(By.XPath("(//div[@id='collapsecard.id']/div/ul/li[2]/div[3]/a/span)[3]")).Click();

            Thread.Sleep(2000);

            wait(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk"));
            print.PrintScreen();

            //Validar Pop-up de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Contratação de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente contratar o serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: PROTEÇÃO FINANCEIRA [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 10,90(\n|\r\n)Valor Cobertura: R\\$ 0,00(\n|\r\n)Periodicidade: Anual(\n|\r\n)(\n|\r\n) CPF Vendedor$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}


            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Não", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnNoCancel")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviço contratado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Contratação", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }




            driver.FindElement(By.Id("btnYesOk")).Click();


            Thread.Sleep(2000);
            print.PrintScreen();

        }

        [Test]
        public void POC004_Cancelar_Proteção_Financeira()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC004_Cancelar_Proteção_Financeira");


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

         

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Contratados"            
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Contratados")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a/span"));
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cancelar Serviço            
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a/span")).Click();
            wait(By.Id("motivoSolicitacaoCancelamento"));

            //Validar tela de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Cancelamento de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente cancelar o contrato do serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: PROTEÇÃO FINANCEIRA [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 10,90(\n|\r\n)Valor Cobertura: R\\$ 0,00(\n|\r\n)Aquisição: 29/11/2018(\n|\r\n)(\n|\r\n) Motivo Solicitação do Cancelamento(\n|\r\n) Selecione\\.\\.\\. Obito Nao solicitou Arrependimento em sete dias Acordo Perdeu o interesse Problemas com atendimento na Assurant Insatisfeito com o Seguro$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}



            try
            {
                Assert.AreEqual("Motivo Solicitação do Cancelamento", driver.FindElement(By.CssSelector("label.control-label")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

           // driver.FindElement.

               
           // List<IWebElement>gridrow = driver.FindElement(By.Xpath("xpath"));




            print.PrintScreen();

            //Selecionar Motivo do Cancelamento
            new SelectElement(driver.FindElement(By.Id("motivoSolicitacaoCancelamento"))).SelectByText("Perdeu o interesse");
            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            //Confirmar Cancelamento de Serviço
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Cancelamento", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviço cancelado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("btnYesOk")).Click();
            Thread.Sleep(2000);
            print.PrintScreen();

        }





        [Test]
        public void POC005_Adesao_Autoproteção()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC005_Adesao_Autoproteção");


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

       

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Disponíveis"
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Disponíveis")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[10]/div[3]/a/span"));
            print.PrintScreen();


            //Validar Seção Serviços Disponíveis
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Aderir AutoProteção
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[9]/div[3]/a/span")).Click();

            Thread.Sleep(2000);

            wait(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk"));
            print.PrintScreen();

            //Validar Pop-up de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Contratação de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente contratar o serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: AUTOPROTEÇÃO SAC [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 8,90(\n|\r\n)Valor Cobertura: R\\$ 5\\.400,00(\n|\r\n)Periodicidade: Anual(\n|\r\n)(\n|\r\n) CPF Vendedor$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}



            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Não", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnNoCancel")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviço contratado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Contratação", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

                       
            driver.FindElement(By.Id("btnYesOk")).Click();


            Thread.Sleep(2000);
            print.PrintScreen();

        }

        [Test]
        public void POC006_Cancelar_Autoproteção()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC006_Cancelar_Autoproteção");


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


            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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


            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Contratados"            
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Contratados")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a"));
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cancelar Serviço            
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a")).Click();
            wait(By.Id("motivoSolicitacaoCancelamento"));

            //Validar tela de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Cancelamento de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente cancelar o contrato do serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: AUTOPROTEÇÃO SAC [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 8,90(\n|\r\n)Valor Cobertura: R\\$ 5\\.400,00(\n|\r\n)Aquisição: 29/11/2018(\n|\r\n)(\n|\r\n) Motivo Solicitação do Cancelamento(\n|\r\n) Selecione\\.\\.\\. Perdeu o interesse Nao solicitou Obito Arrependimento em sete dias Insatisfeito com o seguro Acordo Problemas com atendimento na Assurant$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}




            try
            {
                Assert.AreEqual("Motivo Solicitação do Cancelamento", driver.FindElement(By.CssSelector("label.control-label")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            print.PrintScreen();

            //Selecionar Motivo do Cancelamento
            new SelectElement(driver.FindElement(By.Id("motivoSolicitacaoCancelamento"))).SelectByText("Perdeu o interesse");
            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            //Confirmar Cancelamento de Serviço
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();
            try
            {
                Assert.AreEqual("Cancelamento", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviço cancelado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("btnYesOk")).Click();
            Thread.Sleep(2000);
            print.PrintScreen();

        }



        [Test]
        public void POC007_Adesao_Marisa_Mulher()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC007_Adesao_Marisa_Mulher");


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

           

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("068.081.278-40");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Disponíveis"
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Disponíveis")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[10]/div[3]/a/span"));
            print.PrintScreen();


            //Validar Seção Serviços Disponíveis
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Aderir Marisa Mulher
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[2]/div[3]/a/span")).Click();
            Thread.Sleep(2000);

            wait(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk"));
            print.PrintScreen();

            //Validar Pop-up de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Contratação de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente contratar o serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: MARISA MULHER [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 28,90(\n|\r\n)Valor Cobertura: R\\$ 0,00(\n|\r\n)Periodicidade: Anual(\n|\r\n)(\n|\r\n) CPF Vendedor$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}
                       

            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Não", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnNoCancel")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviço contratado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Contratação", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            driver.FindElement(By.Id("btnYesOk")).Click();


            Thread.Sleep(2000);
            print.PrintScreen();

        }

        [Test]
        public void POC008_Cancelar_Marisa_Mulher()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC008_Cancelar_Marisa_Mulher");


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

            

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("068.081.278-40");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Contratados"            
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Contratados")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a"));
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cancelar Serviço            
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li/div[3]/a")).Click();

            wait(By.Id("motivoSolicitacaoCancelamento"));

            //Validar tela de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Cancelamento de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente cancelar o contrato do serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: MARISA MULHER [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 28,90(\n|\r\n)Valor Cobertura: R\\$ 0,00(\n|\r\n)Aquisição: 29/11/2018(\n|\r\n)(\n|\r\n) Motivo Solicitação do Cancelamento(\n|\r\n) Selecione\\.\\.\\. Obito Problemas com atendimento na Assurant Perdeu o interesse Nao solicitou Arrependimento em sete dias Insatisfeito com o seguro Acordo$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}

                       

            try
            {
                Assert.AreEqual("Motivo Solicitação do Cancelamento", driver.FindElement(By.CssSelector("label.control-label")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            print.PrintScreen();

            //Selecionar Motivo do Cancelamento
            new SelectElement(driver.FindElement(By.Id("motivoSolicitacaoCancelamento"))).SelectByText("Perdeu o interesse");
            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            //Confirmar Cancelamento de Serviço
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Cancelamento", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviço cancelado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("btnYesOk")).Click();
            Thread.Sleep(2000);
            print.PrintScreen();

        }



        [Test]
        public void POC009_Adesao_Marisa_OdontoPlus()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC009_Adesao_Marisa_OdontoPlus");


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

         

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Disponíveis"
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Disponíveis")).Click();


            wait(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[5]/div[3]/a"));
            print.PrintScreen();


            //Validar Seção Serviços Disponíveis
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Aderir ao Plano Marisa Odonto            
            driver.FindElement(By.XPath("//div[2]/div/div/div/div/div/div/div[2]/div/ul/li[5]/div[3]/a")).Click();


            Thread.Sleep(2000);

            wait(By.Id("submita_dependentes"));
            print.PrintScreen();
            //Validar Tela Cadastro Dependentes
            try
            {
                Assert.AreEqual("Cadastro de Dependentes", driver.FindElement(By.LinkText("Cadastro de Dependentes")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Salvar", driver.FindElement(By.Id("submita_dependentes")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("submita_dependentes")).Click();

            wait(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk"));
            print.PrintScreen();

            //Validar Pop-up de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Contratação de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente contratar o serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: MARISA ODONTO(\n|\r\n)Valor Premio: R\\$ 32,90(\n|\r\n)Valor Cobertura: R\\$ 0,00(\n|\r\n)Periodicidade: Anual(\n|\r\n)(\n|\r\n) CPF Vendedor$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}
            


            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Não", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnNoCancel")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviço contratado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Contratação", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            driver.FindElement(By.Id("btnYesOk")).Click();


            Thread.Sleep(2000);
            print.PrintScreen();

        }

        [Test]
        public void POC010_Cancelar_Marisa_OdontoPlus()
        {


            //Método de Print
            ScreenShot print = new ScreenShot("POC_CCM", "POC010_Cancelar_Marisa_OdontoPlus");


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

          

            try
            {
                Assert.AreEqual("Login efetuado com sucesso!", driver.FindElement(By.Id("text-login-msg")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            wait(By.Id("btnSearch"));
            print.PrintScreen();

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

            Thread.Sleep(2000);
            SendKeys.SendWait("{ESC}");

            //Consultar Cliente
            driver.FindElement(By.Id("cpfSearch")).Clear();
            driver.FindElement(By.Id("cpfSearch")).SendKeys("288.189.018-00");
            print.PrintScreen();
            driver.FindElement(By.Id("btnSearch")).Click();

            wait(By.LinkText("Serviços"));
            wait(By.Id("nomeDadosCliente"));
            print.PrintScreen();

            //Validar Página do Cliente
            Assert.AreEqual("PSFLojas - Dashboard", driver.Title);
            Assert.IsTrue(IsElementPresent(By.Id("nomeDadosCliente")));
            try
            {
                Assert.AreEqual("Serviços Disponíveis", driver.FindElement(By.Id("menuCollapsecard.id")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[2]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Informações Cadastrais", driver.FindElement(By.LinkText("Informações Cadastrais")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Cartões do Cliente", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[4]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Retorno Solicitações", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[5]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Validar Menu Lateral

            //Dashboard
            try
            {
                Assert.AreEqual("Dashboard", driver.FindElement(By.CssSelector("strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Serviços
            try
            {
                Assert.AreEqual("Serviços", driver.FindElement(By.CssSelector("#menuServicos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Históricos
            try
            {
                Assert.AreEqual("Históricos", driver.FindElement(By.CssSelector("#menuHistoricos > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cadastros
            try
            {
                Assert.AreEqual("Cadastros", driver.FindElement(By.CssSelector("#menuCadastros > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Solicitações
            try
            {
                Assert.AreEqual("Solicitações", driver.FindElement(By.CssSelector("#menuSolicitacoes > a > strong")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //Acessar Opção "Serviços Contratados"            
            driver.FindElement(By.LinkText("Serviços")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Serviços Contratados")).Click();


            wait(By.XPath("(//div[@id='collapsecard.id']/div/ul/li/div[3]/a[2]/span)[2]"));
            print.PrintScreen();

            try
            {
                Assert.AreEqual("Serviços Contratados", driver.FindElement(By.XPath("(//a[@id='menuCollapsecard.id'])[6]")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            //Cancelar Serviço            
            driver.FindElement(By.XPath("(//div[@id='collapsecard.id']/div/ul/li/div[3]/a[2]/span)[2]")).Click();


            wait(By.Id("motivoSolicitacaoCancelamento"));

            //Validar tela de Confirmação
            try
            {
                Assert.AreEqual("Confirmação de Cancelamento de Serviço", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > #messageHeader > #messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }


            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-body > div > #dialog-message")).Text, "^exact:Deseja realmente cancelar o contrato do serviço abaixo[\\s\\S](\n|\r\n)(\n|\r\n)Serviço: AUTOPROTEÇÃO SAC [\\s\\S]*(\n|\r\n)Valor Premio: R\\$ 8,90(\n|\r\n)Valor Cobertura: R\\$ 5\\.400,00(\n|\r\n)Aquisição: 29/11/2018(\n|\r\n)(\n|\r\n) Motivo Solicitação do Cancelamento(\n|\r\n) Selecione\\.\\.\\. Perdeu o interesse Nao solicitou Obito Arrependimento em sete dias Insatisfeito com o seguro Acordo Problemas com atendimento na Assurant$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}




            try
            {
                Assert.AreEqual("Motivo Solicitação do Cancelamento", driver.FindElement(By.CssSelector("label.control-label")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            print.PrintScreen();

            //Selecionar Motivo do Cancelamento
            new SelectElement(driver.FindElement(By.Id("motivoSolicitacaoCancelamento"))).SelectByText("Perdeu o interesse");
            try
            {
                Assert.AreEqual("Sim", driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            print.PrintScreen();

            //Confirmar Cancelamento de Serviço
            driver.FindElement(By.CssSelector("#modalQuestion > div.modal-dialog > #modalContent > div.modal-footer > #btnYesOk")).Click();

            wait(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            print.PrintScreen();
            try
            {
                Assert.AreEqual("Cancelamento", driver.FindElement(By.Id("messageTitle")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Serviço cancelado com sucesso!", driver.FindElement(By.Id("dialog-message")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Fechar", driver.FindElement(By.Id("btnYesOk")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("btnYesOk")).Click();
            Thread.Sleep(2000);
            print.PrintScreen();

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
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
