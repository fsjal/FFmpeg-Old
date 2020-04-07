using FFmpeg.Model;
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
        private int progress;
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
        private State state = State.Ready;
        public State State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string field) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
    }
}
