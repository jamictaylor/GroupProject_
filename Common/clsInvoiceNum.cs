using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    public class clsInvoiceNum
    {
        /// <summary>
        /// Variable to hold the invoice number
        /// </summary>
        public string invoiceNum { get; set; }
        /// <summary>
        /// Override of the to string function
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return invoiceNum;
        }
    }
}
