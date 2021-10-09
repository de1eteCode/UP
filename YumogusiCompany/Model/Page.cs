using System;

namespace YumogusiCompany.Model
{
    sealed internal class Page
    {
        public delegate void PageHandler(Page selected);
        public static event PageHandler? Selected;

        private static Page? _selectedPage;
        private RelayCommand? _select;
        private int _pageNumber;
        private bool _isSelected;

        public Page(int num)
        {
            Number = num;
        }

        public int Number
        {
            get => _pageNumber;
            set
            {
                if (value < 1)
                    throw new ArgumentException("Not corrected value");
                _pageNumber = value;
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if (value)
                {
                    if (_selectedPage is not null)
                    {
                        _selectedPage.IsSelected = false;
                    }
                    _selectedPage = this;
                }
            }
        }

        public RelayCommand Select
        {
            get
            {
                return _select ??= new RelayCommand(obj =>
                {
                    Selected?.Invoke(this);
                });
            }
        }
    }
}
