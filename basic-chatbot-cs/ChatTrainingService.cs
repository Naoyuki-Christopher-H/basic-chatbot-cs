using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.IO;

namespace basic_chatbot_cs
{
    public class ChatTrainingService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _trainedModel;
        private readonly string _modelPath = "chatModel.zip";
        private readonly string _trainingDataPath = "trainingData.csv";

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
                _trainedModel = _mlContext.Model.Load(_modelPath, out _);
                return;
            }

            IDataView trainingData = _mlContext.Data.LoadFromTextFile<ChatMessageData>(
                _trainingDataPath,
                hasHeader: true,
                separatorChar: ',');

            var dataProcessPipeline = _mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "Label",
                    inputColumnName: nameof(ChatMessageData.BotResponse))
                .Append(_mlContext.Transforms.Text.FeaturizeText(
                    outputColumnName: "Features",
                    inputColumnName: nameof(ChatMessageData.UserInput)));

            var trainer = _mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                labelColumnName: "Label",
                featureColumnName: "Features");

            var trainingPipeline = dataProcessPipeline.Append(trainer)
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue(
                    outputColumnName: "PredictedLabel"));

            _trainedModel = trainingPipeline.Fit(trainingData);
            _mlContext.Model.Save(_trainedModel, trainingData.Schema, _modelPath);
        }

        public void RetrainModel(string newUserInput, string newBotResponse)
        {
            File.AppendAllText(_trainingDataPath, $"\n{newUserInput},{newBotResponse}");
            TrainModel();
        }

        public string PredictResponse(string userInput)
        {
            if (_trainedModel == null || string.IsNullOrEmpty(userInput))
                return "I'm still learning. Please try again.";

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ChatMessageData, ChatMessagePrediction>(_trainedModel);
            var prediction = predictionEngine.Predict(new ChatMessageData { UserInput = userInput });

            return prediction.Response ?? "I didn't understand that. Could you rephrase?";
        }
    }
}