using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace APS.Web.Utilities
{
    public static class WkhtmltopdfDownloader
    {
        private static readonly string downloadUrl = "https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox-0.12.6-1.msvc2015-win64.exe";
        private static readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Rotativa", "wkhtmltopdf.exe");

        public static async Task DownloadWkhtmltopdfAsync()
        {
            // Si el archivo no existe, descargarlo
            if (!File.Exists(outputPath))
            {
                Console.WriteLine("Descargando wkhtmltopdf.exe...");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(downloadUrl);
                    response.EnsureSuccessStatusCode();

                    // Guardar el archivo en la ruta especificada
                    using (var fileStream = new FileStream(outputPath, FileMode.CreateNew))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }
                }

                Console.WriteLine("Descarga completada.");
            }
            else
            {
                Console.WriteLine("wkhtmltopdf.exe ya existe en el directorio de destino.");
            }
        }
    }

}
