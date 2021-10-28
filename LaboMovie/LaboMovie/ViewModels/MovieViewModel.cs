using LaboMovie.Models;
using LaboMovie.Services;
using LaboMovie.MVVMTools;
using LaboMovie.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using LaboMovie.View;

namespace LaboMovie.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {
        public ObservableCollection<Movie> MovieList { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }
        
        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set 
            { 
                SetValue(ref _selectedMovie, value);
                if (!(_selectedMovie is null))
                    App.Current.MainPage.Navigation.PushModalAsync(new MovieDetailView(_selectedMovie));
            }
        }

        private Category _selectedCategory;

        public Category CategoryMovie
        {
            get { return _selectedCategory; }
            set
            {
                SetValue(ref _selectedCategory, value);
                if (!(_selectedCategory is null))
                {
                    if (_selectedCategory.Id != 0)
                        App.Current.MainPage.Navigation.PushModalAsync(new MainPage(_selectedCategory));
                    else
                        App.Current.MainPage.Navigation.PushModalAsync(new MainPage(null));
                }
                OnPropertyChanged();
            }
        }

        public MovieViewModel(Category cat = null)
        {
            if (cat == null)
            {
                MovieList = new ObservableCollection<Movie>();
                CategoryList = new ObservableCollection<Category>();
                LoadItems();
            }
            else
            {
                MovieList = new ObservableCollection<Movie>();
                CategoryList = new ObservableCollection<Category>();
                LoadItems(cat);
            }
        }

        private async void LoadItems()
        {
            MovieList.Clear();

            IEnumerable<Movie> req = await DependencyService.Get<MovieService>().GetAll("http://localhost:36800/api/Movie");
            IEnumerable<Category> reqCat = await DependencyService.Get<CategoryService>().GetAll("http://localhost:36800/api/Category");

            foreach (Movie item in req)
            {
                MovieList.Add(item);
            }

            foreach (Category item in reqCat)
            {
                CategoryList.Add(item);
            }
        }

        private async void LoadItems(Category cat)
        {
            MovieList.Clear();

            IEnumerable<Movie> req = await DependencyService.Get<MovieService>().GetAll("http://localhost:36800/api/Movie");
            IEnumerable<Category> reqCat = await DependencyService.Get<CategoryService>().GetAll("http://localhost:36800/api/Category");

            foreach (Movie item in req)
            {
                if (cat.Id == item.Id)
                    MovieList.Add(item);
            }
            foreach (Category item in reqCat)
            {
                CategoryList.Add(item);
            }
        }
    }
}
