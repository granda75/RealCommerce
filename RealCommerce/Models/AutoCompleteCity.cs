

namespace RealCommerce.Models
{

    //"Version": 1,
    //"Key": "215854",
    //"Type": "City",
    //"Rank": 31,
    //"LocalizedName": "Tel Aviv",
    //"Country": {
    //  "ID": "IL",
    //  "LocalizedName": "Israel"
    //},
    //"AdministrativeArea": {
    //  "ID": "TA",
    //  "LocalizedName": "Tel Aviv"
    //}
    public class AutoCompleteCity
    {
        public int    Version { get; set; }

        public string Key { get; set; }

        public string Type { get; set; }
        public int    Rank { get; set; }

        public string LocalizedName { get; set; }

        public AutoCompleteCountry Country { get; set; }

        public AutoCompleteAdministrativeArea AdministrativeArea { get; set; }

    }
}