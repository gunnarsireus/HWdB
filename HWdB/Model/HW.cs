using HWdB.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace HWdB.Model
{
    public class HwVersion : DropCreateDatabaseIfModelChanges<DataContext>
    {
        public int Id { get; set; }
        public int HwNumberId { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public DateTime Eos { get; set; }
        public DateTime Ltb { get; set; }
        public string Status { get; set; }
        public string Mtbf { get; set; }
        public string FailureRate { get; set; }
        public string RepairLeadTime { get; set; }
        public string RepairStrategy { get; set; }
        public List<ProductGroup> ProductGroups { get; set; }

        //public virtual HwNumber HwNumber { get; set; }
    }

    public class HwNumber : DropCreateDatabaseIfModelChanges<DataContext>
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string SpCode { get; set; }
        public string Size { get; set; }
        public List<HwVersion> HwVersions { get; set; }
    }

    public class ProductGroup : DropCreateDatabaseIfModelChanges<DataContext>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Predecessor { get; set; }
        public string PerdPgVersion { get; set; }
        public bool Locked { get; set; }
        public List<HwVersion> HwVersions { get; set; }
    }
}
