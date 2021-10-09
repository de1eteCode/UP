using YumogusiCompany.Model.DataModel;

namespace YumogusiCompany.Model
{
    internal class Filter
    {
        public Filter(string title, string property)
        {
            Title = title;
            Property = property;
        }

        public string Title { get; set; }
        public string Property { get; set; }

        public bool IsEqual(BaseEntity entity, string search)
        {
            if (entity is null || search is null)
                return false;

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            var value = entity[Property].ToString().ToLower().Contains(search.ToLower());
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            return value;
        }
    }
}
