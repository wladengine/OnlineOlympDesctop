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
    
    public partial class Participant
    {
        public System.Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> SexId { get; set; }
        public string PlaceOfBirth { get; set; }
        public Nullable<int> ClassId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string SchoolName { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public string DocumentSeries { get; set; }
        public string DocumentNumber { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public string DocumentAuthor { get; set; }
        public Nullable<System.DateTime> DocumentValid { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public string ArrivalNumber { get; set; }
        public Nullable<int> ArrivalPlaceId { get; set; }
        public Nullable<System.DateTime> DepatureDate { get; set; }
        public Nullable<int> DepaturePlaceId { get; set; }
        public string DepatureNumber { get; set; }
        public string AdditionalComments { get; set; }
        public bool IsHidden { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
        public virtual Sex Sex { get; set; }
    }
}
