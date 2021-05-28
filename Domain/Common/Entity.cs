using System;
using System.Text.Json.Serialization;

namespace Domain.Common
{
    public abstract class Entity
    {
        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Entity() {}
        
        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
