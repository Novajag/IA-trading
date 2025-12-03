
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.ML;
using ML_2025.Models;
using ML_2025.Services;
using System;
using System.Reflection;
using IA_trading.Log;


namespace ML_2025.Pages
{
    [IgnoreAntiforgeryToken] 
    public class IndexModel : PageModel
    {
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;
        private readonly FeedbackService _feedbackService;

        public IndexModel(
            PredictionEngine<SentimentData, SentimentPrediction> predictionEngine,
            FeedbackService feedbackService)
        {
            _predictionEngine = predictionEngine;
            _feedbackService = feedbackService;
        }

       
        [BindProperty]

        public string InputText { get; set; }
        [BindProperty]
        public string InputText1 { get; set; }
        [BindProperty]
        public string InputText2 { get; set; }

        public SentimentPrediction PredictionResult { get; set; }


        [BindProperty]
        public int valor1 { get; set; }

        [BindProperty]
        public int valor2 { get; set; }

        [BindProperty]
        public bool Prediccion { get; set; }

        [BindProperty]
        public string Probabilidad { get; set; }

        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
           
            InputText = $"valor de la semana pasada {InputText1} , valor esta semana {InputText2}";

            if (string.IsNullOrWhiteSpace(InputText))
                return Page();

            var inputData = new SentimentData { Text = InputText };
            PredictionResult = _predictionEngine.Predict(inputData);

            IA_trading.Log.UserSave.SalvarPesquisa($"O valor da semana pasada {InputText1} , valor esta semana {InputText2}, valor resultado : {PredictionResult.PredictedLabel} y valor probabilidad {(PredictionResult.Probability * 100).ToString("F2")} ");

            return Page();
        }

        public IActionResult OnPostSalvar()
        {
            try
            {
                _feedbackService.GuardarFeedback(valor1, valor2, Prediccion, Probabilidad);
                return new JsonResult(new { mensaje = "Guardado correctamente" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { mensaje = $"Error: {ex.Message}" });
            }
        }
    }
}
