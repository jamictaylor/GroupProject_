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
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (" + InvoiceNum + ", " + LineItemNum + ", "+ ItemCode + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string InsertInvoice(string InvoiceDate, string TotalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#" + InvoiceDate + "# , " + TotalCost + ")";
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

        /// <summary>
        /// SQL method that will execute the retrieval of all of the attributes fromt eh ItemDesc table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetItemDesc()
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


        public static string getItemsFromInvoice(string ItemCode, string InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                                "FROM LineItems, ItemDesc " +
                                "WHERE LineItems.ItemCode = " + ItemCode +
                                 "AND LineItems.InvoiceNum = " + InvoiceNum;
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
