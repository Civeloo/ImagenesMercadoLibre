using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ImagenesMercadoLibre.Models
{
    [Table("variations")] //"variations": [{
    [DataContract]
    public class VariationModel
    {
        [PrimaryKey]
        [DataMember(Name = "id")]
        public string ID { get; set; } //"id": 33898892473,
               
        [ForeignKey(typeof(ItemModel))]     // Specify the foreign key
        public string ItemID { get; set; }

        [DataMember(Name = "price")]
        public double price { get; set; }//"price": 7299,
        //"attribute_combinations": [
        //    {
        //        "id": "COLOR",
        //        "name": "Cor",
        //        "value_id": "162060",
        //        "value_name": "Cinza-espacial",
        //        "value_struct": null,
        //        "values": [
        //            {
        //                "id": "162060",
        //                "name": "Cinza-espacial",
        //                "struct": null
        //            }
        //        ]
        //    }
        //],
        [DataMember(Name = "available_quantity")]
        public int available_quantity { get; set; }//"available_quantity": 1,
        [DataMember(Name = "sold_quantity")]
        public int sold_quantity { get; set; }//"sold_quantity": 0,
        //"sale_terms": [],

        //"picture_ids": [
        //    "948658-MLB31730426082_082019",
        //    "710945-MLB31730422159_082019"
        //],
        [DataMember(Name = "picture_ids")]
        [TextBlob("picture_ids")]
        public List<string>  pictures { get; set; }
        public string picture_ids { get; set; }

    [DataMember(Name = "catalog_product_id")]
        public string catalog_product_id { get; set; }//"catalog_product_id": "MLB12866584" } ]    

        [ManyToOne]      // Many to one relationship with 
        public ItemModel ItemModel { get; set; }
    }    
}
