using NaplatneRampeSrbije.Models;
using NaplatneRampeSrbije.Models.Repositories.Implementations;
using NaplatneRampeSrbije.Models.Repositories.Interfaces;
using NaplatneRampeSrbije.Models.Services;
using NaplatneRampeSrbije.Models.Services.Implementations;
using NaplatneRampeSrbije.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace NaplatneRampeSrbije.ViewsControllers
{
    public partial class EmployeeUView : Window
    {
        private readonly IEmployeeCRUDService _employeeCRUDService;
        private readonly IAddressRepo _addressRepo;
        private readonly ITollBoothRepo _tollBoothRepo;
        private readonly ITollStationRepo _tollStationRepo;
        private readonly Employee _oldEmployee;

        public EmployeeUView(
            IEmployeeCRUDService employeeCRUDService, 
            IAddressRepo addressRepo,
            ITollBoothRepo tollBoothRepo,
            ITollStationRepo tollStationRepo,
            Employee employee)
        {
            InitializeComponent();
            _employeeCRUDService = employeeCRUDService;
            _addressRepo = addressRepo;
            _tollBoothRepo = tollBoothRepo;
            _tollStationRepo = tollStationRepo;
            _oldEmployee = employee;
            FillAddressComboBox();
            SetOldEmployeeValues();
        }

        private void SetOldEmployeeValues()
        {
            firstNameTextBox.Text = _oldEmployee.FirstName;
            lastNameTextBox.Text = _oldEmployee.LastName;
            phoneNumberTextBox.Text = _oldEmployee.PhoneNumber;
            usernameTextBox.Text = _oldEmployee.Username;
            passwordTextBox.Text = _oldEmployee.Password;
            addressComboBox.SelectedItem = _oldEmployee.Address;

            switch (_oldEmployee.Gender)
            {
                case Gender.Male:
                    maleRadioButton.IsChecked = true;
                    break;
                case Gender.Female:
                    femaleRadioButton.IsChecked = true;
                    break;
            }

            switch (_oldEmployee.WorkPlace)
            {
                case WorkPlace.BillingOfficer:
                    workSpaceComboBox.SelectedItem = _oldEmployee.TollBooth;
                    billingOfficerRadioButton.IsChecked = true;
                    break;
                case WorkPlace.TollStationHead:
                    workSpaceComboBox.SelectedItem = _oldEmployee.TollStation;
                    tollStationHeadRadioButton.IsChecked = true;
                    break;
                case WorkPlace.Manager:
                    managerRadioButton.IsChecked = true;
                    break;
                case WorkPlace.Admininstrator:
                    administratorRadioButton.IsChecked = true;
                    break;
            }
        }

        private void updateEmployeeButton_Click(object sender, RoutedEventArgs e)
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

            int id = _oldEmployee.ID;
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
                if (MessageBox.Show("Izmeni?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _employeeCRUDService.UpdateEmployee(id, firstName, lastName, gender, phoneNumber, workPlace, username, password, address.ID, workSpaceID);
                    _ = MessageBox.Show("Radnik uspešno izmenjen", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    EmployeeRUDView employeeRUDView = new EmployeeRUDView(
                    new EmployeeCRUDService(
                        new EmployeeRepo(),
                        new AddressRepo(),
                        new TollBoothRepo(),
                        new TollStationRepo()),
                    new EmployeeRepo());
                    Close();
                    employeeRUDView.Show();
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

        private void FillAddressComboBox()
        {
            List<Address> allAddresses = _addressRepo.GetAll();
            addressComboBox.ItemsSource = allAddresses;
            foreach (Address address in allAddresses)
            {
                if (address.ID == _oldEmployee.Address.ID)
                {
                    addressComboBox.SelectedItem = address;
                    break;
                }
            }
        }

        private void FillWorkSpaceComboBoxWithTollBooths()
        {
            List<TollBooth> allTollBooths = _tollBoothRepo.GetAll();
            workSpaceComboBox.ItemsSource = allTollBooths; 
            foreach (TollBooth tollBooth in allTollBooths)
            {
                if (tollBooth.ID == _oldEmployee.Address.ID)
                {
                    workSpaceComboBox.SelectedItem = tollBooth;
                    break;
                }
            }
        }

        private void FillWorkSpaceComboBoxWithTollStations()
        {
            List<TollStation> allTollStations = _tollStationRepo.GetAll();
            workSpaceComboBox.ItemsSource = allTollStations;
            foreach (TollStation tollStation in allTollStations)
            {
                if (tollStation.ID == _oldEmployee.Address.ID)
                {
                    workSpaceComboBox.SelectedItem = tollStation;
                    break;
                }
            }
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
            EmployeeRUDView employeeRUDView = new EmployeeRUDView(
                new EmployeeCRUDService(
                    new EmployeeRepo(),
                    new AddressRepo(),
                    new TollBoothRepo(),
                    new TollStationRepo()),
                new EmployeeRepo());
            Close();
            employeeRUDView.Show();
        }
    }
}
