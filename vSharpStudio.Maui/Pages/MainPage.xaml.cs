using vSharpStudio.Maui.Models;
using vSharpStudio.Maui.PageModels;

namespace vSharpStudio.Maui.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}