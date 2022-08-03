using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// create a list to hold the list of items from the UI datagrid to save invoice
        /// </summary>
        List<Common.clsItem> clsItems;

        #region private variables
        /// <summary>
        /// variable to hold if cost of item is added or subtracted
        /// </summary>
        private bool bIsNeg = false;

        /// <summary>
        /// variable to hold if form is in edit mode
        /// </summary>
        private bool bIsEditable = false;
        #endregion

        #region public attributes
        /// <summary>
        /// public variable referencing bIsNeg
        /// </summary>
        public bool IsNeg
        {
            get { return bIsNeg; }
            set { bIsNeg = value; }
        }

        /// <summary>
        /// public variable referencing bIsEditable
        /// </summary>
        public bool IsEditable
        {
            get { return bIsEditable; }
            set { bIsEditable = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for winMain
        /// </summary>
        public wndMain()
        {
            try
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
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method called when the New Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // make canvas visible
                canvasInvoice.Visibility = Visibility.Visible;

                // clear the screen from previous user entries
                ClearScreen();

                // ensure buttons are enabled
                ButtonsEnabled();

                // make sure that invoice is not editable
                IsEditable = false;

                InvoiceDataGrid.IsReadOnly = true;

                // Invoice Number text box to say 'TBD'
                txtboxInvoiceNumber.Text = "TBD";
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
                if(!InvoiceDataGrid.Items.IsEmpty)
                { 
                    // confirm date has been added
                    if(DatePickerInvoiceDate.SelectedDate.HasValue)
                    {
                        // remove message labels
                        MessageLabel.Content = "";
                        
                        // save datetime variable
                        clsMainLogic.InvoiceDate = (DateTime)DatePickerInvoiceDate.SelectedDate;

                        // instantiate a new clsItems list to hold the items from the datagrid
                        clsItems = new List<Common.clsItem>();

                        // loop through the InvoiceDataGrid to load the clsItems list
                        for(int i = 0; i < InvoiceDataGrid.Items.Count; i++)
                        {
                            // new object
                            Common.clsItem clsItem = new Common.clsItem();

                            // add the datagrid object to the new object
                            clsItem = (Common.clsItem)InvoiceDataGrid.Items[i];

                            // add the object to the list clsItems
                            clsItems.Add(clsItem);
                        }
                        if(IsEditable == true)  // if this is UPDATING an existing invoice
                        {
                            // call update Invoice - pass in current invoice number, invoice date, total cost and list of items
                            clsMainLogic.UpdateInvoice(txtboxInvoiceNumber.Text ,clsMainLogic.InvoiceDate, clsMainLogic.TotalCost, clsItems);

                            // confirmation message
                            MessageLabel.Content = "Your invoice has been updated and saved.";
                        }
                        else  // if this is saving a new invoice
                        {
                            // variable to hold the invoice number that is created
                            string invoiceNumber;

                            // save invoice, get invoice number and then save items associated with invoice
                            invoiceNumber = clsMainLogic.SaveNewInvoice(clsMainLogic.InvoiceDate, clsMainLogic.TotalCost, clsItems);

                            // Message that invoice has been savad
                            MessageLabel.Content = "Your Invoice has been saved as Invoice Number " + invoiceNumber;

                            // Display the new invoice number in invoice text box
                            txtboxInvoiceNumber.Text = invoiceNumber;
                        } 

                        // in view mode until user selects Edit or Search.
                        ViewOnlyMode();

                        // make invoice button visable
                        btnEditInvoice.Visibility = Visibility.Visible;

                        // confirm not in edit mode
                        IsEditable = false;
                    }
                    else   // error message if no date
                    {
                        MessageLabel.Content = "Enter Date before saving";
                    }
                }
                else // if no items in datagrid
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
        /// Method to disale use of all buttons
        /// </summary>
        private void ViewOnlyMode()
        {
            try
            {
                btnAddItem.IsEnabled = false;
                btRemoveItem.IsEnabled = false;
                btnSaveInvoice.IsEnabled = false;
                DatePickerInvoiceDate.IsEnabled = false;
                cbChooseItems.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to enable use of all buttons
        /// </summary>
        private void ButtonsEnabled()
        {
            try
            {
                btnAddItem.IsEnabled = true;
                btRemoveItem.IsEnabled = true;
                btnSaveInvoice.IsEnabled = true;
                DatePickerInvoiceDate.IsEnabled = true; ;
                cbChooseItems.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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

                // if item selected was not null
                if(cbChooseItems.SelectedItem != null)
                {
                    // Display cost of selected item
                    txtboxCost.Text = clsSelectedItem.Cost;
                }  
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
                if(cbChooseItems.SelectedItem == null)
                {
                    MessageLabel.Content = "Please enter an item before selecting Add.";
                    return;
                }

                // clear message label content
                MessageLabel.Content = "";

                // add clsSelectedItem to the datagrid
                InvoiceDataGrid.Items.Add(clsSelectedItem);

                ItemsControl.Equals(clsSelectedItem, true);

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
                // If Datagrid is empty, give error message and return
                if (InvoiceDataGrid.SelectedItems.Count == 0)
                {
                    MessageLabel.Content = "There are no items to remove.";
                    return;
                }
                if(InvoiceDataGrid.SelectedItems == null)
                {
                    MessageLabel.Content = "Please select an item to remove";
                }
                else // remove item
                {
                    // erase any message labels
                    MessageLabel.Content = "";

                    // bool IsNeg should be false to cost is added
                    IsNeg = true;

                    // call method to compute total cost
                    ComputeTotalCost(((GroupProject.Common.clsItem)InvoiceDataGrid.SelectedItem).Cost, IsNeg);

                    // remove the selected datagrid item from the datagrid
                    InvoiceDataGrid.Items.Remove(InvoiceDataGrid.SelectedItem);

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
            try
            {
                // change mode to edit
                IsEditable = true;

                // enable buttons
                ButtonsEnabled();
            }
            catch (Exception ex)
            {
                // top level message to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
                // variable to hold the cost
                double confirmedDoubleCost;

                if (double.TryParse(Cost, out confirmedDoubleCost) == true)
                {
                    // If cost is not negative, add to total cost
                    if (IsNeg == false)
                    {
                        clsMainLogic.TotalCost += confirmedDoubleCost;
                    }
                    else // if cost is negative, subtract from cost
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

        /// <summary>
        /// Method called when SEARCH WINDOW is closed and Invoice ID has been selected
        /// </summary>
        /// <param name="InvoiceNum"></param>
        public void GetSelection(string InvoiceNum)
        {
            try
            {
                if(InvoiceNum != null)
                {
                    // show the main form
                    this.Show();

                    // Clear the window before leaving
                    ClearScreen(); // screen not displaying properly

                    // ensure invoice canvas is visable
                    canvasInvoice.Visibility = Visibility.Visible;

                    // Edit Mode is false
                    IsEditable = false;

                    // Show Edit Button
                    btnEditInvoice.Visibility = Visibility.Visible;

                    // displayed invoice is in view only mode
                    ViewOnlyMode();

                    // new class
                    Common.clsInvoice clsInvoice = new Common.clsInvoice();

                    // get invoice
                    clsInvoice = (Common.clsInvoice)clsMainLogic.GetInvoice(InvoiceNum);

                    // display invoice number
                    txtboxInvoiceNumber.Text = clsInvoice.InvoiceNum;

                    // display invoice date
                    DatePickerInvoiceDate.Text = clsInvoice.InvoiceDate;

                    // display total cost
                    txtBoxTotalCost.Text = clsInvoice.TotalCost;

                    // instantiate a new clsItems list to hold the items from the datagrid
                    clsItems = new List<Common.clsItem>();

                    clsItems = clsMainLogic.GetInvoiceItemsManager(InvoiceNum);

                    // loop through the InvoiceDataGrid to load the clsItems list
                    for (int i = 0; i < clsItems.Count; i++)
                    {
                        // add Item to the datagrid
                        InvoiceDataGrid.Items.Add(clsItems[i]);
                    }
                }
                else // if invoice number is null, just return
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to clear all labels and text boxes from screen
        /// </summary>
        public void ClearScreen()
        {
            try
            {
                // make invoice visable, but blank
                canvasInvoice.Visibility = Visibility.Visible;

                // clear the combo box
                cbChooseItems.Text = null;

                // clear the cost text
                txtboxCost.Text = "";

                // no binding to datagrid
                InvoiceDataGrid.Items.Clear();

                // invoice number clear
                txtboxInvoiceNumber.Text = "";

                // date set to default
                DatePickerInvoiceDate.SelectedDate = default;

                // total cost cleared
                txtBoxTotalCost.Text = "";

                // clear message box
                MessageLabel.Content = "";

                // no edit button visible
                btnEditInvoice.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }   
        }
        
        /// <summary>
        /// Method to handle error
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
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
