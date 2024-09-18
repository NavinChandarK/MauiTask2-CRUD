using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRUDMauiAppTask2.Models;
using CRUDMauiAppTask2.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CRUDMauiAppTask2.Services;
using System.Linq;
using System.Net;

namespace CRUDMauiAppTask2.ViewModels
{
    public partial class EmployeesViewModel : BaseViewModel
    {
        const string editButtonText = "Update Employee";
        const string createButtonText = "Add Employee";
        public ObservableCollection<Employee> Employees { get; private set; } = new();
        public EmployeesViewModel()
        {
            Title = "Employee List";
            AddEditButtonText = createButtonText;
            IsFormVisible = false;
            IsGridVisible = true;
            GetEmployeeList().Wait();
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string firstName;
        [ObservableProperty]
        string lastName;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string phoneNumber;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int empId;
        [ObservableProperty]
        bool isFormVisible;
        [ObservableProperty]
        bool isGridVisible;

        [RelayCommand]
        async Task GetEmployeeList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Employees.Any()) Employees.Clear();

                var emp = App.EmployeeService.GetEmployees();

                foreach (var emps in emp) Employees.Add(emps);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Employee: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of Employees.", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetEmployeeDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(EmployeeDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            ShowForm();
            Title = "Update Employee";
            AddEditButtonText = editButtonText;
            empId = id;
            var emp = App.EmployeeService.GetEmployee(id);
            FirstName = emp.FirstName;
            LastName = emp.LastName;
            Email = emp.Email;
            PhoneNumber = emp.PhoneNumber;
        }

        [RelayCommand]
        async Task SaveEmployee()
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }

            var emp = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
            };
            

            if (empId != 0)
            {
                emp.Id = empId;
                App.EmployeeService.UpdateEmployee(emp);
                await Shell.Current.DisplayAlert("Info", App.EmployeeService.StatusMessage, "Ok");
            }
            else
            {
                App.EmployeeService.AddEmployee(emp);
                await Shell.Current.DisplayAlert("Info", App.EmployeeService.StatusMessage, "Ok");
            }

            await GetEmployeeList();
            await ShowGrid();
        }

        [RelayCommand]
        async Task DeleteEmployee(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            var result = App.EmployeeService.DeleteEmployee(id);
            if (result == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please try again", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
                await GetEmployeeList();
            }
        }

        [RelayCommand]
        async Task ClearForm()
        {
            if (EmpId != 0)
            {
                AddEditButtonText = editButtonText;
            }
            else
            {
                AddEditButtonText = createButtonText;
                EmpId = 0;
            }
            FirstName = string.Empty;
            Email = string.Empty;
            LastName = string.Empty;
            PhoneNumber = null;
        }

        [RelayCommand]
        async Task ShowForm()
        {
            Title = "Add Employee";
            IsFormVisible = true;
            IsGridVisible = false;
        }

        [RelayCommand]
        async Task ShowGrid()
        {
            EmpId = 0;
            await ClearForm();
            Title = "Employee List";
            IsFormVisible = false;
            IsGridVisible = true;
        }
    }
}
