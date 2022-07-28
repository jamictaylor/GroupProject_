using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    public class clsInvoiceDate
    {
        /// <summary>
        /// Variable to hold the invoice date
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// Override of the to string function
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return InvoiceDate;
        }
    }
}
