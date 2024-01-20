using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using Scorpion.Models;

namespace Scorpion.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        private List<Product> products;

        private Product _lvItem = new Product();
        public ProductListPage()
        {
            InitializeComponent();
        }
        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            products = Services.ProductService.GetListOfProducts();
            Refresh();
        }
        void Refresh()
        {
            ProductListView.ItemsSource = products;
        }
        void ResetFilters()
        {
            FinderEntry.Text = null;
            FilterDatePicker.SelectedIndex = 0;
        }

        void FilterMethod()
        {
            try
            {
                var list = products.AsQueryable();

                list = list.Where(x => x.NameOfProduct.Contains(FinderEntry.Text));

                switch (FilterDatePicker.SelectedIndex)
                {
                    case 1:
                        list = list.OrderBy(x => x.ExpirationDate);
                        break;
                    case 2:
                        list = list.OrderByDescending(x => x.ExpirationDate);
                        break;
                }

                ProductListView.ItemsSource = list.ToList();
            }
            catch
            {
                Refresh();
            }
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FilterMethod();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterMethod();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            ResetFilters();
        }


        private void Add_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new AddProductView(null));
        }

        private void Edit_Clicked(object sender, System.EventArgs e)
        {
            var item = ProductListView.SelectedItem as Models.Product;
            if (item != null)
                Navigation.PushModalAsync(new AddProductView(item));

        }

        private async void Delete_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var item = ProductListView.SelectedItem as Product;
                if (item != null)
                    Services.ProductService.DeleteProduct(item.Id);
                products = Services.ProductService.GetListOfProducts();
                ProductListView.ItemsSource = products;
                if (_lvItem == ProductListView.SelectedItem as Product)
                    _lvItem = new Product();
                ProductListView.SelectedItem = null;
            }
            catch (System.Exception)
            {
                await DisplayAlert("Ошибка", "Не удалось удалить данный продукт", "Ок");
            }
        }

        void Test()
        {
            ProductListView.ItemsSource = new List<Models.Product>()
            {
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                 new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                 new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                 new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                 new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                 new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" },
                new Models.Product(){ NameOfProduct="Тушенка с гавном", DateOfIssue="2020-07-07", ExpirationDate = "2021-07-07" }

            };
        }

        private void OneTap_Tapped(object sender, System.EventArgs e)
        {
            if (sender is Grid grid)
            {
                ProductListView.SelectedItem = grid.BindingContext;
                if (_lvItem == ProductListView.SelectedItem as Product)
                {
                    ProductListView.SelectedItem = null;
                }
                else
                {
                    ProductListView.SelectedItem = grid.BindingContext;
                    _lvItem = ProductListView.SelectedItem as Product;
                }
            }
        }

        private void DoubleTap_Tapped(object sender, System.EventArgs e)
        {
            if (sender is Grid grid && grid.BindingContext != _lvItem)
            {
                ProductListView.SelectedItem = grid.BindingContext;
                _lvItem = ProductListView.SelectedItem as Product;
            }

            if (ProductListView.SelectedItem is Product product)
            {
                Navigation.PushModalAsync(new ProductView(product));
                ProductListView.SelectedItem = null;
                _lvItem = null;
            }


        }
    }
}