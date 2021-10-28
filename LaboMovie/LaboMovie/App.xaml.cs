using LaboMovie.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboMovie
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.RegisterSingleton<MovieService>(new MovieService());
            DependencyService.RegisterSingleton<PersonService>(new PersonService());
            DependencyService.RegisterSingleton<CategoryService>(new CategoryService());
            DependencyService.RegisterSingleton<CastingService>(new CastingService());
            InitializeComponent();

            MainPage = new MainPage();
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
