using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YumogusiCompany.Model;
using YumogusiCompany.Model.Command;
using YumogusiCompany.Model.DataModel;
using YumogusiCompany.ViewModel.Abstract;

namespace YumogusiCompany.ViewModel
{
    internal class EditProductVM : NotifyPropertyChanged, IVM
    {
        private Repository _repository;
        private Product? _product;
        private Category? _selectedCategory;
        private RelayCommand? _addProduct;
        private RelayCommand? _cancel;

        public EditProductVM(Product product)
        {
            _repository = Repository.GetInstance();
            Product = product;
            SelectedCategory = Product.Category;
            OnPropertyChanged("Product");
        }

        public IEnumerable<Category> Categories
        {
            get => _repository.Categories;
        }
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddProduct
        {
            get
            {
                return _addProduct ??= new RelayCommand(obj =>
                {
                    _repository.Add(Product);
                });
            }
        }
        public RelayCommand Cancel
        {
            get
            {
                return _cancel ??= new RelayCommand(obj =>
                {
                    
                });
            }
        }
    }
}
