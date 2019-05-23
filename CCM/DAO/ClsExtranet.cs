using System;

/// //////////////////////////////////////////////////////////////////////////////////////////
/// ///       ///         //////    ///////       ///  ////////  ///    ///////  ///        //
/// //  ////////////  /////////  //  //////  //// ///  ////////  ///  //  /////  ///  ////////
/// //       ///////  ////////  ////  /////  //  ////  ////////  ///  ////  ///  ///        //
/// ///////  ///////  ///////          ////     /////  ////////  ///  //////  /  ///  ////////
/// //      ////////  //////  ////////  ///  //  ////       ///  ///  ////////   ///        //
/// //////////////////////////////////////////////////////////////////////////////////////////

public class ClsExtranet
{
    Connection con = new Connection();

    public ClsExtranet()
    {
    }

    public void Consulta_CPF()
    {
        con.clear();
        con.sql.AppendLine("select CLI_CPF from CCM_CREDITO sample(1) where stat_cod=1 and rownum = 1");

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.ccmcpf = con.dr["CLI_CPF"].ToString();


            }
            else
            {
                this.ccmcpf = null;

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
    }
   
    public void Consulta_Requerimento()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("select u.nome as unidade, r.crm_numero_chamado as protocolo, d.nome as requerimento,");
        con.sql.AppendLine("r.data, r.entidade as alucod, s.crm_acao as status, r.curso as espcod, g.grupo, r.periodo_letivo");
        con.sql.AppendLine("from gsoa.web_req_chamado_solicitacao r, gsoa.WEB_PAR_UNIDADE u, gsoa.web_pas_servico d,");
        con.sql.AppendLine("gsoa.web_pas_grupo g, gsoa.web_log_req_chamado s");
        con.sql.AppendLine("where r.codigo_requerimento = d.codigo_requerimento");
        con.sql.AppendLine("and s.crm_numero_chamado = r.crm_numero_chamado");
        con.sql.AppendLine("and r.codigo_olimpo = u.codigo_olimpo");
        con.sql.AppendLine("and d.id_grupo = g.id_grupo");
        con.sql.AppendLine("order by data desc");

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.unidade = con.dr["unidade"].ToString();
                this.protocolo = con.dr["protocolo"].ToString();
                this.requerimento = con.dr["requerimento"].ToString();
                this.data = con.dr["data"].ToString();
                this.alucod = con.dr["alucod"].ToString();
                this.status = con.dr["status"].ToString();
                this.espcod = con.dr["espcod"].ToString();
                this.grupo = con.dr["grupo"].ToString();
                this.periodo_letivo = con.dr["periodo_letivo"].ToString();

            }
            else
            {
                this.unidade = null;
                this.protocolo = null;
                this.requerimento = null;
                this.data = null;
                this.alucod = null;
                this.status = null;
                this.espcod = null;
                this.grupo = null;
                this.periodo_letivo = null;

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
    }


    public void AlunosCursando()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("select a.alunome, e.espdesc, c.tmacod, a.alucod, c.espcod, c.pelcod, a.alurg, a.alucpf");
        con.sql.AppendLine("from alunos a, aluncurs c, especial e, v_alunsituatual v");
        con.sql.AppendLine("where a.alucod = c.alucod");
        con.sql.AppendLine("and a.alucod = v.alucod");
        con.sql.AppendLine("and c.espcod = e.espcod");
        con.sql.AppendLine("and c.espcod = v.espcod");
        con.sql.AppendLine("and v.asisituacao = 'C'");
        con.sql.AppendLine("and e.esptipo = 'G'");
        con.sql.AppendLine("and c.pelcod >= 20152");
        con.sql.AppendLine("and a.alucod not in (select cc.alucod");
        con.sql.AppendLine("from aluncurs cc, v_Alunsituatual vv");
        con.sql.AppendLine("where cc.alucod = vv.alucod");
        con.sql.AppendLine("and vv.asisituacao <> 'C')");

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.alunome = con.dr["alunome"].ToString();
                this.espdesc = con.dr["espdesc"].ToString();
                this.tmacod = con.dr["tmacod"].ToString();
                this.alucod = con.dr["alucod"].ToString();
                this.espcod = con.dr["espcod"].ToString();
                this.pelcod = con.dr["pelcod"].ToString();
                this.alucpf = con.dr["alucpf"].ToString();
                this.alurg = con.dr["alurg"].ToString();

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

    }




    public void ConsultaConvocados()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("select distinct c.cvecpf, c.cvenome, c.cvesexo, c.cvedatanasc, c.cvecorraca, c.cveestadocivil, ");
        con.sql.AppendLine("c.cvenomemae, c.cvenacionalidade, c.cvenacionalidadetipo, c.cverg, c.cvergexp, c.cverguf,");
        con.sql.AppendLine("c.cvecep, c.cveendereco, c.cvebairro, c.cvecidade, c.cveuf, c.cveemail, c.cveescolaridade, c.cveanoconcensmedio");
        con.sql.AppendLine("from candvest c, vestibul v, especial e ");
        con.sql.AppendLine("where c.cvesituacao = 'C'");
        con.sql.AppendLine("and c.alucod is null");
        con.sql.AppendLine("and c.cveconvop1 = 'S'");
        con.sql.AppendLine("and c.cvecpf is not null");
        con.sql.AppendLine("and c.vescod = v.vescod");
        con.sql.AppendLine("and v.menano = 2017");
        con.sql.AppendLine("and c.cverg is not null");
        con.sql.AppendLine("and c.cvesexo is not null");
        con.sql.AppendLine("and c.cvenomemae is not null");

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.cvecpf = con.dr["cvecpf"].ToString();
                this.cvenome = con.dr["cvenome"].ToString();
                this.cvedatanasc = con.dr["cvedatanasc"].ToString();
                this.cvenomemae = con.dr["cvenomemae"].ToString();
                this.cverg = con.dr["cverg"].ToString();
                this.cvergexp = con.dr["cvergexp"].ToString();
                this.cverguf = con.dr["cverguf"].ToString();
                this.cvecep = con.dr["cvecep"].ToString();
                this.cvendereco = con.dr["cvendereco"].ToString();
                this.cvebairro = con.dr["cvebairro"].ToString();
                this.cveuf = con.dr["cveuf"].ToString();
                this.cveemail = con.dr["cveemail"].ToString();
                this.cveescolaridade = con.dr["cveescolaridade"].ToString();
                this.cveanoconcensmedio = con.dr["cveanoconcensmedio"].ToString();
                this.cvesexo = con.dr["cvesexo"].ToString();
                this.cvecidade = con.dr["cvecidade"].ToString();
                this.cveendereco = con.dr["cveendereco"].ToString();

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

    }



    public void ConsultaConvenios()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("select c.concod,  c.condesc, e.empnomefantasia");
        con.sql.AppendLine("from convenio c, bolsaextensao b, empresa e");
        con.sql.AppendLine("where c.concod = b.concod");
        con.sql.AppendLine("and b.usuarioinc = 'ROSALIJUSTINO'");
        con.sql.AppendLine("and c.consituacao = 'A'");        

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.concod = con.dr["concod"].ToString();
                this.condesc = con.dr["condesc"].ToString();
                this.empnomefantasia = con.dr["empnomefantasia"].ToString();                

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

    }


    public void AguardandoDeferimentoMesa()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("select distinct s.sobnum, s.alucod, s.concod, pb.bsdescricao, trunc(s.sobdatacadastro)");
        con.sql.AppendLine("from solicitabolsa s, convenio c, producao.beneficiostatus pb, bolsaextensao b");
        con.sql.AppendLine("where s.concod = c.concod");
        con.sql.AppendLine("and c.concod = b.concod");
        con.sql.AppendLine("and b.usuarioinc = 'ROSALIJUSTINO'");
        con.sql.AppendLine("and pb.bsflgativo = 'S'");
        con.sql.AppendLine("and pb.sobsituacao = 'S'");

        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.sobnum = con.dr["sobnum"].ToString();
                this.alucod = con.dr["alucod"].ToString();
                this.concod = con.dr["concod"].ToString();
                this.sobdatacadastro = con.dr["sobdatacadastro"].ToString();

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

    }



    public void CandidatosDesclassificados()
    {
        con.clear();
        con.sql.AppendLine("call iuni_multi_empresa.Configura_Empresa(1)");
        con.executa();
        con.clear();
        con.sql.AppendLine("select distinct c.cvecpf, v.vestadocla, c.cvesituacao");
        con.sql.AppendLine("from candvest c, vestibul v, especial e");
        con.sql.AppendLine("where c.cvesituacao = 'D'");
        con.sql.AppendLine("and c.alucod is null ");
        con.sql.AppendLine("and c.cvecpf is not null");
        con.sql.AppendLine("and c.vescod = v.vescod");
        con.sql.AppendLine("and v.menano >= 2017");
        con.sql.AppendLine("and e.esptipo = 'G'");
        con.sql.AppendLine("and v.vestadocla is not null");
        
        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.cvecpf = con.dr["cvecpf"].ToString();               

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

    }






    public string cveendereco { get; set; }
    public string cvecidade { get; set; }
    public string sobdatacadastro { get; set; }
    public string unidade { get; set; }
    public string protocolo { get; set; }
    public string requerimento { get; set; }
    public string data { get; set; }
    public string alucod { get; set; }
    public string status { get; set; }
    public string espcod { get; set; }
    public string grupo { get; set; }
    public string tmacod { get; set; }
    public string alunome { get; set; }
    public string periodo_letivo { get; set; }
    public string pelcod { get; set; }
    public string espdesc { get; set; }
    public string alucpf { get; set; }
    public string alurg { get; set; }   
    public string cvecpf { get; set; }   
    public string cvenome { get; set; }   
    public string cvedatanasc { get; set; }   
    public string cvenomemae { get; set; }   
     public string cverg { get; set; }  
     public string cvergexp { get; set; }  
     public string cverguf { get; set; }  
     public string cvecep { get; set; }  
     public string cvendereco { get; set; }  
     public string cvebairro { get; set; }  
     public string cveuf { get; set; }  
    public string cveemail { get; set; }  
    public string cveescolaridade { get; set; }  
    public string cveanoconcensmedio { get; set; }   
    public string concod { get; set; } 
    public string condesc { get; set; } 
    public string empnomefantasia { get; set; }
    public string sobnum { get; set; }
    public string cvesexo { get; set; } 
    public string ccmcpf { get; set; }

}
    

              


