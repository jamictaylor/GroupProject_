using System;
using System.Collections.Generic;
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
            canvasInvoice.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called when the Save Invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {

            // Once all the items are entered, the user can save the invoice
            //  clsMainLogic.SaveNewInvoice(); // needs work

            // query the max invoice number from the database to diaplay for the invoice number
            //TO DO: MaxInvoice();

            // This will lock in the data for viewing only

            // make Edit Invoice button visible
            btnEditInvoice.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called when selection is changed in the the inventory items combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // variable to hold the selected item
            clsSelectedItem = (Common.clsItem)cbChooseItems.SelectedItem;

            
        }

        /// <summary>
        /// Method to add item to the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            // add clsSelecteItem to the invoice

            // call method to compute total cost
            
            
        }

        /// <summary>
        /// Remove item from invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            // remove clsSelectedItem from invoice
            
            // call method to compute totalCost
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

        // After search window is closed, check property SelectedInvoiceID in the
        // Search Window to see if an invoice is selected.  If so, load the invoice
        //TO DO: NEEDS CODE


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
