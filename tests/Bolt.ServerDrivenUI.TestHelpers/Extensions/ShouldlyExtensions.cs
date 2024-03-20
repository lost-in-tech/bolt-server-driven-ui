using Bolt.Common.Extensions;
using Shouldly;

namespace Bolt.ServerDrivenUI.TestHelpers.Extensions;
public static class ShouldlyExtensions
{
    public static void ShouldMatchApprovedWithDefaultOptions<T>(this T got, string? msg = null, string? discriminator = null)
    {
        var serialized = got.SerializeToPrettyJson();
        
        serialized.ShouldMatchApproved(c =>
        {
            c.UseCallerLocation();
            c.WithStringCompareOptions(StringCompareShould.IgnoreLineEndings);
            c.SubFolder("approved");
            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                c.WithDiscriminator(discriminator);
            }
        }, msg);
    }


    public static void ShouldMatchApprovedWithDefaultOptions(this string got, string? msg = null, string? discriminator = null)
    {
        got.ShouldMatchApproved(c =>
        {
            c.UseCallerLocation();
            c.WithStringCompareOptions(StringCompareShould.IgnoreLineEndings);
            c.SubFolder("approved");
            if (!string.IsNullOrWhiteSpace(discriminator))
            {
                c.WithDiscriminator(discriminator);
            }
        }, msg);
    }
}
