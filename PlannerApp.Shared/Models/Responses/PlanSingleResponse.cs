using System.Text.Json.Serialization;

namespace PlannerApp.Shared.Models
{
    public class PlanSingleResponse
    {
        public Plan Record { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
