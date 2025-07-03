using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace basic_chatbot_cs
{
    public class ChatTrainingService
    {
        private MLContext _mlContext;
        private ITransformer _trainedModel;
        private string _modelPath = "chatModel.zip";
        private string _trainingDataPath = "trainingData.csv";

        public ChatTrainingService()
        {
            _mlContext = new MLContext();
            InitializeTrainingData();
            TrainModel();
        }

        private void InitializeTrainingData()
        {
            if (!File.Exists(_trainingDataPath))
            {
                // Create basic training data if file doesn't exist
                string[] trainingData = {
                    "UserInput,BotResponse",
                    "hello,Hi there! How can I help you today?",
                    "hi,Hello! What can I do for you?",
                    "how are you,I'm doing great, thanks for asking!",
                    "what's your name,I'm a simple chatbot. What's your name?",
                    "bye,Goodbye! Have a wonderful day!",
                    "goodbye,See you later!",
                    "thanks,You're welcome!",
                    "thank you,My pleasure!",
                    "help,I can respond to basic greetings and questions. Try saying hello!",
                    "what time is it,I don't have access to real-time data, but I hope you're having a good day!"
                };

                File.WriteAllLines(_trainingDataPath, trainingData);
            }
        }

        public void TrainModel()
        {
            if (File.Exists(_modelPath))
            {
                // Load existing model
                _trainedModel = _mlContext.Model.Load(_modelPath, out _);
                return;
            }

            // Load training data
            IDataView trainingData = _mlContext.Data.LoadFromTextFile<ChatMessageModel>(
                _trainingDataPath,
                hasHeader: true,
                separatorChar: ',');

            // Data process pipeline
            var dataProcessPipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ChatMessageModel.BotResponse))
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(ChatMessageModel.UserInput)));

            // Trainer
            var trainer = _mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer)
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Train the model
            _trainedModel = trainingPipeline.Fit(trainingData);

            // Save the model
            _mlContext.Model.Save(_trainedModel, trainingData.Schema, _modelPath);
        }

        public void RetrainModel(string newUserInput, string newBotResponse)
        {
            // Append new training data
            File.AppendAllText(_trainingDataPath, $"\n{newUserInput},{newBotResponse}");

            // Retrain the model
            TrainModel();
        }

        public string PredictResponse(string userInput)
        {
            if (_trainedModel == null)
                return "I'm still learning. Please try again.";

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ChatMessageModel, ChatPrediction>(_trainedModel);
            var prediction = predictionEngine.Predict(new ChatMessageModel { UserInput = userInput });

            return prediction.BotResponse;
        }
    }

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