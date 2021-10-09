using System;

namespace YumogusiCompany.Model.DataModel
{
    abstract class BaseEntity : NotifyPropertyChanged
    {
        private Type? _type;

        public object? this[string property]
        {
            get
            {
                if (_type is null)
                    _type = this.GetType();

                return _type.GetProperty(property)?.GetValue(this, null);;
            }
        }
    }
}
