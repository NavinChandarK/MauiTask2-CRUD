using CommunityToolkit.Mvvm.ComponentModel;
using CRUDMauiAppTask2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRUDMauiAppTask2.ViewModels
{
 
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class EmployeeDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Employee employee;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
            Employee = App.EmployeeService.GetEmployee(Id);
        }
    }
}
