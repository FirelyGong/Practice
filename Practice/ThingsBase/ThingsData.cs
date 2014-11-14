using System.Collections.Generic;

namespace Practice.ThingsBase
{
    public abstract class ThingsData
    {
        private readonly Dictionary<string, string> _propertyValues;

        protected ThingsData()
        {
            _propertyValues = new Dictionary<string, string>();
        }

        public string GetValue(string property)
        {
            if (_propertyValues.ContainsKey(property))
            {
                return _propertyValues[property];
            }

            return null;
        }

        public void SetValue(string property, string value)
        {
            if (_propertyValues.ContainsKey(property))
            {
                _propertyValues[property] = value;
            }
            else
            {
                _propertyValues.Add(property, value);
            }
        }
    }
}
