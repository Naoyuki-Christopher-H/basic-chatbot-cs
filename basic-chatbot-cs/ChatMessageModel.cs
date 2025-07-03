using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace basic_chatbot_cs
{
    public class ChatMessageModel
    {
        [LoadColumn(0)]
        public string UserInput { get; set; }

        [LoadColumn(1)]
        public string BotResponse { get; set; }
    }

    public class ChatPrediction
    {
        [ColumnName("PredictedLabel")]
        public string BotResponse { get; set; }
    }
}