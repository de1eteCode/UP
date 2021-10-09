using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using YumogusiCompany.Model;
using YumogusiCompany.Model.DataModel;

namespace YumogusiCompany.ViewModel
{
    internal class ListOfProductVM : NotifyPropertyChanged
    {
        private readonly Repository _repository;

        public ListOfProductVM()
        {
            _repository = new Repository();
            _pages = new ObservableCollection<Page>() { new(1) { IsSelected = true } };
            Page.Selected += SelectPage;
        }

        public IEnumerable<Category> Categories => _repository.Categories;
        public IEnumerable<Product> Products => FilteredElements();

        #region Filter

        private string _searchQuery = "";
        private readonly ObservableCollection<Filter> _filters = new()
        {
            new Filter("Наименование", "Name"),
            new Filter("Артикул", "Article"),
            new Filter("Описание", "Description")
        };

        private Filter? _selectedFilter;
        public Filter SelectedFilter
        {
            get 
            {
                if (_selectedFilter is null)
                    _selectedFilter = _filters.FirstOrDefault();
                return _selectedFilter;
            }
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();
                OnPropertyChanged("Products");
            }
        }
        public IEnumerable<Filter> Filters => _filters;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                OnPropertyChanged("Products");
            }
        }
        private IEnumerable<Product> FilteredElements()
        {
            UpdatePageDisplayer();
            return _repository.Products.Where(item => 
            SelectedFilter.IsEqual(item, SearchQuery)
            ).Skip((CurrentPage - 1) * _recordDisplay).Take(_recordDisplay);
        }

        #endregion
        #region Page Displayer
        public IEnumerable<Page> PagesDisplayed
        {
            get
            {
                var radius = 3;
                var limit = _pages.Count - (radius + 2);
                int skip = CurrentPage - radius;
                
                if (skip < 0)
                    skip = 0;
                else if (limit < skip)
                    skip = limit;

                return _pages.Skip(skip).Take(radius + 2);
            }
        }

        private readonly ObservableCollection<Page> _pages;
        private const int _recordDisplay = 20;
        private int _currentPage = 1;
        private int _countRecords
        {
            get
            {
                if (String.IsNullOrEmpty(SearchQuery))
                    return _repository.CountProducts;
                return _repository.Products.Where(item => SelectedFilter.IsEqual(item, SearchQuery)).ToList().Count;
            }
        }
        private int _maxPage
        {
            get
            {
                var max = (float)_countRecords / (float)_recordDisplay;
                return max > 0 ? (int)(Math.Ceiling(max)) : 1;
            }
        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set 
            { 
                _currentPage = value;
                _pages.Where(page => page.Number == CurrentPage).First().IsSelected = true;
                OnPropertyChanged();
                OnPropertyChanged("Products");
            }
        }
        private void UpdatePageDisplayer()
        {
            // Получить максимальное страниц
            var maxPages = _maxPage;
            var countPages = _pages.Count;

            if (maxPages > countPages)
            {
                // Если макс страниц больше чем есть, то добавить
                var howMuchAddPages = maxPages - countPages;
                var lastPage = PagesDisplayed.Last().Number;

                for (int i = 1; i <= howMuchAddPages; i++)
                {
                    _pages.Add(new Page(lastPage + i));
                }
            }
            else if (maxPages == countPages) { }
            else
            {
                // Иначе удалить лишние
                var countElements = countPages;
                var howMuchRemovePages = countElements - maxPages;
                var goTo = countElements - howMuchRemovePages - 1;
                for (int index = countElements - 1; index > goTo; index--)
                {
                    _pages.RemoveAt(index);
                }
                if (CurrentPage > _pages.Count)
                    CurrentPage = _pages.Count;
            }

            OnPropertyChanged("PagesDisplayed");
        }

        #region Buttons
        private RelayCommand? _first;
        private RelayCommand? _previous;
        private RelayCommand? _next;
        private RelayCommand? _last;

        public RelayCommand GoFirstPage
        {
            get
            {
                return _first ??= new RelayCommand(obj =>
                {
                    if (CurrentPage != 1)
                        CurrentPage = 1;
                });
            }
        }
        public RelayCommand GoPreviousPage
        {
            get
            {
                return _previous ??= new RelayCommand(obj =>
                {
                    if (CurrentPage > 1)
                        CurrentPage--;
                });
            }
        }
        public RelayCommand GoNextPage
        {
            get
            {
                return _next ??= new RelayCommand(obj =>
                {
                    if (CurrentPage + 1 <= _maxPage)
                        CurrentPage++;
                });
            }
        }
        public RelayCommand GoLastPage
        {
            get
            {
                return _last ??= new RelayCommand(obj =>
                {
                    if (CurrentPage != _maxPage)
                        CurrentPage = _maxPage;
                });
            }
        }
        #endregion
        #region Events

        private void SelectPage(Page selected) =>
            CurrentPage = selected.Number;

        #endregion
        #endregion
    }
}
