using BotFactory.Common.Interface;
using BotFactory.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BotFactory.Tools
{
    public class DataContext : INotifyPropertyChanged
    {
        private ITestingUnit _ibot = null;
        private Boolean _response = false;
        private Boolean _working = false;

        #region Properties

        public ITestingUnit IBot
        {
            get { return _ibot; }
            set { SetField(ref _ibot, value, nameof(IBot)); }
        }

        public Boolean Response
        {
            get { return _response; }
            set { SetField(ref _response, value, nameof(Response)); }
        }

        public Boolean Working
        {
            get { return _working; }
            set { SetField(ref _working, value, nameof(Working)); }
        }

        #endregion

        #region INotifyPropertyChanged Interface Methods

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        
    }
}
