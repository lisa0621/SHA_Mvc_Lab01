//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MotoGP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.Team_Rider = new HashSet<Team_Rider>();
        }
    
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public Nullable<MotoGP.Models.Enum.EnumGameClass> Class { get; set; }
        public string Bike { get; set; }
        public Nullable<int> cc { get; set; }
        public Nullable<MotoGP.Models.Enum.EnumBikeCompany> Company { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Team_Rider> Team_Rider { get; set; }
    }
}
