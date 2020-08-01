using CommonLib;
using MVVMMiniLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace VM
{
    public class VendingMachineVM : ObservableObject
    {
        public VendingMachineVM() 
        {
            sum = 0;
            CoinsCust = new ObservableCollection<Coin>() { new Coin(1, 10), new Coin(2, 30), new Coin(5, 20), new Coin(10, 15) };
            CoinsVM = new ObservableCollection<Coin>() { new Coin(1, 100), new Coin(2, 100), new Coin(5, 100), new Coin(10, 100) };
            Products = new ObservableCollection<Product> { new Product("Чай", 13, 10), new Product("Кофе", 18, 20), new Product("Кофе с молоком", 21, 20), new Product("Сок", 35, 15) };
            RefillCmd = new CommandBase(x => Refill((int)x), true);
            BuyCmd = new CommandBase(x => Buy(x as Product), true);
            oddMoneyCmd = new CommandBase(x => Change(), true);
        }

        private int _sum;
        /// <summary>
        /// Сумма внесенных денежных средств
        /// </summary>
        public int sum
        {
            get => _sum;
            set => OnPropertyChanged(ref _sum, value);
        }

        private ObservableCollection<Product> _Products;
        /// <summary>
        /// Продукты в наличии
        /// </summary>
        public ObservableCollection<Product> Products
        {
            get => _Products;
            set => OnPropertyChanged(ref _Products, value);
        }

        private ObservableCollection<Coin> _CoinsCust;
        /// <summary>
        /// Кошелек покупателя
        /// </summary>
        public ObservableCollection<Coin> CoinsCust
        {
            get => _CoinsCust;
            set => OnPropertyChanged(ref _CoinsCust, value);
        }

        private ObservableCollection<Coin> _CoinsVM;
        /// <summary>
        /// Кошелек VM
        /// </summary>
        public ObservableCollection<Coin> CoinsVM
        {
            get => _CoinsVM;
            set => OnPropertyChanged(ref _CoinsVM, value);
        }     

        private ICommand _RefillCmd;
        /// <summary>
        /// Команда пополнения счета
        /// </summary>
        public ICommand RefillCmd
        {
            get => _RefillCmd;
            set => OnPropertyChanged(ref _RefillCmd, value);
        }

        private ICommand _BuyCmd;
        /// <summary>
        /// Команда осуществления покупки
        /// </summary>
        public ICommand BuyCmd
        {
            get => _BuyCmd;
            set => OnPropertyChanged(ref _BuyCmd, value);
        }

        private ICommand _oddMoneyCmd;
        /// <summary>
        /// Команда возврата сдачи
        /// </summary>
        public ICommand oddMoneyCmd
        {
            get => _oddMoneyCmd;
            set => OnPropertyChanged(ref _oddMoneyCmd, value);
        }

        /// <summary>
        /// Метод увеличения внесенной суммы
        /// </summary>
        /// <param name="coinValue">Номинал внесенной монеты</param>
        public void Refill(int coinValue)
        {
            sum += coinValue;
            CoinsVM.Where(x => x.coinValue == coinValue).First().quantity++;
            CoinsCust.Where(x => x.coinValue == coinValue).First().quantity--;
            if(CoinsCust.Any(x => x.quantity == 0))
                CoinsCust.Remove(CoinsCust.Where(x => x.quantity == 0).First());
            
        }

        /// <summary>
        /// Метод осуществления покупки
        /// </summary>
        /// <param name="product">Выбранный продукт</param>
        public void Buy(Product product)
        {
            if (product.quantity != 0)
            {
                if (sum >= product.cost)
                {
                    sum -= product.cost;
                    product.quantity--;
                    if (Products.Any(x => x.quantity == 0))
                        Products.Remove(Products.Where(x => x.quantity == 0).First());
                    MessageBox.Show("Спасибо!");
                }
                else
                    MessageBox.Show("Недостаточно средств");
            }
        }

        /// <summary>
        /// Метод возврата сдачи
        /// </summary>
        public void Change()
        {
            if (sum == 0)
                return;

            foreach( var x in CoinsVM.OrderByDescending(x => x.coinValue))
            {
                int i;
                if(x.quantity != 1)
                    i = CoinsVM.Where(z => z.coinValue == x.coinValue).First().quantity >= sum / x.coinValue ? sum / x.coinValue : 0;
                else
                    i = CoinsVM.Where(z => z.coinValue == x.coinValue).First().quantity >= sum / x.coinValue ? sum / x.coinValue : x.quantity;
                if (i != 0)
                {
                    sum -= i * x.coinValue;
                    x.quantity -= i;
                    if (!CoinsCust.Any(z => z.coinValue == x.coinValue))
                        CoinsCust.Add(new Coin(x.coinValue, i));
                    else
                        CoinsCust.Where(z => z.coinValue == x.coinValue).First().quantity += i;
                }
                if (sum == 0)
                    break;
            }
             if (sum != 0)
             {
                 MessageBox.Show("Извините, но у автомата нет сдачи, купите еще что-нибудь");
             } 
        }

        //public void Change()
        //{
        //    bool moneybox = false;
        //    for (int a = 0; 1 * a <= sum; a++)
        //    {
        //        for (int b = 0; 2 * b <= sum; b++)
        //        {
        //            for (int c = 0; 5 * c <= sum; c++)
        //            {
        //                for (int d = 0; 10 * d <= sum; d++)
        //                {
        //                    if (1 * a + 2 * b + 5 * c + 10 * d == sum)
        //                    {
        //                        foreach (var x in CoinsVM)
        //                        {
        //                            switch (x.coinValue)
        //                            {
        //                                case 1:
        //                                    x.quantity -= a;
        //                                    break;
        //                                case 2:
        //                                    x.quantity -= b;
        //                                    break;
        //                                case 5:
        //                                    x.quantity -= c;
        //                                    break;
        //                                case 10:
        //                                    x.quantity -= d;
        //                                    break;
        //                                default:
        //                                    break;
        //                            }
        //                        }
        //                        foreach (var x in CoinsCust)
        //                        {
        //                            switch (x.coinValue)
        //                            {
        //                                case 1:
        //                                    x.quantity += a;
        //                                    break;
        //                                case 2:
        //                                    x.quantity += b;
        //                                    break;
        //                                case 5:
        //                                    x.quantity += c;
        //                                    break;
        //                                case 10:
        //                                    x.quantity += d;
        //                                    break;
        //                                default:
        //                                    break;
        //                            }
        //                        }

        //                        moneybox = true;
        //                        break;
        //                    }
        //                }
        //                if (moneybox)
        //                {
        //                    break;
        //                }
        //            }
        //            if (moneybox)
        //            {
        //                break;
        //            }
        //        }
        //        if (moneybox)
        //        {
        //            break;
        //        }
        //    }
        //}
    }

}
