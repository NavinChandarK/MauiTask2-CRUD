using CRUDMauiAppTask2.Models;
using SQLite;

namespace CRUDMauiAppTask2.Services
{
    public class EmployeeService(string dbPath)
    {
        SQLiteConnection conn;
        public string StatusMessage;
        int result = 0;

        private void Init()
        {
            if (conn != null)
            {
                return;
            }
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Employee>();
        }
        public List<Employee> GetEmployees()
        {
            try
            {
                Init();
                return conn.Table<Employee>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data";
            }
            return new List<Employee>();
        }
        public Employee GetEmployee(int id)
        {
            try
            {
                Init();
                return conn.Table<Employee>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data";
            }
            return null;
        }
        public void AddEmployee(Employee emp)
        {
            try
            {
                Init();
                if (emp == null)
                {
                    throw new Exception("Invalid Employee Record");
                }

                result = conn.Insert(emp);
                StatusMessage = result == 0 ? "Insert Failed" : "Inserted Record Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Insert data";
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            try
            {
                Init();

                if (emp == null)
                    throw new Exception("Invalid Employee Record");

                result = conn.Update(emp);
                StatusMessage = result == 0 ? "Update Failed" : "Updated Record Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Update data.";
            }
        }

        public int DeleteEmployee(int id)
        {
            try
            {
                Init();
                return conn.Table<Employee>().Delete(q => q.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data";
            }
            return 0;
        }
    }
}
