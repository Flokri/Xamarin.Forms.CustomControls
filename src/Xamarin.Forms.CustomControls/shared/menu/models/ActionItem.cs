using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Forms.CustomControls.Menu
{
    public class ActionItem : INotifyPropertyChanged
    {
        #region Instnaces
        private string _icon;
        private string _fontFamily;
        private ICommand _cmd;
        private object _param;
        private bool _isSelected;
        #endregion


        #region properties
        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                NotifyPorpertyChanged();
            }
        }
        public string FontFamily
        {
            get => _fontFamily;
            set
            {
                _fontFamily = value;
                NotifyPorpertyChanged();
            }
        }
        public ICommand Cmd
        {
            get => _cmd;
            set
            {
                _cmd = value;
                NotifyPorpertyChanged();
            }
        }
        public object Param
        {
            get => _param;
            set
            {
                _param = value;
                NotifyPorpertyChanged();
            }
        }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPorpertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region events

        #endregion

        #region privates
        private void NotifyPorpertyChanged([CallerMemberName] string propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}
