using System.Configuration;
using System.Data;
using System.Windows;
using єMessage.Services;
using єMessage.Stores;
using єMessage.ViewModels;

namespace єMessage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateLoginPage();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private SingInPageViewModel CreateLoginPage()
        {
            return new SingInPageViewModel(new NavigationServices(_navigationStore, CreateSignUpPage));
        }

        private SingUpPageViewModel CreateSignUpPage()
        {
            return new SingUpPageViewModel(new NavigationServices(_navigationStore, CreateLoginPage));
        }
    }

}
