using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRFEngine.Model
{
    public class FormVersion : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; } 
        public Status Status { get; set; }

        public Guid FormId { get; set; }            
        [ForeignKey("FormId")]           
        public Form Form { get; set; }

        public ICollection<FieldVersion> Fields { get; set; }
        public ICollection<RuleVersion> Rules { get; set; }
    }
}