using CommonLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace VM
{
    public class Coin : ObservableObject
    {

        private int _coinValue;
        /// <summary>
        /// 
        /// </summary>
        public int coinValue
        {
            get => _coinValue;
            set => OnPropertyChanged(ref _coinValue, value);
        }


        private int _quantity;
        /// <summary>
        /// 
        /// </summary>
        public int quantity
        {
            get => _quantity;
            set => OnPropertyChanged(ref _quantity, value);
        }
 
        public Coin() { }
        public Coin(int coinValue, int quantity)
        {
            this.coinValue = coinValue;
            this.quantity = quantity;
        }

    }
}
