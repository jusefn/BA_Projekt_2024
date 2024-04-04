using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using wpf.interfaces;

namespace wpf.tabItems
{
    /// <summary>
    /// Interaction logic for Notes.xaml
    /// </summary>
    public partial class Notes : UserControl, IContentProvider
    {

        public Notes()
        {
            InitializeComponent();
        }

        public string GetContent()
        {
            TextRange textRange = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
            return textRange.Text;
        }

        public void SetContent(string content)
        {
            textBox.Document.Blocks.Clear();
            textBox.Document.Blocks.Add(new Paragraph(new Run(content)));
        }
    }
}
