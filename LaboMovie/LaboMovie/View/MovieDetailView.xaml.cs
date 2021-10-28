using LaboMovie.Models;
using LaboMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboMovie.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailView : ContentPage
    {
        public MovieDetailView(Movie movie)
        {
            BindingContext = new MovieDetailsViewModel(movie);
            InitializeComponent();
        }
    }
}