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
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

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
