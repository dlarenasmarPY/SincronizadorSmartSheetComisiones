using SincronizadorSmartSheetComisiones.Business;
using SincronizadorSmartSheetComisiones.Models;
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

        #region [Constantes POK]
        private const string urlPOK = "https://api-gci-rest.integracionplanok.io/api";
        #endregion

        static async Task Main(string[] args)
        {
            //SE OBTIENEN DATOS DESDE SMARTSHEET (POR EL MOMENTO NO SE UTILIZA)
            var objProcesoBusiness = new ProcesoBusiness(Token, Sheet);

            #region [Proceso obtención de datos]

            // se obtiene información de la hoja con su ID
            var rowsSmartSheet = objProcesoBusiness.getDataSmartSheetSDK();
            var token = objProcesoBusiness.getDataPlanOK();
            #region [Se obtienen los datos desde FIN700 y se agregan a smartsheet]

            // se busca en api los datos con sus filtros
            var dataComision = await objProcesoBusiness.GetDataComisiones("https://localhost:7012/SmartSheet/GetSmartSheetComisiones", new Models.Fin7.SmartSheetComisionesRequestModel { FechaDesde="2022-01-01",FechaHasta="2022-02-01",Zona=23 });
            // se recorren los datos obtenidos desde apiFIN7
            foreach (var dc in dataComision)
            {
                var listaExcluida = new List<ParametroModel>();
                var dataRut = dc.EntRut;
                var dataCentroCosto = dc.CreCodigo;
                // se filtra por el rut que se pregunte y centroCosto
                var dataFiltrada = dataComision.Where(dc => dc.EntRut == dataRut && dataCentroCosto == dc.CreCodigo).ToList();


                //pregunto si hay datos y si el rut no se encuentra en la lista excluida
                if (dataFiltrada.Any() && !listaExcluida.Where(le => le.Rut != dc.EntRut && le.CentroCosto != dc.CreCodigo).Any())
                {
                    // se agrega rut a listaExcluida
                    listaExcluida.Add(new ParametroModel { Rut = dc.EntRut, CentroCosto = dc.CreCodigo });

                    //se ordena data por fechaPromesa
                    var dataOrdenada = dataFiltrada.OrderBy(df => df.FechaPromesa).ToList();
                    var rowNumber = 0;
                    foreach (var item in dataOrdenada)
                    {
                        //contador de negocio 
                        rowNumber++;
                        //Variables para agregar y setear filas y columnas
                        var rows = new List<Row>();
                        var cells = new List<Cell>();

                        // se agregan datos a celdas (solo en memoria)
                        var obj1 = objProcesoBusiness.InsertSmartSheet(2321872479643524, item.PtprId).ToList();
                        var obj2 = objProcesoBusiness.InsertSmartSheet(6825472107014020, item.CreNombre).ToList();
                        var obj3 = objProcesoBusiness.InsertSmartSheet(1195972572800900, item.EntRut).ToList();
                        var obj4 = objProcesoBusiness.InsertSmartSheet(3447772386486148, item.CarOfeFecha).ToList();
                        var obj5 = objProcesoBusiness.InsertSmartSheet(7951372013856644, item.PrecioVentaInm).ToList();
                        var obj6 = objProcesoBusiness.InsertSmartSheet(7155859245033348, item.FechaPromesa).ToList();
                        var obj7 = objProcesoBusiness.InsertSmartSheet(1526359710820228, item.PrecioVentaInm).ToList();
                        var obj8 = objProcesoBusiness.InsertSmartSheet(6029959338190724, item.CarOfeNumInterno).ToList();
                        var obj9 = objProcesoBusiness.InsertSmartSheet(3778159524505476, item.EstadoActual).ToList();                        
                        var obj11 = objProcesoBusiness.InsertSmartSheet(5699572200171396, item.EntRazonSocial).ToList();
                        var obj13 = objProcesoBusiness.InsertSmartSheet(7926838204360580, rowNumber.ToString()).ToList();
                        
                        // se obtiene vendedor
                        var pivotRut = item.EntRut.Split('-');
                        var rut = int.Parse(pivotRut[0]);

                        //obtengo el vendedor desde POK
                        var obj = objProcesoBusiness.getVendedor(new Models.POK.VendedorByClienteRequestModel { identificador = string.Empty, valorIdentificador = rut }, token, urlPOK,"Natural");
                        if (obj.Any() && obj != null)
                        {
                            var rutVendedor = obj.FirstOrDefault().vendedor.vendedor_reserva_reciente != null ? obj.FirstOrDefault().vendedor.vendedor_reserva_reciente.rut_largo_vendedor_reserva:string.Empty; 
                            var obj12 = objProcesoBusiness.InsertSmartSheet(8281759151875972, rutVendedor).ToList();
                            cells.AddRange(obj12);
                        }

                        // se agrega a variable que guarda las columnas (memoria)
                        cells.AddRange(obj1);
                        cells.AddRange(obj2);
                        cells.AddRange(obj3);
                        cells.AddRange(obj4);
                        cells.AddRange(obj5);
                        cells.AddRange(obj6);
                        cells.AddRange(obj7);
                        cells.AddRange(obj8);
                        cells.AddRange(obj9);
                        cells.AddRange(obj11);
                        cells.AddRange(obj13);

                        // se pasan las columnas a un objeto que arma la fila completa (en memoria)
                        Smartsheet.Api.Models.Row rowA = new Smartsheet.Api.Models.Row
                        {
                            Cells = cells,
                            ToTop = true
                        };
                        rows.Add(rowA);

                        // se agrega fila a SmartSheet
                        objProcesoBusiness.AddRowSmartSheet(rows);
                        
                    }


                }

                Console.WriteLine("Guarda fila");

            }

            Console.WriteLine("Termino proceso");
            #endregion

            #endregion
        }
    }
}