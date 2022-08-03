using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    /// <summary>
    /// Is this needed to display both the Invoice and invoice items on the main page?
    /// </summary>
    internal class InvoiceAndItems
    {

        public string InvoiceNum { get; set; }

        public string InvoiceDate { get; set; }

        public string TotalCost { get; set; }

        /// <summary>
        /// A list of invoice tiems
        /// </summary>
        private List<Common.clsItem> lstItems = new List<Common.clsItem>();
        
        public List<Common.clsItem> Items
        {
            get { return lstItems; }
            set { lstItems = value; }

        }

        // what the constructor would look like
    //    public InvoiceAndItems()
   //     {
    //        lstItems.Add(new clsItem { Cost = "", Description = "", ItemCode = "" });
    //    }
    }
}
