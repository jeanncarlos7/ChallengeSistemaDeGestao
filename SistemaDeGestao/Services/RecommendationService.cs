public class RecommendationService
{
    private readonly MLContext _mlContext;
    private readonly ITransformer _model;

    public RecommendationService()
    {
        _mlContext = new MLContext();
        _model = _mlContext.Model.Load("Data/MLModels/RecommendationModel.zip", out _);
    }

    public string GetSentiment(string text)
    {
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        var prediction = predictionEngine.Predict(new SentimentData { Text = text });

        return prediction.Prediction ? "Positive Sentiment" : "Negative Sentiment";
    }
}

public class SentimentData
{
    public string Text { get; set; }
}

public class SentimentPrediction
{
    public bool Prediction { get; set; }
    public float Probability { get; set; }
}
