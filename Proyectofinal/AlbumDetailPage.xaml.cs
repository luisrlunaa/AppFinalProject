using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Proyectofinal
{
    public partial class AlbumDetailPage : ContentPage
    {
        private const string URL = @"https://jsonplaceholder.typicode.com/photos?albumId={0}";
        private int AlbumId;
        private HttpClient httpClient = new HttpClient();

        #region Properties

        public ObservableCollection<Photo> Photos { get; set; }

        #endregion Properties

        public AlbumDetailPage(int albumId)
        {
            InitializeComponent();

            AlbumId = albumId;
            Photos = new ObservableCollection<Photo>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //TODO: Implementar el codigo para descargar la lista de fotos de un album
            // Usar la constante URL declarada al inicio de la clase y reemplazar {0}
            // Por la variable albumId

            IsBusy = true;
            var url = string.Format(URL, AlbumId);
            if(url != null)
            {
                var respuestas = await httpClient.GetAsync(url);
                if (respuestas.IsSuccessStatusCode)
                {
                    var jsonResponse = await respuestas.Content.ReadAsStringAsync();
                    try
                    {
                        var fotos = JsonConvert.DeserializeObject<ObservableCollection<Photo>>(jsonResponse);
                        if (fotos.Count > 0)
                        {
                            Photos.Clear();
                            foreach (var foto in fotos)
                            {
                                Photos.Add(foto);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Error", "No se pudo descargar la lista de Fotos", "Ok");
                    }
                }
            }
            IsBusy = false;
        }
    }
}
