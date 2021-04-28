using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Geolocation;
using GeoCoordinatePortable;
using Windows.Services.Maps;
using Windows.UI;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace Nawigacja___Michał_Pieczka
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DaneGeograficzne.pktStartowy.Latitude != 0 && DaneGeograficzne.pktDocelowy.Longitude != 0)
            {
                Geopoint pktStartowy = new Geopoint(DaneGeograficzne.pktStartowy);
                Geopoint pktDocelowy = new Geopoint(DaneGeograficzne.pktDocelowy);

                MapIcon znacznikStart = new MapIcon();
                znacznikStart.Location = pktStartowy;
                znacznikStart.Title = "Tu jestem";

                MapIcon znacznikDocelowa = new MapIcon();
                znacznikDocelowa.Location = pktDocelowy;
                znacznikDocelowa.Title = "Koniec!";

                

                MapPolyline trasaLotem = new MapPolyline();
                trasaLotem.StrokeColor = Windows.UI.Colors.Black;
                trasaLotem.StrokeThickness = 3;
                trasaLotem.StrokeDashed = true;

                var Path = new Geopath(new List<BasicGeoposition>{
                    DaneGeograficzne.pktStartowy,
                    DaneGeograficzne.pktDocelowy
                });
                trasaLotem.Path = Path;

                GeoCoordinate pin1 = new GeoCoordinate(DaneGeograficzne.pktStartowy.Latitude, DaneGeograficzne.pktStartowy.Longitude);
                GeoCoordinate pin2 = new GeoCoordinate(DaneGeograficzne.pktDocelowy.Latitude, DaneGeograficzne.pktDocelowy.Longitude);

                double distanceBetween = pin1.GetDistanceTo(pin2) / 1000;
                
                MapIcon znacznikDystans = new MapIcon();
                znacznikDystans.Location = pktDocelowy;
                znacznikDystans.Title = string.Format("{0} km", Math.Round(distanceBetween, 2));

                mojaMapa.MapElements.Add(znacznikStart);
                mojaMapa.MapElements.Add(znacznikDocelowa);
                mojaMapa.MapElements.Add(trasaLotem);
                mojaMapa.MapElements.Add(znacznikDystans);
                mojaMapa.Center = pktStartowy;

                await mojaMapa.TrySetViewAsync(new Geopoint(DaneGeograficzne.pktStartowy), 8);

                MapRouteFinderResult routeResult =
                 await MapRouteFinder.GetDrivingRouteAsync(
                 new Geopoint(DaneGeograficzne.pktStartowy),
                 new Geopoint(DaneGeograficzne.pktDocelowy),
                 MapRouteOptimization.Time,
                 MapRouteRestrictions.None);

                if (routeResult.Status == MapRouteFinderStatus.Success)
                {
                    // Use the route to initialize a MapRouteView.
                    MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                    viewOfRoute.RouteColor = Colors.Yellow;
                    viewOfRoute.OutlineColor = Colors.Black;

                    // Add the new MapRouteView to the Routes collection
                    // of the MapControl.
                    mojaMapa.Routes.Add(viewOfRoute);

                    // Fit the MapControl to the route.
                    await mojaMapa.TrySetViewBoundsAsync(
                          routeResult.Route.BoundingBox,
                          null,
                          Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                }
            }
            else
            {
                //CoreApplication.Exit();
            }
        }

        private void powMapa_Click(object sender, RoutedEventArgs e)
        {
            mojaMapa.ZoomLevel++;
        }

        private void pomMapa_click(object sender, RoutedEventArgs e)
        {
            mojaMapa.ZoomLevel--;
        }

        private void trybMapy_click(object sender, RoutedEventArgs e)
        {
            var ab = sender as AppBarButton;
            FontIcon fIcon = new FontIcon() {
                FontFamily = FontFamily.XamlAutoFontFamily
            };
            

            if(mojaMapa.Style == MapStyle.Road)
            {//zmiana na satelite
                mojaMapa.Style = MapStyle.AerialWithRoads;
                ab.Label = "Mapa";
                fIcon.Glyph = "M";
            }
            else
            {//zmieniamy na droge
                mojaMapa.Style = MapStyle.Road;
                ab.Label = "Satelita";
                fIcon.Glyph = "S";
            }
            ab.Icon = fIcon;
        }

        private void koordynaty_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Koordynaty));
        }


        
    }
}
