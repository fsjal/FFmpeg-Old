using System.ComponentModel;

namespace FFmpeg.Entities
{
    public class Media : INotifyPropertyChanged
    {
        private string eta;
        public string Eta
        {
            get
            {
                return eta;
            }
            set
            {
                eta = value;
                OnPropertyChanged("Eta");
            }
        }
        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }
        private int progress = 50;
        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }
        public string Path { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string field) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
    }
}
