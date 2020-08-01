using CommonLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace VM
{
    public class Product : ObservableObject
    {

        private string _position;
        /// <summary>
        /// 
        /// </summary>
        public string position
        {
            get => _position;
            set => OnPropertyChanged(ref _position, value);
        }


        private int _cost;
        /// <summary>
        /// 
        /// </summary>
        public int cost
        {
            get => _cost;
            set => OnPropertyChanged(ref _cost, value);
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

        public Product() { }
        public Product(string position, int cost, int quantity)
        {
            this.position = position;
            this.cost = cost;
            this.quantity = quantity;
        }
    }
}
