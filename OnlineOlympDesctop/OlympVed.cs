//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineOlympDesctop
{
    using System;
    using System.Collections.Generic;
    
    public partial class OlympVed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OlympVed()
        {
            this.PersonInOlympVed = new HashSet<PersonInOlympVed>();
        }
    
        public System.Guid Id { get; set; }
        public int OlympYear { get; set; }
        public int ClassId { get; set; }
        public bool IsLocked { get; set; }
        public bool IsLoad { get; set; }
    
        public virtual SchoolClass_local SchoolClass { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInOlympVed> PersonInOlympVed { get; set; }
    }
}
