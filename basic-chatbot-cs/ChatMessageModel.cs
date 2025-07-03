using Microsoft.ML.Data;

namespace basic_chatbot_cs
{
    public class ChatMessageData
    {
        [LoadColumn(0)]
        public string? UserInput { get; set; }

        [LoadColumn(1)]
        public string? BotResponse { get; set; }
    }

    public class ChatMessagePrediction
    {
        [ColumnName("PredictedLabel")]
        public string? Response { get; set; }
    }
}