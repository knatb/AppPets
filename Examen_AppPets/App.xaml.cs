using Examen_AppPets.Data;
using Examen_AppPets.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen_AppPets
{
    public partial class App : Application
    {
        static PetsDatabase petsDatabase;
        public static PetsDatabase PetsDatabase
        {
            get
            {
                if (petsDatabase == null) petsDatabase = new PetsDatabase();
                return petsDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            //var nav = new NavigationPage(new PetsPage());
            //nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            //nav.BarTextColor = Color.White;
            //MainPage = nav;

            MainPage = new NavigationPage(new PetsPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
