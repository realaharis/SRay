using System.Globalization;

namespace ServiceLib.Common;

/// <summary>Country grouping from cached egress tests, with explicit label fallback.</summary>
public static class ProfileCountry
{
    private static readonly HashSet<string> Codes = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
        .Select(c => new RegionInfo(c.Name).TwoLetterISORegionName)
        .ToHashSet(StringComparer.OrdinalIgnoreCase);

    public static string GetCode(string? ipInfo, string? remarks)
    {
        var match = Regex.Match(ipInfo ?? "", @"\(([A-Za-z]{2})\)");
        if (match.Success && Codes.Contains(match.Groups[1].Value))
            return match.Groups[1].Value.ToUpperInvariant();

        var runes = (remarks ?? "").EnumerateRunes().ToArray();
        for (var i = 0; i + 1 < runes.Length; i++)
        {
            var a = runes[i].Value - 0x1F1E6;
            var b = runes[i + 1].Value - 0x1F1E6;
            if (a is < 0 or > 25 || b is < 0 or > 25) continue;
            var code = $"{(char)('A' + a)}{(char)('A' + b)}";
            if (Codes.Contains(code)) return code;
        }
        return "?";
    }
}
