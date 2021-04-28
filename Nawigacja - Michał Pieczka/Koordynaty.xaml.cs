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
using Windows.Services.Maps;

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

        BasicGeoposition pktStart = new BasicGeoposition();
        BasicGeoposition pktKoniec = new BasicGeoposition();

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

            pktStart.Latitude = mojeZGPS.Coordinate.Point.Position.Latitude;
            pktStart.Longitude = mojeZGPS.Coordinate.Point.Position.Longitude;
            DaneGeograficzne.pktStartowy = pktStart;

            tbGPS.Text = "długość: " + mojeZGPS.Coordinate.Point.Position.Longitude.ToString() + "; szerokosc: " + mojeZGPS.Coordinate.Point.Position.Latitude.ToString();
        }
        async private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            var textCelu = tbCel.Text;
            if (textCelu != "")
            {
                Geopoint hintPoint = new Geopoint(pktStart);
                MapLocationFinderResult result =
                    await MapLocationFinder.FindLocationsAsync(textCelu, hintPoint, 3);

                var dl_geogr = result.Locations[0].Point.Position.Longitude;
                var szer_geogr = result.Locations[0].Point.Position.Latitude;

                pktKoniec.Latitude = szer_geogr;
                pktKoniec.Longitude = dl_geogr;

                DaneGeograficzne.pktDocelowy = pktKoniec;

                if (result.Status == MapLocationFinderStatus.Success)
                {
                    tbDlug_cel.Text = "Długość geograficzna: " +
                        dl_geogr;
                    tbSzer_cel.Text = "Szerokość geograficzna: " +
                        szer_geogr;
                }
            }
            else
            {
                tbCel.Text = "Wprowadź poprawne dane lokalizacyjne";
            }
        }

    }
}
