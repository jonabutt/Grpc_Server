using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ToDoGrpc.Complex
{
    public class ComplexResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fork")]
        public bool Fork { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("forks_url")]
        public string ForksUrl { get; set; }

        [JsonPropertyName("keys_url")]
        public string KeysUrl { get; set; }

        [JsonPropertyName("collaborators_url")]
        public string CollaboratorsUrl { get; set; }

        [JsonPropertyName("teams_url")]
        public string TeamsUrl { get; set; }

        [JsonPropertyName("hooks_url")]
        public string HooksUrl { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime PushedAt { get; set; }
   
        [JsonPropertyName("topics")]
        public List<string> Topics { get; set; }

        [JsonPropertyName("permissions")]
        public Permissions Permissions { get; set; }


  
    }
}
