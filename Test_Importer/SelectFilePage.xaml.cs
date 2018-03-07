using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using Test_Importer.ImporterFolder;

namespace Test_Importer
{
    /// <summary>
    /// Interaction logic for SelectFilePage.xaml
    /// </summary>
    public partial class SelectFilePage : Page
    {
        public SelectFilePage()
        {
            InitializeComponent();
        }

        private void SelectExcelFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                using(ImporterClass import = new ImporterClass())
                {
                    import.RunValidation(openFile.FileName);
                }
            }
        }
    }
}
