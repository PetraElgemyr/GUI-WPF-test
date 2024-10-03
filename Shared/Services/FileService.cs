namespace Shared.Services;

public class FileService
{
    // man vill generera constructor till filepath så att man kan ange en filepath när man instansierar FileService
    private readonly string _filePath = "";

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public bool SaveToFile(string content)
    {
        try
        {
            using var sw = new StreamWriter(_filePath);
            sw.WriteLine(content);
            return true;
        }
        catch
        {
        }
        return false;


    }


    public string GetFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                var content = sr.ReadToEnd();
                return content;
            }
        }
        catch
        {
        }
        return null!;


    }
}
