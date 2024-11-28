using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.ValueObjects
{
    public abstract class SoftDeletableEntity<TId> : Entity<TId>
    {
        public TId Id { get; protected set; }
        protected SoftDeletableEntity(TId id) : base(id)
        {
        }

        public bool IsDeleted { get; protected set; } = false;
        public DateTime? DeletionDate { get; protected set; } = null;

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> entity)
            {
                return false;
            }

            if (ReferenceEquals(this, obj) == false)
            {
                return false;
            }

            if(GetType() != entity.GetType())
            {
                return false;
            }

            return EqualityComparer<TId>.Equals(this, obj); 
         }



        public void Delete() 
        {   
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;    
        }
        public void Restore() 
        {
            IsDeleted = false;
            DeletionDate = null;
        }
    }
}
