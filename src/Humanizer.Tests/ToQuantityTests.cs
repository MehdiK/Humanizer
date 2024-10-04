[UseCulture("en-US")]
public class ToQuantityTests
{
    [Theory]
    [InlineData("case", 0, "0 cases")]
    [InlineData("case", 1, "1 case")]
    [InlineData("case", -1, "-1 case")]
    [InlineData("case", 5, "5 cases")]
    [InlineData("case", -5, "-5 cases")]
    [InlineData("man", 0, "0 men")]
    [InlineData("man", 1, "1 man")]
    [InlineData("man", -1, "-1 man")]
    [InlineData("man", 2, "2 men")]
    [InlineData("man", -2, "-2 men")]
    [InlineData("men", 2, "2 men")]
    [InlineData("men", -2, "-2 men")]
    [InlineData("process", 2, "2 processes")]
    [InlineData("process", -2, "-2 processes")]
    [InlineData("process", 1, "1 process")]
    [InlineData("process", -1, "-1 process")]
    [InlineData("processes", 2, "2 processes")]
    [InlineData("processes", -2, "-2 processes")]
    [InlineData("processes", 1, "1 process")]
    [InlineData("processes", -1, "-1 process")]
    [InlineData("slice", 1, "1 slice")]
    [InlineData("slice", -1, "-1 slice")]
    [InlineData("slice", 2, "2 slices")]
    [InlineData("slice", -2, "-2 slices")]
    [InlineData("slices", 1, "1 slice")]
    [InlineData("slices", -1, "-1 slice")]
    [InlineData("slices", 2, "2 slices")]
    [InlineData("slices", -2, "-2 slices")]
    public void ToQuantity(string word, int quantity, string expected) =>
        Assert.Equal(expected, word.ToQuantity(quantity));

    [Theory]
    [InlineData("case", 0, "cases")]
    [InlineData("case", 1, "case")]
    [InlineData("case", -1, "case")]
    [InlineData("case", 5, "cases")]
    [InlineData("case", -5, "cases")]
    [InlineData("man", 0, "men")]
    [InlineData("man", 1, "man")]
    [InlineData("man", -1, "man")]
    [InlineData("man", 2, "men")]
    [InlineData("man", -2, "men")]
    [InlineData("men", 2, "men")]
    [InlineData("men", -2, "men")]
    [InlineData("process", 2, "processes")]
    [InlineData("process", -2, "processes")]
    [InlineData("process", 1, "process")]
    [InlineData("process", -1, "process")]
    [InlineData("processes", 2, "processes")]
    [InlineData("processes", -2, "processes")]
    [InlineData("processes", 1, "process")]
    [InlineData("processes", -1, "process")]
    public void ToQuantityWithNoQuantity(string word, int quantity, string expected) =>
        Assert.Equal(expected, word.ToQuantity(quantity, ShowQuantityAs.None));

    [Theory]
    [InlineData("case", 0, "0 cases")]
    [InlineData("case", 1, "1 case")]
    [InlineData("case", -1, "-1 case")]
    [InlineData("case", 5, "5 cases")]
    [InlineData("case", -5, "-5 cases")]
    [InlineData("man", 0, "0 men")]
    [InlineData("man", 1, "1 man")]
    [InlineData("man", -1, "-1 man")]
    [InlineData("man", 2, "2 men")]
    [InlineData("man", -2, "-2 men")]
    [InlineData("men", 2, "2 men")]
    [InlineData("men", -2, "-2 men")]
    [InlineData("process", 2, "2 processes")]
    [InlineData("process", -2, "-2 processes")]
    [InlineData("process", 1, "1 process")]
    [InlineData("process", -1, "-1 process")]
    [InlineData("processes", 2, "2 processes")]
    [InlineData("processes", -2, "-2 processes")]
    [InlineData("processes", 1, "1 process")]
    [InlineData("processes", -1, "-1 process")]
    public void ToQuantityNumeric(string word, int quantity, string expected) =>
        Assert.Equal(expected, word.ToQuantity(quantity));

    [Theory]
    [InlineData("hour", -1, "-1 hour")]
    [InlineData("hour", -0.5, "-0.5 hours")]
    [InlineData("hour", -22.4, "-22.4 hours")]
    [InlineData("hour", 1, "1 hour")]
    [InlineData("hour", 0.5, "0.5 hours")]
    [InlineData("hour", 22.4, "22.4 hours")]
    public void ToDoubleQuantityNumeric(string word, double quantity, string expected) =>
        // ReSharper disable once RedundantArgumentDefaultValue
        Assert.Equal(expected, word.ToQuantity(quantity));

