using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Data
{

    public class JsonItemnoteClass : INotifyPropertyChanged
    {
        private bool _isok = false;

        //public event PropertyChangedEventHandler PropertyChanged;

        public int ID { get; set; }
        public string Name { get; set; }
        public Boolean isOn
        {
            get
            {
                return _isok;
            }
            set
            {
                if (_isok != value)
                {

                    _isok = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }




    public class JsonItemNote
    {
        public List<JsonItemnoteClass> NoteInfo { get; set; }
    }



}
