using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NaplatneRampeSrbije.ViewsControllers
{
    /// <summary>
    /// Interaction logic for KorisnikCRUDView.xaml
    /// </summary>
    public partial class EmployeeCRUDView : Window
    {
        private readonly IEmployeeCRUDService _employeeCRUDService;
        private readonly IAddressRepo _addressRepo;
        private readonly ITollBoothRepo _tollBoothRepo;
        private readonly ITollStationRepo _tollStationRepo;

        public EmployeeCRUDView(
            IEmployeeCRUDService employeeCRUDService,
            IAddressRepo addressRepo,
            ITollBoothRepo tollBoothRepo,
            ITollStationRepo tollStationRepo)
        {
            InitializeComponent();
            _employeeCRUDService = employeeCRUDService;
            _addressRepo = addressRepo;
            _tollBoothRepo = tollBoothRepo;
            _tollStationRepo = tollStationRepo;
            FillAddressComboBox();
            billingOfficerRadioButton.IsChecked = true;
        }

        private void registerEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckTextBoxesFilled();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            Gender gender = GetCheckedGender();
            string phoneNumber = phoneNumberTextBox.Text;
            WorkPlace workPlace = GetCheckedWorkPlace();
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            Address address = (Address)addressComboBox.SelectedItem;
            int workSpaceID = GetWorkSpaceID();

            try
            {
                if (MessageBox.Show("Kreiraj?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _employeeCRUDService.CreateEmployee(firstName, lastName, gender, phoneNumber, workPlace, username, password, address.ID, workSpaceID);
                    _ = MessageBox.Show("Radnik uspešno registrovan", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    firstNameTextBox.Clear();
                    lastNameTextBox.Clear();
                    phoneNumberTextBox.Clear();
                    usernameTextBox.Clear();
                    passwordTextBox.Clear();
                    maleRadioButton.IsChecked = true;
                    billingOfficerRadioButton.IsChecked = true;
                    FillWorkSpaceComboBoxWithTollBooths();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckTextBoxesFilled()
        {
            if (string.IsNullOrEmpty(firstNameTextBox.Text))
            {
                throw new Exception("Ime nije popunjeno");
            }
            if (string.IsNullOrEmpty(lastNameTextBox.Text))
            {
                throw new Exception("Prezime nije popunjeno");
            }
            if (string.IsNullOrEmpty(phoneNumberTextBox.Text))
            {
                throw new Exception("Broj telefona nije popunjen");
            }
            if (string.IsNullOrEmpty(usernameTextBox.Text))
            {
                throw new Exception("Korisničko ime nije popunjeno");
            }
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                throw new Exception("Lozinka nije popunjena");
            }
        }

        private Gender GetCheckedGender()
        {
            if ((bool)maleRadioButton.IsChecked)
            {
                return Gender.Male;
            }
            else
            {
                return Gender.Female;
            }
        }

        private int GetWorkSpaceID()
        {
            int workSpaceID = 0;
            if ((bool)billingOfficerRadioButton.IsChecked)
            {
                TollBooth tollBooth = (TollBooth)workSpaceComboBox.SelectedItem;
                workSpaceID = tollBooth.ID;
            }
            else if ((bool)tollStationHeadRadioButton.IsChecked)
            {
                TollStation tollStation = (TollStation)workSpaceComboBox.SelectedItem;
                workSpaceID = tollStation.ID;
            }

            return workSpaceID;
        }

        private WorkPlace GetCheckedWorkPlace()
        {
            if ((bool)billingOfficerRadioButton.IsChecked)
            {
                return WorkPlace.BillingOfficer;
            }
            else if ((bool)tollStationHeadRadioButton.IsChecked)
            {
                return WorkPlace.TollStationHead;
            }
            else if ((bool)managerRadioButton.IsChecked)
            {
                return WorkPlace.Manager;
            }
            else
            {
                return WorkPlace.Admininstrator;
            }
        }

        private void displayEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeRUDView employeeRUDVIew = new EmployeeRUDView(
                new EmployeeCRUDService(
                    new EmployeeRepo(),
                    new AddressRepo(),
                    new TollBoothRepo(),
                    new TollStationRepo()),
                new EmployeeRepo());
            Close();
            employeeRUDVIew.Show();
        }

        private void FillAddressComboBox()
        {
            List<Address> allAddresses = _addressRepo.GetAll();
            addressComboBox.ItemsSource = allAddresses;
            addressComboBox.SelectedItem = allAddresses[0];
        }

        private void FillWorkSpaceComboBoxWithTollBooths()
        {
            List<TollBooth> allTollBooths = _tollBoothRepo.GetAll();
            workSpaceComboBox.ItemsSource = allTollBooths;
            workSpaceComboBox.SelectedItem = allTollBooths[0];
        }

        private void FillWorkSpaceComboBoxWithTollStations()
        {
            List<TollStation> allTollStations = _tollStationRepo.GetAll();
            workSpaceComboBox.ItemsSource = allTollStations;
            workSpaceComboBox.SelectedItem = allTollStations[0];
        }

        private void enableWorkSpaceComboBox_Checked(object sender, RoutedEventArgs e)
        {
            workSpaceComboBox.IsEnabled = true;
            if ((bool)billingOfficerRadioButton.IsChecked)
            {
                FillWorkSpaceComboBoxWithTollBooths();
            }
            else
            {
                FillWorkSpaceComboBoxWithTollStations();
            }
        }

        private void disableWorkSpaceComboBox_Checked(object sender, RoutedEventArgs e)
        {
            workSpaceComboBox.IsEnabled = false;
            workSpaceComboBox.SelectedItem = null;
            workSpaceComboBox.ItemsSource = null;
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            AdministratorMainView administratorMainView = new AdministratorMainView();
            Close();
            administratorMainView.Show();
        }
    }
}
