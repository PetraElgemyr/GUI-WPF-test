using Newtonsoft.Json;
using Shared.Models;

namespace Shared.Services;

public class CustomerService
{
    private readonly FileService _fileService;
    private List<Customer> _customers = [];

    // Generera constructor till CustomerService så att CustomereService också tar emot en filepath, som sen skickas vidare till fileService
    public CustomerService(FileService fileService)
    {
        _fileService = fileService;
    }

    public bool AddToList(Customer customer)
    {
        try
        {
            _customers.Add(customer);
            _fileService.SaveToFile(JsonConvert.SerializeObject(_customers));
            return true;

        }
        catch
        {
        }
        return false;
    }

    public IEnumerable<Customer> GetCustomers()
    {
        try
        {
            var json = _fileService.GetFromFile();
            if (!string.IsNullOrEmpty(json))
            {
                _customers = JsonConvert.DeserializeObject<List<Customer>>(json)!;
            }
            return _customers;
        }
        catch
        {
        }
        return null!;
    }

}

