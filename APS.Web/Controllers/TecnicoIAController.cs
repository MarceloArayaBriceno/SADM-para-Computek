using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using APS.Data.Models;

namespace APS.Web.Controllers
{
    public class TecnicoIAController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _endpoint;
        private readonly ILogger<TecnicoIAController> _logger;

        public TecnicoIAController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<TecnicoIAController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["Azure:OpenAI:Key"];
            _endpoint = configuration["Azure:OpenAI:Endpoint"].TrimEnd('/');
            _logger = logger;

            // Configurar encabezados para Azure OpenAI
            _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Preguntar(AzureOpenAI.Prompt prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt.Texto))
            {
                ModelState.AddModelError("Texto", "Por favor, ingrese un texto para consultar.");
                return View("Index");
            }

            try
            {
                // Preparar solicitud para Azure OpenAI
                var openAIRequest = new AzureOpenAI.OpenAIRequest
                {
                    messages = new List<AzureOpenAI.ChatMessage>
                    {
                        new AzureOpenAI.ChatMessage
                        {
                            role = "system",
                            content = "Eres un asistente técnico especializado en computación. Proporciona respuestas únicamente sobre problemas técnicos y de computación. Si la pregunta no está relacionada con computación, responde indicando que solo puedes ayudar con ese tema."
                        },
                        new AzureOpenAI.ChatMessage
                        {
                            role = "user",
                            content = prompt.Texto
                        }
                    },
                    temperature = 0.7,
                    max_tokens = 800
                };

                // Serializar solicitud
                var requestBody = JsonConvert.SerializeObject(openAIRequest);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Enviar solicitud a Azure OpenAI
                var requestUri = $"{_endpoint}/openai/deployments/gpt-4o-pdf/chat/completions?api-version=2024-08-01-preview";
                var response = await _httpClient.PostAsync(requestUri, content);

                // Verificar respuesta
                response.EnsureSuccessStatusCode();

                // Leer respuesta
                var responseBody = await response.Content.ReadAsStringAsync();
                var openAIResponse = JsonConvert.DeserializeObject<AzureOpenAI.OpenAIResponse>(responseBody);

                // Obtener texto de respuesta
                var respuesta = new AzureOpenAI.Respuesta
                {
                    Texto = openAIResponse?.choices?[0]?.message?.content ?? "No se pudo generar una respuesta."
                };

                ViewBag.Respuesta = respuesta.Texto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar con la IA");
                ViewBag.Respuesta = "Ocurrió un error al procesar su solicitud. Por favor, intente nuevamente.";
            }

            return View("Index");
        }
    }
}
