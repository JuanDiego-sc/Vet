using System;
using Application.Blog.Commands;
using Application.Blog.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR;

public class BlogHub(IMediator mediator) : Hub
{
    public async Task SendMessage(AddMessage.Command command)
    {
        var result = await mediator.Send(command);

        if (!result.IsSuccess) throw new HubException(result.Error);

        await Clients.Group(command.BlogId).SendAsync("ReceiveMessage", result.Value);
       
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var blogId = httpContext?.Request.Query["blogId"];

        if (string.IsNullOrWhiteSpace(blogId)) throw new HubException("BlogId is not found");

        await Groups.AddToGroupAsync(Context.ConnectionId, blogId!);

        var result = await mediator.Send(new GetMessagesList.Query { BlogId = blogId! });

        await Clients.Caller.SendAsync("LoadMessages", result.Value);
    }
}
