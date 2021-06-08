using System;
using System.Text.Json.Serialization;

namespace CovidTracking.API.Models.Entities
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
