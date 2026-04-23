using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class SalesTaxEntityTests
    {
        
        [TestMethod]
        public void Serialize_SalesTaxCalculationInput()
        {
            var input = new SalesTaxCalculationInput
            {
                TransactionDate = "2026-05-15",
                Subject = new SalesTaxSubjectInput { QbCustomerId = "1" },
                Shipping = new SalesTaxShippingInput
                {
                    ShipFromAddress = new SalesTaxAddressInput { FreeFormAddressLine = "2700 Coast Ave CA, US 94043" },
                    ShipToAddress = new SalesTaxAddressInput { FreeFormAddressLine = "2700 Coast Ave CA, US 94043" },
                    ShippingFee = new SalesTaxMoneyInput { Value = 10.99m }
                },
                LineItems = new[]
                {
                    new SalesTaxLineItemInput
                    {
                        NumberOfUnits = 1,
                        PricePerUnitExcludingTaxes = new SalesTaxMoneyInput { Value = 100 },
                        ProductVariantTaxability = new SalesTaxProductVariantInput { ProductVariantId = "prod-1" }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"transactionDate\":\"2026-05-15\""));
            Assert.IsTrue(json.Contains("\"qbCustomerId\":\"1\""));
            Assert.IsTrue(json.Contains("\"freeFormAddressLine\":\"2700 Coast Ave CA, US 94043\""));
            Assert.IsTrue(json.Contains("\"value\":10.99"));
            Assert.IsTrue(json.Contains("\"numberOfUnits\":1"));
            Assert.IsTrue(json.Contains("\"value\":100"));
            Assert.IsTrue(json.Contains("\"productVariantId\":\"prod-1\""));
        }

        [TestMethod]
        public void Deserialize_SalesTaxCalculation()
        {
            var json = @"{
                ""transactionDate"": ""2026-05-15"",
                ""shipping"": {
                    ""shipFromAddress"": { ""streetAddressLine1"": ""2700 Coast Ave"" },
                    ""shipToAddress"": { ""streetAddressLine1"": ""2700 Coast Ave"" },
                    ""shippingFee"": { ""value"": 10.99 },
                    ""taxAmount"": { ""value"": 0.00 }
                },
                ""lineItems"": {
                    ""nodes"": [
                        {
                            ""numberOfUnits"": 1,
                            ""totalPriceExcludingTaxes"": { ""value"": 100.00 },
                            ""taxAmount"": { ""value"": 9.12 },
                            ""productVariantTaxability"": { ""classificationCode"": ""EUC-09020802"" },
                            ""taxDetails"": [
                                {
                                    ""taxAmount"": { ""value"": 6.24 },
                                    ""taxableAmount"": { ""value"": 100.00 },
                                    ""taxRate"": {
                                        ""name"": ""California State"",
                                        ""taxRate"": { ""taxRateReferenceId"": ""-1"" }
                                    },
                                    ""ratePercentageApplied"": { ""rate"": 6.25 }
                                }
                            ]
                        }
                    ]
                },
                ""taxTotals"": {
                    ""totalTaxAmountExcludingShipping"": { ""value"": 9.12 },
                    ""aggregatedTaxesExcludingShippingByRate"": [
                        {
                            ""taxableAmount"": { ""value"": 100.00 },
                            ""taxAmount"": { ""value"": 6.24 },
                            ""ratePercentageApplied"": { ""rate"": 6.25 },
                            ""taxRate"": { ""name"": ""California State"", ""taxRate"": { ""taxRateReferenceId"": ""-1"" } }
                        }
                    ]
                }
            }";

            var calc = JsonConvert.DeserializeObject<SalesTaxCalculation>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(calc);
            Assert.AreEqual("2026-05-15", calc.TransactionDate);

            // Shipping
            Assert.IsNotNull(calc.Shipping);
            Assert.AreEqual("2700 Coast Ave", calc.Shipping.ShipFromAddress?.StreetAddressLine1);
            Assert.AreEqual(10.99m, calc.Shipping.ShippingFee?.Value);
            Assert.AreEqual(0.00m, calc.Shipping.TaxAmount?.Value);

            // Line items
            Assert.IsNotNull(calc.LineItems?.Nodes);
            Assert.AreEqual(1, calc.LineItems.Nodes.Length);
            Assert.AreEqual(1, calc.LineItems.Nodes[0].NumberOfUnits);
            Assert.AreEqual(100.00m, calc.LineItems.Nodes[0].TotalPriceExcludingTaxes?.Value);
            Assert.AreEqual(9.12m, calc.LineItems.Nodes[0].TaxAmount?.Value);
            Assert.AreEqual("EUC-09020802", calc.LineItems.Nodes[0].ProductVariantTaxability?.ClassificationCode);

            // Tax details
            Assert.IsNotNull(calc.LineItems.Nodes[0].TaxDetails);
            Assert.AreEqual(1, calc.LineItems.Nodes[0].TaxDetails.Length);
            Assert.AreEqual(6.24m, calc.LineItems.Nodes[0].TaxDetails[0].TaxAmount?.Value);
            Assert.AreEqual(100.00m, calc.LineItems.Nodes[0].TaxDetails[0].TaxableAmount?.Value);
            Assert.AreEqual("California State", calc.LineItems.Nodes[0].TaxDetails[0].TaxRate?.Name);
            Assert.AreEqual(6.25m, calc.LineItems.Nodes[0].TaxDetails[0].RatePercentageApplied?.Rate);

            // Tax totals
            Assert.IsNotNull(calc.TaxTotals);
            Assert.AreEqual(9.12m, calc.TaxTotals.TotalTaxAmountExcludingShipping?.Value);
            Assert.IsNotNull(calc.TaxTotals.AggregatedTaxesExcludingShippingByRate);
            Assert.AreEqual(1, calc.TaxTotals.AggregatedTaxesExcludingShippingByRate.Length);
            Assert.AreEqual(6.24m, calc.TaxTotals.AggregatedTaxesExcludingShippingByRate[0].TaxAmount?.Value);
        }

        [TestMethod]
        public void Deserialize_FullSalesTaxGraphQLResponse()
        {
            var json = @"{
                ""data"": {
                    ""indirectTaxCalculateSaleTransactionTax"": {
                        ""taxCalculation"": {
                            ""transactionDate"": ""2026-05-15"",
                            ""lineItems"": {
                                ""nodes"": [
                                    {
                                        ""numberOfUnits"": 2,
                                        ""totalPriceExcludingTaxes"": { ""value"": 200.00 },
                                        ""taxAmount"": { ""value"": 18.25 }
                                    }
                                ]
                            },
                            ""taxTotals"": {
                                ""totalTaxAmountExcludingShipping"": { ""value"": 18.25 }
                            }
                        }
                    }
                }
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<CalculateSalesTaxData>>(json);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.Result?.TaxCalculation);
            Assert.AreEqual("2026-05-15", response.Data.Result.TaxCalculation.TransactionDate);
            Assert.AreEqual(18.25m, response.Data.Result.TaxCalculation.TaxTotals?.TotalTaxAmountExcludingShipping?.Value);
        }
    }
}
