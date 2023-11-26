using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApi.Entity
{
    public class TagEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PostEntity> Posts { get; set; }

        public TagEntity(DateTime createTime, string name)
        {
            Id = Guid.NewGuid();
            CreateTime = createTime;
            Name = name;
        }
        
        public TagEntity()
        {
        }
    }
}