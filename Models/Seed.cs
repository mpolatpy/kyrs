using OfficeOpenXml;

namespace PetAdoption.Models
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Pets.Any())
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, @"Models/", "Pet-Data.xlsx");
                var file = new FileInfo(filePath);
                await LoadDataFromExcel(file, context);
            }
        }

        private static async Task LoadDataFromExcel(FileInfo file, DataContext context)
        {
            List<Pet> pets = new();
            using var package = new ExcelPackage(file);
            await package.LoadAsync(file);

            // the list of pets are expected to be in the first sheet in the Excel workbook
            var ws = package.Workbook.Worksheets[0];

            int row = 2; // First row is used for titles
            int col = 1;

            // check the first col in each row, and process rows with IDs
            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
            {
                var pet = new Pet
                {
                    Id = int.Parse(ws.Cells[row, col].Value.ToString()),
                    Name = ws.Cells[row, col + 1].Value.ToString(),
                    Type = ws.Cells[row, col + 2].Value.ToString(),
                    Gender = ws.Cells[row, col + 3].Value.ToString(),
                    ZipCode = ws.Cells[row, col + 4].Value.ToString()
                };
                
                pets.Add(pet);
                row += 1;
            }

            await context.AddRangeAsync(pets);
            await context.SaveChangesAsync();
        }
    }
}