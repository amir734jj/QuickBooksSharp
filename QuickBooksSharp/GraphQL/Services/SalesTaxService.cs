using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class CalculateSalesTaxData
    {
        [JsonPropertyName("indirectTaxCalculateSaleTransactionTax")]
        public SalesTaxCalculationPayload? Result { get; set; }
    }

    public class SalesTaxCalculationPayload
    {
        [JsonPropertyName("taxCalculation")]
        public SalesTaxCalculation? TaxCalculation { get; set; }
    }

    public class SalesTaxService : ISalesTaxService
    {
        private const string DefaultTaxFields = @"
            taxCalculation {
                transactionDate
                shipping {
                    shipFromAddress {
                        streetAddressLine1
                    }
                    shipToAddress {
                        streetAddressLine1
                    }
                    shippingFee {
                        value
                    }
                    taxAmount {
                        value
                    }
                }
                lineItems {
                    nodes {
                        numberOfUnits
                        totalPriceExcludingTaxes {
                            value
                        }
                        taxAmount {
                            value
                        }
                        productVariantTaxability {
                            classificationCode
                        }
                        taxDetails {
                            taxAmount {
                                value
                            }
                            taxableAmount {
                                value
                            }
                            ratePercentageApplied {
                                ... on IndirectTax_RatePercentage {
                                    rate
                                }
                            }
                            taxRate {
                                name
                                taxRate {
                                    taxRateReferenceId
                                }
                            }
                        }
                    }
                }
                taxTotals {
                    totalTaxAmountExcludingShipping {
                        value
                    }
                    aggregatedTaxesExcludingShippingByRate {
                        taxableAmount {
                            value
                        }
                        taxAmount {
                            value
                        }
                        ratePercentageApplied {
                            ... on IndirectTax_RatePercentage {
                                rate
                            }
                        }
                        taxRate {
                            name
                            taxRate {
                                taxRateReferenceId
                            }
                        }
                    }
                }
            }";

        private readonly GraphQLClient _client;

        public SalesTaxService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<CalculateSalesTaxData>> CalculateSalesTaxAsync(SalesTaxCalculationInput input, string? fields = null)
        {
            var selectedFields = fields ?? DefaultTaxFields;
            var query = $@"
                mutation CalculateSalesTax($input: IndirectTax_TaxCalculationInput!) {{
                    indirectTaxCalculateSaleTransactionTax(input: $input) {{
                        ... on IndirectTax_TaxCalculationPayload {{
                            {selectedFields}
                        }}
                    }}
                }}";

            return await _client.SendMutationAsync<CalculateSalesTaxData>(query, new { input }, "CalculateSalesTax");
        }
    }
}
