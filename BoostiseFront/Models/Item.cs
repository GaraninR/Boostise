using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoostiseFront.Models
{
    public class Item
    {
        public int id {get; set;}
        public string? firstName {get; set;}
        public string? lastName {get; set;}
        public string? phoneNumber {get; set;}
        public string? email {get; set;}
        public string? advText {get; set;}
        public int age {get; set;}
        public float priceUSD {get; set;}
        public float course {get; set;}
        public float priceUAH {get; set;}

    }
}