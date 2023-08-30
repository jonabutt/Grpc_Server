using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Text.Json;
using ToDoGrpc.Complex;
using ToDoGrpc.Models;

namespace ToDoGrpc.Services
{
    public class GetDataService: GetData.GetDataBase
    {
        public override async Task<GetResponse> Get(GetRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new GetResponse
            {
                Message = "gRPC, which stands for Google Remote Procedure Call, is a cutting-edge open-source framework for building efficient and high-performance distributed systems. It is designed to facilitate communication between applications, allowing them to seamlessly exchange data and invoke functions across different programming languages and platforms. gRPC leverages Protocol Buffers (protobufs) as its interface definition language, enabling developers to define the structure of their data and services in a language-agnostic way. One of the standout features of gRPC is its support for HTTP/2, which offers advantages such as multiplexing, flow control, and header compression, making it incredibly efficient for data transmission. This technology is particularly popular in microservices architectures and cloud-native applications, where low latency and high throughput communication between services is paramount. With its rich ecosystem and language support, gRPC is a powerful tool for building robust and scalable distributed systems.",
            });
        }

        public override async Task<GetComplexRootResponse> GetComplex(GetRequest request, ServerCallContext context)
        {
            string z = System.IO.File.ReadAllText(@"Data/Data.json");
            var xx =  JsonSerializer.Deserialize<List<ComplexResponse>>(z);
            GetComplexRootResponse ret = new GetComplexRootResponse();
           ret.GetComplex.Add(ToComplexResponse(xx));
            return await Task.FromResult(ret);
        }

        private List<GetComplex> ToComplexResponse(List<ComplexResponse> complexResponse)
        {
            return complexResponse.Select(c => {
                var complex = new GetComplex()
                {
                    CollaboratorsUrl = c.CollaboratorsUrl,
                    CreatedAt = Timestamp.FromDateTimeOffset(c.CreatedAt),
                    Description = c.Description,
                    Fork = c.Fork,
                    ForksUrl = c.ForksUrl,
                    FullName = c.FullName,
                    HooksUrl = c.HooksUrl,
                    HtmlUrl = c.HtmlUrl,
                    Id = c.Id,
                    KeysUrl = c.KeysUrl,
                    Name = c.Name,
                    NodeId = c.NodeId,
                    Permissions = new Permissions()
                    {
                        Admin = c.Permissions.Admin,
                        Maintain = c.Permissions.Maintain,
                        Pull = c.Permissions.Pull,
                        Push = c.Permissions.Push,
                        Triage = c.Permissions.Triage,
                    },
                    Owner = new Owner()
                    {
                        Id = c.Owner.Id,
                        AvatarUrl = c.Owner.AvatarUrl,
                        Url = c.Owner.Url,
                        EventsUrl = c.Owner.EventsUrl, 
                        FollowersUrl = c.Owner.FollowersUrl,
                        FollowingUrl = c.Owner.FollowingUrl,
                        GistsUrl = c.Owner.GistsUrl,
                        GravatarId = c.Owner.GravatarId,
                        HtmlUrl = c.Owner.HtmlUrl,
                        Login = c.Owner.Login,
                        NodeId = c.Owner.NodeId,
                        OrganizationsUrl = c.Owner.OrganizationsUrl,
                        RepoUrl = c.Owner.ReposUrl,
                        SiteAdmin = c.Owner.SiteAdmin,
                        StarredUrl = c.Owner.StarredUrl,
                        SubscriptionsUrl = c.Owner.SubscriptionsUrl,
                        Type = c.Owner.Type
                    },
                    Private = c.Private,
                    PushedAt = Timestamp.FromDateTimeOffset(c.PushedAt),
                    TeamsUrl = c.TeamsUrl,
                    UpdatedAt = Timestamp.FromDateTimeOffset(c.UpdatedAt),
                    Url = c.Url
                };
                complex.Topics.Add(c.Topics);
                return complex;
            }).ToList();
        }
    }
}
