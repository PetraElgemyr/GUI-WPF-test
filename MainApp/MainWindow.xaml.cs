using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace MainApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly CustomerService _customerService;
    private ObservableCollection<Customer> _customers = [];
    public MainWindow(CustomerService customerService)
    {
        InitializeComponent();
        _customerService = customerService;
        UpdateListView();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        var customer = new Customer
        {
            Name = InputName.Text,
            Email = InputEmail.Text
        };
       
        _customerService.AddToList(customer);
        InputName.Text = "";
        InputEmail.Text = "";

        UpdateListView();
    }

    private void UpdateListView()
    {
        _customers.Clear();
        foreach (var item in _customerService.GetCustomers())
        {
            _customers.Add(item);
        }

        // Det nedan funkar ej
        // _customers = _customerService.GetCustomers() as ObservableCollection<Customer>;
        //_customers = (ObservableCollection<Customer>) _customerService.GetCustomers();  
        LvCustomers.ItemsSource = _customers;

    }
}