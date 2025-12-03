using System;
using System.IO;

namespace IA_trading.Log
{
    public static class UserSave
    {
        private static readonly string _caminhoArquivo;

        static UserSave()
        {
      
            var carpetaML = Path.Combine(Directory.GetCurrentDirectory(), "MLModels");


            Directory.CreateDirectory(carpetaML);

            _caminhoArquivo = Path.Combine(carpetaML, "UserLog.csv");

            if (!File.Exists(_caminhoArquivo))
            {
                using var writer = new StreamWriter(_caminhoArquivo, append: false);
                writer.WriteLine("Data,Texto");
            }
        }


        public static void SalvarPesquisa(string texto)
        {

            if (string.IsNullOrWhiteSpace(texto))
                return;

  
            texto = texto.Replace("\"", "\"\"");

            using var writer = new StreamWriter(_caminhoArquivo, append: true);
            writer.WriteLine($"\"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\",\"{texto}\"");
        }
    }
}
