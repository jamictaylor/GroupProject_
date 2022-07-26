using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            ds = db.ExecuteSQLStatement(clsMainSQL.GetItems(), ref iRet);

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
      //  public void SaveNewInvoice(Common.clsInvoice)
      //  {
            // code to save invoice
      //  }
    }

   
    /// <summary>
    /// Method to be sent to UI when user wants to edit an existing invoice
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    //private void EditInvoice(clsOldInvoice, clsNewInvoice)
    //{

    //}

    // Get the invoice and items for the selected invoice from search window
    //GetInvoice(InvoiceNumber) returns clsInvoice
   // private clsInvoice GetInvoice(InvoiceNumber)
   // {

  //  }


}
