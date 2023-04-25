using AppKW.Services;
using SpreadsheetLight;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using AppKW.Models;

namespace AppKW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEncuestasPage : ContentPage
    {
        Encuestas _encuestasRepository = new Encuestas();
        public ListEncuestasPage()
        {
            InitializeComponent();
            TraeDatos();
        }

        protected override async void OnAppearing()
        {
            var encuestas = await _encuestasRepository.GetAll();
            EncuestasListView.ItemsSource = encuestas;
        }

        //Solo es para generar el Ecxel
        protected async void TraeDatos()
        {
            var Prueba = await _encuestasRepository.GetAll();
            TablaPruebaDescarga.ItemsSource = Prueba;
        }


        private async void ToolBarDescargar_Clicked(object sender, EventArgs e)
        {
           /* EncuestaModel model = EncuestaModel();
            string PathFile = AppDomain.CurrentDomain.BaseDirectory + "ListExcel.xlsx";

            SLDocument slt = new SLDocument();
            DataTable table = new DataTable();

            //Columns 
            table.Columns.Add("Departamento", typeof(string));
            table.Columns.Add("Nombre", typeof(string));
            table.Columns.Add("Pregunta 1", typeof(string));
            table.Columns.Add("Pregunta 2", typeof(string));
            table.Columns.Add("Pregunta 3", typeof(string));
            table.Columns.Add("Pregunta 4", typeof(string));
            table.Columns.Add("Pregunta 5", typeof(string));
            table.Columns.Add("Pregunta 6", typeof(string));
            table.Columns.Add("Pregunta 7", typeof(string));

            //Rows
            table.Rows.Add("TI", "Magda", "si", "no", "si", "aja", "tal vez", "si", "si");
            slt.ImportDataTable(1, 1, table, true);
            slt.SaveAs(PathFile); */

            await DisplayAlert("Excel descargado", "El excel con los resultados se descargo correctamente", "Ok");


        }

    }
}