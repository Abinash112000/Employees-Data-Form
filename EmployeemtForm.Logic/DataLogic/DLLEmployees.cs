using EmployeemtForm.Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeemtForm.Logic.DataLogic
{
    public class DLLEmployees
    {
        private string _connectionString;
        SqlConnection connection;
        public DLLEmployees(string connectionString)
        {
            _connectionString = connectionString;
            connection = new SqlConnection(_connectionString);

        }
        public DataTable GetEmployeeData()
        {
            DataTable dtEmployeeData = new DataTable();
            connection.Open();
            string sqlQuery = "SELECT * FROM EMPLOYEE_DATA";
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            SqlDataAdapter adaptorData = new SqlDataAdapter(cmd);
            adaptorData.Fill(dtEmployeeData);
            connection.Close();
            return dtEmployeeData;

        }
        public DataTable GetEmployeeData(int empId)
        {
            DataTable dtEmployeeData = new DataTable();
            connection.Open();
            string sqlQuery = $"SELECT * FROM EMPLOYEE_DATA WHERE EMPLOYEE_ID = {empId}";
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            SqlDataAdapter adaptorData = new SqlDataAdapter(cmd);
            adaptorData.Fill(dtEmployeeData);
            connection.Close();
            return dtEmployeeData;

        }
        public bool AddEmployees(EmployeeModel employeesData)
        {
            bool issaved = false;
            string sqlQuery = "";
            DataTable dtEmployeeData = GetEmployeeData(employeesData.EmployeeID);
            connection.Open();
            if (dtEmployeeData.Rows.Count > 0)
            {
                sqlQuery = $"UPDATE EMPLOYEE_DATA SET FIRST_NAME = '{employeesData.FirstName}', LAST_NAME = '{employeesData.LastName}',MOBILE_NO = '{employeesData.MobileNo}',ADDRESS = '{employeesData.Address}',ISACTIVE= '{employeesData.IsActive}' WHERE EMPLOYEE_ID = {employeesData.EmployeeID}";

            }
            else
            {
                sqlQuery = $"INSERT INTO EMPLOYEE_DATA VALUES('{employeesData.FirstName}','{employeesData.LastName}','{employeesData.MobileNo}','{employeesData.Address}',1)";

            }
            SqlCommand cmd = new SqlCommand(sqlQuery,connection);
            if (cmd.ExecuteNonQuery() == 1)
            {
                issaved = true;
            } ;
            connection.Close();
            return issaved;
        }
        public bool DeleteData(int empId)
        {
            bool issaved = false;
            connection.Open();
            string sqlQuery = $"DELETE FROM EMPLOYEE_DATA WHERE EMPLOYEE_ID = {empId}";
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            if (cmd.ExecuteNonQuery() == 1)
            {
                issaved = true;
            };
            connection.Close();
            return issaved;
        }
    }
}
