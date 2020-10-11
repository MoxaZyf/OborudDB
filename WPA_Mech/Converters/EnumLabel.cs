using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using WPA_Mech.Properties;

namespace WPA_Mech.Converters
{
    public class EnumLabel : IValueConverter
    {
        private readonly Dictionary<Type, Dictionary<string, Enum>> _enumsValues = new Dictionary<Type, Dictionary<string, Enum>>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Resources.ResourceManager.GetString(value.GetType().Name + "_" + value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var label = value as string;
            if (label != null)
            {
                var values = GetValues(targetType);
                Enum result;
                if (values != null && values.TryGetValue(label, out result))
                {
                    return result;
                }
            }
            return Binding.DoNothing;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, Enum> GetValues(Type type)
        {
            Dictionary<string, Enum> values;
            if (!_enumsValues.TryGetValue(type, out values))
            {
                values = Enum.GetValues(type).OfType<Enum>().ToDictionary(v => Resources.ResourceManager.GetString(v.GetType().Name + "_" + v), v => v);
                _enumsValues.Add(type, values);
            }
            return values;
        }
    }


    public class EnumToItemsSource : MarkupExtension
    {
        private readonly Type _type;

        public EnumToItemsSource(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(_type);
        }
    }


    public class EnumDescription : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Binding.DoNothing;
            return value.GetType()
                        .GetField(value.ToString())
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .Cast<DescriptionAttribute>()
                        .Select(x => x.Description)
                        .FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region MarkupExtension

        public EnumDescription()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        #endregion
    }
}
