using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EmployeesList.Models;
namespace EmployeesList.Views
{
    public partial class MainWindow : Window
    {
        private EmployeeRepository _repository;
        private ListBox _employeeListBox;
        private TextBox _nameTextBox, _departmentTextBox;
        private DatePicker _dateOfJoiningPicker;

        public MainWindow()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            _employeeListBox = this.FindControl<ListBox>("EmployeeListBox");
            _nameTextBox = this.FindControl<TextBox>("NameTextBox");
            _departmentTextBox = this.FindControl<TextBox>("DepartmentTextBox");
            _dateOfJoiningPicker = this.FindControl<DatePicker>("DateOfJoiningPicker");

            this.FindControl<Button>("AddButton").Click += AddEmployee_Click;
            this.FindControl<Button>("EditButton").Click += EditEmployee_Click;
            this.Loaded += MainWindow_Loaded;
            this.FindControl<Button>("DeleteButton").Click += DeleteEmployee_Click;
        }

        private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            DisplayEmployees();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.DataContext = this;

        }

        private void AddEmployee_Click(object sender, EventArgs e)
        {
            var employee = new Employee
            {
                Name = _nameTextBox.Text,
                Department = _departmentTextBox.Text,
                DateOfJoining = _dateOfJoiningPicker.SelectedDate?.DateTime ?? default
            };

            _repository.AddEmployee(employee);
            DisplayEmployees();
        }

        private void EditEmployee_Click(object sender, EventArgs e)
        {
            if (_employeeListBox.SelectedItem is Employee selectedEmployee)
            {
                selectedEmployee.Name = _nameTextBox.Text;
                selectedEmployee.Department = _departmentTextBox.Text;
                selectedEmployee.DateOfJoining = _dateOfJoiningPicker.SelectedDate?.DateTime ?? default;
                _repository.EditEmployee(selectedEmployee);
                DisplayEmployees();
            }
        }


        private void DeleteEmployee_Click(object sender, EventArgs e)
        {
            if (_employeeListBox.SelectedItem is Employee selectedEmployee)
            {
                _repository.DeleteEmployee(selectedEmployee.ID);
                DisplayEmployees();
            }
        }
        public ObservableCollection<Employee> Employees { get; set; }
        private void DisplayEmployees()
        {
            // Assuming GetEmployees() returns a List<Employee> or ObservableCollection<Employee>
           
            this.Employees = _repository.GetEmployees();
            _employeeListBox.ItemsSource = Employees;  // Use ItemsSource instead of Items
            
        }

        
    }
}
