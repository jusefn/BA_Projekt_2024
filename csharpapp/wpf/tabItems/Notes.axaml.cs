using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media.TextFormatting;
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
            return textBox.Document.Text;
        }

        public void SetContent(string content)
        {
            textBox.Document.Text = content;
        }
    }
}
