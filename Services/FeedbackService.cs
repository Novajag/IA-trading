using System;
using System.IO;

namespace ML_2025.Services
{
    public class FeedbackService
    {
        private readonly string _archivoSecundario;

        public FeedbackService()
        {
            var carpetaML = Path.Combine(Directory.GetCurrentDirectory(), "MLModels");

            
            Directory.CreateDirectory(carpetaML);

            
            _archivoSecundario = Path.Combine(carpetaML, "FeedbackLog.csv");
        }

        public void GuardarFeedback(int valor1, int valor2, bool prediccion, string probabilidad)
        {
            if (!File.Exists(_archivoSecundario))
            {
                using var writer = new StreamWriter(_archivoSecundario, append: false);
                writer.WriteLine("Fecha,Texto,Prediccion,Probabilidade");
            }

            using var writer2 = new StreamWriter(_archivoSecundario, append: true);
            writer2.WriteLine(
                $"\"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\"," +
                $"\"valor de la semana pasada {valor1} , valor esta semana {valor2}\"," +
                $"{prediccion},{probabilidad}"
            );
        }
    }
}
