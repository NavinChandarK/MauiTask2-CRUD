using CRUDMauiAppTask2.ViewModels;

namespace CRUDMauiAppTask2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(EmployeesViewModel employeesViewModel)
        {
            InitializeComponent();
            BindingContext = employeesViewModel;
        }
    }

}
