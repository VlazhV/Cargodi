namespace Cargodi.DataAccess.Data.Seeds;

public static class Generator
{
    
    public static string GenerateLicenseNumber()
    {
        Random random = new Random();
        string[] letters = { "A", "B", "C", "E", "H", "K", "M", "N", "O", "P", "T", "Y" };
        string licenseNumber = "";

        // Generate 2 random letters
        licenseNumber += letters[random.Next(letters.Length)];
        licenseNumber += letters[random.Next(letters.Length)];

        // Generate 4 random numbers
        for (int i = 0; i < 4; i++)
        {
            licenseNumber += random.Next(10).ToString();
        }

        return licenseNumber;
    }
    
    public static string GenerateLicense()
    {
        Random random = new Random();
        string[] letters = { "A", "B", "C", "E", "H", "K", "M", "N", "O", "P", "T", "Y" };
        string licenseNumber = "";

        // Generate 2 random letters
        licenseNumber += letters[random.Next(letters.Length)];
        licenseNumber += letters[random.Next(letters.Length)];

        // Generate 4 random numbers
        for (int i = 0; i < 8; i++)
        {
            licenseNumber += random.Next(10).ToString();
        }

        return licenseNumber;
    }


    public static string GenerateMark()
    {
        Random random = new Random();
        string[] marks = { "BMW", "Audi", "Mercedes", "Toyota", "Ford", "Volkswagen", "Nissan", "Honda", "Hyundai", "Kia" };
        return marks[random.Next(marks.Length)];
    }

    public static int GenerateRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }
}