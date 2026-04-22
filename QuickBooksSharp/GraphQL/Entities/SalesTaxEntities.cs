using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
    public class SalesTaxCalculationInput
    {
        [JsonPropertyName("transactionDate")]
        public string TransactionDate { get; set; } = null!;

        [JsonPropertyName("subject")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxSubjectInput? Subject { get; set; }

        [JsonPropertyName("shipping")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxShippingInput? Shipping { get; set; }

        [JsonPropertyName("lineItems")]
        public SalesTaxLineItemInput[] LineItems { get; set; } = null!;
    }

    public class SalesTaxSubjectInput
    {
        [JsonPropertyName("qbCustomerId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? QbCustomerId { get; set; }
    }

    public class SalesTaxShippingInput
    {
        [JsonPropertyName("shipFromAddress")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxAddressInput? ShipFromAddress { get; set; }

        [JsonPropertyName("shipToAddress")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxAddressInput? ShipToAddress { get; set; }

        [JsonPropertyName("shippingFee")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxMoneyInput? ShippingFee { get; set; }
    }

    public class SalesTaxAddressInput
    {
        [JsonPropertyName("freeFormAddressLine")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FreeFormAddressLine { get; set; }

        [JsonPropertyName("streetAddressLine1")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StreetAddressLine1 { get; set; }

        [JsonPropertyName("streetAddressLine2")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StreetAddressLine2 { get; set; }

        [JsonPropertyName("city")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? City { get; set; }

        [JsonPropertyName("stateProvinceCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StateProvinceCode { get; set; }

        [JsonPropertyName("postalCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PostalCode { get; set; }

        [JsonPropertyName("countryCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CountryCode { get; set; }
    }

    public class SalesTaxMoneyInput
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }

    public class SalesTaxLineItemInput
    {
        [JsonPropertyName("numberOfUnits")]
        public int NumberOfUnits { get; set; }

        [JsonPropertyName("pricePerUnitExcludingTaxes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxMoneyInput? PricePerUnitExcludingTaxes { get; set; }

        [JsonPropertyName("productVariantTaxability")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SalesTaxProductVariantInput? ProductVariantTaxability { get; set; }
    }

    public class SalesTaxProductVariantInput
    {
        [JsonPropertyName("productVariantId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ProductVariantId { get; set; }
    }

    // Response types

    public class SalesTaxCalculation
    {
        [JsonPropertyName("transactionDate")]
        public string? TransactionDate { get; set; }

        [JsonPropertyName("shipping")]
        public SalesTaxShipping? Shipping { get; set; }

        [JsonPropertyName("lineItems")]
        public SalesTaxLineItems? LineItems { get; set; }

        [JsonPropertyName("taxTotals")]
        public SalesTaxTotals? TaxTotals { get; set; }
    }

    public class SalesTaxShipping
    {
        [JsonPropertyName("shipFromAddress")]
        public SalesTaxAddress? ShipFromAddress { get; set; }

        [JsonPropertyName("shipToAddress")]
        public SalesTaxAddress? ShipToAddress { get; set; }

        [JsonPropertyName("shippingFee")]
        public SalesTaxMoney? ShippingFee { get; set; }

        [JsonPropertyName("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }
    }

    public class SalesTaxAddress
    {
        [JsonPropertyName("streetAddressLine1")]
        public string? StreetAddressLine1 { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("stateProvinceCode")]
        public string? StateProvinceCode { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("countryCode")]
        public string? CountryCode { get; set; }
    }

    public class SalesTaxMoney
    {
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }

    public class SalesTaxLineItems
    {
        [JsonPropertyName("nodes")]
        public SalesTaxLineItem[]? Nodes { get; set; }
    }

    public class SalesTaxLineItem
    {
        [JsonPropertyName("numberOfUnits")]
        public int? NumberOfUnits { get; set; }

        [JsonPropertyName("totalPriceExcludingTaxes")]
        public SalesTaxMoney? TotalPriceExcludingTaxes { get; set; }

        [JsonPropertyName("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }

        [JsonPropertyName("productVariantTaxability")]
        public SalesTaxProductVariant? ProductVariantTaxability { get; set; }

        [JsonPropertyName("taxDetails")]
        public SalesTaxDetail[]? TaxDetails { get; set; }
    }

    public class SalesTaxProductVariant
    {
        [JsonPropertyName("classificationCode")]
        public string? ClassificationCode { get; set; }
    }

    public class SalesTaxDetail
    {
        [JsonPropertyName("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }

        [JsonPropertyName("taxableAmount")]
        public SalesTaxMoney? TaxableAmount { get; set; }

        [JsonPropertyName("taxRate")]
        public SalesTaxRateInfo? TaxRate { get; set; }

        [JsonPropertyName("ratePercentageApplied")]
        public SalesTaxRatePercentage? RatePercentageApplied { get; set; }
    }

    public class SalesTaxRateInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("taxRate")]
        public SalesTaxRateRef? TaxRate { get; set; }
    }

    public class SalesTaxRateRef
    {
        [JsonPropertyName("taxRateReferenceId")]
        public string? TaxRateReferenceId { get; set; }
    }

    public class SalesTaxRatePercentage
    {
        [JsonPropertyName("rate")]
        public decimal? Rate { get; set; }
    }

    public class SalesTaxTotals
    {
        [JsonPropertyName("totalTaxAmountExcludingShipping")]
        public SalesTaxMoney? TotalTaxAmountExcludingShipping { get; set; }

        [JsonPropertyName("aggregatedTaxesExcludingShippingByRate")]
        public SalesTaxAggregatedRate[]? AggregatedTaxesExcludingShippingByRate { get; set; }
    }

    public class SalesTaxAggregatedRate
    {
        [JsonPropertyName("taxableAmount")]
        public SalesTaxMoney? TaxableAmount { get; set; }

        [JsonPropertyName("taxAmount")]
        public SalesTaxMoney? TaxAmount { get; set; }

        [JsonPropertyName("ratePercentageApplied")]
        public SalesTaxRatePercentage? RatePercentageApplied { get; set; }

        [JsonPropertyName("taxRate")]
        public SalesTaxRateInfo? TaxRate { get; set; }
    }
}
