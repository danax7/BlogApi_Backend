using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogApi.DTO;
using BlogApi.DTO.AuthorDTO;

namespace BlogApi.Entity
{
    public class AuthorEntity
    {
        [Required] public Guid Id { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public DateTime BirthDate { get; set; }

        public UserEntity User { get; set; }
        public DateTime Created { get; set; }

        public List<PostEntity>? Posts { get; set; }

        public AuthorEntity(UserEntity user)
        {
            Id = user.Id;
            FullName = user.FullName;
            BirthDate = user.BirthDate;
            User = user;
            Created = DateTime.Now;
        }

        public AuthorEntity()
        {
        }
    }
}