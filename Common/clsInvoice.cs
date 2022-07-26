using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    internal class clsInvoice
    {
        #region private Attributes
        /// <summary>
        /// private variable to hold the InvoiceID for the invoice
        /// </summary>
        private string sInvoiceNum = "";

        /// <summary>
        /// private variable to hold the Invoice Date for the invoice
        /// </summary>
        private string sInvoiceDate = "";


        /// <summary>
        /// private variable to hold the total cost of the invoice
        /// </summary>
        private string sTotalCost = "";

        #endregion

        #region public properties
        /// <summary>
        /// public property of sInvoiceNum
        /// </summary>
        public string InvoiceNum  { get { return sInvoiceNum; } set { sInvoiceNum = value; } }

        /// <summary>
        /// public property of sInvoiceDate
        /// </summary>
        public string InvoiceDate { get { return sInvoiceDate; } set { sInvoiceDate = value; } }

        /// <summary>
        /// public property of sTotalCost
        /// </summary>
        public string TotalCost { get { return sTotalCost; } set { sTotalCost = value; } }

        #endregion

        #region Methods
        // ToString override? would be needed for a combo box


        #endregion
    }
}
