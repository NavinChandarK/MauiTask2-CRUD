using CRUDMauiAppTask2.Services;

namespace CRUDMauiAppTask2
{
    public partial class App : Application
    {
        public static EmployeeService EmployeeService { get; set; }
        public App(EmployeeService employeeService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            EmployeeService = employeeService;
        }
    }
}
