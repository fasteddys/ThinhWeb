using CQRS.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.ViewComponents
{
    public class AdminPageMenuListViewComponent : ViewComponent
    {
        private IMediator _mediator;

        public AdminPageMenuListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(string UserId)
        {
            var getMenuListByUserId = new GetMenusByUserId() { UserId = UserId };
            var menus = await _mediator.Send(getMenuListByUserId);
            return View(menus);
        }
    }
}
