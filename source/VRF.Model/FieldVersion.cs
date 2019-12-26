using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRFEngine.Model
{
    public class FieldVersion : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; } 
        public Status Status { get; set; }

        public Guid FieldId { get; set; }            
        [ForeignKey("FieldId")]           
        public Field Field { get; set; }
    }
}