using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
    public class clsSearchSQL
    {
        /// <summary>
        /// Method to create the sql statement to get all the data from the database
        /// </summary>
        /// <returns></returns>
        public static string GetAllData()
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices";
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select ID
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <returns></returns>
        public static string GetSelectedInvoiceID(string invoiceID)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE InvoiceNum = " + invoiceID;
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select Date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetSelectedDate(string date)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE InvoiceDate = #" + date + "#";
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select cost
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string GetSelectedCost(string cost)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE TotalCost = " + cost;
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select Id and date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetIDandDate(string id, string date)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE InvoiceNum = " + id + " AND InvoiceDate = #" + date + "#";
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select ID and cost
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string GetIDandCost(string id, string cost)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE InvoiceNum = " + id + " AND TotalCost = " + cost;
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select date and cost
        /// </summary>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string GetDateandCost(string date, string cost)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE InvoiceDate = #" + date + "# AND TotalCost = " + cost;
            return sql;
        }
        /// <summary>
        /// Method to create the sql statement to get the data with a select ID, date and cost
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static string GetAllSelected(string id, string date, string cost)
        {
            string sql;
            sql = "SELECT *" +
                  "FROM Invoices " +
                  "WHERE InvoiceNum = " + id + " AND InvoiceDate = #" + date + "# AND TotalCost = " + cost;
            return sql;
        }
        /// <summary>
        /// Method to get the sql statement to fill the invoice number box
        /// </summary>
        /// <returns></returns>
        public static string GetInvoiceNumCombo()
        {
            string sql;
            sql = "SELECT InvoiceNum " +
                  "FROM Invoices";
            return sql;
        }
        /// <summary>
        /// Method to get the sql statement to fill the date box
        /// </summary>
        /// <returns></returns>
        public static string GetInvoiceDateCombo()
        {
            string sql;
            sql = "SELECT DISTINCT InvoiceDate " +
                  "FROM Invoices";
            return sql;
        }
        /// <summary>
        /// Method to get the sql statement to fill the cost box
        /// </summary>
        /// <returns></returns>
        public static string GetInvoiceCostCombo()
        {
            string sql;
            sql = "SELECT DISTINCT TotalCost " +
                  "FROM Invoices";
            return sql;
        }
    }
}
