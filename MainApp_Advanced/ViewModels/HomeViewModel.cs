using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;

namespace MainApp_Advanced.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;

    [ObservableProperty]
    private ObservableCollection<Customer> _customers = [];

    [ObservableProperty]
    private Customer _customer = new Customer();

    [ObservableProperty]
    private Customer _newCustomer = new Customer();

    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private string _email = "";


    public HomeViewModel(IServiceProvider serviceProvider, CustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
        GetCustomers();
    }

    public void GetCustomers()
    {
        Customers.Clear();
        foreach (var customer in _customerService.GetCustomers())
        {
            Customers.Add(customer);
        }
    }

    [RelayCommand]
    public void AddCustomer()
    {
        _customerService.AddToList(NewCustomer);
        NewCustomer = new Customer(); // bara för att tömma fälten
        GetCustomers();
    }



    [RelayCommand]
    public void DeleteCustomer(Customer customer)
    {
        if (customer != null)
        {
            Console.WriteLine("Deleting customer: " + customer.Name);
            _customerService.DeleteFromList(customer);
            Customer = new Customer(); // bara för att tömma fälten
            GetCustomers();
        } else
        {
            Console.WriteLine("Customer is null");
        }
    }

    [RelayCommand]
    public void GoToSettings()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<SettingsViewModel>();
    }
}
