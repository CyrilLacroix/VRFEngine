using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRFEngine.Model
{
    public class RuleVersion : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; } 
        public Status Status { get; set; }
        public RuleType Type { get; set; }
        public string Value { get; set; }

        public Guid RuleId { get; set; }            
        [ForeignKey("RuleId")]           
        public Rule Rule { get; set; }

        public Guid FieldVersionId { get; set; }            
        [ForeignKey("FieldVersionId")]           
        public FieldVersion FieldVersion { get; set; }

        public Guid FormVersionId { get; set; }            
        [ForeignKey("FormVersionId")]           
        public FormVersion FormVersion { get; set; }
    }
}