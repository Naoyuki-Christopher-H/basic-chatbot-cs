using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace basic_chatbot_cs
{
    public partial class MainWindow : Window
    {
        private readonly ChatTrainingService _trainingService;
        private readonly ChatHistory _chatHistory = new ChatHistory();
        private readonly string _messagesFilePath = "conversations.txt";
        private bool _isLearningMode = false;
        private string _lastUserInput = "";

        public MainWindow()
        {
            InitializeComponent();
            _trainingService = new ChatTrainingService();
            EnsureConversationFileExists();
            ChatBox.ItemsSource = _chatHistory.Messages;
            UpdateLearningStatus();
        }

        private void EnsureConversationFileExists()
        {
            if (!File.Exists(_messagesFilePath))
            {
                File.Create(_messagesFilePath).Close();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
            }
        }

        private void ProcessUserInput()
        {
            var userInput = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            if (_isLearningMode)
            {
                _trainingService.RetrainModel(_lastUserInput, userInput);
                _chatHistory.AddMessage($"Learned: When you say '{_lastUserInput}', I'll respond '{userInput}'", false, "#34C759");
                _isLearningMode = false;
                UpdateLearningStatus();
            }
            else
            {
                _lastUserInput = userInput;
                string response = _trainingService.PredictResponse(userInput);

                if (response.Contains("I'm still learning") || response.Contains("I didn't understand"))
                {
                    response += "\n\nCan you teach me how to respond? Type your preferred response.";
                    _isLearningMode = true;
                    UpdateLearningStatus();
                }

                _chatHistory.AddMessage(userInput, true);
                _chatHistory.AddMessage(response, false);

                SaveConversation(userInput, response);
            }

            InputTextBox.Clear();
            ScrollToBottom();
        }

        private void SaveConversation(string userInput, string botResponse)
        {
            try
            {
                File.AppendAllText(_messagesFilePath,
                    $"[{DateTime.Now}]\nUser: {userInput}\nBot: {botResponse}\n\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving conversation: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ScrollToBottom()
        {
            if (ChatBox.Items.Count > 0)
            {
                var border = (Border)VisualTreeHelper.GetChild(ChatBox, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }

        private void UpdateLearningStatus()
        {
            LearningStatus.Text = _isLearningMode ? "Learning Mode" : "Normal Mode";
        }
    }

    public class ChatMessage
    {
        public string Text { get; set; } = string.Empty;
        public Color BackgroundColor { get; set; }
        public Brush TextColor { get; set; } = Brushes.Black;
        public HorizontalAlignment HorizontalAlignment { get; set; }
    }

    public class ChatHistory
    {
        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();

        public void AddMessage(string text, bool isUserMessage, string? customColor = null)
        {
            var message = new ChatMessage
            {
                Text = text,
                HorizontalAlignment = isUserMessage ? HorizontalAlignment.Right : HorizontalAlignment.Left
            };

            if (!string.IsNullOrEmpty(customColor))
            {
                message.BackgroundColor = (Color)ColorConverter.ConvertFromString(customColor);
            }
            else
            {
                message.BackgroundColor = isUserMessage ?
                    Color.FromRgb(0, 122, 255) :
                    Color.FromRgb(229, 229, 234);
            }

            Messages.Add(message);
        }
    }
}