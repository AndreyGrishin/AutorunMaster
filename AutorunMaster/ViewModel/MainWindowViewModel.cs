using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using AutorunMaster.Infrastructure;
using AutorunMaster.Model;
using AutorunMaster.Model.Entities;

namespace AutorunMaster.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ReadData();
        }

        #region Properties

        private ObservableCollection<AutorunObject> _autorunObjects;
        public ObservableCollection<AutorunObject> AutorunObjects
        {
            get => _autorunObjects ?? new ObservableCollection<AutorunObject>();
            set
            {
                _autorunObjects = value;
                OnPropertyChanged("AutorunObjects");
            }
        }

        #endregion

        #region Commands

        private RelayCommand _openFileLocationCommand;
        public RelayCommand OpenFileLocationCommand => _openFileLocationCommand ?? (_openFileLocationCommand = new RelayCommand(OpenFileLocation));

        #endregion

        #region Methods

        /// <summary>
        /// Opens the folder where the selected file is located
        /// </summary>
        /// <param name="obj"></param>
        private static void OpenFileLocation(object obj)
        {
            string folderPath = new FileInfo((string)obj).DirectoryName;

            Process.Start(folderPath ?? throw new InvalidOperationException());
        }

        /// <summary>
        /// Loads data executable files
        /// </summary>
        private async void ReadData()
        {
            AutorunService autorunService = new AutorunService();

            AutorunObjects = new ObservableCollection<AutorunObject>(await autorunService.GetAutorunObjects());
        }

        #endregion
    }
}
