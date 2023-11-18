using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogApi.DTO.TagDto;

namespace BlogApi.Entity
{
    public class TagEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public string Name { get; set; }
        
        public TagEntity(Guid id, DateTime createTime, string name)
        {
            Id = id;
            CreateTime =  createTime;
            Name = name;
        }
    }                                                                                                                       
}
