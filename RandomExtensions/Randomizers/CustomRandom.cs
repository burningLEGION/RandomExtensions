using System;
using System.Reflection;

namespace RandomExtensions.Randomizers
{
    public sealed class CustomRandom : IRandomizeble
    {
        private readonly Type _type;

        public CustomRandom(Type type)
        {
            _type = type;
        }

        public object RandomizeObject()
        {
            try
            {
                var instance = Activator.CreateInstance(_type);
                var properties = _type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                for (var i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
                    var random = CommonVariables.Instance.Random.Randomize(property.PropertyType);
                    property.SetValue(instance, random, null);
                }
                var fields = _type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                for (var i = 0; i < fields.Length; i++)
                {
                    var field = fields[i];
                    var random = CommonVariables.Instance.Random.Randomize(field.FieldType);
                    field.SetValue(instance, random);
                }

                return instance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object RandomizeObject(object from, object to)
        {
            //todo need to implement random by inner parameters
            return RandomizeObject();
        }
    }
}
