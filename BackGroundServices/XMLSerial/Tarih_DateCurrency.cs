using System;
using System.Diagnostics.CodeAnalysis;

namespace TcmbImplementationStudy.BackGroundServices.XMLSerial
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Tarih_DateCurrency : IEquatable<Tarih_DateCurrency>, IComparable<Tarih_DateCurrency>
    {

        private byte unitField;

        private string isimField;

        private string currencyNameField;

        private decimal forexBuyingField;

        private string forexSellingField;

        private string banknoteBuyingField;

        private string banknoteSellingField;

        private string crossRateUSDField;

        private string crossRateOtherField;

        private byte crossOrderField;

        private string kodField;

        private string currencyCodeField;

        /// <remarks/>
        public byte Unit
        {
            get => this.unitField;
            set => this.unitField = value;
        }

        /// <remarks/>
        public string Isim
        {
            get => this.isimField;
            set => this.isimField = value;
        }

        /// <remarks/>
        public string CurrencyName
        {
            get => this.currencyNameField;
            set => this.currencyNameField = value;
        }

        /// <remarks/>
        public decimal ForexBuying
        {
            get => this.forexBuyingField;
            set => this.forexBuyingField = value;
        }

        /// <remarks/>
        public string ForexSelling
        {
            get => this.forexSellingField;
            set => this.forexSellingField = value;
        }

        /// <remarks/>
        public string BanknoteBuying
        {
            get => this.banknoteBuyingField;
            set => this.banknoteBuyingField = value;
        }

        /// <remarks/>
        public string BanknoteSelling
        {
            get => this.banknoteSellingField;
            set => this.banknoteSellingField = value;
        }

        /// <remarks/>
        public string CrossRateUSD
        {
            get => this.crossRateUSDField;
            set => this.crossRateUSDField = value;
        }

        /// <remarks/>
        public string CrossRateOther
        {
            get => this.crossRateOtherField;
            set => this.crossRateOtherField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte CrossOrder
        {
            get => this.crossOrderField;
            set => this.crossOrderField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Kod
        {
            get => this.kodField;
            set => this.kodField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CurrencyCode
        {
            get => this.currencyCodeField;
            set => this.currencyCodeField = value;
        }

        public int CompareTo([AllowNull] Tarih_DateCurrency other)
        {
            return other == null ? 1 : this.ForexBuying.CompareTo(other.ForexBuying);
        }

        public bool Equals([AllowNull] Tarih_DateCurrency other)
        {
            return other != null && this.ForexBuying.Equals(other.ForexBuying);
        }
    }
}