using FFmpeg.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FFmpeg.Controls
{
    /// <summary>
    /// Interaction logic for Toolbar.xaml
    /// </summary>
    public partial class Toolbar : UserControl
    {

        public ICommand Exit { get; private set; }
        public ICommand Minimize { get; private set; }

        public Toolbar()
        {
            InitializeComponent();
            Exit = new RelayCommand((e) => ExitApplication(), (e) => true);
            Minimize = new RelayCommand((e) => MinimizeApplication(), (e) => true);
            DataContext = this;
        }

        private void ExitApplication() => Application.Current.Shutdown();
        
        private void MinimizeApplication() => Window.GetWindow(this).WindowState = WindowState.Minimized;
    }
}
