﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Issues = new HashSet<Issue>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
