using Examen_AppPets.Models;
using Examen_AppPets.Renders;
using Examen_AppPets.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Examen_AppPets.UWP.Renders
{
    public class CustomMapRenderer : MapRenderer
    {
        MapControl nativeMap;
        MarkerWindow markerWindow;
        bool markerWindowShown = false;
        PetModel Pet;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap.MapElementClick -= OnMapElementClick;
                nativeMap.Children.Clear();
                markerWindow = null;
                nativeMap = null;
            }

            if (e.NewElement != null)
            {
                this.Pet = (e.NewElement as CustomMap).Pet;

                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MapControl;
                nativeMap.Children.Clear();
                nativeMap.MapElementClick += OnMapElementClick;

                var snPosition = new BasicGeoposition
                {
                    Latitude = Pet.Latitude,
                    Longitude = Pet.Longitude
                };

                var snPoint = new Geopoint(snPosition);

                var mapIcon = new MapIcon();
                mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///pin.png"));
                //cuando se hace zoom siempre será visible
                mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                mapIcon.Location = snPoint; //cooirdenadas generadas anteriormente
                mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0); //tamaño del icono

                nativeMap.MapElements.Add(mapIcon);
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon; //traer primer o unico map icon
            if (mapIcon != null)
            {
                if (!markerWindowShown)
                {
                    if (markerWindow == null) markerWindow = new MarkerWindow(Pet);

                    var snPosition = new BasicGeoposition
                    {
                        Latitude = Pet.Latitude,
                        Longitude = Pet.Longitude
                    };

                    var snPoint = new Geopoint(snPosition);

                    nativeMap.Children.Add(markerWindow);
                    MapControl.SetLocation(markerWindow, snPoint);
                    MapControl.SetNormalizedAnchorPoint(markerWindow, new Windows.Foundation.Point(0.5, 1.0));

                    markerWindowShown = true;
                }
                else
                {
                    nativeMap.Children.Remove(markerWindow);
                    markerWindowShown = false;
                }
            }
        }
    }
}
