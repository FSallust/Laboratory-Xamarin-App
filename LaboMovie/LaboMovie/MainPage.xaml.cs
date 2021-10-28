using LaboMovie.Models;
using LaboMovie.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboMovie
{
    public partial class MainPage : ContentPage
    {
        public MainPage(Category cat = null)
        {
            BindingContext = new MovieViewModel(cat);
            InitializeComponent();
        }
    }
}
