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
    
    public partial class PersonFile
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> PersonId { get; set; }
        public Nullable<System.Guid> ParticipantId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileExtention { get; set; }
        public byte[] FileData { get; set; }
        public string Comment { get; set; }
        public System.DateTime LoadDate { get; set; }
        public string MimeType { get; set; }
        public string FailReason { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> FileTypeId { get; set; }
        public Nullable<bool> Approved { get; set; }
    
        public virtual FileType FileType { get; set; }
    }
}
