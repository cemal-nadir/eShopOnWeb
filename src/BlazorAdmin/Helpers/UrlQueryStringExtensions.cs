using System;

namespace BlazorAdmin.Helpers;

public static class UrlQueryStringExtensions
{
    public static string GenerateUrlQueryString<TFilter>(this TFilter filter) where TFilter : class
    {
        var queryString = string.Empty;

        var properties = typeof(TFilter).GetProperties();

        foreach (var property in properties)
        {

            var propertyName = property.Name;
            var propertyType = property.PropertyType;

            // Check if the property type is supported
            if (IsSupportedType(propertyType))
            {
                var value = property.GetValue(filter);
                if (value is null) continue;
                var formattedValue = FormatValue(propertyType, value);
                queryString += $"{(queryString == string.Empty ? string.Empty : "&")}{propertyName}={formattedValue}";
            }
            else
            {
                throw new NotSupportedException($"Property type '{propertyType}' is not supported for building query string.");
            }
        }

        return queryString;

        bool IsSupportedType(Type type)
        {

            return type == typeof(int?) ||
                   type == typeof(decimal?) ||
                   type == typeof(bool?) ||
                   type == typeof(string) ||
                   type == typeof(DateTimeOffset?) ||
                   type.IsEnum;
        }

        string FormatValue(Type type, object value)
        {
            return type == typeof(DateTimeOffset?) ?
                ((DateTimeOffset)value).UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ") : value.ToString();
        }
    }
}
