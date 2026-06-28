using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Portfolio_ZoranSimeunovic.Data;

namespace Portfolio_ZoranSimeunovic.Filters;

public class ValidateQNavFilter(AppDbContext db) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        if (descriptor?.ControllerName == "Admin")
        {
            await next();
            return;
        }

        var token = context.HttpContext.Request.Cookies["q_ref"];
        if (!string.IsNullOrEmpty(token))
        {
            var valid = await db.Questionnaires.AnyAsync(q =>
                q.Token == token &&
                q.TokenExpiresAt > DateTime.UtcNow &&
                q.CompletedAt == null);

            if (valid)
                (context.Controller as Controller)!.ViewBag.QuestionnaireToken = token;
            else
                context.HttpContext.Response.Cookies.Delete("q_ref");
        }

        await next();
    }
}
