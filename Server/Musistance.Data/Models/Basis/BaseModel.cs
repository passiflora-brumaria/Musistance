using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musistance.Data.Models.Basis
{
    /// <summary>
    /// Base class for database models.
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseModel()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// PRIMARY KEY.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time of creation of this entity.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Soft delete flag. If null, the entity has not been deleted; otherwise, date and time of deletion of this entity.
        /// </summary>
        public DateTime? DeletedAt {  get; set; }

        /// <summary>
        /// Deletes the present entity.
        /// </summary>
        public void MarkAsDeleted ()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
