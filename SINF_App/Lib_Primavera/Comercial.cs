using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
using SINF_App.Models;

//using Interop.StdBESql800;
//using Interop.StdBSSql800;

namespace SINF_App.Lib_Primavera
{

    public class Comercial
    {

        /// <summary>
        /// Global variables   
        /// </summary>
        public const string companyName = "BELAFLOR";
        public const string userName = "admin";
        public const string passWord = "_admin";


        #region Login
        
        public static RespostaErro Login(Login login)
        {
            RespostaErro erro = new RespostaErro();

            try
            {

                if (PriEngine.InitializeCompany(login.Company, login.Username, login.Password) == true)
                {
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";

                }
                else
                {
                    erro.Erro = 2;
                    erro.Descricao = "Impossivel fazer login";

                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;

            }

            return erro;
        }    

        #endregion Login;


        # region Cliente

        public static List<Cliente> ListaClientes(Login loginInfo)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Cliente cli = new Cliente();
            List<Cliente> listClientes = new List<Cliente>();


            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    cli = new Cliente();
                    cli.CodCliente = objList.Valor("Cliente");
                    cli.NomeCliente = objList.Valor("Nome");
                    cli.Moeda = objList.Valor("Moeda");
                    cli.NumContribuinte = objList.Valor("NumContribuinte");

                    listClientes.Add(cli);
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Cliente GetCliente(Login loginInfo,string codCliente)
        {
            ErpBS objMotor = new ErpBS();
            GcpBECliente objCli = new GcpBECliente();

            Cliente myCli = new Cliente();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username,loginInfo.Password) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }


