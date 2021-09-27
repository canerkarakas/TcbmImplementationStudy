namespace TcmbImplementationStudy.BackGroundServices.XMLSerial
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Tarih_Date
    {

        private Tarih_DateCurrency[] currencyField;

        private string tarihField;

        private string dateField;

        private string bulten_NoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Currency")]
        public Tarih_DateCurrency[] Currency
        {
            get => this.currencyField;
            set => this.currencyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Tarih
        {
            get => this.tarihField;
            set => this.tarihField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Date
        {
            get => this.dateField;
            set => this.dateField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Bulten_No
        {
            get => this.bulten_NoField;
            set => this.bulten_NoField = value;
        }
    }
}