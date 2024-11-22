using AppClassLibrary.Enums;
using AppClassLibrary.Handlers;
using AppClassLibrary.Models;
using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpf.interfaces;
using wpf.tabItems;

namespace wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                ContentTypeEnum contentTypeEnum = ContentTypeEnum.NOTES;
                if (tabs.SelectedContent.GetType().Name == "Calculator")
                {
                    contentTypeEnum = ContentTypeEnum.CALCULATOR;
                }

                try
                {
                    if (tabs.SelectedContent is IContentProvider contentProvider)
                    {
                        contentProvider.SetContent(FileHandler.LoadFile(path, contentTypeEnum));
                    }
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error Loading File", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                ContentTypeEnum contentTypeEnum = ContentTypeEnum.NOTES;
                if (tabs.SelectedContent.GetType().Name == "Calculator")
                {
                    contentTypeEnum = ContentTypeEnum.CALCULATOR;
                }
                try
                {
                    if(tabs.SelectedContent is IContentProvider contentProvider)
                    {
                        FileHandler.SaveFile(contentProvider.GetContent(), path, contentTypeEnum);
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error Loading File", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}