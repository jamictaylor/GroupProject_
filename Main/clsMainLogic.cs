using System;
using System.Collections.Generic;
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
        // create an object to reference the list of items from clsItem
        public List<Common.clsItem> items;

        /// <summary>
        /// create an object of type clsDataAccess to access the database
        /// </summary>
        clsDataAccess db;


        /// <summary>
        /// invoice class
        /// </summary>
        Common.clsInvoice invoice;

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
            

            // instantiate a new dataAccess class
            db = new clsDataAccess();

        }


        /// <summary>
        /// Method to get the items that the UI will call on for the combobox
        /// </summary>
        /// <returns></returns>
        public List<Common.clsItem> GetItemsManager()
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

        /// <summary>
        /// method to save a new invoice
        /// </summary>
        /// <param name=""></param>
        public void SaveNewInvoice(DateTime InvoiceDateTime, double TotalCost)
        {
            try
            {
                // cast datetime to a string
                string StringDate = InvoiceDate.ToString("dd/MM/yyyy");

                // cast total cost to a string
                string StringTotalCost = TotalCost.ToString();

                // Number of values affected by the non-query statement    
                int iRet = 0; 

                // execute the non-query to insert invoice into Invoice table
                iRet = db.ExecuteNonQuery(clsMainSQL.InsertInvoice(StringDate, StringTotalCost));

                int rowsAffected = iRet;

                // If row was successfully added, get max row
                if(rowsAffected > 0)
                {
                    // Query the max invoice number from the invoice table
                    string maxInvoiceNum = db.ExecuteScalarSQL(clsMainSQL.GetMaxInvoiceNum());

                    // Insert line items to LineItems invoice once you have the invoice ID
                    InsertLineItems(maxInvoiceNum);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void InsertLineItems(string InvoiceNum)
        {

        }

        /// <summary>
        /// Method to be sent to UI when user wants to edit an existing invoice
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        //private void EditInvoice(clsOldInvoice, clsNewInvoice)
        //{

        //}

        // Get the invoice and items for the selected invoice from SEARCH WINDOW
        //   GetInvoice(InvoiceNumber) returns clsInvoice
        public object GetInvoice(string sInvoiceNumber)
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
    }
}
