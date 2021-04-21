using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Geolocation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace Nawigacja___Michał_Pieczka
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class Koordynaty : Page
    {
        public Koordynaty()
        {
            this.InitializeComponent();
            gdzieJaNaMapie();
        }

        private void btnPowrot_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        async void gdzieJaNaMapie()
        {
            Geolocator mojGPS = new Geolocator()
            {
                DesiredAccuracy = PositionAccuracy.High
            };
            Geoposition mojeZGPS = await mojGPS.GetGeopositionAsync();
            tbGPS.Text = "długość: " + mojeZGPS.Coordinate.Point.Position.Longitude + "; szerokosc: " + mojeZGPS.Coordinate.Point.Position.Latitude;
        }
    }
}
