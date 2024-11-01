using Microsoft.ML.Data;

namespace SistemaDeGestao.Models
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string Text { get; set; }
    }
}
