namespace RSpeditionWEB.Components.ComponentsBase
{
    public class AttrHelperClass : BlazorTimer
    {
        public (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) AttrCountry { get; private set; }
        public (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) AttrCurrency { get; private set; }
        public (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) AttrTRideCostCategory { get; private set; }
        public (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) AttrTFuelCardType { get; private set; }
        public (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) AttrTDivision { get; private set; }
        public (List<PropertyInfo> props, PropertyInfo textProp, string labelForSearch, string placeholder, PropertyInfo keyPropInfo) AttrFuelCardView { get; private set; }

        protected void InitAttrValForInput()
        {
            this.AttrCountry = typeof(CountryResponse).GetValuesForInputSearch();
            this.AttrCurrency = typeof(CurrencyResponse).GetValuesForInputSearch();
            this.AttrTRideCostCategory = typeof(FuelTypeResponse).GetValuesForInputSearch();
            this.AttrTFuelCardType = typeof(FuelProviderResponse).GetValuesForInputSearch();
            this.AttrTDivision = typeof(DivisionResponse).GetValuesForInputSearch();
            this.AttrFuelCardView = typeof(FuelCardFullModel).GetValuesForInputSearch();
        }
    }
}
