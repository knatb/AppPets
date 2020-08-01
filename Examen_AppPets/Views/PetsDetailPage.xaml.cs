using Examen_AppPets.Models;
using Examen_AppPets.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen_AppPets.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetsDetailPage : ContentPage
    {
        public PetsDetailPage()
        {
            InitializeComponent();

            BindingContext = new PetsDetailViewModel();
        }

        public PetsDetailPage(PetModel petSelected)
        {
            InitializeComponent();

            BindingContext = new PetsDetailViewModel(petSelected);
        }
    }
}