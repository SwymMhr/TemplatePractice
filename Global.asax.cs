using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using TemplatingPractice.pages.report.employee_info;

namespace TemplatingPractice
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "", "~/pages/other-pages/page-login.aspx");

            routes.MapPageRoute("Page-login", "page-login", "~/pages/other-pages/page-login.aspx");
            routes.MapPageRoute("Page-register", "page-register", "~/pages/other-pages/page-register.aspx");
            routes.MapPageRoute("Page-recoverpw", "page-recoverpw", "~/pages/other-pages/page-recoverpw.aspx");
            routes.MapPageRoute("Page-confirm-mail", "page-confirm-mail", "~/pages/other-pages/page-confirm-mail.aspx");
            routes.MapPageRoute("Page-logout", "page-logout", "~/pages/other-pages/page-logout.aspx");

            routes.MapPageRoute("Dashboard", "dashboard", "~/pages/dashboard/dashboard.aspx");

            routes.MapPageRoute("CustomerSupport", "customerSupport", "~/pages/customer-support/customerSupport.aspx");

            routes.MapPageRoute("Company", "company", "~/pages/system-setup/company.aspx");

            routes.MapPageRoute("BranchList", "branchList", "~/pages/system-setup/branchList.aspx");
            routes.MapPageRoute("AddBranch", "addBranch", "~/pages/system-setup/addBranch.aspx");
            routes.MapPageRoute("EditBranch", "editBranch", "~/pages/system-setup/editBranch.aspx");

            routes.MapPageRoute("DepartmentList", "departmentList", "~/pages/system-setup/departmentList.aspx");
            routes.MapPageRoute("AddDepartment", "addDepartment", "~/pages/system-setup/addDepartment.aspx");
            routes.MapPageRoute("EditDepartment", "editDepartment", "~/pages/system-setup/editDepartment.aspx");

            routes.MapPageRoute("GradeList", "gradeList", "~/pages/system-setup/gradeList.aspx");
            routes.MapPageRoute("AddGrade", "addGrade", "~/pages/system-setup/addGrade.aspx");
            routes.MapPageRoute("EditGrade", "editGrade", "~/pages/system-setup/editGrade.aspx");

            routes.MapPageRoute("DesignationList", "designationList", "~/pages/system-setup/designationList.aspx");
            routes.MapPageRoute("AddDesignation", "addDesignation", "~/pages/system-setup/addDesignation.aspx");
            routes.MapPageRoute("EditDesignation", "editDesignation", "~/pages/system-setup/editDesignation.aspx");

            routes.MapPageRoute("LeaveList", "leaveList", "~/pages/system-setup/leaveList.aspx");
            routes.MapPageRoute("AddLeave", "addLeave", "~/pages/system-setup/addLeave.aspx");
            routes.MapPageRoute("EditLeave", "editLeave", "~/pages/system-setup/editLeave.aspx");

            routes.MapPageRoute("WorkHourList", "workHourList", "~/pages/system-setup/roster/workHourList.aspx");
            routes.MapPageRoute("AddWorkHour", "addWorkHour", "~/pages/system-setup/roster/addWorkHour.aspx");
            routes.MapPageRoute("RosterAssign", "rosterAssign", "~/pages/system-setup/roster/rosterAssign.aspx");

            routes.MapPageRoute("EmployeeList", "employeeList", "~/pages/hr-management/employeeList.aspx");
            routes.MapPageRoute("AddEmployee", "addEmployee", "~/pages/hr-management/addEmployee.aspx");
            routes.MapPageRoute("InactiveList", "inactiveList", "~/pages/hr-management/inactiveList.aspx");

            routes.MapPageRoute("ForceAttendance", "forceAttendance", "~/pages/attendance-management/forceAttendance.aspx");
            routes.MapPageRoute("ForceAttendanceBatch", "forceAttendanceBatch", "~/pages/attendance-management/forceAttendanceBatch.aspx");

            routes.MapPageRoute("QuickAttendance", "quickAttendance", "~/pages/report/attendance-report/quickAttendance.aspx");
            routes.MapPageRoute("DailyAttendance", "dailyAttendance", "~/pages/report/attendance-report/dailyAttendance.aspx");
            routes.MapPageRoute("DailyAttendanceList", "dailyAttendanceList", "~/pages/report/attendance-report/dailyAttendanceList.aspx");
            routes.MapPageRoute("MonthlyAttendance", "monthlyAttendance", "~/pages/report/attendance-report/monthlyAttendance.aspx");
            routes.MapPageRoute("MonthlyAttendanceList", "monthlyAttendanceList", "~/pages/report/attendance-report/monthlyAttendanceList.aspx");
            routes.MapPageRoute("DatewiseAttendance", "datewiseAttendance", "~/pages/report/attendance-report/datewiseAttendance.aspx");
            routes.MapPageRoute("DatewiseAttendanceList", "datewiseAttendanceList", "~/pages/report/attendance-report/datewiseAttendanceList.aspx");
            
            routes.MapPageRoute("EmployeeReport", "employeeReport", "~/pages/report/employee-info/employeeReport.aspx");
            routes.MapPageRoute("EmployeeDetailInfo", "employeeDetailInfo", "~/pages/report/employee-info/employeeDetailInfo.aspx");
            routes.MapPageRoute("ViewEmployeeDetailInfo", "viewEmployeeDetailInfo", "~/pages/report/employee-info/viewEmployeeDetailInfo.aspx");

            routes.MapPageRoute("LogHistory", "logHistory", "~/pages/logHistory.aspx");
            routes.MapPageRoute("LogHistoryList", "logHistoryList", "~/pages/logHistoryList.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}