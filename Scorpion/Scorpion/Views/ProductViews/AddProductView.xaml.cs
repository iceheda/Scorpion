using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Scorpion.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scorpion.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductView : ContentPage
    {
        Product _product = new Product();
        bool _isEdit;
        public AddProductView(Product product)
        {
            InitializeComponent();
            CultureInfo englishUSCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishUSCulture;
            if (product == null)
            {
                _isEdit = false;
            }
            else
            {
                _isEdit = true;
                _product = product;
            }
            grid1.BindingContext = _product;

            if (_isEdit == true)
            {
                try
                {
                    DateOfIssueDatePicker.Date = DateTime.Parse(_product.DateOfIssue);
                    ExpirationDateDatePicker.Date = DateTime.Parse(_product.ExpirationDate);
                }
                catch (Exception)
                {
                }
            }
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            if (_isEdit == true)
            {
                var item = new Product()
                {
                    Id = _product.Id,
                    NameOfProduct = EditorNameOfProduct.Text,
                    Quantity = Convert.ToInt32(EntryCount.Text),
                    NotificationDay = Convert.ToInt32(EntryNotificationDay.Text),
                    DateOfIssue = DateOfIssueDatePicker.Date.ToShortDateString(),
                    ExpirationDate = ExpirationDateDatePicker.Date.ToShortDateString(),
                    Comment = EditorComment.Text
                };

                Services.ProductService.UpdateProduct(item);

                Services.ToastService.ToastShow("Сохранено");
                Navigation.PopModalAsync();
            }
            else
            {

                try
                {
                    var item = new Product()
                    {
                        NameOfProduct = EditorNameOfProduct.Text,
                        Quantity = int.Parse(EntryCount.Text),
                        NotificationDay = int.Parse(EntryNotificationDay.Text),
                        DateOfIssue = DateOfIssueDatePicker.Date.ToShortDateString(),
                        ExpirationDate = ExpirationDateDatePicker.Date.ToShortDateString(),
                        Comment = EditorComment.Text
                    };

                    Services.ProductService.SaveProduct(item);

                    Services.ToastService.ToastShow("Сохранено");
                    Navigation.PopModalAsync();
                }
                catch
                {
                    DisplayAlert("Ошибка", "Что-то пошло не так", "Oк");
                    return;
                }
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}