﻿[MemoryDiagnoser(false)]
public class EnumBenchmarks
{
    public enum EnumUnderTest
    {
        [Description("The description")]
        MemberWithDescriptionAttribute,
        MemberWithoutDescriptionAttribute,

        [Display(Description = "Display value")]
        MemberWithDisplayAttribute,
    }

    [Benchmark(Description = "Enum.Humanize")]
    public string Humanize() => EnumUnderTest.MemberWithDisplayAttribute.Humanize();
}