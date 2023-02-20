using SincronizadorSmartSheetComisiones.Business;
using SpreadsheetLight;
using System.Net;
using System.Net.Http.Headers;



namespace SincronizadorSmartSheetComisiones
{
    public class Program
    {
        #region [constantes smartsheet]
        private static readonly string Token = "pG3jS2R7ZTBOH5nXz6wmXXpAeZsz6YHMkIt71";
        private static readonly string BaseURL = "https://api.smartsheet.com/2.0/";
        private static readonly long Sheet = 3879278040377220;
        private static readonly string Workspace = "1230173090670468";
        private static readonly string folder = "8337434724329348";
        #endregion

        #region [constantes Fin7]
        private static readonly string BaseURLFin7 = "http://172.18.10.23:8888/";
        #endregion

        static async Task Main(string[] args)
        {
            var objProcesoBusiness = new ProcesoBusiness(Token,Sheet);

            #region [Proceso obtención de datos]

            // se obtiene información de la hoja con su ID
            objProcesoBusiness.getDataSmartSheetSDK();
            await objProcesoBusiness.getData<List<object>>($"{BaseURL}sheets/{Sheet}");
            


            #endregion
        }
    }
}