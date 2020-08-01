using Examen_AppPets.Data;
using Examen_AppPets.Models;
using Examen_AppPets.Services;
using Examen_AppPets.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Examen_AppPets.ViewModel
{
    public class PetsDetailViewModel : BaseViewModel
    {
        Command saveCommand;
        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(Save));

        Command deleteCommand;
        public Command DeleteCommand => deleteCommand ?? (deleteCommand = new Command(Delete));
        Command _mapCommand;
        public Command MapCommand => _mapCommand ?? (_mapCommand = new Command(MapAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePicture));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPicture));


        PetModel petSelected;
        public PetModel PetSelected
        {
            get => petSelected;
            set => SetProperty(ref petSelected, value);
        }

        ImageSource imageSource_;
        public ImageSource ImageSource_
        {
            get => imageSource_;
            set => SetProperty(ref imageSource_, value);
        }
        string imageBase64;
        public string ImageBase64
        {
            get => imageBase64;
            set => SetProperty(ref imageBase64, value);
        }
        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        string comments;
        public string Comments
        {
            get => comments;
            set => SetProperty(ref comments, value);
        }
        string race;
        public string Race
        {
            get => race;
            set => SetProperty(ref race, value);
        }
        string gender;
        public string Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }
        string weight;
        public string Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }
        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }
        double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }
        int id;
        public int ID
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        DateTime birthdate;
        public DateTime BirthDate
        {
            get => birthdate;
            set => SetProperty(ref birthdate, value);
        }

        //public string petAge()
        //{
        //    var today = DateTime.Today;
        //    // Calculate the age.
        //    var age = today.Year - birthdate.Year;
        //    // Go back to the year the person was born in case of a leap year
        //    if (birthdate.Date > today.AddYears(-age)) age--;
        //    return age;
        //}       

        /*        
            DateTime birthdate = (DateTime)value;
            if(value == null  string.IsNullOrEmpty(value.ToString())  DateTime.Today < birthdate)
            {
                return "0";
            }
            int petAge = DateTime.Today.Year - birthdate.Year;
            if( birthdate.Month > DateTime.Today.Month)
            {
                --petAge;
            }
            return petAge;
        */

        public PetsDetailViewModel()
        {
            PetSelected = new PetModel();
        }

        public PetsDetailViewModel(PetModel petSelected)
        {
            PetSelected = petSelected;
            ImageBase64 = petSelected.ImageBase64;
        }


        private async void Save()
        {
            await App.PetsDatabase.SavePetAsync(PetSelected);
            PetsViewModel.GetInstance().LoadPets();
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void Delete()
        {
            await App.PetsDatabase.DeletePetAsync(PetSelected);
            PetsViewModel.GetInstance().LoadPets();
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        private async void TakePicture()
        {
            //await CrossMedia.Current.Initialize(); esto se hace en el main activity de android

            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null) //si el archivo es nulo, se sale
                return;

            PetSelected.ImageBase64 = new ImageService().AuxImageAsBase64(file.Path); //se actualiza la imagen en memoria de la que habia martillada
            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK"); //se anda el path de donde quedo la imagen

            ImageSource_ = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream(); //del archivo que ya obtuvo el plugin 
                return stream;
            });
        }
        private async void SelectPicture(object obj)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported) //si no hay soporte, manda un alert
            {
                await Application.Current.MainPage.DisplayAlert("Error", ":( Not supported action.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full
            });

            if (file == null) //si el archivo es nulo, se sale
                return;

            PetSelected.ImageBase64 = ImageBase64 = await new ImageService().ConvertImageFileToBase64(file.Path); //se actualiza la imagen en memoria de la que habia martillada
            /*await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK"); //se anda el path de donde quedo la imagen

            ImageSource_ = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream(); //del archivo que ya obtuvo el plugin 
                return stream;
            });*/
        }
        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new MapPage(new PetModel
            {
                Name = petSelected.Name,
                Comments = petSelected.Comments,
                BirthDate = petSelected.BirthDate,
                ImageBase64 = petSelected.ImageBase64,
                Latitude = petSelected.Latitude,
                Longitude = petSelected.Longitude,
        } ));
    }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    PetSelected.Latitude = location.Latitude;
                    PetSelected.Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
