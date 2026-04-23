using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL.Entities
{
    public class SalesTaxCalculationInput
    {
        [JsonProperty("transactionDate")]
        public string TransactionDate { get; set; } = null!;
        [JsonProperty("subject")]
        public SalesTaxSubjectInput? Subject { get; set; }
        [JsonProperty("shipping")]
        public SalesTaxShippingInput? Shipping { get; set; }
        [JsonProperty("lineItems")]
        public SalesTaxLineItemInput[] LineItems { get; set; } = null!;
    }

    public class SalesTaxSubjectInput
    {
        [JsonProperty("qbCustomerId")]
        public string? QbCustomerId { get; set; }
    }

    public class SalesTaxShippingInput
    {
        [JsonProperty("shipFromAddress")]
        public SalesTaxAddressInput? ShipFromAddress { get; set; }
        [JsonProperty("shipToAddress")]
        public SalesTaxAddressInput? ShipToAddress { get; set; }
        [JsonProperty("shippingFee")]
        public SalesTaxMoneyInput? ShippingFee { get; set; }
    }

    public class SalesTaxAddressInput
    {
        [JsonProperty("freeFormAddressLine")]
        public string? FreeFormAddressLine { get; set; }
        [JsonProperty("streetAddressLine1")]
        public string? StreetAddressLine1 { get; set; }
        [JsonProperty("streetAddressLine2")]
        public string? StreetAddressLine2 { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("stateProvinceCode")]
        public string? StateProvinceCode { get; set; }
        [JsonProperty("postalCode")]
        public string? PostalCode { get; set; }
        [JsonProperty("countryCode")]
        public string? CountryCode { get; set; }
    }

    public class SalesTaxMoneyInput
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
    }

    public class SalesTaxLineItemInput
    {
        [JsonProperty("numberOfUnits")]
        public int NumberOfUnits { get; set; }
        [JsonProperty("pricePerUnitExcludingTaxes")]
        public SalesTaxMoneyInput? PricePerUnitExcludingTaxes { get; set; }
        [JsonProperty("productVariantTaxability")]
        public SalesTaxProductVariantInput? ProductVariantTaxability { get; set; }
    }

    public class SalesTaxProductVariantInput
    {
        [JsonProperty("productVariantId")]
        public string? ProductVariantId { get; set; }
    }

    // Response types

    public class SalesTaxCalculation
    {
        [JsonProperty("transactionDate")]
        public string? TransactionDate { get; set; }
        [JsonProperty("shipping")]
        public SalesTaxShipping? Shipping { get; set; }
        [JsonProperty("lineItems")]
        public SalesTaxLineItems? LineItems { get; set; }
        [JsonProperty("taxTotals")]
        public SalesTaxTotals? TaxTotals { get; set; }
    }

    public class SalesTaxShipping
    {
        [JsonProperty("shipFromAddress")]
        public SalesTaxAddress? ShipFromAddress { get; set; }
        [JsonProperty("shipToAddress")]
        public SalesTaxAddress? ShipToAddress { get; set; }
        [JsonProperty("shippingFee")]
        public SalesTaxMoney? ShippingFee { get; set; }
        [JsonProperty("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }
    }

    public class SalesTaxAddress
    {
        [JsonProperty("streetAddressLine1")]
        public string? StreetAddressLine1 { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("stateProvinceCode")]
        public string? StateProvinceCode { get; set; }
        [JsonProperty("postalCode")]
        public string? PostalCode { get; set; }
        [JsonProperty("countryCode")]
        public string? CountryCode { get; set; }
    }

    public class SalesTaxMoney
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
    }

    public class SalesTaxLineItems
    {
        [JsonProperty("nodes")]
        public SalesTaxLineItem[]? Nodes { get; set; }
    }

    public class SalesTaxLineItem
    {
        [JsonProperty("numberOfUnits")]
        public int? NumberOfUnits { get; set; }
        [JsonProperty("totalPriceExcludingTaxes")]
        public SalesTaxMoney? TotalPriceExcludingTaxes { get; set; }
        [JsonProperty("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }
        [JsonProperty("productVariantTaxability")]
        public SalesTaxProductVariant? ProductVariantTaxability { get; set; }
        [JsonProperty("taxDetails")]
        public SalesTaxDetail[]? TaxDetails { get; set; }
    }

    public class SalesTaxProductVariant
    {
        [JsonProperty("classificationCode")]
        public string? ClassificationCode { get; set; }
    }

    public class SalesTaxDetail
    {
        [JsonProperty("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }
        [JsonProperty("taxableAmount")]
        public SalesTaxMoney? TaxableAmount { get; set; }
        [JsonProperty("taxRate")]
        public SalesTaxRateInfo? TaxRate { get; set; }
        [JsonProperty("ratePercentageApplied")]
        public SalesTaxRatePercentage? RatePercentageApplied { get; set; }
    }

    public class SalesTaxRateInfo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("taxRate")]
        public SalesTaxRateRef? TaxRate { get; set; }
    }

    public class SalesTaxRateRef
    {
        [JsonProperty("taxRateReferenceId")]
        public string? TaxRateReferenceId { get; set; }
    }

    public class SalesTaxRatePercentage
    {
        [JsonProperty("rate")]
        public decimal? Rate { get; set; }
    }

    public class SalesTaxTotals
    {
        [JsonProperty("totalTaxAmountExcludingShipping")]
        public SalesTaxMoney? TotalTaxAmountExcludingShipping { get; set; }
        [JsonProperty("aggregatedTaxesExcludingShippingByRate")]
        public SalesTaxAggregatedRate[]? AggregatedTaxesExcludingShippingByRate { get; set; }
    }

    public class SalesTaxAggregatedRate
    {
        [JsonProperty("taxableAmount")]
        public SalesTaxMoney? TaxableAmount { get; set; }
        [JsonProperty("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }
        [JsonProperty("ratePercentageApplied")]
        public SalesTaxRatePercentage? RatePercentageApplied { get; set; }
        [JsonProperty("taxRate")]
        public SalesTaxRateInfo? TaxRate { get; set; }
    }
}