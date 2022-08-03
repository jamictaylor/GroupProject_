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
        /// <summary>
        /// SQL statement to update invoice date and invoice total cost based on invoice number
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateInvoice(string InvoiceNumber, string InvoiceDate, string TotalCost)
        {
            try
            {
                string sSQL = "UPDATE Invoices " +
                                "SET InvoiceDate = " + InvoiceDate + ", TotalCost = " + TotalCost + 
                                " WHERE InvoiceNum = " + InvoiceNumber;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to delete current items in database for given invoice number
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteItems(string InvoiceNumber)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems " +
                                "WHERE InvoiceNum = " + InvoiceNumber;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to insert line items into the Line Items Table
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="LineItemNum"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertLineItems(string InvoiceNum, string LineItemNum, string ItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                                "VALUES (" + InvoiceNum + ", " + LineItemNum + ", \'"+ ItemCode + "\')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to Insert Invoice into the Invoices table
        /// </summary>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertInvoice(string InvoiceDate, string TotalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) " +
                                "VALUES (#" + InvoiceDate + "# , " + TotalCost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement select the Invoice Number, Invoice Date and Total Cost from given invoice number
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string SelectInvoice(string InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost " +
                                "FROM Invoices " +
                                "WHERE InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement Retrieves of all of the attributes from the ItemDesc table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetItemDesc()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                                "FROM ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to get items from code and inventory number combined from database
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceItems(string InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                                "FROM LineItems, ItemDesc " +
                                "WHERE LineItems.ItemCode = ItemDesc.ItemCode " +
                                 "AND LineItems.InvoiceNum = " + InvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to get max invoice number
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetMaxInvoiceNum()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum)" +
                                "FROM Invoices";
                       
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