        public static RespostaErro UpdCliente(Login loginInfo,Cliente cliente)
        {
            RespostaErro erro = new RespostaErro();
            ErpBS objMotor = new ErpBS();
            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static RespostaErro DelCliente(Login loginInfo,string codCliente)
        {

            RespostaErro erro = new RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static RespostaErro InsereClienteObj(Login loginInfo,Cliente cli)
        {

            RespostaErro erro = new RespostaErro();

            GcpBECliente myCli = new GcpBECliente();
            try
            {
                if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        /*
        public static void InsereCliente(string codCliente, string nomeCliente, string numContribuinte, string moeda)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();

            GcpBECliente myCli = new GcpBECliente();

            objMotor = mp.AbreEmpresa("DEMO", "", "", "Default");

            myCli.set_Cliente(codCliente);
            myCli.set_Nome(nomeCliente);
            myCli.set_NumContribuinte(numContribuinte);
            myCli.set_Moeda(moeda);

            objMotor.Comercial.Clientes.Actualiza(myCli);

        }


        */


        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Artigo
        public static Artigo GetArtigo(Login loginInfo,string codArtigo)
        {


            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Artigo myArt = new Artigo();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

        public static List<Artigo> ListaArtigos(Login loginInfo)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Artigo art = new Artigo();
            List<Artigo> listArts = new List<Artigo>();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo;

        //------------------------------------ ENCOMENDA ---------------------


        #region Encomenda
        public static Object TransformaEncomenda(DocCompra documentoOriginal, TipoDoc tipoFinal, Login loginInfo, Dictionary<int,double> quantidadesAConverter, string idArmazem, string codDocExterno)
        {

            RespostaErro erro = new RespostaErro();

            GcpBEDocumentoCompra objInicial;

            GcpBEDocumentoCompra objFinal = new GcpBEDocumentoCompra();

            GcpBELinhasDocumentoCompra objLinEnc = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();



            List<LinhaDocCompra> lstlindc = new List<LinhaDocCompra>();



            try

            {
                if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
                {
                
                


                    objInicial=PriEngine.Engine.Comercial.Compras.EditaID(documentoOriginal.id);//get original document

                   



                    // --- Criar os cabeçalhos da GR

                    objFinal.set_Entidade(objInicial.get_Entidade());

                    objFinal.set_Serie(documentoOriginal.Serie);

                    objFinal.set_Tipodoc("VFA");

                    objFinal.set_TipoEntidade("F");
                    objFinal.set_NumDocExterno(objInicial.get_NumDocExterno()+1);//Should be something else



                    objFinal = PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(objFinal,rl);

                    GcpBELinhasDocumentoCompra linhasOriginal = objInicial.get_Linhas();

                    foreach (KeyValuePair<int, double> entry in quantidadesAConverter)
                        {

                        int nrLinha = entry.Key;
                        double quantAConverter = entry.Value;


                        //faz verificaçoes necessarias
                        if (quantAConverter == 0.0) continue;
                        double quantPossivelDeConverter = 0;


                        if (nrLinha > linhasOriginal.NumItens - 1) continue;//if the line doesnt exist continue
                        GcpBELinhaDocumentoCompra line = linhasOriginal[nrLinha+1];

                        double q = line.get_Quantidade();
                        double qs = line.get_QuantSatisfeita();
                        quantPossivelDeConverter = line.get_Quantidade() - line.get_QuantSatisfeita();

 
                        if (quantAConverter > quantPossivelDeConverter)
                        {
                            erro.Descricao="Impossivel converter quantidade desejada para linha número " + nrLinha;
                            erro.Erro = 427;//change this code
                            return erro;
                        }


                        //tudo está ok aqui... entao faz conversao

                        string previousArmazem = line.get_Armazem();
                        line.set_Armazem(idArmazem);//temporarly change armazem to the desired one

                        PriEngine.Engine.Comercial.Internos.CopiaLinha("C", objInicial, "C", objFinal, nrLinha + 1, quantAConverter);


                        line.set_Armazem(previousArmazem);//change to the original armazem again!

                        line.set_QuantSatisfeita(line.get_QuantSatisfeita() + quantAConverter);

                       
                    }
                     
                       
                       

       

                    PriEngine.Engine.IniciaTransaccao();

                    PriEngine.Engine.Comercial.Compras.Actualiza(objInicial, "");

                    PriEngine.Engine.Comercial.Compras.Actualiza(objFinal, "");



                    PriEngine.Engine.TerminaTransaccao();



                    return DocCompra.fromERPType(objFinal);

                }

                else

                {

                    erro.Erro = 1;

                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;



                }



            }

            catch (Exception ex)

            {

                PriEngine.Engine.DesfazTransaccao();

                erro.Erro = 1;

                erro.Descricao = ex.Message;

                return erro;

            }
        
        

        

        }



        #endregion Encomenda;

        // ------------------------ Documentos de Compra --------------------------//
        #region DocCompra
        public static List<DocCompra> VGR_List(Login loginInfo)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            DocCompra dc = new DocCompra();
            List<DocCompra> listdc = new List<DocCompra>();
            LinhaDocCompra lindc = new LinhaDocCompra();
            List<LinhaDocCompra> listlindc = new List<LinhaDocCompra>();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }



        public static List<EncomendaFornecedor> ECF_List(Login loginInfo,string idDoc = null, string descricaoFornecedor = null, string idFornecedor = null, string idArtigo = null)
        {

            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            StdBELista objListLinStatus;
            EncomendaFornecedor dc = new EncomendaFornecedor();
            List<EncomendaFornecedor> listdc = new List<EncomendaFornecedor>();
            LinhaDocCompra lindc = new LinhaDocCompra();
            List<LinhaDocCompra> listlindc = new List<LinhaDocCompra>();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
           {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='ECF'");
                while (!objListCab.NoFim())
                {
                    bool desiredFornecedor = idFornecedor == null || idFornecedor.Equals(objListCab.Valor("Entidade"), StringComparison.Ordinal);
                    //bool desiredDocumentID = idDoc == null || idDoc.Equals(objListCab.Valor("id"));
                    bool desiredDocumentID = idDoc == null || idDoc.Equals(objListCab.Valor("NumDoc").ToString());
                    
                    var test_id = objListCab.Valor("id");
                    var test_NumDoc = objListCab.Valor("NumDoc");


                    bool desiredDescricaoFornecedor = descricaoFornecedor == null;

                    if (!desiredDescricaoFornecedor)
                    {
                        string idFornecedorObtido = objListCab.Valor("Entidade");
                        desiredDescricaoFornecedor = desiredDescricaoFornecedor || FornecedorID_Descricao_Existe(idFornecedorObtido, descricaoFornecedor);
                    }
                    


                    if (!desiredFornecedor || !desiredDocumentID || !desiredDescricaoFornecedor)
                    {
                        objListCab.Seguinte();
                        continue;
                    }

                    dc = new EncomendaFornecedor();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT Id, idCabecCompras, NumLinha, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    
                    
                    listlindc = new List<LinhaDocCompra>();


                    bool desiredProductPresent = idArtigo == null;


                    while (!objListLin.NoFim())
                    {

                        if (!desiredProductPresent)
                        {//if we havent found the product yet
                            desiredProductPresent = idArtigo.Equals(objListLin.Valor("Artigo"), StringComparison.Ordinal);//check ig this is it
                        }

                        lindc = new LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");
                        lindc.NumLinha = objListLin.Valor("NumLinha");


                        objListLinStatus=PriEngine.Engine.Consulta("SELECT QuantTrans from LinhasComprasStatus where IdLinhasCompras='"+objListLin.Valor("Id")+"'");


                        lindc.QuantidadeSatisfeita = objListLinStatus.Valor("QuantTrans");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    if (!desiredProductPresent)
                    {
                        objListCab.Seguinte();
                        continue;
                    }
                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;


        }
        public static EncomendaFornecedor ECF_Single(Login loginInfo, string idDoc = null, string descricaoFornecedor = null, string idFornecedor = null, string idArtigo = null)
        {

            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            StdBELista objListLinStatus;
            EncomendaFornecedor dc = new EncomendaFornecedor();
            List<EncomendaFornecedor> listdc = new List<EncomendaFornecedor>();
            LinhaDocCompra lindc = new LinhaDocCompra();
            List<LinhaDocCompra> listlindc = new List<LinhaDocCompra>();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='ECF'");
                while (!objListCab.NoFim())
                {
                    bool desiredDocumentID = idDoc.Equals(objListCab.Valor("id").ToString());
                    var tmp = objListCab.Valor("NumDoc").ToString();
                    if (!desiredDocumentID)
                    {
                        objListCab.Seguinte();
                        continue;
                    }

                    dc = new EncomendaFornecedor();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT Id, idCabecCompras, NumLinha, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");


                    listlindc = new List<LinhaDocCompra>();
                    while (!objListLin.NoFim())
                    {

                        lindc = new LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");
                        lindc.NumLinha = objListLin.Valor("NumLinha");


                        objListLinStatus = PriEngine.Engine.Consulta("SELECT QuantTrans from LinhasComprasStatus where IdLinhasCompras='" + objListLin.Valor("Id") + "'");
                        lindc.QuantidadeSatisfeita = objListLinStatus.Valor("QuantTrans");
                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;
                    return dc;
                }
            }

            return null;

        }

        public static RespostaErro VGR_New(Login loginInfo,DocCompra dc)
        {
            RespostaErro erro = new RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<LinhaDocCompra> lstlindv = new List<LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        #endregion DocCompra;

        // ------ Documentos de venda ----------------------
        #region DocVenda


        public static RespostaErro Encomendas_New(Login loginInfo,DocVenda dv)
        {
            RespostaErro erro = new RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<LinhaDocVenda> lstlindv = new List<LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        public static List<DocCompra> Encomendas_List(Login loginInfo)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            DocCompra dv = new DocCompra();
            List<DocCompra> listdv = new List<DocCompra>();
            LinhaDocCompra lindv = new LinhaDocCompra();
            List<LinhaDocCompra> listlindv = new
            List<LinhaDocCompra>();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new DocCompra();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, QuantidadeSatisfeita, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new LinhaDocCompra();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindv.QuantidadeSatisfeita = objListLin.Valor("QuantidadeSatisfeita");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


        public static DocCompra Encomenda_Get(Login loginInfo,string numdoc)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objListCab;
            StdBELista objListLin;
            DocCompra dv = new DocCompra();
            LinhaDocCompra lindv = new LinhaDocCompra();
            List<LinhaDocCompra> listlindv = new List<LinhaDocCompra>();

            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {

                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new DocCompra();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao,QuantidadeSatisfeita, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<LinhaDocCompra>();

                while (!objListLin.NoFim())
                {
                    lindv = new LinhaDocCompra();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    lindv.QuantidadeSatisfeita = objListLin.Valor("QuantidadeSatisfeita");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }


        #endregion DocVenda;

        // --------------- Armazens ------------------------------
        #region Armazens
        public static List<SINF_App.Models.Armazem> ListaArmazens(Login loginInfo)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Armazem arm = new Armazem();
            List<Armazem> listArmazens = new List<Armazem>();


            if (PriEngine.InitializeCompany(loginInfo.Company, loginInfo.Username, loginInfo.Password) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Armazem, Descricao, Morada FROM Armazens");

                while (!objList.NoFim())
                {

                    arm = new Armazem();
                    arm.ID = objList.Valor("Armazem");
                    arm.Descricao = objList.Valor("Descricao");
                    arm.Morada = objList.Valor("Morada");

                    listArmazens.Add(arm);
                    objList.Seguinte();

                }

                return listArmazens;
            }
            else
                return null;
        }


        #endregion Armazens

        // ---------------- Fornecedores ---------------------------
        #region Fornecedores
        /**
         * 
         * Retorna true se o id e a descriçao de um fornecedor corresponderem. Se perfectMatch=false, entao é verificado se a descriçao fornecida está contida na descricao real
         * 
         * 
         **/
        public static bool FornecedorID_Descricao_Existe(string id, string desc, bool perfectMatch = false)
        {

            if (id == null || desc == null) return false;


            StdBELista objList;



            string queryString = "SELECT Fornecedor, Nome FROM Fornecedores WHERE Fornecedor = '" + id+"'";
            objList = PriEngine.Engine.Consulta(queryString);


            if (perfectMatch)
            {
                while (!objList.NoFim())
                {
                    if (desc.Equals(objList.Valor("Nome"), StringComparison.Ordinal)) return true;
                    objList.Seguinte();

                }

            }

            else
            {

                while (!objList.NoFim())
                {
                    string name = objList.Valor("Nome");
                    if (name.Contains(desc)) return true;
                    objList.Seguinte();

                }

            }

            return false;
        }

        #endregion Fornecedores
    }
}