using System;
using System.ComponentModel.DataAnnotations;
using BlogApi.Entity;

namespace BlogApi.DTO.TagDto
{
    public class TagDto
    {
        public Guid id { get; set; }
        [Required] public DateTime createTime { get; set; }
        [MinLength(1)] [Required] public string name { get; set; }
        
        public TagDto(TagEntity tagEntity)
        {
            id = tagEntity.Id;
            createTime = tagEntity.CreateTime;
            name = tagEntity.Name;
        }
        
        public TagDto()
        {
            
        }
    }
}