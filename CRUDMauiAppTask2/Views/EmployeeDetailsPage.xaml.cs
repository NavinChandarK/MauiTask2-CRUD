using CRUDMauiAppTask2.ViewModels;

namespace CRUDMauiAppTask2.Views;

public partial class EmployeeDetailsPage : ContentPage
{
    public EmployeeDetailsPage(EmployeeDetailsViewModel employeeDetailsViewModel)
    {
        InitializeComponent();
        BindingContext = employeeDetailsViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}