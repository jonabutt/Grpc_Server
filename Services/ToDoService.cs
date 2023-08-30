using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ToDoGrpc.Data;
using ToDoGrpc.Models;

namespace ToDoGrpc.Services
{
    public class ToDoService : ToDoIt.ToDoItBase
    {
        private readonly AppDbContext _dbContext;
        public ToDoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CreateToDoResponse> CreateToDo(CreateToDoRequest request, ServerCallContext context)
        {
            if (request.Title == string.Empty || request.Description == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply a valid object"));
            }

            var toDoItem = new ToDoItem
            {
                Title = request.Title,
                Description = request.Description,
            };

            await _dbContext.AddAsync(toDoItem);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new CreateToDoResponse
            {
                Id = toDoItem.Id,
            });
        }

        public override async Task<ReadToDoResponse> ReadToDo(ReadToDoRequest request, ServerCallContext context)
        {

            var todo = await _dbContext.ToDoItems.SingleOrDefaultAsync(t => t.Id == request.Id);
            if (todo == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "ToDo not found"));
            }
            return await Task.FromResult(new ReadToDoResponse
            {
                Id = todo.Id,
                Description = todo.Description,
                Title = todo.Title,
                ToDoStatus = todo.ToDoStatus
            });
        }
        public override async Task<GetAllResponse> ListToDo(GetAllRequest request, ServerCallContext context)
        {
            var response = new GetAllResponse();
            var todoList = await _dbContext.ToDoItems.ToListAsync();

            foreach (var todo in todoList)
            {
                response.ToDo.Add(new ReadToDoResponse
                {
                    Id = todo.Id,
                    Description = todo.Description,
                    Title = todo.Title,
                    ToDoStatus = todo.ToDoStatus
                });
            }
            return await Task.FromResult(response);
        }

        public override async Task<UpdateToDoResponse> UpdateToDo(UpdateToDoRequest request, ServerCallContext context)
        {

            if (request.Id <= 0 || request.Title == string.Empty || request.Description == string.Empty || request.ToDoStatus == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must supply a valid object"));
            }
            var todo = await _dbContext.ToDoItems.SingleOrDefaultAsync(t => t.Id == request.Id);
            if (todo == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "ToDo not found"));
            }
            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.ToDoStatus = request.ToDoStatus;
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new UpdateToDoResponse
            {
                Id = todo.Id
            });
        }

        public override async Task<DeleteToDoResponse> DeleteToDo(DeleteToDoRequest request, ServerCallContext context)
        {
            var todo = await _dbContext.ToDoItems.SingleOrDefaultAsync(t => t.Id == request.Id);
            if (todo == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "ToDo not found"));
            }
            int toDoId = todo.Id;
            _dbContext.ToDoItems.Remove(todo);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new DeleteToDoResponse
            {
                Id = toDoId
            });
        }
    }
}
