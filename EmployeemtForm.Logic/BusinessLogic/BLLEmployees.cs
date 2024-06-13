using EmployeemtForm.Logic.DataLogic;
using EmployeemtForm.Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeemtForm.Logic.BusinessLogic
{
    public class BLLEmployees
    {
        static string connectionString;
        DLLEmployees DLLEmployees;
        public BLLEmployees(string appConnectionString) {
            connectionString = appConnectionString;
            DLLEmployees = new DLLEmployees(connectionString);
        }
        public bool AddEmployees(EmployeeModel employeesData)
        {
            return DLLEmployees.AddEmployees(employeesData);
        }
        public List<EmployeeModel> GetEmployeeData()
        {
            DataTable dtEmployeedata = DLLEmployees.GetEmployeeData();
            List<EmployeeModel > employees = new List<EmployeeModel>();
            
            if(dtEmployeedata.Rows.Count>0 && dtEmployeedata!= null)
            {
                foreach (DataRow dr in dtEmployeedata.Rows)
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.EmployeeID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.FirstName = Convert.ToString(dr["FIRST_NAME"]);
                    employee.LastName = Convert.ToString(dr["LAST_NAME"]);
                    employee.MobileNo = Convert.ToInt64(dr["MOBILE_NO"]);
                    employee.Address = Convert.ToString(dr["ADDRESS"]);
                    employee.IsActive = Convert.ToBoolean(dr["ISACTIVE"]);
                    employees.Add(employee);
                }
                
            }
            return employees;
            

        }
        public List<EmployeeModel> GetEmployeeData(int empId)
        {
            DataTable dtEmployeedata = DLLEmployees.GetEmployeeData(empId);
            List<EmployeeModel> employees = new List<EmployeeModel>();

            if (dtEmployeedata.Rows.Count > 0 && dtEmployeedata != null)
            {
                foreach (DataRow dr in dtEmployeedata.Rows)
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.EmployeeID = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                    employee.FirstName = Convert.ToString(dr["FIRST_NAME"]);
                    employee.LastName = Convert.ToString(dr["LAST_NAME"]);
                    employee.MobileNo = Convert.ToInt64(dr["MOBILE_NO"]);
                    employee.Address = Convert.ToString(dr["ADDRESS"]);
                    employee.IsActive = Convert.ToBoolean(dr["ISACTIVE"]);
                    employees.Add(employee);
                }

            }
            return employees;
        }
        public bool DeleteData(int empId)
        {
            return DLLEmployees.DeleteData(empId);
        }
    }
}
