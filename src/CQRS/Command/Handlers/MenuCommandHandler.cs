using AutoMapper;
using CQRS.Command.BlogCategoryCommands;
using CQRS.Command.MenuCommands;
using CQRS.Dto.Out.BlogCategory;
using CQRS.Dto.Out.MenuDto;
using Domain.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Command.Handlers
{
    public class MenuCommandHandler : IRequestHandler<CreateMenuCommand, MenuDto>
    {
        private ApplicationDbContext _dbContext;
        private ILogger<MenuCommandHandler> _logger;
        private IMapper _mapper;

        public MenuCommandHandler(ApplicationDbContext dbContext, ILogger<MenuCommandHandler> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<MenuDto> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var menu = new Menu
                {
                    Id = Guid.NewGuid().ToString("N"),
                    TextDisplay = request.TextDisplay,
                    Url = request.Url,
                    Status = request.Status,
                };
                _dbContext.Menu.Add(menu);
                await _dbContext.SaveChangesAsync();

                var menuDto = _mapper.Map<MenuDto>(menu);
                return menuDto;
            }
            catch (Exception e)
            {
                _logger.LogError("Create menu", e);
            }
            return null;
        }
    }
}
