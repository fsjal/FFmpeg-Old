using System;
using System.Collections.Generic;
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

namespace FFmpeg.Controls
{
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : UserControl
    {
        public string Hint
        {
            get { return (string) GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(Input), new PropertyMetadata(""));

        public string InputText
        {
            get { return (string) GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register("InputText", typeof(string), typeof(Input), new PropertyMetadata(""));

        public Input()
        {
            InitializeComponent();
           
            //DataContext = this;
        }
    }
}
