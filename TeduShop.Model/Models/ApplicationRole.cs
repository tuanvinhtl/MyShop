﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Model.Models
{
    public class ApplicationRole :IdentityRole
    {
        public ApplicationRole() : base()
        {

        }

        [StringLength(250)]
        public string Description { set; get; }
    }
}
