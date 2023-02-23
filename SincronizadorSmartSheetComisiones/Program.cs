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
        
            #region [Se obtienen los datos desde FIN700 y se agregan a smartsheet]

            // se busca en api los datos con sus filtros
            var dataComision = await objProcesoBusiness.GetDataComisiones("https://localhost:7012/SmartSheet/GetSmartSheetComisiones", new Models.Fin7.SmartSheetComisionesRequestModel { FechaDesde="2022-01-01",FechaHasta="2022-02-01",Zona=23 });
            dataComision.ForEach(dc =>
            {
                // se recorren los datos obtenidos desde apiFIN7

                //Variables para agregar y setear filas y columnas
                var rows = new List<Row>();
                var cells = new List<Cell>();

                // se agregan datos a celdas (solo en memoria)
                var obj1 = objProcesoBusiness.InsertSmartSheet(2321872479643524, dc.PtprId).ToList();
                var obj2 = objProcesoBusiness.InsertSmartSheet(6825472107014020, dc.CreNombre).ToList();
                var obj3 = objProcesoBusiness.InsertSmartSheet(1195972572800900, dc.EntRut).ToList();
                var obj4 = objProcesoBusiness.InsertSmartSheet(3447772386486148, dc.CarOfeFecha).ToList();
                var obj5 = objProcesoBusiness.InsertSmartSheet(7951372013856644, dc.PrecioVentaInm).ToList();
                var obj6 = objProcesoBusiness.InsertSmartSheet(7155859245033348, dc.FechaPromesa).ToList();
                var obj7 = objProcesoBusiness.InsertSmartSheet(1526359710820228, dc.PrecioVentaInm).ToList();
                var obj8 = objProcesoBusiness.InsertSmartSheet(6029959338190724, dc.CarOfeNumInterno).ToList();
                var obj9 = objProcesoBusiness.InsertSmartSheet(3778159524505476, dc.EstadoActual).ToList();
                var obj10 = objProcesoBusiness.InsertSmartSheet(8281759151875972, string.Empty).ToList();
                var obj11 = objProcesoBusiness.InsertSmartSheet(5699572200171396, dc.EntRazonSocial).ToList();

                // se agrega a variable que guarda las columnas
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
                cells.AddRange(obj11);


                // se pasan las columnas a un objeto que arma la fila completa (en memoria)
                Smartsheet.Api.Models.Row rowA = new Smartsheet.Api.Models.Row
                {
                    Cells = cells,
                    ToTop= true
                 };
                rows.Add(rowA);

                // se agrega fila a SmartSheet
                objProcesoBusiness.AddRowSmartSheet(rows);



            });
            #endregion

            #endregion
        }
    }
}