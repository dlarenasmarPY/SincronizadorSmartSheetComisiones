﻿using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using NLog;
using SincronizadorSmartSheetComisiones.Models.POK;
using Smartsheet.Api;
using Smartsheet.Api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorSmartSheetComisiones.Business
{
    public class ProcesoBusiness
    {
        private string _tokenSmartSheet;
        private long _sheetSmartSheet1;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        #region variables CGI TEST
        private static string usserAPI = ConfigurationManager.AppSettings["usserAPI"];//Convert.ToInt32(ConfigurationManager.AppSettings["usserAPI"]);
        private static string passAPI = ConfigurationManager.AppSettings["passAPI"];
        private const string credentials = "{ \"username\": {1}, \"password\": \"{2}\"}";
        private static string DATA = credentials.Replace("{1}", usserAPI).Replace("{2}", passAPI);//"{ \"username\": 99999999, \"password\": \"t0kTJvp3\"}";
        //private const string usserPass = "99999999:t0kTJvp3";
        //private const string apiKey = "1035e275034dc0b056fe80db53e52f14";
        private static string UrlBase = ConfigurationManager.AppSettings["urlAPI"];//"https: //api-gci-rest.integracionplanok.io/api";
        #endregion
        public ProcesoBusiness(string tokenSmartSheet,long sheetSmartSheet1)
        {
            _tokenSmartSheet = tokenSmartSheet;
            _sheetSmartSheet1 = sheetSmartSheet1;

        }

        #region [Datos SmartSheet]
        public async Task<T> getData<T>(string url) where T : class
        {
            string baseUrl = url;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Authorization", $"Bearer {_tokenSmartSheet}");
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                return JsonConvert.DeserializeObject<T>(data);

                            }
                            else
                            {
                                Console.WriteLine("NO Data----------");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
                return null;
            }
        }

        public void getDataSmartSheetSDK()
        {
            SmartsheetClient smartsheet = new SmartsheetBuilder().SetAccessToken(_tokenSmartSheet).Build();
            var principalSheet = smartsheet.SheetResources.GetSheet(_sheetSmartSheet1,null,null,null,null,null,null,null);
            //principalSheet.GetColumnById()
            foreach (var item in principalSheet.Rows)
            {
                // se recorren todas las filas de la hoja.
                if (item.Id == 0L)
                {

                }
            }
            
        }
        #endregion

        #region [Datos PlanOK]
        public void getDataPlanOK()
        {
            
            #region API - CGI PARAMETROS
            string url = ConfigurationManager.AppSettings["urlAPI"] ?? string.Empty;
            _logger.Info($"[Parametros] urlAPI {url}");
            string key = ConfigurationManager.AppSettings["keyAPI"] ?? string.Empty;
            _logger.Info($"[Parametros] KeyAPI {key}");
            string method = "/login?apikey=" + key;
            #endregion

            #region [metodos]
            string method2 = "/negocios";
            string method3 = "/proyectos";
            string method4 = "/escritura";
            string method5 = "/promesa";
            string method6 = "/promesas/cancelar";
            string method7 = "/reservas/cancelar";
            string method8 = "/clientes/naturales";
            string method9 = "/negocios/{id}/reservar-escritura";

            string method10 = "/negocios/{id}/reservas";
            string method11 = "/negocios/{id}/promesas";
            string method12 = "/negocios/{id}/modificar-cliente-reserva";
            string method13 = "/negocios/{id}/modificar-cliente-promesa";
            string method14 = "/negocios/{id}/agregar-producto-secundario-o-adicional-reserva";
            string method15 = "/negocios/{id}/eliminar-producto-secundario-o-adicional-reserva";
            string method16 = "/negocios/{id}/agregar-producto-secundario-o-adicional-promesa";
            string method17 = "/negocios/{id}/eliminar-producto-secundario-o-adicional-promesa";
            #endregion

            var loginRequest = LoginMethodPost(url, method, string.Empty);
            var token = loginRequest.token;

        }


        public LoginResponseModel LoginMethodPost(string url, string method, string parameters)
        {
            var urlMethod = url + method;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlMethod);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = DATA.Length;
            var loginResponse = new LoginResponseModel();
            //request.Headers.Add();

            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(DATA);
            }

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    loginResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponseModel>(response);
                    Console.Out.WriteLine(response);
                    //loginResponse = response;
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }

            return loginResponse;
        }
        #endregion

        #region [Datos Fin700]
        public static async Task<object?> getDataFIN7(string url)
        {
            string baseUrl = url;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if (data != null)
                            {
                                return JsonConvert.DeserializeObject<object>(data);

                            }
                            else
                            {
                                Console.WriteLine("NO Data----------");
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
                return null;
            }
        }
        #endregion

    }
}