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
    /// Interaction logic for KorisnikRUDView.xaml
    /// </summary>
    public partial class EmployeeRUDView : Window
    {
        private readonly IEmployeeCRUDService _employeeCRUDService;
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeRUDView(IEmployeeCRUDService employeeCRUDService, IEmployeeRepo employeeRepo)
        {
            InitializeComponent();
            _employeeCRUDService = employeeCRUDService;
            _employeeRepo = employeeRepo;
            FillEmployeeDataGrid();
        }

        private void FillEmployeeDataGrid()
        {
            employeeDataGrid.Items.Clear();
            foreach (Employee employee in _employeeRepo.GetAll())
            {
                employeeDataGrid.Items.Add(employee);
            }
        }

        private void updateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = employeeDataGrid.SelectedItem as Employee;
            if (employee == null)
            {
                _ = MessageBox.Show("Radnik nije izabran", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EmployeeUView employeeUView = new EmployeeUView(
                new EmployeeCRUDService(
                    new EmployeeRepo(),
                    new AddressRepo(),
                    new TollBoothRepo(),
                    new TollStationRepo()),
                new AddressRepo(),
                new TollBoothRepo(),
                new TollStationRepo(),
                employee);
            Close();
            employeeUView.Show();
        }

        private void deleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = employeeDataGrid.SelectedItem as Employee;
            if (employee == null)
            {
                _ = MessageBox.Show("Radnik nije izabran", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("Izbriši?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _employeeCRUDService.DeleteEmployee(employee.ID);
                _ = MessageBox.Show("Uspešno uklonjen radnik", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                FillEmployeeDataGrid();
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeCRUDView employeeCRUDView = new EmployeeCRUDView(
                new EmployeeCRUDService(
                    new EmployeeRepo(),
                    new AddressRepo(),
                    new TollBoothRepo(),
                    new TollStationRepo()),
                new AddressRepo(),
                new TollBoothRepo(),
                new TollStationRepo());
            Close();
            employeeCRUDView.Show();
        }
    }
}
