using SincronizadorSmartSheetComisiones.Business;
using Smartsheet.Api.Models;
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
        private static readonly long Sheet = 508533100832644;
        private static readonly string Workspace = "1230173090670468";
        private static readonly string folder = "8337434724329348";
        #endregion

        #region [constantes Fin7]
        private static readonly string BaseURLFin7 = "http://172.18.10.23:8888/";
        #endregion

        static async Task Main(string[] args)
        {
            
            var objProcesoBusiness = new ProcesoBusiness(Token, Sheet);
 
            
            #region [Proceso obtención de datos]

            // se obtiene información de la hoja con su ID
            var rowsSmartSheet = objProcesoBusiness.getDataSmartSheetSDK();
        
            #region [Se obtienen los datos desde FIN700]

            var dataComision = await objProcesoBusiness.GetDataComisiones("https://localhost:7012/SmartSheet/GetSmartSheetComisiones", new Models.Fin7.SmartSheetComisionesRequestModel { FechaDesde="2022-01-01",FechaHasta="2022-02-01",Zona=23 });
            dataComision.ForEach(dc =>
            {
                var rows = new List<Row>();
                 var cells = new List<Cell>();
                //aqui se empieza a trabajar con los datos de fin700

                var obj1 = objProcesoBusiness.InsertSmartSheet(2321872479643524, dc.PtprId).ToList();
                
                var obj2 = objProcesoBusiness.InsertSmartSheet(6825472107014020, dc.CreNombre).ToList();
                var obj3 = objProcesoBusiness.InsertSmartSheet(1195972572800900, dc.EntRazonSocial).ToList();
                var obj4 = objProcesoBusiness.InsertSmartSheet(3447772386486148, dc.CarOfeFecha).ToList();
                var obj5 = objProcesoBusiness.InsertSmartSheet(7951372013856644, dc.PrecioVentaInm).ToList();
                var obj6 = objProcesoBusiness.InsertSmartSheet(7155859245033348, dc.FechaPromesa).ToList();
                var obj7 = objProcesoBusiness.InsertSmartSheet(1526359710820228, dc.PrecioVentaInm).ToList();
                var obj8 = objProcesoBusiness.InsertSmartSheet(6029959338190724, dc.CarOfeNumInterno).ToList();
                var obj9 = objProcesoBusiness.InsertSmartSheet(3778159524505476, dc.EstadoActual).ToList();
                var obj10 = objProcesoBusiness.InsertSmartSheet(8281759151875972, string.Empty).ToList();
                cells.AddRange(obj1);
                cells.AddRange(obj2);
                cells.AddRange(obj3);
                cells.AddRange(obj4);
                cells.AddRange(obj5);
                cells.AddRange(obj6);
                cells.AddRange(obj7);
                cells.AddRange(obj8);
                cells.AddRange(obj9);
                cells.AddRange(obj10);



                Smartsheet.Api.Models.Row rowA = new Smartsheet.Api.Models.Row
                {
                    Cells = cells,
                    ToTop= true
                 };
                rows.Add(rowA);

                objProcesoBusiness.AddRowSmartSheet(rows);



            });
            #endregion

            #endregion
        }
    }
}