using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RankTumbleApp
{
    public class RankCategory : INotifyPropertyChanged
    {
        #region Fields

        private string name;
      
        #endregion

        [PrimaryKey, AutoIncrement]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Description { get; set; }

        #region InotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

}
