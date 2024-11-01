using Microsoft.ML;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Services
{
    public class RecommendationService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public RecommendationService()
        {
            _mlContext = new MLContext();
            _model = _mlContext.Model.Load("Data/MLModels/SentimentModel.zip", out _);
        }

        // Método para treinar e salvar o modelo de análise de sentimentos
        public void TrainSentimentModel()
        {
            // Carregar dados de exemplo para treinamento (substitua com o caminho real)
            var data = _mlContext.Data.LoadFromTextFile<SentimentData>("Data/sentiment_data.csv", hasHeader: true);

            // Definir pipeline de treinamento
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                          .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            // Treinar o modelo
            var model = pipeline.Fit(data);

            // Salvar o modelo treinado
            _mlContext.Model.Save(model, data.Schema, "Data/MLModels/SentimentModel.zip");
        }

        // Método para prever o sentimento usando o modelo carregado
        public string GetSentiment(string text)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
            var prediction = predictionEngine.Predict(new SentimentData { Text = text });

            return prediction.Prediction ? "Positive Sentiment" : "Negative Sentiment";
        }

        // Método para obter recomendações com base em uma categoria (substitua com a lógica real)
        public object GetRecommendation(string category)
        {
            // Lógica para obter recomendações com base na categoria (substitua com a lógica real)
            return new List<string> { "Recommendation 1", "Recommendation 2", "Recommendation 3" };
        }
    }
}
