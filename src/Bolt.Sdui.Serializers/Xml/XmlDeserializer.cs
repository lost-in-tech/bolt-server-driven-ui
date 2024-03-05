using System.Reflection;
using System.Xml;
using Bolt.Sdui.Core;

namespace Ensemble.Core.Serializers.Xml;

internal sealed class XmlDeserializer
{
    private readonly ITypeRegistry _registry;

    public XmlDeserializer(ITypeRegistry registry)
    {
        _registry = registry;
    }

    public IElement? Deserialize(Stream stream)
    {
        using var reader = XmlReader.Create(stream);

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                return BuildElement(reader);
            }
        }

        return null;
    }


    private IElement? BuildElement(XmlReader reader)
    {
        var name = reader.Name;

        var typeData = _registry.TryGet(name);

        if (typeData != null)
        {
            return BuildElement(typeData, reader);
        }

        return null;
    }

    private IElement? BuildElement(TypeData type, XmlReader reader)
    {
        var element = BuildObject(type, reader);

        if (reader.IsEmptyElement) return element as IElement;

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement) break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                var propData = type.TryGetProperty(reader.Name);
                if (propData != null)
                {
                    if (reader.IsEmptyElement)
                    {
                        if (propData.TypeData != null)
                        {
                            var propValue = BuildObject(propData.TypeData, reader);

                            SetPropertyValue(propData.PropertyInfo, element, propValue);
                        }
                    }
                    else
                    {
                        var elements = BuildElements(reader);

                        SetPropertyValue(propData.PropertyInfo, element, elements);
                    }
                }
            }
        }

        return element as IElement;
    }


    private IElement[] BuildElements(XmlReader reader)
    {
        var result = new List<IElement>();

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement) { break; }

            if (reader.NodeType == XmlNodeType.Element)
            {
                var element = BuildElement(reader);
                if (element != null)
                {
                    result.Add(element);
                }
            }
        }

        return result.ToArray();
    }

    private object? BuildObject(TypeData type, XmlReader reader)
    {
        var result = Activator.CreateInstance(type.Type);

        foreach (var prop in type.Properties)
        {
            if (prop.Value.TypeData == null || prop.Value.TypeData.IsSimpleType)
            {
                var att = reader.GetAttribute(prop.Key);

                SetPropertyValue(prop.Value.PropertyInfo, result, att);
            }
        }

        return result;
    }


    private void SetPropertyValue(PropertyInfo prop, object? source, object? value)
    {
        if (value == null) return;

        if (prop.PropertyType.IsEnum)
        {
            prop.SetValue(source, Enum.Parse(prop.PropertyType, (string)value));
        }


        else if(prop.PropertyType == typeof(int))
        {
            prop.SetValue(source, int.TryParse(value.ToString(), out var result) ? result : 0);
        }
        else if (prop.PropertyType == typeof(int?))
        {
            prop.SetValue(source, int.TryParse(value.ToString(), out var result) ? result : null);
        }


        else if (prop.PropertyType == typeof(long))
        {
            prop.SetValue(source, long.TryParse(value.ToString(), out var result) ? result : 0);
        }
        else if (prop.PropertyType == typeof(long?))
        {
            prop.SetValue(source, long.TryParse(value.ToString(), out var result) ? result : null);
        }


        else if (prop.PropertyType == typeof(decimal))
        {
            prop.SetValue(source, decimal.TryParse(value.ToString(), out var result) ? result : 0);
        }
        else if (prop.PropertyType == typeof(decimal?))
        {
            prop.SetValue(source, decimal.TryParse(value.ToString(), out var result) ? result : null);
        }


        else if (prop.PropertyType == typeof(double))
        {
            prop.SetValue(source, double.TryParse(value.ToString(), out var result) ? result : 0);
        }
        else if (prop.PropertyType == typeof(double?))
        {
            prop.SetValue(source, double.TryParse(value.ToString(), out var result) ? result : null);
        }



        else if (prop.PropertyType == typeof(bool))
        {
            prop.SetValue(source, bool.TryParse(value.ToString(), out var result) ? result : false);
        }
        else if (prop.PropertyType == typeof(bool?))
        {
            prop.SetValue(source, bool.TryParse(value.ToString(), out var result) ? result : null);
        }


        else if (prop.PropertyType == typeof(DateTime))
        {
            prop.SetValue(source, DateTime.TryParse(value.ToString(), out var result) ? result : DateTime.MinValue);
        }
        else if (prop.PropertyType == typeof(DateTime?))
        {
            prop.SetValue(source, DateTime.TryParse(value.ToString(), out var result) ? result : null);
        }

        else if (prop.PropertyType == typeof(float))
        {
            prop.SetValue(source, float.TryParse(value.ToString(), out var result) ? result : 0);
        }
        else if (prop.PropertyType == typeof(float?))
        {
            prop.SetValue(source, float.TryParse(value.ToString(), out var result) ? result : null);
        }


        else if (prop.PropertyType == typeof(short))
        {
            prop.SetValue(source, short.TryParse(value.ToString(), out var result) ? result : 0);
        }
        else if (prop.PropertyType == typeof(short?))
        {
            prop.SetValue(source, short.TryParse(value.ToString(), out var result) ? result : null);
        }


        else
        {
            prop.SetValue(source, value);
        }
    }
}