using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogApi.DTO;
using BlogApi.DTO.AuthorDTO;

namespace BlogApi.Entity
{
    public class AuthorEntity
    {
        [Required] public Guid Id { get; set; }
        [ForeignKey("User")] [Required] public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public List<PostEntity>? Posts { get; set; }

        public AuthorEntity(UserEntity user)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            //User = user;
            FullName = user.FullName;
            BirthDate = user.BirthDate;
            Created = DateTime.Now;
        }

        public AuthorEntity()
        {
        }
    }
}