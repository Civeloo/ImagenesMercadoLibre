using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImagenesMercadoLibre.Models
{
    //public class SaleTerm
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string value_id { get; set; }
    //    public string value_name { get; set; }
    //    public object value_struct { get; set; }
    //}

    //public class Description
    //{
    //    public string id { get; set; }
    //}

    //public class Shipping
    //{
    //    public string mode { get; set; }
    //    public List<object> methods { get; set; }
    //    public List<object> tags { get; set; }
    //    public object dimensions { get; set; }
    //    public bool local_pick_up { get; set; }
    //    public bool free_shipping { get; set; }
    //    public string logistic_type { get; set; }
    //    public bool store_pick_up { get; set; }
    //}

    //public class City
    //{
    //    public string name { get; set; }
    //}

    //public class State
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Country
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}

    //public class Neighborhood
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}

    //public class City2
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}

    //public class State2
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}

    //public class SearchLocation
    //{
    //    public Neighborhood neighborhood { get; set; }
    //    public City2 city { get; set; }
    //    public State2 state { get; set; }
    //}

    //public class SellerAddress
    //{
    //    public string comment { get; set; }
    //    public string address_line { get; set; }
    //    public string zip_code { get; set; }
    //    public City city { get; set; }
    //    public State state { get; set; }
    //    public Country country { get; set; }
    //    public SearchLocation search_location { get; set; }
    //    public double latitude { get; set; }
    //    public double longitude { get; set; }
    //    public int id { get; set; }
    //}

    //public class Location
    //{
    //}

    //public class Geolocation
    //{
    //    public double latitude { get; set; }
    //    public double longitude { get; set; }
    //}

    //public class Attribute
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string value_id { get; set; }
    //    public string value_name { get; set; }
    //    public object value_struct { get; set; }
    //    public string attribute_group_id { get; set; }
    //    public string attribute_group_name { get; set; }
    //}
    [Table("Item")]
    [DataContract]
    public class ItemModel
    {
        public ItemModel()
        {
            //variations = new List<VariationModel>();
        }
        [PrimaryKey]
        [DataMember(Name = "id")]
        public string ID { get; set; }
        public string site_id { get; set; }
        [DataMember(Name = "title")]
        public string title { get; set; }
        //public object subtitle { get; set; }
        [DataMember(Name = "seller_id")]
        public int seller_id { get; set; }
        [DataMember(Name = "category_id")]
        public string category_id { get; set; }
        //public object official_store_id { get; set; }
        [DataMember(Name = "price")]
        public double price { get; set; }
        [DataMember(Name = "base_price")]
        public double base_price { get; set; }
        //public object original_price { get; set; }
        //public object inventory_id { get; set; }
        [DataMember(Name = "currency_id")]
        public string currency_id { get; set; }
        [DataMember(Name = "initial_quantity")]
        public int initial_quantity { get; set; }
        [DataMember(Name = "available_quantity")]
        public int available_quantity { get; set; }
        [DataMember(Name = "sold_quantity")]
        public int sold_quantity { get; set; }
        //public List<SaleTerm> sale_terms { get; set; }
        [DataMember(Name = "buying_mode")]
        public string buying_mode { get; set; }
        [DataMember(Name = "listing_type_id")]
        public string listing_type_id { get; set; }
        [DataMember(Name = "start_time")]
        public DateTime start_time { get; set; }
        [DataMember(Name = "stop_time")]
        public DateTime stop_time { get; set; }
        [DataMember(Name = "end_time")]
        public DateTime end_time { get; set; }
        [DataMember(Name = "expiration_time")]
        public DateTime expiration_time { get; set; }
        [DataMember(Name = "condition")]
        public string condition { get; set; }
        [DataMember(Name = "permalink")]
        public string permalink { get; set; }
        [DataMember(Name = "thumbnail")]
        public string thumbnail { get; set; }
        [DataMember(Name = "secure_thumbnail")]
        public string secure_thumbnail { get; set; }
        //public List<Picture> pictures { get; set; }
        [DataMember(Name = "video_id")]
        public string video_id { get; set; }
        //public List<Description> descriptions { get; set; }
        [DataMember(Name = "accepts_mercadopago")]
        public bool accepts_mercadopago { get; set; }
        //public List<object> non_mercado_pago_payment_methods { get; set; }
        //public Shipping shipping { get; set; }
        [DataMember(Name = "international_delivery_mode")]
        public string international_delivery_mode { get; set; }
        //public SellerAddress seller_address { get; set; }
        //public object seller_contact { get; set; }
        //public Location location { get; set; }
        //public Geolocation geolocation { get; set; }
        //public List<object> coverage_areas { get; set; }
        //public List<Attribute> attributes { get; set; }
        //public List<object> warnings { get; set; }
        [DataMember(Name = "listing_source")]
        public string listing_source { get; set; }

        [DataMember(Name = "variations")]
        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation       
        public List<VariationModel> variations { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
        //public List<object> sub_status { get; set; }
        //public List<string> tags { get; set; }
        //public object warranty { get; set; }
        //public object catalog_product_id { get; set; }
        [DataMember(Name = "domain_id")]
        public string domain_id { get; set; }
        [DataMember(Name = "seller_custom_field")]
        public string seller_custom_field { get; set; }//object
        //public object parent_item_id { get; set; }
        //public object differential_pricing { get; set; }
        //public List<object> deal_ids { get; set; }
        [DataMember(Name = "automatic_relist")]
        public bool automatic_relist { get; set; }
        [DataMember(Name = "date_created")]
        public DateTime date_created { get; set; }
        [DataMember(Name = "last_updated")]
        public DateTime last_updated { get; set; }
        [DataMember(Name = "health")]
        public double health { get; set; }
        [DataMember(Name = "catalog_listing")]
        public bool catalog_listing { get; set; }
        //public List<object> item_relations { get; set; }
        public string seller_sku { get; set; }
    }
}
