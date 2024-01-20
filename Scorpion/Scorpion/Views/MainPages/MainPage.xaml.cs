using System;
using System.ComponentModel;
using ReactiveUI;
using Scorpion.Messages;
using Xamarin.Forms;

namespace Scorpion.Views.MainPages
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new WelcomePage());
            MessageBus.Current.Listen<OpenPageMessage>().Subscribe(x =>
            {
                Detail = new NavigationPage(x.Page);
                IsPresented = false;
            });
        }
    }
}