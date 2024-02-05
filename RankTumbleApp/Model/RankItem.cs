using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RankTumbleApp
{
    public class RankItem : INotifyPropertyChanged
    {
        #region Fields

        public int id;
        private string name;
        private string description;
        private int rankPoints;

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

        public string Description
        {
            get { return description; }
            set
            {
                this.description = value;
                RaisePropertyChanged("Description");
            }
        }

        public int RankPoints
        {
            get { return this.rankPoints; }
            set
            {
                this.rankPoints = value;
                this.RaisePropertyChanged("RankPoints");
            }
        }

        public string Category { get; set; }

        #region InotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    public class RankItemData : INotifyPropertyChanged
    {
        #region Fields

        public int id;
        private string name;
        private string description;
        private int rankPoints;
     
        private bool isEditMode;
        #endregion

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

        public string Description
        {
            get { return description; }
            set
            {
                this.description = value;
                RaisePropertyChanged("Description");
            }
        }

        public int RankPoints
        {
            get { return this.rankPoints; }
            set
            {
                this.rankPoints = value;
                this.RaisePropertyChanged("RankPoints");
            }
        }

        public bool IsEditMode
        {
            get { return this.isEditMode; }
            set
            {
                this.isEditMode = value;
                this.RaisePropertyChanged("IsEditMode");
            }
        }

        public string Category { get; set; }

        public int Rank { get; set; }

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
