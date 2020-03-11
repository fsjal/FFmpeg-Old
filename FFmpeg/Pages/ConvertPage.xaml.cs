using FFmpeg.Commands;
using FFmpeg.Entities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace FFmpeg.Pages
{
    /// <summary>
    /// Interaction logic for ConvertPage.xaml
    /// </summary>
    public partial class ConvertPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Media> Medias { get; } = new ObservableCollection<Media>();
        public RelayCommand AddFiles { get; } = new RelayCommand();
        public RelayCommand Convert { get; } = new RelayCommand();

        // Media
        private string mediaStart;
        public string MediaStart
        {
            get
            {
                return mediaStart;
            }
            set
            {
                mediaStart = value;
                OnPropertyChanged("MediaStart");
            }
        }
        private string mediaEnd;
        public string MediaEnd
        {
            get
            {
                return mediaEnd;
            }
            set
            {
                mediaEnd = value;
                OnPropertyChanged("MediaEnd");
            }
        }

        // Audio
        public ObservableCollection<Media> AudioCodecs { get; } = new ObservableCollection<Media>();
        private int audioBitrate;
        public int AudioBitrate
        {
            get
            {
                return audioBitrate;
            }
            set
            {
                audioBitrate = value;
                OnPropertyChanged("AudioBitrate");
            }
        }
        private bool isAudioDisabled;
        public bool IsAudioDisabled
        {
            get
            {
                return isAudioDisabled;
            }
            set
            {
                isAudioDisabled = value;
                OnPropertyChanged("IsAudioDisabled");
            }
        }

        // Video
        public ObservableCollection<Media> VideosCodecs { get; } = new ObservableCollection<Media>();
        private int videoWidth;
        public int VideoWidth
        {
            get
            {
                return videoWidth;
            }
            set
            {
                videoWidth = value;
                OnPropertyChanged("VideoWidth");
            }
        }        
        private int videoHeight;
        public int VideoHeight
        {
            get
            {
                return videoHeight;
            }
            set
            {
                videoHeight = value;
                OnPropertyChanged("VideoHeight");
            }
        }
        private int videoBitrate;
        public int VideoBitrate
        {
            get
            {
                return videoBitrate;
            }
            set
            {
                videoBitrate = value;
                OnPropertyChanged("VideoBitrate");
            }
        }
        public ObservableCollection<Media> VideosPresets { get; } = new ObservableCollection<Media>();
        private int videoQuality;
        public int VideoQuality
        {
            get
            {
                return videoQuality;
            }
            set
            {
                videoQuality = value;
                OnPropertyChanged("VideoQuality");
            }
        }
        private bool isVideoDisabled;
        public bool IsVideoDisabled
        {
            get
            {
                return isVideoDisabled;
            }
            set
            {
                isVideoDisabled = value;
                OnPropertyChanged("IsVideoDisabled");
            }
        }

        public ConvertPage()
        {
            InitializeComponent();
            SetupCommands();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetupCommands()
        {
            AddFiles.CanExecuteCommand = (e) => true;
            AddFiles.ExecuteCommand = (e) => OnAddFileClicked();
            Convert.CanExecuteCommand = (e) => Medias.Count != 0;
            Convert.ExecuteCommand = (e) => OnAddFileClicked();
        }

        private void OnAddFileClicked()
        {
            /*var medias = GetFiles();
            if (medias != null)
            {
                Medias.Clear();
                medias.ToList().ForEach(media => Medias.Add(media));
            }*/
            MessageBox.Show(VideoWidth.ToString());
        }

        private Media[] GetFiles()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string[] filters = new string[] { "mp4", "mp3", "mkv", "wav", "ogg", "webm", "gif", "wmv", "m4a",
                "avi", "mov", "flac", "ts" }.Select(it => $"*.{it}").ToArray();
            string filterStr = string.Join("; ", filters);
            
            fileDialog.Filter = $"Media Files ({filterStr})|{filterStr}";
            fileDialog.Multiselect = true;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == true)
            {
                 return fileDialog.FileNames.Select(name => new Media { FileName = Path.GetFileName(name) }).ToArray();
                
            }
            return null;
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
    }
}
