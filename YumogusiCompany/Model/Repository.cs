using System;
using System.Collections.Generic;
using System.Linq;
using YumogusiCompany.Model.DataModel;

namespace YumogusiCompany.Model
{
    internal class Repository
    {
        #region Singleton
        private static object _locker = new object();
        
        private static Repository? _repository;
        public static Repository GetInstance()
        {
            lock (_locker)
            {
                if (_repository is null)
                    _repository = new Repository();
                return _repository;
            }
        }

        #endregion


        private readonly List<Category> _categories = new();
        private IEnumerable<Product> _products;

        private Repository()
        {
            CreateCategory("Бытовая техника");
            CreateCategory("Все для ванной");
            CreateCategory("Все для сада");
            CreateCategory("Все для кухни");
            CreateCategory("Все для гаража");
        }

        public IEnumerable<Category> Categories => _categories;
        public IEnumerable<Product> Products
        {
            get
            {
                if (_products is null)
                {
                    _products = new List<Product>();
                    if (_products is List<Product> prodList)
                    {
                        foreach (var category in _categories)
                        {
                            foreach (var product in category.Products)
                            {
                                prodList.Add(product);
                            }
                        }
                    }
                }
                return _products;
            }
        }
        public int CountProducts
        {
            get
            {
                int count = 0;
                _categories.ForEach(item =>
                {
                    count += item.Products.Count;
                });
                return count;
            }
        }

        private void CreateCategory(string nameCategory)
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = nameCategory
            };

            var products = new List<Product>();

            for (int i = 1; i < 26; i++)
            {
                products.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Article = $"_{category.Name}_art_test_{i}",
                    Name = $"_name_test_{i}",
                    Description = $"_desc_test_{i}",
                    UrlLogo = null,
                    Count = 1,
                    Price = 1000,
                    Category = category
                });
            }

            category.Products = products;
            _categories.Add(category);

        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product product)
        {
            //var cat = Categories.Where(category => category.Products.Where(prod => prod.Id == product.Id).FirstOrDefault() != null).FirstOrDefault();
            //cat.Products.Remove(product);
        }
    }
}
