using GroupProject.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    /// <summary>
    /// Business logic for the Main Window
    /// </summary>
    internal class clsMainLogic
    {
        /// <summary>
        /// create an object of type clsDataAccess to access the database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// class of type invoice
        /// </summary>
        Common.clsInvoice invoice;

        /// <summary>
        /// List of objects of type clsItem
        /// </summary>
        public List<Common.clsItem> items;

        #region private properties
        /// <summary>
        /// variable to hold the total cost
        /// </summary>
        private static double dTotalCost = 0.00;

        /// <summary>
        /// private datetime variable to hold the invoice date
        /// </summary>
        private static DateTime dInvoiceDate;
        #endregion

        #region public attributes
        /// <summary>
        /// public variable referening dTotalCost
        /// </summary>
        public static double TotalCost
        {
            get { return dTotalCost; }
            set { dTotalCost = value; }
        }

        /// <summary>
        /// public datetime referencing dDateTime
        /// </summary>
        public static DateTime InvoiceDate
        {
            get { return dInvoiceDate; }
            set { dInvoiceDate = value; }
        }
        #endregion

        /// <summary>
        /// constructor for Main Logic
        /// </summary>
        public clsMainLogic()
        {
            try
            {
                // instantiate a new dataAccess class
                db = new clsDataAccess();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            } 
        }

        /// <summary>
        /// Method to get the all of the items from database
        /// </summary>
        /// <returns></returns>
        public List<Common.clsItem> GetItemsManager()
        {
            try
            {
                // instantiate DataAccess Class
                clsDataAccess db = new clsDataAccess();

                // instantiate a dataset to hold the data we get back
                DataSet ds = new DataSet();

                // The number of return values
                int iRet = 0;

                // create initial items list
                items = new List<Common.clsItem>();

                // use the data access class in order to execute a SQL statement
                ds = db.ExecuteSQLStatement(clsMainSQL.GetItemDesc(), ref iRet);

                // loop through the dataset.  For each row, creat a new clsItem, fill it up and add List of Items
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // create a new item object
                    Common.clsItem item = new Common.clsItem();

                    // ItemCode
                    item.ItemCode = dr[0].ToString();

                    // Item Description
                    item.Description = dr[1].ToString();

                    // Item Cost
                    item.Cost = dr[2].ToString();

                    // add to the list of items
                    items.Add(item);
                }
                // return this list
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method gets the Item object and its attributes from the database table for specific invoice number
        /// </summary>
        /// <param name="InvoiceNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Common.clsItem> GetInvoiceItemsManager(string InvoiceNumber)
        {
            try
            {
                // instantiate DataAccess Class
                clsDataAccess db = new clsDataAccess();

                // instantiate a dataset to hold the data we get back
                DataSet ds = new DataSet();

                // The number of return values
                int iRet = 0;

                // create initial items list
                items = new List<Common.clsItem>();

                // use the data access class in order to execute a SQL statement
                ds = db.ExecuteSQLStatement(clsMainSQL.GetInvoiceItems(InvoiceNumber), ref iRet);

                // loop through the dataset.  For each row, creat a new clsItem, fill it up and add List of Items
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // create a new item object
                    Common.clsItem item = new Common.clsItem();

                    // ItemCode
                    item.ItemCode = dr[0].ToString();

                    // Item Description
                    item.Description = dr[1].ToString();

                    // Item Cost
                    item.Cost = dr[2].ToString();

                    // add to the list of items
                    items.Add(item);

                }
                // return this list
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// method to save a new invoice to database table
        /// </summary>
        /// <param name=""></param>
        public string SaveNewInvoice(DateTime InvoiceDateTime, double TotalCost, List<clsItem> clsItems)
        {
            try
            {
                string maxInvoiceNum = "";

                // cast datetime to a string
                string StringDate = InvoiceDate.ToString("dd/MM/yyyy");

                // cast total cost to a string
                string StringTotalCost = TotalCost.ToString();

                // Number of values affected by the non-query statement    
                int iRet = 0; 

                // execute the non-query to insert invoice into Invoice table
                iRet = db.ExecuteNonQuery(clsMainSQL.InsertInvoice(StringDate, StringTotalCost));

                // variable to hold iRet
                int rowsAffected = iRet;

                // If row was successfully added, get max row
                if(rowsAffected > 0)
                {
                    // Query the max invoice number from the invoice table
                    maxInvoiceNum = db.ExecuteScalarSQL(clsMainSQL.GetMaxInvoiceNum());

                    // Insert line items to LineItems invoice using maxInvoiceNum
                    InsertLineItems(maxInvoiceNum, clsItems);
                }
                
                // return max invoice number
                return maxInvoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

       
        /// <summary>
        /// Method called to insert Items into the database for specified invoice number
        /// </summary>
        /// <param name="InvoiceNum"></param>
        public void InsertLineItems(string InvoiceNum, List<clsItem> clsItems)
        {
            try
            {
                // Number of values affected by the non-query statement    
                int iRet = 0;

                // loop through the list of items and insert each clsItem object into LineItems table   
                for (int i = 0; i < clsItems.Count; i++)
                {
                    // execute the non-query to insert invoice into Invoice table
                    iRet += db.ExecuteNonQuery(clsMainSQL.InsertLineItems(InvoiceNum, (i + 1).ToString(), clsItems[i].ItemCode));
                }

                int rowsAffected = iRet;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            } 
        }

        /// <summary>
        /// Method to save updated invoice
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public void UpdateInvoice(string InvoiceNumber, DateTime InvoiceDateTime, double TotalCost, List<clsItem> clsItems)
        {
            try
            {
                // cast datetime to a string
                string StringDate = InvoiceDate.ToString("dd/MM/yyyy");

                // cast total cost to a string
                string StringTotalCost = TotalCost.ToString();

                // Number of values affected by the non-query statement    
                int iRet = 0;

                // execute the non-query to insert invoice invoice table
                iRet = db.ExecuteNonQuery(clsMainSQL.UpdateInvoice(InvoiceNumber, StringDate, StringTotalCost));

                // number of rows deleted
                int iDel;

                // delete and re-add invoice items
                iDel = db.ExecuteNonQuery(clsMainSQL.DeleteItems(InvoiceNumber));

                // insert line items again into invoice
                InsertLineItems(InvoiceNumber, clsItems);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        // Gets the invoice and items for the selected invoice from SEARCH WINDOW
        /// </summary>
        public object GetInvoice(string sInvoiceNumber)
        {
            try
            {
                // instantiate a new DataAccess Class
                clsDataAccess db = new clsDataAccess();

                // instantiate a dataset to hold the data we get back
                DataSet ds = new DataSet();

                // The number of return values
                int iRet = 0;

                // Get invoice date & total cost from invoice database table using given invoice number
                ds = db.ExecuteSQLStatement(Main.clsMainSQL.SelectInvoice(sInvoiceNumber), ref iRet);

                // create a new invoice object
                Common.clsInvoice clsInvoice = new Common.clsInvoice();

                // loop through the dataset. for each Row, create a new clsFlight, fill it up add List of Flights
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // invoiceID
                    clsInvoice.InvoiceNum = dr[0].ToString();

                    // invoice date
                    clsInvoice.InvoiceDate = dr[1].ToString();

                    // invoice total cost
                    clsInvoice.TotalCost = dr[2].ToString();
                }
                return clsInvoice;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
