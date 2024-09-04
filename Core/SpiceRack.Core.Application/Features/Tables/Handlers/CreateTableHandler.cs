using System;
using AutoMapper;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Features.Tables.Commands;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Application.Features.Tables.Handlers;

public class CreateTableHandler : IRequestHandler<CreateTableCommand, Response<GetTableResponse>>
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public CreateTableHandler(ITableRepository tableRepository, IMapper mapper)
    {
      _tableRepository = tableRepository;
      _mapper = mapper;
    }

    public async Task<Response<GetTableResponse>> Handle(CreateTableCommand request, CancellationToken cancellationToken)
    {
       var table = _mapper.Map<Table>(request.TableRequest);
       table.Status = TableStatus.Available;
       await _tableRepository.AddAsync(table);

       var response = _mapper.Map<GetTableResponse>(table);
       return new Response<GetTableResponse>(response);
    }
}
