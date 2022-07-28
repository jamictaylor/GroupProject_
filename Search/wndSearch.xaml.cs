using GroupProject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Variable that will be passed to the main window
        /// </summary>
        string selectedID;
        /// <summary>
        /// Variable to hold the item in the ID combo box
        /// </summary>
        string comboID = "0";
        /// <summary>
        /// Variable to hold the item in the Date combo box
        /// </summary>
        string comboDate = "0";
        /// <summary>
        /// Variable to hold the item in the cost combo box
        /// </summary>
        string comboCost = "0";
        /// <summary>
        /// Constuctor for the search window
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
                clsSearchLogic clsSearchLogic = new clsSearchLogic();
                DataSet ds = clsSearchLogic.GetInitialDataGrid();
                datagridInvoices.ItemsSource = ds.Tables[0].DefaultView;
                comboInvoiceAmount.ItemsSource = clsSearchLogic.GetCostCombo();
                comboInvoiceDate.ItemsSource = clsSearchLogic.GetDateCombo();
                comboInvoiceID.ItemsSource = clsSearchLogic.GetNumCombo();
            }
            catch(Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method called when the cancel button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method called when the invoice ID combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboInvoiceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsInvoiceNum invoiceNum = (clsInvoiceNum)comboInvoiceID.SelectedItem;
                clsSearchLogic searchLogic = new clsSearchLogic();
                string selectedNum; ;
                selectedNum = invoiceNum.ToString();
                comboID = selectedNum;
                string selectedDate = comboDate;
                string selectedCost = comboCost;
                DataSet ds = searchLogic.updateDataGrid(selectedNum, selectedDate, selectedCost);
                datagridInvoices.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch(Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method called when the select button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get invoice id of the selected row. pass the id to a variable that the main window can access then hide the window
                this.Hide();
            }
            catch(Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method called when the invoice date combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsInvoiceDate invoiceDate = (clsInvoiceDate)comboInvoiceDate.SelectedItem;
                clsSearchLogic searchLogic = new clsSearchLogic();
                string selectedDate;
                selectedDate = invoiceDate.ToString();
                comboDate = selectedDate;
                string selectedNum = comboID;
                string selectedCost = comboCost;
                DataSet ds = searchLogic.updateDataGrid(selectedNum, selectedDate, selectedCost);
                datagridInvoices.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch(Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when the invoice Cost combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboInvoiceAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsInvoiceCost invoiceCost = (clsInvoiceCost)comboInvoiceAmount.SelectedItem;
                clsSearchLogic searchLogic = new clsSearchLogic();
                string selectedCost;
                selectedCost = invoiceCost.ToString();
                comboCost = selectedCost;
                string selectedDate = comboDate;
                string selectedNum = comboID;
                DataSet ds = searchLogic.updateDataGrid(selectedNum, selectedDate, selectedCost);
                datagridInvoices.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch(Exception ex)
            {
                HandleException(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Method to handle exceptions
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="Method"></param>
        /// <param name="Message"></param>
        public void HandleException(string Class, string Method, string Message)
        {
            try
            {
                MessageBox.Show(Class + "." + Method + " -> " + Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Handling Broke " + ex.Message);
            }
        }
    }
}
