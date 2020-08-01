using Examen_AppPets.Models;
using Examen_AppPets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Examen_AppPets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage(PetModel petSelected)
        {
            InitializeComponent();
            // aqui aparece el punto guardado
            MapPet.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(petSelected.Latitude, petSelected.Longitude),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(petSelected.ImageBase64);
            petSelected.ImageBase64 = imagePath;
            MapPet.Pet = petSelected;
            //añadir el punto en el mapa
            MapPet.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = petSelected.Name,
                    Position = new Position(
                        petSelected.Latitude, petSelected.Longitude)
                }
            );

            Name.Text = petSelected.Name;
            BirthDate.Text = petSelected.BirthDate.ToShortDateString();
            Comments.Text = petSelected.Comments;
        }

    }
}