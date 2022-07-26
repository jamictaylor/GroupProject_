using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    internal class clsMainSQL
    {
        public static string UpdateInvoices(string TotalCost, string InvoiceID)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET TotalCost = " + TotalCost + "WHERE InvoiceNum =" + InvoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string InsertItem(string InvoiceNum, string LineItemNum, string ItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (123, 1, 'AA')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string InsertInvoice(string InvoiceNum, string LineItemNum, string ItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#4/13/2018#, 100)";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to execute the SQL statement to select the Invoice Number, Invoice Date and Total Cost
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SelectInvoice(string InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string GetItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


    }
}
