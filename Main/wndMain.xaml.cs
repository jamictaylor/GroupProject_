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

namespace GroupProject.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// Class that dislays the search window
        /// </summary>
        Search.wndSearch wndSearchForm;

        /// <summary>
        /// Main Logic class - holds business logic for window
        /// </summary>
        Main.clsMainLogic clsMainLogic;

        /// <summary>
        /// Create an object of type clsDataAccess to access the Database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Variable to hold the selected item in the list of items
        /// </summary>
        Common.clsItem clsSelectedItem;

        #region private variables

        

        /// <summary>
        /// variable to hold if cost of item is added or subtracted
        /// </summary>
        private bool bIsNeg = false;

        #endregion
       

        /// <summary>
        /// public variable referencing bIsNeg
        /// </summary>
        public bool IsNeg
        {
            get { return bIsNeg; }
            set { bIsNeg = value; }
        }


        #region public attributes

        #endregion


        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            // instantiate and add a new Search Window
            wndSearchForm = new Search.wndSearch();

            // instantiate a new MainLogic class
            clsMainLogic = new clsMainLogic();

            // instantiate a new sclsDataAccess class
            db = new clsDataAccess();

            // bind items to its items source property and display in Items combo Box
            cbChooseItems.ItemsSource = clsMainLogic.GetItemsManager();
            
        }

        /// <summary>
        /// Method called when the New Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            // make canvas visible
            canvasInvoice.Visibility = Visibility.Visible;

            // Invoice number textbox set to read only
           // txtboxInvoiceNumber.IsReadOnly = true;

            // Invoice Number text box to say 'TBD'
            txtboxInvoiceNumber.Text = "TBD";
        }

        /// <summary>
        /// Method called when the Save Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // confirm there are items listed in the datagrid
                if(InvoiceDataGrid != null)
                {
                    // confirm date has been added
                    if(DatePickerInvoiceDate.SelectedDate.HasValue)
                    {
                        // save datetime variable
                        clsMainLogic.InvoiceDate = (DateTime)DatePickerInvoiceDate.SelectedDate;

                        // save invoice, get invoice number and then save items associated with invoice
                        clsMainLogic.SaveNewInvoice(clsMainLogic.InvoiceDate, clsMainLogic.TotalCost);

                        // Lock in the data for viewing only


                        // allow for edit mode
                        // make invoice button visable
                        btnEditInvoice.Visibility = Visibility.Visible;

                    }
                    // error message if no value
                    else
                    {
                        MessageLabel.Content = "Enter Date before saving";
                    }
                }
                else
                {
                    // message label displaying to enter items
                    MessageLabel.Content = "Enter invoice items before saving";
                } 
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }            
        }

        /// <summary>
        /// Method called when selection is changed in the the inventory items combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // variable to hold the selected item
                clsSelectedItem = (Common.clsItem)cbChooseItems.SelectedItem;

                // Display cost of selected item
                txtboxCost.Text = clsSelectedItem.Cost;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method to add item to the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // add clsSelectedItem to the datagrid
                InvoiceDataGrid.Items.Add(clsSelectedItem);

                // bool IsNeg should be false to cost is added
                IsNeg = false;

                // call method to compute total cost
                ComputeTotalCost(clsSelectedItem.Cost, IsNeg);
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Remove item from invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If Datagrid is empty, do nothing and return
                if (InvoiceDataGrid.SelectedItems.Count == 0)
                {
                    return;
                }
                else // remove item
                {
                    // remove the selected datagrid item from the datagrid
                    InvoiceDataGrid.Items.Remove(InvoiceDataGrid.SelectedItem);

                    // bool IsNeg should be false to cost is added
                    IsNeg = true;

                    // call method to compute total cost
                    ComputeTotalCost(clsSelectedItem.Cost, IsNeg);
                }              
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method to call when selected invoice is now to be edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Method to display the Search Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Hide the main window
                this.Hide();

                

                // Show the add passenger form
                wndSearchForm.Show();

                // show the main form
                this.Show();
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Method called everytime an invoice item is added or deleted to invoice
        /// </summary>
        public void ComputeTotalCost(string Cost, bool isNeg)
        {
            try
            {
                // add cost of all invoice items and provide total

                double confirmedDoubleCost;

                if (double.TryParse(Cost, out confirmedDoubleCost) == true)
                {
                    // If cost is not negative, add to total cost
                    if (IsNeg == false)
                    {
                        clsMainLogic.TotalCost += confirmedDoubleCost;
                    }
                    // if cost is negative, subtract from cost
                    else
                    {
                        clsMainLogic.TotalCost -= confirmedDoubleCost;
                    }
                }

                // send total cost to the total cost text box
                txtBoxTotalCost.Text = clsMainLogic.TotalCost.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }
        // After search window is closed, check property SelectedInvoiceID in the
        // Search Window to see if an invoice is selected.  If so, load the invoice
        //TO DO: NEEDS CODE
        public void GetSelection(string InvoiceNum)
        {
            // Clear the window before leaving
            ClearScreen(); // screen not displaying properly

            // new class
            Common.clsInvoice clsInvoice = new Common.clsInvoice();

            // get invoice
            clsInvoice = (Common.clsInvoice)clsMainLogic.GetInvoice(InvoiceNum);

            // display invoice number
            txtboxInvoiceNumber.Text = clsInvoice.InvoiceNum; // screen not displaying properly
                
            // display invoice date
            DatePickerInvoiceDate.Text = clsInvoice.InvoiceDate; // screen not displaying properly

            // display total cost
            txtBoxTotalCost.Text = clsInvoice.TotalCost; // screen not displaying properly
            
            // get items connected to invoice
            
                
                // display items in datagrid


        }

        public void ClearScreen()
        {
            

            cbChooseItems.SelectedIndex = 0;

            InvoiceDataGrid.BindingGroup = null;

            txtboxInvoiceNumber.Text = "";

            DatePickerInvoiceDate.SelectedDate = default;

            txtBoxTotalCost.Text = "";
        }

        public void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                // would write to a file or database here
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }



    }
}
