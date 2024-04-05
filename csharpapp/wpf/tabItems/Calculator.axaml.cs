using System.ComponentModel;
using System.Text;
using wpf.interfaces;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace wpf.tabItems
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl, IContentProvider
    {

        private readonly StringBuilder _stringBuilder;
        private long? _firstOperand;
        private long? _secondOperand;
        private string? _operatorString;
        public Calculator()
        {
            InitializeComponent();
            _stringBuilder = new StringBuilder();
        }


        public string GetContent()
        {
            if (historyField.Text != null) return historyField.Text;

            return "";
        }

        public void SetContent(string content)
        {
            historyField.Text = content;
            string[] parts = content.Split('=');
            _stringBuilder.Clear();
            _stringBuilder.Append(parts.LastOrDefault()!.Replace("\n", "").Replace("\r", ""));
            _firstOperand = Convert.ToInt64(_stringBuilder.ToString());
            calcField.Text = _stringBuilder.ToString().Replace("\n", "").Replace("\r", "");


        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender; // Cast sender to Button
            _stringBuilder.Append(button.Content?.ToString()); // Get the content of the clicked button
            calcField.Text = _stringBuilder.ToString();
            if(_firstOperand != null)
            {
                _secondOperand = Convert.ToInt64(button.Content?.ToString());
            }
            else
            {
               _firstOperand = Convert.ToInt64(button.Content?.ToString());
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender; // Cast sender to Button
            _stringBuilder.Append(button.Content?.ToString()); // Get the content of the clicked button
            calcField.Text = _stringBuilder.ToString();
            _operatorString = button.Content?.ToString();
        }

        private async void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            long result = 0;
            if(_firstOperand == null || _secondOperand == null)
            {
                var msgBox = MessageBoxManager.GetMessageBoxStandard("Operands can not be empty.", "Error Calculating",
                    ButtonEnum.Ok, Icon.Error);
                await msgBox.ShowAsync();

            }
            else
            {
                switch (_operatorString)
                {
                    case ("+"):
                        result = (long)(_firstOperand + _secondOperand);
                        break;
                    case ("-"):
                        result = (long)(_firstOperand - _secondOperand);
                        break;
                    case ("*"):
                        result = (long)(_firstOperand * _secondOperand);
                        break;
                    case ("/"):
                        result = (long)(_firstOperand / _secondOperand);
                        break;
                    default:
                        break;
                }
            }
            historyField.Text += _stringBuilder.ToString()+"="+result+'\n';
            _stringBuilder.Clear();
            _stringBuilder.Append(result);
            calcField.Text = _stringBuilder.ToString();
            _firstOperand = result;
            _secondOperand = null;
        }
    }
}
