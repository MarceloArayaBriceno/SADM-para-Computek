using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.Data.Models
{
    public class AzureOpenAI
    {
        public class Prompt
        {
            public string Texto { get; set; }
        }

        // Modelo para la respuesta de IA
        public class Respuesta
        {
            public string Texto { get; set; }
        }

        // Modelo para interacción general con Azure OpenAI
        public class Interaccion
        {
            public string Prompt { get; set; }
            public string Respuesta { get; set; }
        }

        // Modelo detallado para la solicitud de chat a Azure OpenAI
        public class OpenAIRequest
        {
            public List<ChatMessage> messages { get; set; }
            public double temperature { get; set; } = 0.7;
            public int max_tokens { get; set; } = 800;
        }

        // Modelo para mensajes de chat
        public class ChatMessage
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        // Modelo para deserializar la respuesta de Azure OpenAI
        public class OpenAIResponse
        {
            public List<OpenAIChoice> choices { get; set; }
        }

        // Modelo para manejar las opciones de respuesta
        public class OpenAIChoice
        {
            public ChatMessage message { get; set; }
        }

    }
}
