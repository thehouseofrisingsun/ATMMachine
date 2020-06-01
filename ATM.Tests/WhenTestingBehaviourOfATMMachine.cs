using ATMMachine.CustomExceptions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Tests
{
    [TestFixture]
    public class WhenTestingBehaviourOfATMMachine
    {
        private IATMMachine _atmMachine;

        private IBankAccount _bankAccount;

        [SetUp]
        public void SetUp()
        {
            _bankAccount = Substitute.For<BankAccount>();
            _atmMachine = new ATMMachine(_bankAccount);
        }

        [Test]
        public void ItShouldntFail_WhenWithdrawMoney([Values(5, 15, 20, 25)]int amount)
        {
            //Arrange
            _bankAccount.Balance = 50;

            //Act
            var money = _atmMachine.WithdrawMoney(amount);

            //Assert
            Assert.AreEqual(amount, money.Amount);
        }

        [Test]
        public void ItShouldThrowInsufficientAmountOnAccountForWithdrawException_WhenWithdrawAmountLessThanInAccount()
        {
            //Arrange
            _bankAccount.Balance = 3;

            //Act

            //Assert
            Assert.Throws<InsufficientAmountOnAccountForWithdrawException>(() => _atmMachine.WithdrawMoney(20));
        }

        [Test]
        public void ItShouldThrowWrongWithdrawAmountException_WhenTryingToWithdrawAmountWhichNotDevivedBy_5([Values(1, 4, 8, 23)]int amount)
        {
            //Arange
            _bankAccount.Balance = 50;

            //Act

            //Assert
            Assert.Throws<WrongWithdrawAmountException>(() => _atmMachine.WithdrawMoney(amount));
        }

        [Test]
        public void ItShouldThrowWrongNoteLoadedExceptin_WhenLoadingNoteDistinctFrom_5_10_20_50([Values(100, 1)] int note)
        {
            //Arrange
            Money money = new Money
            {
                Notes = new Dictionary<PaperNote, int>()
                {
                    { new PaperNote(), note }
                }
            };

            //Act

            //Assert
            Assert.Throws<WrongNoteLoadedException>(() => _atmMachine.LoadMoney(money));
        }

        [Test]
        public void ItShouldAddTransactionInTheList_WhenCallingWithdrawAmount()
        {
            //Arrange
            _bankAccount.Balance = 50;
            int amount = 20;

            //Act
            _atmMachine.WithdrawMoney(amount);

            //Assert
            _bankAccount.Received(1).Transactions.Add(Arg.Any<Transaction>());
        }

        [Test]
        public void ItShouldWithdrawExactAmountOfMoney_WhenCallingWithdrawAmount()
        {
            //Arrange
            _bankAccount.Balance = 50;
            int amount = 15;

            //Act
            var money = _atmMachine.WithdrawMoney(amount);

            //Assert
            Assert.AreEqual(money.Amount, amount);
        }

        [Test]
        public void ItShouldChargeClientCardWithCommissionFee_WhenCallingWithdrawAmount()
        {
            //Arrange
            _bankAccount.Balance = 50;
            var initialBalance = _bankAccount.Balance;
            int amount = 15;

            //Act
            _atmMachine.WithdrawMoney(amount);

            //Assert
            Assert.AreEqual(_bankAccount.Balance, initialBalance - amount - (decimal)0.01 * amount);
        }
    }
}
