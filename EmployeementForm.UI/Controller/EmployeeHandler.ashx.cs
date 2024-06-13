using EmployeemtForm.Logic.BusinessLogic;
using EmployeemtForm.Logic.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using Westwind.Web;

namespace EmployeementForm.UI.Controller
{
    /// <summary>
    /// Summary description for EmployeeHandler
    /// </summary>
    public class EmployeeHandler : CallbackHandler
    {

        private string _connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [CallbackMethod()]
        public string GetEmployeeData()
        {
            BLLEmployees objBllEmployee = new BLLEmployees(_connectionstring);
            List<EmployeeModel> empData= objBllEmployee.GetEmployeeData();
            string empDataResponse = JsonConvert.SerializeObject(empData);
            return empDataResponse;
        }

        [CallbackMethod()]
        public bool AddUpdateEmployees()
        { 
            var issuccess = false;
            string formData = Request.Form[0];
            EmployeeModel empdata = JsonConvert.DeserializeObject<EmployeeModel>(formData);
            if (!string.IsNullOrEmpty(empdata.ToString()))
            {
                BLLEmployees objBllEmployee = new BLLEmployees(_connectionstring);
                issuccess =  objBllEmployee.AddEmployees(empdata);
            }
            return issuccess;
        }
        [CallbackMethod()]
        public string GetEmployeeDataById(int empId)
        {
            BLLEmployees objBllEmployee = new BLLEmployees(_connectionstring);
            List<EmployeeModel> empData = objBllEmployee.GetEmployeeData(empId);
            string empDataResponse = JsonConvert.SerializeObject(empData);
            return empDataResponse;
        }
        [CallbackMethod()]
        public bool DeleteData(int empId)
        {
            var issuccess = false;
            
            if (!string.IsNullOrEmpty(empId.ToString()))
            {
                BLLEmployees objBllEmployee = new BLLEmployees(_connectionstring);
                issuccess = objBllEmployee.DeleteData(empId);
            }
            return issuccess;
        }
    }
}