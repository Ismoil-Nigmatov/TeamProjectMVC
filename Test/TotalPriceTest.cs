﻿using Microsoft.Extensions.Configuration;
using Moq;
using TeamProjectMVC.Repository.Impl;

namespace Test
{
    public class TotalPriceTest
    {
        [Fact]
        public void CalculateTotalPrice_Should_ReturnCorrectTotalPrice()
        {
            ulong quantity = 5;
            var price = 10.0; 
            var vat = "0.2";

            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c["Vat"]).Returns(vat);

            var productRepository = new ProductRepository(null!, configuration.Object);

            var totalPrice = productRepository.CalculateTotalPrice(quantity, price);

            var expectedTotalPrice = (quantity * price) * (1 + Convert.ToDouble(vat));
            Assert.Equal(expectedTotalPrice, totalPrice.Result);
        }
    }
}
