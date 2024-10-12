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
            _model = _mlContext.Model.Load("Data/MLModels/RecommendationModel.zip", out _);
        }

        public void TrainRecommendationModel()
        {
            var data = _mlContext.Data.LoadFromTextFile<ProductData>("Data/product_data.csv", separatorChar: ',', hasHeader: true);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(ProductData.ProductCategory))
                          .Append(_mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(data);

            _mlContext.Model.Save(model, data.Schema, "Data/MLModels/RecommendationModel.zip");
        }

        public string GetRecommendation(string category)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ProductData, ProductRecommendation>(_model);
            var prediction = predictionEngine.Predict(new ProductData { ProductCategory = category });
            return $"Recommended product for category '{category}' has a score of {prediction.Score}.";
        }
    }

    public class ProductRecommendation
    {
        public float Score { get; set; }
    }
}
