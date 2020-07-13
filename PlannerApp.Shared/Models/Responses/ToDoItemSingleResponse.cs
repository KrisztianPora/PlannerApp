using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Shared.Models.Responses
{
    public class ToDoItemSingleResponse
    {
        public ToDoItem Record { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
