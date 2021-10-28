using LaboMovie.Models;
using LaboMovie.Services;
using LaboMovie.Tools;
using LaboMovie.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace LaboMovie.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
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

        private Movie _movies;
        public Movie movies
        {
            get { return _movies; }
            set { _movies = value; }
        }

        public ObservableCollection<Person> PersonList { get; set; }

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                SetValue(ref _selectedPerson, value);
                if (!(_selectedPerson is null))
                    App.Current.MainPage.Navigation.PushModalAsync(new PersonDetailView(_selectedPerson.Id));
            }
        }

        private Category _category;
        public Category categories
        {
            get { return _category; }
            set { SetValue(ref _category, value); }
        }

        public MovieDetailsViewModel(Movie movie)
        {
            movies = movie;
            PersonList = new ObservableCollection<Person>();
            LoadItems();
        }

        private async void LoadItems()
        {
            PersonList.Clear();

            IEnumerable<Casting> req = await DependencyService.Get<CastingService>().GetAll("http://localhost:36800/api/Casting");

            Person realisator = await DependencyService.Get<PersonService>().GetById("http://localhost:36800/api/Person/" + movies.RealisatorId);
            Person scenarist = await DependencyService.Get<PersonService>().GetById("http://localhost:36800/api/Person/" + movies.ScenaristId);
            Category category = await DependencyService.Get<CategoryService>().GetById("http://localhost:36800/api/Category/" + movies.CategoryId);
            categories = category;

            realisator.LastName += " | Realisateur";
            scenarist.LastName += " | Scenarist";

            PersonList.Add(realisator);
            PersonList.Add(scenarist);


            foreach (Casting item in req)
            {
                if (item.MovieId == movies.Id)
                {
                    Person actor = await DependencyService.Get<PersonService>().GetById("http://localhost:36800/api/Person/" + item.PersonId);

                    actor.LastName += " | " + item.Role;

                    PersonList.Add(actor);
                }
            }
        }
    }
}
