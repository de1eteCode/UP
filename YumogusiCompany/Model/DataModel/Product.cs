using System;

namespace YumogusiCompany.Model.DataModel
{
    internal class Product : BaseEntity
    {
        private Guid _id;
        private string? _article;
        private string? _name;
        private string? _description;
        private string? _urlLogo;
        private int _count;
        private int _price;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string? Article
        {
            get { return _article; }
            set { _article = value; OnPropertyChanged(); }
        }
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string? Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }
        public string? UrlLogo
        {
            get { return _urlLogo; }
            set { _urlLogo = value; OnPropertyChanged(); }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }
        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        public Category? Category { get; set; }
    }
}
