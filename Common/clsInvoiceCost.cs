using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    public class clsInvoiceCost
    {
        /// <summary>
        /// Variable to hold the cost
        /// </summary>
        public string invoiceCost { get; set; }
        /// <summary>
        /// Override of the to string function
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return invoiceCost;
        }
    }
}
