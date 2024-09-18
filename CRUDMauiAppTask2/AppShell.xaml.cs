using CRUDMauiAppTask2.Views;

namespace CRUDMauiAppTask2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EmployeeDetailsPage), typeof(EmployeeDetailsPage));
;        }
    }
}
