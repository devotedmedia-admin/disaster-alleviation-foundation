using DAF.Models;
using DAF.Pages;

namespace ProgramLogic.nUnitTests
{
    public class FundsTest
    {
        private Funds funds = null!;
        private Goods goods = null!;
        private allocateModel allocate = null!;

        [SetUp]
        public void Setup()
        {
            funds = new Funds();
            goods = new Goods();
        }

        [Test]
        public void CalculateGoods_LessThanTest()
        {
            //Assign

            var amount = 500;

            //Act 0878255600
            var availableAmount = goods.CalculateGoods();

            //Assert
            Assert.That(amount, Is.LessThanOrEqualTo(availableAmount));

        }

        [Test]
        public void CalculateMoney_LessThanTest()
        {
            //Assign

            var amount = 0;

            //Act 0878255600
            var availableAmount = funds.CalculateMoney();

            //Assert
            Assert.That(amount, Is.LessThanOrEqualTo(availableAmount));

        }
    }
}