    [Theory]
    [InlineData("case", 0, "zero cases")]
    [InlineData("case", 1, "one case")]
    [InlineData("case", -1, "minus one case")]
    [InlineData("case", 5, "five cases")]
    [InlineData("case", -5, "minus five cases")]
    [InlineData("man", 0, "zero men")]
    [InlineData("man", 1, "one man")]
    [InlineData("man", -1, "minus one man")]
    [InlineData("man", 2, "two men")]
    [InlineData("man", -2, "minus two men")]
    [InlineData("men", 2, "two men")]
    [InlineData("men", -2, "minus two men")]
    [InlineData("process", 2, "two processes")]
    [InlineData("process", -2, "minus two processes")]
    [InlineData("process", 1, "one process")]
    [InlineData("process", -1, "minus one process")]
    [InlineData("processes", 2, "two processes")]
    [InlineData("processes", -2, "minus two processes")]
    [InlineData("processes", 1200, "one thousand two hundred processes")]
    [InlineData("processes", -1200, "minus one thousand two hundred processes")]
    [InlineData("processes", 1, "one process")]
    [InlineData("processes", -1, "minus one process")]
    public void ToQuantityWords(string word, int quantity, string expected) =>
        Assert.Equal(expected, word.ToQuantity(quantity, ShowQuantityAs.Words));

    [Theory]
    [InlineData("case", 0, null, "0 cases")]
    [InlineData("case", 1, null, "1 case")]
    [InlineData("case", -1, null, "-1 case")]
    [InlineData("case", 2, null, "2 cases")]
    [InlineData("case", -2, null, "-2 cases")]
    [InlineData("case", 1, "N0", "1 case")]
    [InlineData("case", -1, "N0", "-1 case")]
    [InlineData("case", 2, "N0", "2 cases")]
    [InlineData("case", -2, "N0", "-2 cases")]
    [InlineData("case", 123456, "N0", "123,456 cases")]
    [InlineData("case", -123456, "N0", "-123,456 cases")]
    [InlineData("case", 123456, "N2", "123,456.00 cases")]
    [InlineData("case", -123456, "N2", "-123,456.00 cases")]
    [InlineData("dollar", 0, "C0", "$0 dollars")]
    [InlineData("dollar", 1, "C0", "$1 dollar")]
    [InlineData("dollar", 2, "C0", "$2 dollars")]
    [InlineData("dollar", 2, "C2", "$2.00 dollars")]
    public void ToQuantityWordsWithCurrentCultureFormatting(string word, int quantity, string? format, string expected) =>
        Assert.Equal(expected, word.ToQuantity(quantity, format));

    [Theory]
    [InlineData("case", 0, "N0", "it-IT", "0 cases")]
    [InlineData("case", 1, "N0", "it-IT", "1 case")]
    [InlineData("case", -1, "N0", "it-IT", "-1 case")]
    [InlineData("case", 2, "N0", "it-IT", "2 cases")]
    [InlineData("case", -2, "N0", "it-IT", "-2 cases")]
    [InlineData("case", 1234567, "N0", "it-IT", "1.234.567 cases")]
    [InlineData("case", -1234567, "N0", "it-IT", "-1.234.567 cases")]
    [InlineData("case", 1234567, "N2", "it-IT", "1.234.567,00 cases")]
    [InlineData("case", -1234567, "N2", "it-IT", "-1.234.567,00 cases")]
    [InlineData("euro", 0, "C0", "es-ES", "0 € euros")]
    [InlineData("euro", 1, "C0", "es-ES", "1 € euro")]
    [InlineData("euro", -1, "C0", "es-ES", "-1 € euro")]
    [InlineData("euro", 2, "C0", "es-ES", "2 € euros")]
    [InlineData("euro", -2, "C0", "es-ES", "-2 € euros")]
    [InlineData("euro", 2, "C2", "es-ES", "2,00 € euros")]
    [InlineData("euro", -2, "C2", "es-ES", "-2,00 € euros")]
    public void ToQuantityWordsWithCustomCultureFormatting(string word, int quantity, string format, string cultureCode, string expected)
    {
        var culture = new CultureInfo(cultureCode);

        Assert.Equal(expected, word.ToQuantity(quantity, format, culture), GetStringComparer(culture));
    }

    internal static StringComparer GetStringComparer(CultureInfo culture) =>
        StringComparer.Create(culture, false);
}