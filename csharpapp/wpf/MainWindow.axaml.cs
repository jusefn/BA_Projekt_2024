using AppClassLibrary.Enums;
using AppClassLibrary.Handlers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using wpf.interfaces;


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

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            var storage = topLevel?.StorageProvider;
            var openOptions = new FilePickerOpenOptions
            {
                Title = "Open File"
            };
            if (storage != null)
            {
                var result = await storage.OpenFilePickerAsync(openOptions);
                {
                    string path = result[0].Path.AbsolutePath;
                    ContentTypeEnum contentTypeEnum = ContentTypeEnum.NOTES;
                    if (tabs.SelectedContent != null && tabs.SelectedContent.GetType().Name == "Calculator")
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
                        var msgBox = MessageBoxManager.GetMessageBoxStandard(exception.Message, "Error Loading File", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                        await msgBox.ShowAsync();
                    }
                }
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(this);
            var storage = topLevel?.StorageProvider;
            var saveOptions = new FilePickerSaveOptions
            {
                ShowOverwritePrompt = true,
                DefaultExtension = "txt",
                Title = "Save File"
            };
            if (storage != null)
            {
                var result = await storage.SaveFilePickerAsync(saveOptions);
            
                if (result is not null)
                {
                    string path = result.Path.AbsolutePath;
                    ContentTypeEnum contentTypeEnum = ContentTypeEnum.NOTES;
                    if (tabs.SelectedContent != null && tabs.SelectedContent.GetType().Name == "Calculator")
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
                        var msgBox = MessageBoxManager.GetMessageBoxStandard(exception.Message, "Error Loading File", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                        await msgBox.ShowAsync();
                    }
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}