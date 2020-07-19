using System.ComponentModel;

namespace Domain
{
    public enum Currency
    {
        [Description("ZAR")]
        ZAR,
        [Description("USD")]
        USD,
        [Description("EUR")]
        EUR,
        [Description("GBP")]
        GBP,
        [Description("CNY")]
        CNY,
        [Description("JPY")]
        JPY,
        [Description("HKD")]
        HKD,
        [Description("AUD")]
        AUD
    }
}
