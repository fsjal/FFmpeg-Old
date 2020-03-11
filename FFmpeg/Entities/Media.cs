using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FFmpeg.Entities
{
    public class Media : INotifyPropertyChanged
    {
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
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string field) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(field));
    }
}
