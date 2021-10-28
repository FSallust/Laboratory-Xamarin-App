using LaboMovie.Models;
using LaboMovie.Services;
using LaboMovie.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LaboMovie.ViewModels
{
    class PersonDetailsViewModel : ViewModelBase
    {
        /*Fermer la page courante*/
        private Command _closeCommand;
        public Command CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new Command(CloseMethod)); }
        }

        private void CloseMethod()
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        private Person _person;
        public Person person
        {
            get { return _person; }
            set { SetValue(ref _person, value); ; }
        }

        public int Id { get; set; }

        public PersonDetailsViewModel(int Id)
        {
            this.Id = Id;
            LoadItems();
        }

        private async void LoadItems()
        {
            Person pers = await DependencyService.Get<PersonService>().GetById("http://localhost:36800/api/Person/" + Id);
            person = pers;
        }

    }
}
