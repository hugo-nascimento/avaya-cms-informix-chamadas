using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Informix_Avaya_Chamadas
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aqui é definido o nome que aparecerá na barra de título do App Console
            string titulo = "CSU - Integration Informix Avaya CMS - Versão ";

            //Aqui é definida a variável que armazena a versão do App
            string versao = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;

            //Essa variável armazena uma nova linha
            string linha = Environment.NewLine;

            string rotina = "Conecta com o SGBD Informix...";
            Console.Title = string.Concat(titulo, versao);

            Console.WriteLine(rotina);
            Console.WriteLine("...");
            Console.WriteLine(linha);


            Console.WriteLine("Conectando no SGBD Informix...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui configuramos a conexão com o Informix Database através de DSN, o que facilita a conexão por OdbcConnection
            OdbcConnection conn = new OdbcConnection(@"DSN=seuDSN;UID=seu_usuario_cms;PWD=sua_senha_cms;"); // Aqui vc define seu DSN, seu usuário CMS e senha CMS
            conn.Open(); 

            Console.WriteLine("Conectado com sucesso!");
            Console.WriteLine("...");
            Console.WriteLine(linha);


            Console.WriteLine("Formatando consulta SQL");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui definimos a variável dataref, que armazena a data atual -30 minutos e utilizamos o formato de data "M/d/yyyy" , que é o formato do informix
            //Esta data (DateTime.Now.AddMinutes(-30)) você pode alterar conforme sua necessidade
            string dataref = DateTime.Now.AddMinutes(-30).ToString("M/d/yyyy"); 

            //Aqui definimos a consulta SQL para consultar no Informix
            string sSQL = string.Concat("SELECT * FROM hsplit WHERE ROW_DATE = DATE('", dataref, "');");

            Console.WriteLine("Formatação concluída!");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            OdbcCommand cmd = new OdbcCommand(sSQL, conn);

            Console.WriteLine("Executando consulta SQL");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui executamos a consulta SQL
            OdbcDataReader dr = cmd.ExecuteReader();

            Console.WriteLine("Consulta executada com sucesso!");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            Console.WriteLine("Criando arquivo de texto...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui definimos a variável x que manipula arquivos txt via stream
            StreamWriter x;

            //Aqui criamos o arquivo de texto que receberá os dados do Informix
            x = File.CreateText(@"C:\CMS\informix.txt");

            Console.WriteLine("Gravando registros da consulta no arquivo...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui, gravamos os dados do cabeçalho do arquivo (que são os nomes dos campos da tabela hsplit)
            //Esta tabela possui 274 campos
            //Você pode optar em trazer todos os campos, ou adaptar para que traga somente os campos de seu interesse
            //Neste projeto, optei por trazer todos os campos
            x.Write(
                "row_date;starttime;starttime_utc;intrvl;acd;split;tenant;i_stafftime;i_availtime;i_acdtime;i_acwtime;i_acwouttime;i_acwintime;i_auxtime;i_auxouttime;" +
                "i_auxintime;i_othertime;maxstaffed;acwincalls;acwintime;auxincalls;auxintime;acwoutcalls;acwouttime;acwoutoffcalls;acwoutofftime;acwoutadjcalls;auxoutcalls;" +
                "auxouttime;auxoutoffcalls;auxoutofftime;auxoutadjcalls;event1;event2;event3;event4;event5;event6;event7;event8;event9;assists;inflowcalls;acdcalls;anstime;acdtime;" +
                "acwtime;o_acdcalls;o_acdtime;o_acwtime;acdcalls1;acdcalls2;acdcalls3;acdcalls4;acdcalls5;acdcalls6;acdcalls7;acdcalls8;acdcalls9;acdcalls10;backupcalls;holdcalls;" +
                "holdtime;holdabncalls;transferred;conference;abncalls;abntime;abncalls1;abncalls2;abncalls3;abncalls4;abncalls5;abncalls6;abncalls7;abncalls8;abncalls9;abncalls10;" +
                "dequecalls;dequetime;busycalls;busytime;disccalls;disctime;outflowcalls;outflowtime;interflowcalls;lowcalls;medcalls;highcalls;topcalls;acceptable;servicelevel;" +
                "period1;period2;period3;period4;period5;period6;period7;period8;period9;maxinqueue;maxocwtime;callsoffered;periodchg;svclevelchg;i_ringtime;ringtime;ringcalls;" +
                "abnringcalls;o_abncalls;o_othercalls;da_acwincalls;da_acwintime;da_acwocalls;da_acwotime;noansredir;incomplete;acdauxoutcalls;i_acdaux_outtime;i_acdauxintime;" +
                "i_acdothertime;phantomabns;othercalls;othertime;slvlabns;slvloutflows;i_arrived;i_auxtime0;i_auxtime1;i_auxtime2;i_auxtime3;i_auxtime4;i_auxtime5;i_auxtime6;i_auxtime7;" +
                "i_auxtime8;i_auxtime9;i_da_acdtime;i_da_acwtime;i_tavailtime;i_tauxtime;maxtop;i_normtime;i_ol1time;i_ol2time;i_tothertime;max_tot_percents;acdcalls_r1;acdcalls_r2;" +
                "i_acdtime_r1;i_acdtime_r2;i_acwtime_r1;i_acwtime_r2;i_ringtime_r1;i_ringtime_r2;i_othertime_r1;i_othertime_r2;i_auxtime_r1;i_auxtime_r2;i_otherstbytime_r1;i_otherstbytime_r2;" +
                "i_auxstbytime_r1;i_auxstbytime_r2;i_behindtime;i_autoreservetime;targetpercent;targetpctchg;targetseconds;targetsecchg;targetacdcalls;targetabns;targetoutflows;" +
                "intrdeliveries;agsurpdeliveries;agsurpprefcalls;agsurpnprefcalls;callsurpdeliveries;i_auxtime10;i_auxtime11;i_auxtime12;i_auxtime13;i_auxtime14;i_auxtime15;i_auxtime16;" +
                "i_auxtime17;i_auxtime18;i_auxtime19;i_auxtime20;i_auxtime21;i_auxtime22;i_auxtime23;i_auxtime24;i_auxtime25;i_auxtime26;i_auxtime27;i_auxtime28;i_auxtime29;i_auxtime30;" +
                "i_auxtime31;i_auxtime32;i_auxtime33;i_auxtime34;i_auxtime35;i_auxtime36;i_auxtime37;i_auxtime38;i_auxtime39;i_auxtime40;i_auxtime41;i_auxtime42;i_auxtime43;i_auxtime44;i_auxtime45;" +
                "i_auxtime46;i_auxtime47;i_auxtime48;i_auxtime49;i_auxtime50;i_auxtime51;i_auxtime52;i_auxtime53;i_auxtime54;i_auxtime55;i_auxtime56;i_auxtime57;i_auxtime58;i_auxtime59;i_auxtime60;" +
                "i_auxtime61;i_auxtime62;i_auxtime63;i_auxtime64;i_auxtime65;i_auxtime66;i_auxtime67;i_auxtime68;i_auxtime69;i_auxtime70;i_auxtime71;i_auxtime72;i_auxtime73;i_auxtime74;i_auxtime75;" +
                "i_auxtime76;i_auxtime77;i_auxtime78;i_auxtime79;i_auxtime80;i_auxtime81;i_auxtime82;i_auxtime83;i_auxtime84;i_auxtime85;i_auxtime86;i_auxtime87;i_auxtime88;i_auxtime89;i_auxtime90;" +
                "i_auxtime91;i_auxtime92;i_auxtime93;i_auxtime94;i_auxtime95;i_auxtime96;i_auxtime97;i_auxtime98;i_auxtime99;icrpullcalls;icrpulltime;icrpullringcalls;redirectcalls;partial_archive"
                );

            //Aqui quebramos a linha para que os próximos dados sejam escritos na linha seguinte
            x.Write(linha);

            //Aqui: enquando a variável dr (Data Reader) estiver lendo os dados:
            while (dr.Read())
            {
                //Aqui escrevemos no arquivo txt os dados do campo "row_date" da linha atual
                // Utilizamos o método ToString() para formatar a data como "yyyy-MM-dd" que é o formato padrão para a maioria dos SGBD's do mercado, caso queira importar estes dados
                x.Write(string.Concat(Convert.ToDateTime(dr["row_date"].ToString()).ToString("yyyy-MM-dd"), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "starttime" da linha atual
                x.Write(string.Concat(dr["starttime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "starttime_utc" da linha atual
                // Utilizamos o método DateTimeOffset.FromUnixTimeSeconds() para converter em UTC milissegundos
                // Em seguida utilizamos o método ToLocalTime() para converter no formato de Data/Hora local
                // Após utilizamos o método ToString() para deixar no formato "yyyy-MM-dd HH:mm:ss" que é o formato DateTime/TimeStamp para a maioria dos SGBD's do mercado, caso queira importar estes dados
                x.Write(string.Concat(DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(dr["starttime_utc"].ToString())).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "intrvl" da linha atual
                x.Write(string.Concat(dr["intrvl"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acd" da linha atual
                x.Write(string.Concat(dr["acd"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acd" da linha atual
                x.Write(string.Concat(dr["split"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "tenant" da linha atual
                x.Write(string.Concat(dr["tenant"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_stafftime" da linha atual
                x.Write(string.Concat(dr["i_stafftime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_availtime" da linha atual
                x.Write(string.Concat(dr["i_availtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acdtime" da linha atual
                x.Write(string.Concat(dr["i_acdtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acwtime" da linha atual
                x.Write(string.Concat(dr["i_acwtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acwouttime" da linha atual
                x.Write(string.Concat(dr["i_acwouttime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acwintime" da linha atual
                x.Write(string.Concat(dr["i_acwintime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime" da linha atual
                x.Write(string.Concat(dr["i_auxtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime" da linha atual
                x.Write(string.Concat(dr["i_auxouttime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxintime" da linha atual
                x.Write(string.Concat(dr["i_auxintime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_othertime" da linha atual
                x.Write(string.Concat(dr["i_othertime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "maxstaffed" da linha atual
                x.Write(string.Concat(dr["maxstaffed"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwincalls" da linha atual
                x.Write(string.Concat(dr["acwincalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwintime" da linha atual
                x.Write(string.Concat(dr["acwintime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxincalls" da linha atual
                x.Write(string.Concat(dr["auxincalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxintime" da linha atual
                x.Write(string.Concat(dr["auxintime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwoutcalls" da linha atual
                x.Write(string.Concat(dr["acwoutcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwouttime" da linha atual
                x.Write(string.Concat(dr["acwouttime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwouttime" da linha atual
                x.Write(string.Concat(dr["acwoutoffcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwoutofftime" da linha atual
                x.Write(string.Concat(dr["acwoutofftime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwoutadjcalls" da linha atual
                x.Write(string.Concat(dr["acwoutadjcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxoutcalls" da linha atual
                x.Write(string.Concat(dr["auxoutcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxouttime" da linha atual
                x.Write(string.Concat(dr["auxouttime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxouttime" da linha atual
                x.Write(string.Concat(dr["auxoutoffcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxoutofftime" da linha atual
                x.Write(string.Concat(dr["auxoutofftime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "auxoutadjcalls" da linha atual
                x.Write(string.Concat(dr["auxoutadjcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event1" da linha atual
                x.Write(string.Concat(dr["event1"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event2" da linha atual
                x.Write(string.Concat(dr["event2"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event3" da linha atual
                x.Write(string.Concat(dr["event3"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event4" da linha atual
                x.Write(string.Concat(dr["event4"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event5" da linha atual
                x.Write(string.Concat(dr["event5"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event6" da linha atual
                x.Write(string.Concat(dr["event6"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event7" da linha atual
                x.Write(string.Concat(dr["event7"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event8" da linha atual
                x.Write(string.Concat(dr["event8"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "event9" da linha atual
                x.Write(string.Concat(dr["event9"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "assists" da linha atual
                x.Write(string.Concat(dr["assists"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "inflowcalls" da linha atual
                x.Write(string.Concat(dr["inflowcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls" da linha atual
                x.Write(string.Concat(dr["acdcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "anstime" da linha atual
                x.Write(string.Concat(dr["anstime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdtime" da linha atual
                x.Write(string.Concat(dr["acdtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acwtime" da linha atual
                x.Write(string.Concat(dr["acwtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "o_acdcalls" da linha atual
                x.Write(string.Concat(dr["o_acdcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "o_acdtime" da linha atual
                x.Write(string.Concat(dr["o_acdtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "o_acwtime" da linha atual
                x.Write(string.Concat(dr["o_acwtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls1" da linha atual
                x.Write(string.Concat(dr["acdcalls1"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls2" da linha atual
                x.Write(string.Concat(dr["acdcalls2"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls3" da linha atual
                x.Write(string.Concat(dr["acdcalls3"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls4" da linha atual
                x.Write(string.Concat(dr["acdcalls4"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls5" da linha atual
                x.Write(string.Concat(dr["acdcalls5"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls6" da linha atual
                x.Write(string.Concat(dr["acdcalls6"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls7" da linha atual
                x.Write(string.Concat(dr["acdcalls7"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls8" da linha atual
                x.Write(string.Concat(dr["acdcalls8"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls9" da linha atual
                x.Write(string.Concat(dr["acdcalls9"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls10" da linha atual
                x.Write(string.Concat(dr["acdcalls10"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "backupcalls" da linha atual
                x.Write(string.Concat(dr["backupcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "holdcalls" da linha atual
                x.Write(string.Concat(dr["holdcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "holdtime" da linha atual
                x.Write(string.Concat(dr["holdtime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "holdabncalls" da linha atual
                x.Write(string.Concat(dr["holdabncalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "transferred" da linha atual
                x.Write(string.Concat(dr["transferred"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "conference" da linha atual
                x.Write(string.Concat(dr["conference"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls" da linha atual
                x.Write(string.Concat(dr["abncalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls" da linha atual
                x.Write(string.Concat(dr["abntime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls1" da linha atual
                x.Write(string.Concat(dr["abncalls1"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls2" da linha atual
                x.Write(string.Concat(dr["abncalls2"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls3" da linha atual
                x.Write(string.Concat(dr["abncalls3"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls4" da linha atual
                x.Write(string.Concat(dr["abncalls4"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls5" da linha atual
                x.Write(string.Concat(dr["abncalls5"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls6" da linha atual
                x.Write(string.Concat(dr["abncalls6"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls7" da linha atual
                x.Write(string.Concat(dr["abncalls7"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls8" da linha atual
                x.Write(string.Concat(dr["abncalls8"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls9" da linha atual
                x.Write(string.Concat(dr["abncalls9"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abncalls10" da linha atual
                x.Write(string.Concat(dr["abncalls10"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "dequecalls" da linha atual
                x.Write(string.Concat(dr["dequecalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "dequetime" da linha atual
                x.Write(string.Concat(dr["dequetime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "busycalls" da linha atual
                x.Write(string.Concat(dr["busycalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "busytime" da linha atual
                x.Write(string.Concat(dr["busytime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "disccalls" da linha atual
                x.Write(string.Concat(dr["disccalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "disctime" da linha atual
                x.Write(string.Concat(dr["disctime"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "outflowcalls" da linha atual
                x.Write(string.Concat(dr["outflowcalls"].ToString(), ';')); // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "outflowtime" da linha atual
                x.Write(string.Concat(dr["outflowtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "interflowcalls" da linha atual
                x.Write(string.Concat(dr["interflowcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "lowcalls" da linha atual
                x.Write(string.Concat(dr["lowcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "medcalls" da linha atual
                x.Write(string.Concat(dr["medcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "highcalls" da linha atual
                x.Write(string.Concat(dr["highcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "topcalls" da linha atual
                x.Write(string.Concat(dr["topcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acceptable" da linha atual
                x.Write(string.Concat(dr["acceptable"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "servicelevel" da linha atual
                x.Write(string.Concat(dr["servicelevel"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period1" da linha atual
                x.Write(string.Concat(dr["period1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period2" da linha atual
                x.Write(string.Concat(dr["period2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period3" da linha atual
                x.Write(string.Concat(dr["period3"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period4" da linha atual
                x.Write(string.Concat(dr["period4"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period5" da linha atual
                x.Write(string.Concat(dr["period5"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period6" da linha atual
                x.Write(string.Concat(dr["period6"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period7" da linha atual
                x.Write(string.Concat(dr["period7"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period8" da linha atual
                x.Write(string.Concat(dr["period8"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "period9" da linha atual
                x.Write(string.Concat(dr["period9"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "maxinqueue" da linha atual
                x.Write(string.Concat(dr["maxinqueue"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "maxocwtime" da linha atual
                x.Write(string.Concat(dr["maxocwtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "callsoffered" da linha atual
                x.Write(string.Concat(dr["callsoffered"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "periodchg" da linha atual
                x.Write(string.Concat(dr["periodchg"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "svclevelchg" da linha atual
                x.Write(string.Concat(dr["svclevelchg"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_ringtime" da linha atual
                x.Write(string.Concat(dr["i_ringtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "ringtime" da linha atual
                x.Write(string.Concat(dr["ringtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "ringcalls" da linha atual
                x.Write(string.Concat(dr["ringcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "abnringcalls" da linha atual
                x.Write(string.Concat(dr["abnringcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "o_abncalls" da linha atual
                x.Write(string.Concat(dr["o_abncalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "o_othercalls" da linha atual
                x.Write(string.Concat(dr["o_othercalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "da_acwincalls" da linha atual
                x.Write(string.Concat(dr["da_acwincalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "da_acwintime" da linha atual
                x.Write(string.Concat(dr["da_acwintime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "da_acwocalls" da linha atual
                x.Write(string.Concat(dr["da_acwocalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "da_acwotime" da linha atual
                x.Write(string.Concat(dr["da_acwotime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "noansredir" da linha atual
                x.Write(string.Concat(dr["noansredir"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "incomplete" da linha atual
                x.Write(string.Concat(dr["incomplete"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdauxoutcalls" da linha atual
                x.Write(string.Concat(dr["acdauxoutcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acdaux_outtime" da linha atual
                x.Write(string.Concat(dr["i_acdaux_outtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acdauxintime" da linha atual
                x.Write(string.Concat(dr["i_acdauxintime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acdothertime" da linha atual
                x.Write(string.Concat(dr["i_acdothertime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "phantomabns" da linha atual
                x.Write(string.Concat(dr["phantomabns"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "othercalls" da linha atual
                x.Write(string.Concat(dr["othercalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "othertime" da linha atual
                x.Write(string.Concat(dr["othertime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "slvlabns" da linha atual
                x.Write(string.Concat(dr["slvlabns"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "slvloutflows" da linha atual
                x.Write(string.Concat(dr["slvloutflows"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_arrived" da linha atual
                x.Write(string.Concat(dr["i_arrived"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime0" da linha atual
                x.Write(string.Concat(dr["i_auxtime0"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime1" da linha atual
                x.Write(string.Concat(dr["i_auxtime1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime2" da linha atual
                x.Write(string.Concat(dr["i_auxtime2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime3" da linha atual
                x.Write(string.Concat(dr["i_auxtime3"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime4" da linha atual
                x.Write(string.Concat(dr["i_auxtime4"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime5" da linha atual
                x.Write(string.Concat(dr["i_auxtime5"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime6" da linha atual
                x.Write(string.Concat(dr["i_auxtime6"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime7" da linha atual
                x.Write(string.Concat(dr["i_auxtime7"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime8" da linha atual
                x.Write(string.Concat(dr["i_auxtime8"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime9" da linha atual
                x.Write(string.Concat(dr["i_auxtime9"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_da_acdtime" da linha atual
                x.Write(string.Concat(dr["i_da_acdtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_da_acwtime" da linha atual
                x.Write(string.Concat(dr["i_da_acwtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_tavailtime" da linha atual
                x.Write(string.Concat(dr["i_tavailtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_tauxtime" da linha atual
                x.Write(string.Concat(dr["i_tauxtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "maxtop" da linha atual
                x.Write(string.Concat(dr["maxtop"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_normtime" da linha atual
                x.Write(string.Concat(dr["i_normtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_ol1time" da linha atual
                x.Write(string.Concat(dr["i_ol1time"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_ol2time" da linha atual
                x.Write(string.Concat(dr["i_ol2time"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_tothertime" da linha atual
                x.Write(string.Concat(dr["i_tothertime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "max_tot_percents" da linha atual
                x.Write(string.Concat(dr["max_tot_percents"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls_r1" da linha atual
                x.Write(string.Concat(dr["acdcalls_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "acdcalls_r2" da linha atual
                x.Write(string.Concat(dr["acdcalls_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acdtime_r1" da linha atual
                x.Write(string.Concat(dr["i_acdtime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acdtime_r2" da linha atual
                x.Write(string.Concat(dr["i_acdtime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acwtime_r1" da linha atual
                x.Write(string.Concat(dr["i_acwtime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_acwtime_r2" da linha atual
                x.Write(string.Concat(dr["i_acwtime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_ringtime_r1" da linha atual
                x.Write(string.Concat(dr["i_ringtime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_ringtime_r2" da linha atual
                x.Write(string.Concat(dr["i_ringtime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_othertime_r1" da linha atual
                x.Write(string.Concat(dr["i_othertime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_othertime_r2" da linha atual
                x.Write(string.Concat(dr["i_othertime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime_r1" da linha atual
                x.Write(string.Concat(dr["i_auxtime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime_r2" da linha atual
                x.Write(string.Concat(dr["i_auxtime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_otherstbytime_r1" da linha atual
                x.Write(string.Concat(dr["i_otherstbytime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_otherstbytime_r2" da linha atual
                x.Write(string.Concat(dr["i_otherstbytime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxstbytime_r1" da linha atual
                x.Write(string.Concat(dr["i_auxstbytime_r1"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxstbytime_r2" da linha atual
                x.Write(string.Concat(dr["i_auxstbytime_r2"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_behindtime" da linha atual
                x.Write(string.Concat(dr["i_behindtime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_autoreservetime" da linha atual
                x.Write(string.Concat(dr["i_autoreservetime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetpercent" da linha atual
                x.Write(string.Concat(dr["targetpercent"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetpctchg" da linha atual
                x.Write(string.Concat(dr["targetpctchg"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetseconds" da linha atual
                x.Write(string.Concat(dr["targetseconds"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetsecchg" da linha atual
                x.Write(string.Concat(dr["targetsecchg"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetacdcalls" da linha atual
                x.Write(string.Concat(dr["targetacdcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetabns" da linha atual
                x.Write(string.Concat(dr["targetabns"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "targetoutflows" da linha atual
                x.Write(string.Concat(dr["targetoutflows"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "intrdeliveries" da linha atual
                x.Write(string.Concat(dr["intrdeliveries"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "agsurpdeliveries" da linha atual
                x.Write(string.Concat(dr["agsurpdeliveries"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "agsurpprefcalls" da linha atual
                x.Write(string.Concat(dr["agsurpprefcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "agsurpnprefcalls" da linha atual
                x.Write(string.Concat(dr["agsurpnprefcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "callsurpdeliveries" da linha atual
                x.Write(string.Concat(dr["callsurpdeliveries"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime10" da linha atual
                x.Write(string.Concat(dr["i_auxtime10"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime11" da linha atual
                x.Write(string.Concat(dr["i_auxtime11"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime12" da linha atual
                x.Write(string.Concat(dr["i_auxtime12"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime13" da linha atual
                x.Write(string.Concat(dr["i_auxtime13"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime14" da linha atual
                x.Write(string.Concat(dr["i_auxtime14"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime15" da linha atual
                x.Write(string.Concat(dr["i_auxtime15"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime16" da linha atual
                x.Write(string.Concat(dr["i_auxtime16"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime17" da linha atual
                x.Write(string.Concat(dr["i_auxtime17"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime18" da linha atual
                x.Write(string.Concat(dr["i_auxtime18"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime19" da linha atual
                x.Write(string.Concat(dr["i_auxtime19"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime20" da linha atual
                x.Write(string.Concat(dr["i_auxtime20"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime21" da linha atual
                x.Write(string.Concat(dr["i_auxtime21"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime22" da linha atual
                x.Write(string.Concat(dr["i_auxtime22"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime23" da linha atual
                x.Write(string.Concat(dr["i_auxtime23"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime24" da linha atual
                x.Write(string.Concat(dr["i_auxtime24"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime25" da linha atual
                x.Write(string.Concat(dr["i_auxtime25"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime26" da linha atual
                x.Write(string.Concat(dr["i_auxtime26"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime27" da linha atual
                x.Write(string.Concat(dr["i_auxtime27"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime28" da linha atual
                x.Write(string.Concat(dr["i_auxtime28"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime29" da linha atual
                x.Write(string.Concat(dr["i_auxtime29"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime30" da linha atual
                x.Write(string.Concat(dr["i_auxtime30"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime31" da linha atual
                x.Write(string.Concat(dr["i_auxtime31"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime32" da linha atual
                x.Write(string.Concat(dr["i_auxtime32"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime33" da linha atual
                x.Write(string.Concat(dr["i_auxtime33"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime34" da linha atual
                x.Write(string.Concat(dr["i_auxtime34"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime35" da linha atual
                x.Write(string.Concat(dr["i_auxtime35"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime36" da linha atual
                x.Write(string.Concat(dr["i_auxtime36"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime37" da linha atual
                x.Write(string.Concat(dr["i_auxtime37"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime38" da linha atual
                x.Write(string.Concat(dr["i_auxtime38"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime39" da linha atual
                x.Write(string.Concat(dr["i_auxtime39"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime40" da linha atual
                x.Write(string.Concat(dr["i_auxtime40"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime41" da linha atual
                x.Write(string.Concat(dr["i_auxtime41"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime42" da linha atual
                x.Write(string.Concat(dr["i_auxtime42"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime43" da linha atual
                x.Write(string.Concat(dr["i_auxtime43"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime44" da linha atual
                x.Write(string.Concat(dr["i_auxtime44"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime45" da linha atual
                x.Write(string.Concat(dr["i_auxtime45"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime46" da linha atual
                x.Write(string.Concat(dr["i_auxtime46"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime47" da linha atual
                x.Write(string.Concat(dr["i_auxtime47"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime48" da linha atual
                x.Write(string.Concat(dr["i_auxtime48"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime49" da linha atual
                x.Write(string.Concat(dr["i_auxtime49"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime50" da linha atual
                x.Write(string.Concat(dr["i_auxtime50"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime51" da linha atual
                x.Write(string.Concat(dr["i_auxtime51"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime52" da linha atual
                x.Write(string.Concat(dr["i_auxtime52"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime53" da linha atual
                x.Write(string.Concat(dr["i_auxtime53"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime54" da linha atual
                x.Write(string.Concat(dr["i_auxtime54"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime55" da linha atual
                x.Write(string.Concat(dr["i_auxtime55"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime56" da linha atual
                x.Write(string.Concat(dr["i_auxtime56"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime57" da linha atual
                x.Write(string.Concat(dr["i_auxtime57"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime58" da linha atual
                x.Write(string.Concat(dr["i_auxtime58"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime59" da linha atual
                x.Write(string.Concat(dr["i_auxtime59"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime60" da linha atual
                x.Write(string.Concat(dr["i_auxtime60"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime61" da linha atual
                x.Write(string.Concat(dr["i_auxtime61"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime62" da linha atual
                x.Write(string.Concat(dr["i_auxtime62"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime63" da linha atual
                x.Write(string.Concat(dr["i_auxtime63"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime64" da linha atual
                x.Write(string.Concat(dr["i_auxtime64"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime65" da linha atual
                x.Write(string.Concat(dr["i_auxtime65"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime66" da linha atual
                x.Write(string.Concat(dr["i_auxtime66"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime67" da linha atual
                x.Write(string.Concat(dr["i_auxtime67"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime68" da linha atual
                x.Write(string.Concat(dr["i_auxtime68"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime69" da linha atual
                x.Write(string.Concat(dr["i_auxtime69"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime70" da linha atual
                x.Write(string.Concat(dr["i_auxtime70"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime71" da linha atual
                x.Write(string.Concat(dr["i_auxtime71"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime72" da linha atual
                x.Write(string.Concat(dr["i_auxtime72"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime73" da linha atual
                x.Write(string.Concat(dr["i_auxtime73"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime74" da linha atual
                x.Write(string.Concat(dr["i_auxtime74"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime75" da linha atual
                x.Write(string.Concat(dr["i_auxtime75"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime76" da linha atual
                x.Write(string.Concat(dr["i_auxtime76"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime77" da linha atual
                x.Write(string.Concat(dr["i_auxtime77"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime78" da linha atual
                x.Write(string.Concat(dr["i_auxtime78"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime79" da linha atual
                x.Write(string.Concat(dr["i_auxtime79"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime80" da linha atual
                x.Write(string.Concat(dr["i_auxtime80"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime81" da linha atual
                x.Write(string.Concat(dr["i_auxtime81"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime82" da linha atual
                x.Write(string.Concat(dr["i_auxtime82"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime83" da linha atual
                x.Write(string.Concat(dr["i_auxtime83"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime84" da linha atual
                x.Write(string.Concat(dr["i_auxtime84"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime85" da linha atual
                x.Write(string.Concat(dr["i_auxtime85"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime86" da linha atual
                x.Write(string.Concat(dr["i_auxtime86"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime87" da linha atual
                x.Write(string.Concat(dr["i_auxtime87"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime88" da linha atual
                x.Write(string.Concat(dr["i_auxtime88"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime89" da linha atual
                x.Write(string.Concat(dr["i_auxtime89"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime90" da linha atual
                x.Write(string.Concat(dr["i_auxtime90"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime91" da linha atual
                x.Write(string.Concat(dr["i_auxtime91"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime92" da linha atual
                x.Write(string.Concat(dr["i_auxtime92"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime93" da linha atual
                x.Write(string.Concat(dr["i_auxtime93"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime94" da linha atual
                x.Write(string.Concat(dr["i_auxtime94"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime95" da linha atual
                x.Write(string.Concat(dr["i_auxtime95"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime96" da linha atual
                x.Write(string.Concat(dr["i_auxtime96"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime97" da linha atual
                x.Write(string.Concat(dr["i_auxtime97"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime98" da linha atual
                x.Write(string.Concat(dr["i_auxtime98"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "i_auxtime99" da linha atual
                x.Write(string.Concat(dr["i_auxtime99"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "icrpullcalls" da linha atual
                x.Write(string.Concat(dr["icrpullcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "icrpulltime" da linha atual
                x.Write(string.Concat(dr["icrpulltime"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "icrpullringcalls" da linha atual
                x.Write(string.Concat(dr["icrpullringcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "redirectcalls" da linha atual
                x.Write(string.Concat(dr["redirectcalls"].ToString(), ';'));  // Colocamos o ponto e vírgula no final, para servir como delimitador

                //Aqui escrevemos no arquivo txt os dados do campo "partial_archive" da linha atual
                x.Write(dr["partial_archive"].ToString());  // Aqui não colocamos o ponto e vírgula no final, porque é o final da linha atual

                //Aqui quebramos a linha para que os próximos dados sejam escritos na linha seguinte
                x.Write(linha);
            }

            Console.WriteLine("Registros gravados com sucesso...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            Console.WriteLine("Salvando e Fechando o Arquivo...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui fechamos o arquivo txt
            x.Close();

            Console.WriteLine("Fechando a conexão com o Informix...");
            Console.WriteLine("...");
            Console.WriteLine(linha);

            //Aqui fechamos o objeto Data Reader
            dr.Close();

            //Aqui fechamos a conexão com o Avaya Informix Database
            conn.Close();

        }
    }
}
