using System;
using XFramework.Db.PetaPoco.Models;

namespace XFramework.DDD.Domain
{
    [PrimaryKey("Id", AutoIncrement = false)]
    public abstract class AggregateRoot :  IAggregateRoot
    {
        public string Id { get; set; }

        public int Version { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        
        protected AggregateRoot() { }

        protected AggregateRoot(string id)
        {
            Id = id;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            Version = 1;
        }

        protected void Updated()
        {
            UpdatedOn = DateTime.Now;
            Version += 1;
        }
    }
}
