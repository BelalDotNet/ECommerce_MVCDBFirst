//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Orders = new HashSet<Order>();
            this.ShoppingCarts = new HashSet<ShoppingCart>();
        }
    
        public int ProductID { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public System.DateTime FirstRelease { get; set; }
        public bool IsActive { get; set; }
        public string ProductDetails { get; set; }
        public decimal Price { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    
        public virtual MobileBrand MobileBrand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}