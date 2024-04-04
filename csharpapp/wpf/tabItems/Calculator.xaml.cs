using System.Windows.Controls;
using wpf.interfaces;

namespace wpf.tabItems
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl, IContentProvider
    {

        public Calculator()
        {
            InitializeComponent();
        }

        public string GetContent()
        {
            //TextRange textRange = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            //return textRange.Text;
            return "Test";
        }

        public void SetContent(string content)
        {
            throw new NotImplementedException();
        }
    }
}
