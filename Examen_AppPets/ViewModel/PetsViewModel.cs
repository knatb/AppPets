using Examen_AppPets.Models;
using Examen_AppPets.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Examen_AppPets.ViewModel
{
    public class PetsViewModel : BaseViewModel
    {
        static PetsViewModel instace;

        //COMANDOS
        Command newPetCommand;
        public Command NewPetCommand => newPetCommand ?? (newPetCommand = new Command(_newPet));

        //Command selectTaskCommand;
        //public Command SelectTaskCommand => selectTaskCommand ?? (selectTaskCommand = new Command(_selectTask));

        //items source que se bindea a list view
        List<PetModel> pets;
        public List<PetModel> Pets
        {
            get => pets;
            set => SetProperty(ref pets, value);
        }

        PetModel petSelected;
        public PetModel PetSelected
        {
            get => petSelected;
            set
            {
                if (SetProperty(ref petSelected, value))
                {
                    _SelectPet();
                }
            }
        }

        public PetsViewModel()
        {
            instace = this;
            LoadPets();
        }

        public static PetsViewModel GetInstance()
        {
            if (instace == null) instace = new PetsViewModel();
            return instace;
        }

        public async void LoadPets()
        {
            Pets = await App.PetsDatabase.GetAllPetsAsync();
        }

        private void _newPet()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PetsDetailPage());
        }

        private void _SelectPet()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PetsDetailPage(PetSelected));
        }

    }
}
