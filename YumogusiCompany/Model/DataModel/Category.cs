using System;
using System.Collections.Generic;

namespace YumogusiCompany.Model.DataModel
{
    internal class Category : BaseEntity
    {
        private Guid _guid;
        private string? _name;

        public Guid Id
        {
            get { return _guid; }
            set { _guid = value; OnPropertyChanged(); }
        }
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged();}
        }

        public ICollection<Product>? Products { get; set; }
    }
}
