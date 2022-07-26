using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    internal class clsItem
    {
        #region private attributes

        /// <summary>
        /// private variable to hold the ItemCode for each invoice item
        /// </summary>
        private string sItemCode = "";

        /// <summary>
        /// private variable to hold the Description for each invoice item
        /// </summary>
        private string sDescription = "";

        /// <summary>
        /// private variable to hold the Cost for each invoice item
        /// </summary>
        private string sCost = "";
        
        #endregion

        #region public properties

        /// <summary>
        /// public property of sItemCode
        /// </summary>
        public string ItemCode { get { return sItemCode; } set { sItemCode = value; } }

        /// <summary>
        /// public property of sDescription
        /// </summary>
        public string Description { get { return sDescription; } set { sDescription = value; } }
        
        /// <summary>
        /// public property of sCost
        /// </summary>
        public string Cost { get { return sCost; } set { sCost = value; } }

        #endregion

        /// <summary>
        /// override ToString method to be called by the combobox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                // return the flightNumber and Aircraft type to be displayed in combobox
                return ItemCode + " - " + Description;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
