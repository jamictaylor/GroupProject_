using GroupProject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
    internal class clsSearchLogic
    {
        /// <summary>
        /// Variable to access the database
        /// </summary>
        clsDataAccess DB = new clsDataAccess();
        /// <summary>
        /// Method to get the data set for data grid
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataSet GetInitialDataGrid()
        {
            try
            {
                int rows = 0;
                DataSet ds = new DataSet();
                string sql = clsSearchSQL.GetAllData();
                ds = DB.ExecuteSQLStatement(sql, ref rows);
                return ds;
            }
            catch(Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to get the list to fill the cost combo box
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoiceCost> GetCostCombo()
        {
            try
            {
                int row = 0;
                List<clsInvoiceCost> list = new List<clsInvoiceCost>();
                DataSet ds = new DataSet();
                string sql = clsSearchSQL.GetInvoiceCostCombo();
                ds = DB.ExecuteSQLStatement(sql, ref row);
                for (int i = 0; i < row; i++)
                {
                    clsInvoiceCost clsInvoiceCost = new clsInvoiceCost();
                    clsInvoiceCost.invoiceCost = ds.Tables[0].Rows[i][0].ToString();
                    list.Add(clsInvoiceCost);
                }
                return list;
            }
            catch(Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to get the list to fill the Date box
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoiceDate> GetDateCombo()
        {
            try
            {
                int row = 0;
                List<clsInvoiceDate> list = new List<clsInvoiceDate>();
                DataSet ds = new DataSet();
                string sql = clsSearchSQL.GetInvoiceDateCombo();
                ds = DB.ExecuteSQLStatement(sql, ref row);
                for (int i = 0; i < row; i++)
                {
                    clsInvoiceDate Date = new clsInvoiceDate();
                    Date.InvoiceDate = ds.Tables[0].Rows[i][0].ToString();
                    list.Add(Date);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to get the list to fill the number box
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoiceNum> GetNumCombo()
        {
            try
            {
                int row = 0;
                List<clsInvoiceNum> list = new List<clsInvoiceNum>();
                DataSet ds = new DataSet();
                string sql = clsSearchSQL.GetInvoiceNumCombo();
                ds = DB.ExecuteSQLStatement(sql, ref row);
                for (int i = 0; i < row; i++)
                {
                    clsInvoiceNum Num = new clsInvoiceNum();
                    Num.invoiceNum = ds.Tables[0].Rows[i][0].ToString();
                    list.Add(Num);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Method to get the data set to update the data grid
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="Date"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataSet updateDataGrid(string Num, string Date, string Cost)
        {
            try
            {
                DataSet ds = new DataSet();
                int rows = 0;
                if (Num != "0" && Date == "0" && Cost == "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetSelectedInvoiceID(Num);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                if (Num != "0" && Date != "0" && Cost == "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetIDandDate(Num, Date);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                if (Num != "0" && Date != "0" && Cost != "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetAllSelected(Num, Date, Cost);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                if (Num == "0" && Date != "0" && Cost == "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetSelectedDate(Date);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                if (Num == "0" && Date != "0" && Cost != "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetDateandCost(Date, Cost);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                if (Num == "0" && Date == "0" && Cost != "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetSelectedCost(Cost);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                if (Num != "0" && Date == "0" && Cost != "0")
                {
                    string sql;
                    sql = clsSearchSQL.GetIDandCost(Num, Cost);
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
                else
                {
                    string sql = clsSearchSQL.GetAllData();
                    ds = DB.ExecuteSQLStatement(sql, ref rows);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
