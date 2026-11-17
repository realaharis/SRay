using ServiceLib.Common;
var cases = new (string? ip, string? label, string expected)[] {
(null, null, "?"), ("", "plain US server", "?"),
("(US) 1.1.1.1", "🇩🇪 label", "US"), ("(de) 1.1.1.1", "", "DE"),
("garbage", "🇯🇵 Tokyo", "JP"), ("(ZZ) error", "🇳🇱 fallback", "NL"),
("", "🇿🇿 invalid", "?"), ("", "🇺 lone indicator", "?"),
("", "prefix 🇮🇷 suffix", "IR"), ("", "🇩🇪🇺🇸", "DE"),
("timeout", "Frankfurt", "?"), ("(GB) 1.1.1.1", "🇺🇸", "GB")
};
foreach (var c in cases) {
 var actual=ProfileCountry.GetCode(c.ip,c.label);
 if(actual!=c.expected) throw new Exception($"Expected {c.expected}, got {actual}");
}
Console.WriteLine($"PASS: {cases.Length} country provenance and unknown cases");
