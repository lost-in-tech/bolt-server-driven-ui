namespace Ensemble.Core.Serializers;
internal static class TypeHelper
{
    public static bool IsSimpleType(Type type)
    {
        return type == typeof(string)
            || type == typeof(int)
            || type == typeof(int?)
            || type == typeof(long)
            || type == typeof(long?)
            || type == typeof(float)
            || type == typeof(float?)
            || type == typeof(double)
            || type == typeof(double?)
            || type == typeof(short)
            || type == typeof(short?)
            || type == typeof(bool)
            || type == typeof(bool?)
            || type == typeof(DateTime)
            || type == typeof(DateTime?)
            || type.IsEnum;
    }
}
