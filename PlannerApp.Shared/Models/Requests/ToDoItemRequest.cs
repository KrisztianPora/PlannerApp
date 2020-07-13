using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace PlannerApp.Shared.Models.Requests
{
    public class ToDoItemRequest
    {
        public string Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public string PlanId { get; set; }
    }
}
