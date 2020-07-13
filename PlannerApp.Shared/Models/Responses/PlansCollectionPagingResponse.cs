using System.Text.Json.Serialization;

namespace PlannerApp.Shared.Models
{
    public class PlansCollectionPagingResponse
    {
        public Plan[] Records { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int? NextPage { get; set; }

        public int Count { get; set; }
    }
}
