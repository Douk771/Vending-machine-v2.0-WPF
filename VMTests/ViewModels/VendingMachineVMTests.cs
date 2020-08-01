using Microsoft.VisualStudio.TestTools.UnitTesting;
using VM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace VM.Tests
{
    [TestClass()]
    public class VendingMachineVMTests
    {
        VendingMachineVM vm;

        [TestInitialize]
        public void Setup()
        {
            vm = new VendingMachineVM();
            vm.CoinsCust = new ObservableCollection<Coin>() { new Coin(1, 10), new Coin(2, 30), new Coin(5, 20), new Coin(10, 15) };
            vm.CoinsVM = new ObservableCollection<Coin>() { new Coin(1, 2), new Coin(2, 0), new Coin(5, 1), new Coin(10, 0) };
            vm.sum = 7;
        }

        [TestMethod()]
        public void ChangeTest()
        {
            VendingMachineVM expected = new VendingMachineVM();
            expected.CoinsCust = new ObservableCollection<Coin>() { new Coin(1, 12), new Coin(2, 30), new Coin(5, 21), new Coin(10, 15) };
            expected.CoinsVM = new ObservableCollection<Coin>() { new Coin(1, 0), new Coin(2, 0), new Coin(5, 0), new Coin(10, 0) };
            expected.sum = 0;

            vm.Change();
            VendingMachineVM actual = vm;
            
            Assert.AreEqual(expected.CoinsVM.Where(x => x.coinValue == 1).First().quantity, actual.CoinsVM.Where(x => x.coinValue == 1).First().quantity);
            Assert.AreEqual(expected.CoinsVM.Where(x => x.coinValue == 2).First().quantity,actual.CoinsVM.Where(x => x.coinValue == 2).First().quantity);
            Assert.AreEqual(expected.CoinsVM.Where(x => x.coinValue == 5).First().quantity, actual.CoinsVM.Where(x => x.coinValue == 5).First().quantity);
            Assert.AreEqual(expected.CoinsVM.Where(x => x.coinValue == 10).First().quantity, actual.CoinsVM.Where(x => x.coinValue == 10).First().quantity);
            Assert.AreEqual(expected.sum, actual.sum);
        }
    }
}