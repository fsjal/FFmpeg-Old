using FFmpeg.Commands;
using FFmpeg.Entities;
using FFmpeg.Model;
using FFmpeg.Model.Codecs;
using FFmpeg.Util;
using log4net;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace FFmpeg.Pages
{
    public partial class ConvertPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Media> Medias { get; } = new ObservableCollection<Media>();
        public RelayCommand AddFiles { get; } = new RelayCommand();
        public RelayCommand Convert { get; } = new RelayCommand();

        // Media
        private string? mediaStart;
        public string? MediaStart
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
        private string? mediaEnd;
        public string? MediaEnd
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
        public ObservableCollection<AudioCodec> AudioCodecs { get; } = 
            new ObservableCollection<AudioCodec>(Codecs.AUDIOS);
        public AudioCodec SelectedAudioCodec { get; set; }
        private int? audioBitrate;
        public int? AudioBitrate
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
        private bool? isAudioDisabled = false;
        public bool? IsAudioDisabled
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
        public ObservableCollection<VideoCodec> VideoCodecs { get; } = 
            new ObservableCollection<VideoCodec>(Codecs.VIDEOS);
        public VideoCodec SelectedVideoCodec { get; set; }
        private int? videoWidth;
        public int? VideoWidth
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
        private int? videoHeight;
        public int? VideoHeight
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
        private int? videoBitrate;
        public int? VideoBitrate
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
        public ObservableCollection<string> VideoPresets { get; } = new ObservableCollection<string>(Codecs.VIDEO_PRESETS);
        public string? SelectedVideoPreset { get; set; }
        private int? videoQuality;
        public int? VideoQuality
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
        private bool? isVideoDisabled = false;
        public bool? IsVideoDisabled
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

        // Logging
        private ILog log = Logger.Log;

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
            // Convert.CanExecuteCommand = (e) => Medias.Count != 0;
            Convert.CanExecuteCommand = (e) => true;
            Convert.ExecuteCommand = (e) => OnConvert();
        }

        private void OnAddFileClicked()
        {
            var medias = GetFiles();
            if (medias != null)
            {
                medias.ToList().ForEach(media => Medias.Add(media));
            }
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
                 return fileDialog.FileNames.Select(name => new Media { FileName = Path.GetFileName(name), Path = name }).ToArray();
            }
            return null;
        }

        private void OnConvert()
        {
            List<IParam> parameters = new List<IParam>();
            parameters.Add(new SimpleParam("-ss", MediaStart));
            parameters.Add(new SimpleParam("-to", MediaEnd));
            parameters.Add(new SimpleParam("b:a", AudioBitrate != null ? $"{AudioBitrate}K" : null));
            parameters.Add(new SimpleParam("b:v", VideoBitrate != null ? $"{VideoBitrate}K" : null));
            if (IsVideoDisabled == true) parameters.Add(new SimpleParam("-vn", ""));
            if (IsAudioDisabled == true) parameters.Add(new SimpleParam("-an", ""));
            if (VideoWidth != null || VideoHeight != null)
            {
                int? width = VideoWidth != null && VideoWidth != 0 ? VideoWidth : -1;
                int? height = VideoHeight != null && VideoHeight != 0 ? VideoHeight : -1;

                parameters.Add(new SimpleParam("-vf", $"scale={width}:{height}"));
            }
            parameters.Add(SelectedVideoCodec);
            parameters.Add(SelectedAudioCodec);
            parameters.Add(new SimpleParam("-preset", 
                SelectedVideoPreset == Codecs.VIDEO_PRESETS[0] ? null : SelectedVideoPreset));
            if (SelectedVideoCodec.Value != null && VideoQuality != 0)
            {
                parameters.Add(new SimpleParam(SelectedVideoCodec.QualityKey, VideoQuality));
            }
            StartConvert(parameters.Where(e => e.Value != null).ToList());
        }

        private async void StartConvert(List<IParam> parameters)
        {
            string param = string.Join(" ", parameters.Select(e => $"{e.Key} {e.Value}"));
            foreach (Media media in Medias)
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                await FFmpegConverter.ConvertAsync(media, parameters, tokenSource.Token);
                log.Info("finished");
            }
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
