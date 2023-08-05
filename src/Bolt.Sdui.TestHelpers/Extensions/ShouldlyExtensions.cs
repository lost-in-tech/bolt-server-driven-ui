using Bolt.Common.Extensions;
using Shouldly;

namespace Bolt.Sdui.TestHelpers.Extensions;
public static class ShouldlyExtensions
{
    public static void ShouldMatchApprovedWithDefaultOptions<T>(this T got, string msg = null, string discriminator = null)
    {
        got.SerializeToPrettyJson().ShouldMatchApproved(c =>
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
